using DDDOct18Csharp8;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ContactTests
{
    [TestClass]
    public class RenderTests
    {
        [TestMethod]
        public void RenderContact()
        {
            var contact = new Contact("TestFirst", "TestMiddle", "TestLast", 64);
            var expected = "Name: TestFirst T. TestLast" + Environment.NewLine +
                "Age: 64" + Environment.NewLine;
            var renderer = new RenderContact();
            var actual = renderer.Render(contact);

            Assert.AreEqual(expected, actual);
        }
    }
}
