using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace BionCore.Tests.Lineal
{
    [TestClass]
    public class BionLinealExtensionsTest
    {
        //[TestMethod, TestCategory("Lineal"), TestCategory("BionCore")]
        //public void GenericTest()
        //{

        //    List<int> temp, elemento = null;

        //    #region -------- CSHARP 6 --------
        //    temp = (elemento ?? new List<int>());
        //    temp.Reverse();
        //    (temp as ICollection<int>).Add(4);
        //    #endregion [-------- CSHARP 6 --------]

        //    #region -------- CSHARP LEGACY --------
        //    temp = elemento;
        //    if (temp == null)
        //        temp = new List<int>();
        //    temp.Reverse();
        //    ((ICollection<int>)temp).Add(4);
        //    #endregion [-------- CSHARP LEGACY --------]


        //    #region -------- BION LINEAL 1 --------
        //    elemento.IfNull(new List<int>()).IfNotNull(c => c.Reverse()).As<ICollection<int>>().Add(4);
        //    #endregion [-------- BION LINEAL 1 --------]


        //    #region -------- BION LINEAL 2 --------
        //    List<int> Elemento = null;
        //    Elemento.IfNull(new List<int>()).Do(c => c.Reverse()).As<ICollection<int>>().Add(4);
        //    #endregion [-------- BION LINEAL 2 --------]

        //    "".NullIfEmpty().IfNull("otroValor").Do(c => c.Do());
        //    ICollection<int?> q = null;
        //}

        #region ======== AS ========
        [TestMethod, TestCategory("Lineal"), TestCategory("BionCore")]
        public void TestValidAs()
        {
            List<string> valueSimple = new List<string> { "Hola" };

            var collection = valueSimple.As<ICollection<string>>();
            Assert.IsNotNull(collection);
            Assert.AreEqual("Hola", collection.First());
        }

        [TestMethod, TestCategory("Lineal"), TestCategory("BionCore")]
        public void TestInvalidAs()
        {
            List<string> valueSimple = new List<string> { "Hola" };

            var collection = valueSimple.As<ICollection<object>>();
            Assert.IsNull(collection);
        }


        [TestMethod, TestCategory("Lineal"), TestCategory("BionCore")]
        public void TestValidAsDefault()
        {
            List<string> valueSimple = new List<string> { "Hola" };
            var defaultValue = new List<string> { "Adios" };
            var collection = valueSimple.As<ICollection<string>>(defaultValue);
            Assert.IsNotNull(collection);
            Assert.AreEqual("Hola", collection.First());
        }


        [TestMethod, TestCategory("Lineal"), TestCategory("BionCore")]
        public void TestInvalidAsDefault()
        {
            List<string> valueSimple = new List<string> { "Hola" };
            var defaultValue = new List<object> { "Adios" };

            var collection = valueSimple.As<ICollection<object>>(defaultValue);
            Assert.AreEqual(defaultValue, collection);
        }
        #endregion [======== AS ========]

        #region ======== FORCE AS ========
        [TestMethod, TestCategory("Lineal"), TestCategory("BionCore")]
        public void TestValidForceAs()
        {
            List<string> valueSimple = new List<string> { "Hola" };

            var collection = valueSimple.ForceAs<ICollection<string>>();
            Assert.IsNotNull(collection);
            Assert.AreEqual("Hola", collection.First());
        }


        [TestMethod, TestCategory("Lineal"), TestCategory("BionCore")]
        public void TestInvalidForceAs()
        {
            List<string> valueSimple = new List<string> { "Hola" };

            Assert.ThrowsException<InvalidCastException>(() =>
            {
                var collection = valueSimple.ForceAs<ICollection<object>>();
            });

        }
        #endregion [======== FORCE AS ========]

        #region ======== IfNull ========
        [TestMethod, TestCategory("Lineal"), TestCategory("BionCore")]
        public void TestIfNullForNullString()
        {
            string emptyString = null;
            var result = emptyString.IfNull("Hello");

            Assert.AreEqual("Hello", result);
        }


        [TestMethod, TestCategory("Lineal"), TestCategory("BionCore")]
        public void TestIfNullForNotNullString()
        {
            var emptyString = "Hello";
            var result = emptyString.IfNull("Goodbye");

            Assert.AreEqual("Hello", result);
        }

        [TestMethod, TestCategory("Lineal"), TestCategory("BionCore")]
        public void TestIfNullForNullPolymorph()
        {
            ICollection<string> empty = null;
            var defaultValue = new List<string> { "Hello" };
            var result = empty.IfNull(defaultValue);

            Assert.AreEqual(defaultValue, result);
        }

        [TestMethod, TestCategory("Lineal"), TestCategory("BionCore")]
        public void TestIfNullForNotNullPolymorph()
        {
            ICollection<string> notEmpty = new List<string> { "Goodbye" };
            var defaultValue = new List<string> { "Hello" };
            var result = notEmpty.IfNull(defaultValue);

            Assert.AreEqual(notEmpty, result);
        }



        [TestMethod, TestCategory("Lineal"), TestCategory("BionCore")]
        public void TestIfNullNulleableEmpty()
        {
            int? value = null;
            int defaultValue = 45;

            var actual = value.IfNull(defaultValue);

            Assert.AreEqual(defaultValue, actual);
        }

        [TestMethod, TestCategory("Lineal"), TestCategory("BionCore")]
        public void TestIfNullNulleableNotEmpty()
        {
            int? value = 14;
            int defaultValue = 45;

            var actual = value.IfNull(defaultValue);

            Assert.AreEqual(value, actual);
        }


        [TestMethod, TestCategory("Lineal"), TestCategory("BionCore")]
        public void TestIfNullFunctionEmpty()
        {
            int? value = null;
            int defaultValue = 45;

            var actual = value.IfNull(() => defaultValue);
            Assert.AreEqual(defaultValue, actual);

        }

        [TestMethod, TestCategory("Lineal"), TestCategory("BionCore")]
        public void TestIfNullFunctionNull()
        {
            int? value = null;
            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                var actual = value.IfNull((Func<int?>)null);
            });

        }


        [TestMethod, TestCategory("Lineal"), TestCategory("BionCore")]
        public void TestIfNullFunctionNoyEmpty()
        {
            int? value = 15;
            int defaultValue = 45;

            var actual = value.IfNull(() => defaultValue);
            Assert.AreEqual(value, actual);

        }
        #endregion [======== IfNull ========]

        #region ======== IfNotNull ========
        [TestMethod, TestCategory("Lineal"), TestCategory("BionCore")]
        public void TestIfNotNullNotNull()
        {
            int? value = 15;
            int defaultValue = 45;

            var actual = value.IfNotNull(v => defaultValue + v);
            Assert.AreEqual(value + defaultValue, actual);

        }

        [TestMethod, TestCategory("Lineal"), TestCategory("BionCore")]
        public void TestIfNotNullNull()
        {
            int? value = null;
            int defaultValue = 45;

            var actual = value.IfNotNull(v => defaultValue + v);
            Assert.AreEqual(null, actual);

        }


        [TestMethod, TestCategory("Lineal"), TestCategory("BionCore")]
        public void TestIfNotNullActionNotNull()
        {
            int? value = 15;
            int defaultValue = 45;

            var actual = value.IfNotNull(v => { defaultValue += value.Value; });
            Assert.AreEqual(60, defaultValue);

        }


        [TestMethod, TestCategory("Lineal"), TestCategory("BionCore")]
        public void TestIfNotNullActionNull()
        {
            int? value = null;
            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                var actual = value.IfNotNull((Action<int?>)null);
            });

        }

        [TestMethod, TestCategory("Lineal"), TestCategory("BionCore")]
        public void TestIfNotNullFunctionNull()
        {
            int? value = null;
            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                var actual = value.IfNotNull((Func<int?, int?>)null);
            });

        }
        #endregion [======== IfNotNull ========]

        #region ======== Is ========
        [TestMethod, TestCategory("Lineal"), TestCategory("BionCore")]
        public void TestIsTypeValid()
        {
            var value = "Hola Mundo";
            Assert.IsTrue(value.Is<string>());
        }

        [TestMethod, TestCategory("Lineal"), TestCategory("BionCore")]
        public void TestIsTypeInvalid()
        {
            var value = "Hola Mundo";
            Assert.IsFalse(value.Is<int>());
        }



        [TestMethod, TestCategory("Lineal"), TestCategory("BionCore")]
        public void TestIsNotTypeValid()
        {
            var value = 15;
            Assert.IsTrue(value.IsNot<string>());
        }

        [TestMethod, TestCategory("Lineal"), TestCategory("BionCore")]
        public void TestIsNotTypeInvalid()
        {
            var value = 15;
            Assert.IsFalse(value.IsNot<int>());
        }
        #endregion [======== Is ========]


        #region ======== Do ========
        [TestMethod, TestCategory("Lineal"), TestCategory("BionCore")]
        public void TestDoCorrect()
        {
            int value = 2;
            int value2 = 3;

            value.Do(v => value2 += v);

            Assert.AreEqual(5, value2);
        }

        [TestMethod, TestCategory("Lineal"), TestCategory("BionCore")]
        public void TestDoFunctionNull()
        {
            int value = 2;
            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                value.Do(null);
            });
        }


        [TestMethod, TestCategory("Lineal"), TestCategory("BionCore")]
        public void TestDoEmpty()
        {
            int value = 1;
            int value2 = 2;
            int _someAction()
            {
                value2 += 2;
                return value;
            }

            var actual = _someAction().Do();

            Assert.AreEqual(actual, value);
            Assert.AreEqual(4, value2);
        }
        #endregion [======== Do ========]

        #region ======== IsNull ========
        [TestMethod, TestCategory("Lineal"), TestCategory("BionCore")]
        public void TestIsNullStructValid()
        {
            int? value = null;
            var actual = value.IsNull();

            Assert.IsTrue(actual);

        }

        [TestMethod, TestCategory("Lineal"), TestCategory("BionCore")]
        public void TestIsNullStructInvalid()
        {
            int? value = 14;
            var actual = value.IsNull();

            Assert.IsFalse(actual);

        }


        [TestMethod, TestCategory("Lineal"), TestCategory("BionCore")]
        public void TestIsNullClassValid()
        {
            string value = null;
            var actual = value.IsNull();

            Assert.IsTrue(actual);

        }

        [TestMethod, TestCategory("Lineal"), TestCategory("BionCore")]
        public void TestIsNullClassInvalid()
        {
            string value = "HelloWorld";
            var actual = value.IsNull();

            Assert.IsFalse(actual);

        }
        #endregion [======== IsNull ========]


        #region ======== IsNotNull ========
        [TestMethod, TestCategory("Lineal"), TestCategory("BionCore")]
        public void TestIsNotNullStructValid()
        {
            int? value = null;
            var actual = value.IsNotNull();

            Assert.IsFalse(actual);

        }

        [TestMethod, TestCategory("Lineal"), TestCategory("BionCore")]
        public void TestIsNotNullStructInvalid()
        {
            int? value = 14;
            var actual = value.IsNotNull();

            Assert.IsTrue(actual);

        }


        [TestMethod, TestCategory("Lineal"), TestCategory("BionCore")]
        public void TestIsNotNullClassValid()
        {
            string value = null;
            var actual = value.IsNotNull();

            Assert.IsFalse(actual);

        }

        [TestMethod, TestCategory("Lineal"), TestCategory("BionCore")]
        public void TestIsNotNullClassInvalid()
        {
            string value = "HelloWorld";
            var actual = value.IsNotNull();

            Assert.IsTrue(actual);

        }
        #endregion [======== IsNull ========]


        #region ======== NullIfEmpty ========
        [TestMethod, TestCategory("Lineal"), TestCategory("BionCore")]
        public void TestNullIfEmptyInvalid()
        {
            string value = "Hello";
            var actual = value.NullIfEmpty();

            Assert.AreEqual(value, actual);
        }


        [TestMethod, TestCategory("Lineal"), TestCategory("BionCore")]
        public void TestNullIfEmptyNullValid()
        {
            string value = null;
            var actual = value.NullIfEmpty();

            Assert.AreEqual(value, actual);
        }

        [TestMethod, TestCategory("Lineal"), TestCategory("BionCore")]
        public void TestNullIfEmptyEmptyValid()
        {
            string value = "";
            var actual = value.NullIfEmpty();

            Assert.AreEqual(null, actual);
        }


        [TestMethod, TestCategory("Lineal"), TestCategory("BionCore")]
        public void TestNullIfEmptyWhitespaceValid()
        {
            string value = "    ";
            var actual = value.NullIfEmpty(true);

            Assert.AreEqual(null, actual);
        }

        [TestMethod, TestCategory("Lineal"), TestCategory("BionCore")]
        public void TestNullIfEmptyWhitespaceInvalid()
        {
            string value = "";
            var actual = value.NullIfEmpty(true);

            Assert.AreEqual(null, actual);
        }


        [TestMethod, TestCategory("Lineal"), TestCategory("BionCore")]
        public void TestNullIfEmptyNotWhitespaceValid()
        {
            string value = "    ";
            var actual = value.NullIfEmpty(false);

            Assert.AreEqual(value, actual);
        }

        [TestMethod, TestCategory("Lineal"), TestCategory("BionCore")]
        public void TestNullIfEmptyNotWhitespaceInvalid()
        {
            string value = "";
            var actual = value.NullIfEmpty(false);

            Assert.AreEqual(null, actual);
        }

        [TestMethod, TestCategory("Lineal"), TestCategory("BionCore")]
        public void TestNullIfEmptyNotWhitespaceAndTextInvalid()
        {
            string value = "Pepe";
            var actual = value.NullIfEmpty(false);

            Assert.AreEqual(value, actual);
        }

        [TestMethod, TestCategory("Lineal"), TestCategory("BionCore")]
        public void TestNullIfEmptyWhitespaceAndTextInvalid()
        {
            string value = "Pepe";
            var actual = value.NullIfEmpty(true);

            Assert.AreEqual(value, actual);
        }
        #endregion [======== NullIfEmpty ========]


    }
}
