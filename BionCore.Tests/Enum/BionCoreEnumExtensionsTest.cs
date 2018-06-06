using BionCore.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BionCore.Tests.Enum
{
    enum TestEnum
    {
        S1 = BLongEnumValues.V01,
        S2 = BLongEnumValues.V02,
        S3 = BLongEnumValues.V03,
        S4 = BLongEnumValues.V04,

        NS5 = 2,
        NS6 = 4,
        NS7 = 8,

    }

    [TestClass]
    public class BionCoreEnumExtensionsTest
    {
        [TestMethod]
        public void ContainsAny2EnumValid()
        {
            var test = TestEnum.S1 | TestEnum.S2;

            Assert.IsTrue(test.ContainsAny(TestEnum.S1));
        }

        [TestMethod]
        public void ContainsAny2EnumInvalid()
        {
            var test = TestEnum.S1 | TestEnum.S2;

            Assert.IsFalse(test.ContainsAny(TestEnum.S3));
        }

        [TestMethod]
        public void ContainsAnyEnumValidInvalidValue()
        {
            var test = TestEnum.S1 | TestEnum.S2;

            Assert.IsTrue(test.ContainsAny(TestEnum.NS5));
        }
    }
}
