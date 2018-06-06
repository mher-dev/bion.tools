using BionCore.Tools;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace BionCore.Tests.StaticTools
{
    internal class A
    {
        public string ValueA { get; set; }
        public virtual string Value { get; set; }
    }

    internal class B : A 
    {
        public string ValueB { get; set; }
        public override string Value { get; set; }
    }


    internal class C : B
    {
        public string ValueC { get; set; }
        public override string Value { get; set; }
    }



    [Microsoft.VisualStudio.TestTools.UnitTesting.TestClass]
    public class BionCoreTypeSwitch
    {

        #region ======== Simple Empty Type ========
        [TestMethod]
        public void TestEmptySimpleTypeSwitchEmptySwitch()
        {
            TypeSwitch.For<string>(sw => { });
        }

        [TestMethod]
        public void TestEmptySimpleTypeSwitchOneCorrectCase()
        {
            bool isCorrect = false;
            TypeSwitch.For<string>(sw =>
            {
                sw.Case<string>(() =>
                {
                    isCorrect = true;

                });
            });

            Assert.IsTrue(isCorrect);
        }

        [TestMethod]
        public void TestEmptySimpleTypeSwitchOneIncorrectCase()
        {
            bool isCorrect = true;
            TypeSwitch.For<string>(sw =>
            {
                sw.Case<int>(() =>
                {
                    Assert.Fail();

                });
            });

            Assert.IsTrue(isCorrect);
        }

        [TestMethod]
        public void TestEmptySimpleTypeSwitchTwoCasesOneCorrect()
        {
            bool isCorrect = false;
            TypeSwitch.For<string>(sw =>
            {
                sw.Case<int>(() =>
                {
                    Assert.Fail();
                });

                sw.Case<string>(() =>
                {
                    isCorrect = true;
                });
            });

            Assert.IsTrue(isCorrect);
        }

        [TestMethod]
        public void TestEmptySimpleTypeSwitchTwoCasesNoCorrect()
        {
            TypeSwitch.For<double>(sw =>
            {
                sw.Case<int>(() =>
                {
                    Assert.Fail();
                });

                sw.Case<string>(() =>
                {
                    Assert.Fail();
                });
            });
        }

        [TestMethod]
        public void TestEmptySimpleTypeSwitchDefaultCase()
        {
            bool isCorrect = false;
            TypeSwitch.For<string>(sw =>
            {
                sw.Default(() =>
                {
                    isCorrect = true;
                });
            });

            Assert.IsTrue(isCorrect);
        }

        [TestMethod]
        public void TestEmptySimpleTypeSwitchIncorrectDefaultCase()
        {
            bool isCorrect = false;
            TypeSwitch.For<string>(sw =>
            {
                sw.Case<int>(() =>
                {
                    Assert.Fail();

                });

                sw.Default(() =>
                {
                    isCorrect = true;
                });
            });

            Assert.IsTrue(isCorrect);
        }
        #endregion [======== Simple Empty Type ========]


        #region ======== Simple Empty Type ========
        [TestMethod]
        public void TestEmptyClassTypeSwitchEmptySwitch()
        {
            TypeSwitch.For<A>(sw => { });
        }

        [TestMethod]
        public void TestEmptyClassDirectTypeSwitchOneCorrectCase()
        {
            bool isCorrect = false;
            TypeSwitch.For<A>(sw =>
            {
                sw.Case<A>(() =>
                {
                    isCorrect = true;

                });
            });

            Assert.IsTrue(isCorrect);
        }

        [TestMethod]
        public void TestEmptyClassFirstChildTypeSwitchOneCorrectCase()
        {
            bool isCorrect = false;
            TypeSwitch.For<B>(sw =>
            {
                sw.Case<A>(() =>
                {
                    isCorrect = true;

                });
            });

            Assert.IsTrue(isCorrect);
        }

        [TestMethod]
        public void TestEmptyClassSecondChildTypeSwitchOneCorrectCase()
        {
            bool isCorrect = false;
            TypeSwitch.For<C>(sw =>
            {
                sw.Case<A>(() =>
                {
                    isCorrect = true;

                });
            });

            Assert.IsTrue(isCorrect);
        }

        [TestMethod]
        public void TestEmptyClassParentTypeSwitchOneCorrectCase()
        {
            bool isCorrect = true;
            TypeSwitch.For<A>(sw =>
            {
                sw.Case<C>(() =>
                {
                    Assert.Fail();
                });
            });

            Assert.IsTrue(isCorrect);
        }


        [TestMethod]
        public void TestEmptyClassTypeSwitchTwoCasesOneCorrect()
        {
            bool isCorrect = false;
            TypeSwitch.For<A>(sw =>
            {
                sw.Case<B>(() =>
                {
                    Assert.Fail();
                });

                sw.Case<A>(() =>
                {
                    isCorrect = true;
                });
            });

            Assert.IsTrue(isCorrect);
        }

        [TestMethod]
        public void TestEmptyClassTypeSwitchTwoCasesTwoCorrect()
        {
            bool isCorrect = false;
            TypeSwitch.For<B>(sw =>
            {
                sw.Case<B>(() =>
                {
                    isCorrect = true;
                });

                sw.Case<A>(() =>
                {
                    Assert.Fail();
                });
            });

            Assert.IsTrue(isCorrect);
        }

        [TestMethod]
        public void TestEmptyClassTypeSwitchTwoCasesNoCorrect()
        {
            TypeSwitch.For<A>(sw =>
            {
                sw.Case<B>(() =>
                {
                    Assert.Fail();
                });

                sw.Case<C>(() =>
                {
                    Assert.Fail();
                });
            });
        }


        [TestMethod]
        public void TestEmptyClassTypeSwitchDefaultCase()
        {
            bool isCorrect = false;
            TypeSwitch.For<A>(sw =>
            {
                sw.Default(() =>
                {
                    isCorrect = true;
                });
            });

            Assert.IsTrue(isCorrect);
        }

        [TestMethod]
        public void TestEmptyClassTypeSwitchIncorrectDefaultCase()
        {
            bool isCorrect = false;
            TypeSwitch.For<A>(sw =>
            {
                sw.Case<B>(() =>
                {
                    Assert.Fail();

                });

                sw.Default(() =>
                {
                    isCorrect = true;
                });
            });

            Assert.IsTrue(isCorrect);
        }
        #endregion [======== Simple Empty Type ========]

    }
}
