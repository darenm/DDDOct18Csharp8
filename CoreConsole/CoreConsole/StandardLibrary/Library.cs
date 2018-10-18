
using static System.Console;

public class LibraryClass
{
    public static async System.Threading.Tasks.Task Main()
    {
        await foreach (var i in new LibraryClass().M())
        {
            Write(i);
        }
    }
    //public static async System.Threading.Tasks.Task Main()
    //{
    //    _ = nameof(System.Threading.Tasks.ValueTask);
    //    var enumerator = new LibraryClass().M().GetAsyncEnumerator();
    //    await enumerator.WaitForNextAsync();

    //    var value = enumerator.TryGetNext(out bool success);
    //    Assert(success);
    //    Assert(value == 42);

    //    enumerator.TryGetNext(out success);
    //    Assert(!success);

    //    try
    //    {
    //        await enumerator.WaitForNextAsync();
    //        Write("UNREACHABLE");
    //    }
    //    catch (System.Exception e)
    //    {
    //        Assert(e.Message == "message");
    //    }
    //    Write("Done");
    //}

    public async System.Collections.Generic.IAsyncEnumerable<int> M()
    {
        yield return 42;
        await System.Threading.Tasks.Task.Delay(100);
        yield return 43;
    }
}

namespace System.Collections.Generic
{
    public interface IAsyncEnumerable<out T>
    {
        IAsyncEnumerator<T> GetAsyncEnumerator();
    }

    public interface IAsyncEnumerator<out T> : System.IAsyncDisposable
    {
        System.Threading.Tasks.ValueTask<bool> MoveNextAsync();
        T Current { get; }
    }
}
namespace System
{
    public interface IAsyncDisposable
    {
        System.Threading.Tasks.ValueTask DisposeAsync();
    }
}

namespace System.Runtime.CompilerServices
{
    public interface IStrongBox<T>
    {
        ref T Value { get; }
    }
}

namespace System.Threading.Tasks
{
    using System.Runtime.CompilerServices;
    using System.Runtime.ExceptionServices;
    using System.Threading.Tasks.Sources;

    public struct ManualResetValueTaskSourceLogic<TResult>
    {
        private static readonly Action<object> s_sentinel = new Action<object>(s => throw new InvalidOperationException());

        private readonly IStrongBox<ManualResetValueTaskSourceLogic<TResult>> _parent;
        private Action<object> _continuation;
        private object _continuationState;
        private object _capturedContext;
        private ExecutionContext _executionContext;
        private bool _completed;
        private TResult _result;
        private ExceptionDispatchInfo _error;
        private short _version;

        public ManualResetValueTaskSourceLogic(IStrongBox<ManualResetValueTaskSourceLogic<TResult>> parent)
        {
            _parent = parent ?? throw new ArgumentNullException(nameof(parent));
            _continuation = null;
            _continuationState = null;
            _capturedContext = null;
            _executionContext = null;
            _completed = false;
            _result = default;
            _error = null;
            _version = 0;
        }

        public short Version => _version;

        private void ValidateToken(short token)
        {
            if (token != _version)
            {
                throw new InvalidOperationException();
            }
        }

        public ValueTaskSourceStatus GetStatus(short token)
        {
            ValidateToken(token);

            return
                !_completed ? ValueTaskSourceStatus.Pending :
                _error == null ? ValueTaskSourceStatus.Succeeded :
                _error.SourceException is OperationCanceledException ? ValueTaskSourceStatus.Canceled :
                ValueTaskSourceStatus.Faulted;
        }

        public TResult GetResult(short token)
        {
            ValidateToken(token);

            if (!_completed)
            {
                throw new InvalidOperationException();
            }

            _error?.Throw();
            return _result;
        }

        public void Reset()
        {
            _version++;

            _completed = false;
            _continuation = null;
            _continuationState = null;
            _result = default;
            _error = null;
            _executionContext = null;
            _capturedContext = null;
        }

        public void OnCompleted(Action<object> continuation, object state, short token, ValueTaskSourceOnCompletedFlags flags)
        {
            if (continuation == null)
            {
                throw new ArgumentNullException(nameof(continuation));
            }
            ValidateToken(token);

            if ((flags & ValueTaskSourceOnCompletedFlags.FlowExecutionContext) != 0)
            {
                _executionContext = ExecutionContext.Capture();
            }

            if ((flags & ValueTaskSourceOnCompletedFlags.UseSchedulingContext) != 0)
            {
                SynchronizationContext sc = SynchronizationContext.Current;
                if (sc != null && sc.GetType() != typeof(SynchronizationContext))
                {
                    _capturedContext = sc;
                }
                else
                {
                    TaskScheduler ts = TaskScheduler.Current;
                    if (ts != TaskScheduler.Default)
                    {
                        _capturedContext = ts;
                    }
                }
            }

            _continuationState = state;
            if (Interlocked.CompareExchange(ref _continuation, continuation, null) != null)
            {
                _executionContext = null;

                object cc = _capturedContext;
                _capturedContext = null;

                switch (cc)
                {
                    case null:
                        Task.Factory.StartNew(continuation, state, CancellationToken.None, TaskCreationOptions.DenyChildAttach, TaskScheduler.Default);
                        break;

                    case SynchronizationContext sc:
                        sc.Post(s =>
                        {
                            var tuple = (Tuple<Action<object>, object>)s;
                            tuple.Item1(tuple.Item2);
                        }, Tuple.Create(continuation, state));
                        break;

                    case TaskScheduler ts:
                        Task.Factory.StartNew(continuation, state, CancellationToken.None, TaskCreationOptions.DenyChildAttach, ts);
                        break;
                }
            }
        }

        public void SetResult(TResult result)
        {
            _result = result;
            SignalCompletion();
        }

        public void SetException(Exception error)
        {
            _error = ExceptionDispatchInfo.Capture(error);
            SignalCompletion();
        }

        private void SignalCompletion()
        {
            if (_completed)
            {
                throw new InvalidOperationException();
            }
            _completed = true;

            if (Interlocked.CompareExchange(ref _continuation, s_sentinel, null) != null)
            {
                if (_executionContext != null)
                {
                    ExecutionContext.Run(
                        _executionContext,
                        s => ((IStrongBox<ManualResetValueTaskSourceLogic<TResult>>)s).Value.InvokeContinuation(),
                        _parent ?? throw new InvalidOperationException());
                }
                else
                {
                    InvokeContinuation();
                }
            }
        }

        private void InvokeContinuation()
        {
            object cc = _capturedContext;
            _capturedContext = null;

            switch (cc)
            {
                case null:
                    _continuation(_continuationState);
                    break;

                case SynchronizationContext sc:
                    sc.Post(s =>
                    {
                        ref ManualResetValueTaskSourceLogic<TResult> logicRef = ref ((IStrongBox<ManualResetValueTaskSourceLogic<TResult>>)s).Value;
                        logicRef._continuation(logicRef._continuationState);
                    }, _parent ?? throw new InvalidOperationException());
                    break;

                case TaskScheduler ts:
                    Task.Factory.StartNew(_continuation, _continuationState, CancellationToken.None, TaskCreationOptions.DenyChildAttach, ts);
                    break;
            }
        }
    }
}
