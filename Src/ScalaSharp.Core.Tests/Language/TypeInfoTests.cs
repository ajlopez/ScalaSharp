﻿namespace ScalaSharp.Core.Tests.Language
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using ScalaSharp.Core.Language;

    [TestClass]
    public class TypeInfoTests
    {
        [TestMethod]
        public void MakeTypeInfo()
        {
            Assert.AreEqual("Int", TypeInfo.Make(1).Name);
            Assert.AreEqual("Double", TypeInfo.Make(1.2).Name);
            Assert.AreEqual("Null", TypeInfo.Make(null).Name);
            Assert.AreEqual("String", TypeInfo.Make("foo").Name);
            Assert.AreEqual("Boolean", TypeInfo.Make(false).Name);
            Assert.AreEqual("Boolean", TypeInfo.Make(true).Name);
            Assert.AreEqual("System.IO.FileInfo", TypeInfo.Make(new System.IO.FileInfo(".")).Name);
        }
    }
}
