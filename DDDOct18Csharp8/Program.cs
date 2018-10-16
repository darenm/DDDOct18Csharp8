using System;

//[module: System.Runtime.CompilerServices.NonNullTypes]

namespace DDDOct18Csharp8
{

    class Program
    {
        static void Main(string[] args)
        {
            var service = new ContactService();
            var renderer = new RenderContact();
            renderer.Render(service.Get());
        }
    }
}
