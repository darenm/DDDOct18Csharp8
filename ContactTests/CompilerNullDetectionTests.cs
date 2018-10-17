using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

//[module: System.Runtime.CompilerServices.NonNullTypes]
namespace ContactTests
{
    [TestClass]
    public class CompilerNullDetectionTests
    {
        [TestMethod]
        public void CanDetectNullString()
        {
            string test = null;

            // bang
            Console.WriteLine(test.Length);

            // ==
            // is
            // ReferenceEquals
            // IsNullOrWhiteSpace

            if (test == null)
            {
                return;
            }
            else
            {
                Console.WriteLine(test.Length);
            }

            if (test is null)
            {
                Assert.IsTrue(true);
            }
            else
            {
                Console.WriteLine(test.Length);
            }

            if (ReferenceEquals(test, null))
            {
                Assert.IsTrue(true);
            }
            else
            {
                Console.WriteLine(test.Length);
            }

            if (string.IsNullOrWhiteSpace(test))
            {
                Assert.IsTrue(true);
            }
            else
            {
                Console.WriteLine(test.Length);
            }

            if (test is null)
            {
                return;
            }

            Console.WriteLine(test.Length);
        }

        [TestMethod]
        public void CrazyTest()
        {
            var cc = new CrazyClass();

            Console.WriteLine(cc.CrazyProperty.Length);

            // also can't detect thread craziness
        }


        [TestMethod]
        public void WhyPreferIsInsteadofEquals()
        {
            CrazyClass cc = null;

            if (cc == null)
            {
                Console.WriteLine("cc is null");
            }
            else
            {
                Console.WriteLine("cc is not null");
            }

            if (cc is null)
            {
                Console.WriteLine("cc is null");
            }
            else
            {
                Console.WriteLine("cc is not null");
            }
        }
    }
}
