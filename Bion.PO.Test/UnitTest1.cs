using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bion.PO;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

namespace Bion.PO.Test
{
    [TestClass]
    public class UnitTest1
    {
        const int MaxSize = 20;
        #region ######## SimpleClass ########



        [TestMethod]
        public void TestSimplePublicPropery()
        {

            int seedCount = MaxSize;
            var origin = new List<TestClassSimple>();
            for (int seed = 0; seed < seedCount; seed++)
            {
                origin.Add(GenerateClassSimple(seed));
            }

            var destination = new List<TestClassSimple>();
            for (int seed = 0; seed < seedCount; seed++)
            {
                destination.Add(this.PackUnpack(origin[seed], BPOPoliticsReadDepth.Public, BPOPoliticsMemberTypes.Property));
            }


            origin = new List<TestClassSimple>();
            for (int seed = 0; seed < seedCount; seed++)
            {
                origin.Add(GenerateClassSimple(seed));
            }


            for (int seed = 0; seed < seedCount; seed++)
            {
                var oItem = origin[seed];
                var dItem = destination[seed];

                Assert.IsTrue(oItem.Equals(dItem));
            }

        }

        [TestMethod]
        public void TestSimplePrivateField()
        {

            int seedCount = MaxSize;
            var origin = new List<TestClassSimple>();
            for (int seed = 0; seed < seedCount; seed++)
            {
                origin.Add(GenerateClassSimple(seed));
            }

            var destination = new List<TestClassSimple>();
            for (int seed = 0; seed < seedCount; seed++)
            {
                destination.Add(this.PackUnpack(origin[seed], BPOPoliticsReadDepth.Private, BPOPoliticsMemberTypes.Field));
            }


            origin = new List<TestClassSimple>();
            for (int seed = 0; seed < seedCount; seed++)
            {
                origin.Add(GenerateClassSimple(seed));
            }

            for (int seed = 0; seed < seedCount; seed++)
            {
                var oItem = origin[seed];
                var dItem = destination[seed];

                Assert.IsTrue(oItem.Equals(dItem));
            }

        }
        #endregion [######## SimpleClass ########]

        #region ======== TestList ========

        [TestMethod]
        public void TestListPublicPropery()
        {

            int seedCount = MaxSize;
            var origin = new List<TestClassLists>();
            for (int seed = 0; seed < seedCount; seed++)
            {
                origin.Add(GenerateClassList(seed, seedCount));
            }

            var destination = new List<TestClassLists>();
            for (int seed = 0; seed < seedCount; seed++)
            {
                destination.Add(this.PackUnpack(origin[seed], BPOPoliticsReadDepth.Public, BPOPoliticsMemberTypes.Property));
            }

            origin = new List<TestClassLists>();
            for (int seed = 0; seed < seedCount; seed++)
            {
                origin.Add(GenerateClassList(seed, seedCount));
            }
            for (int seed = 0; seed < seedCount; seed++)
            {
                var oItem = origin[seed];
                var dItem = destination[seed];
                var result = oItem.Equals(dItem);
                Assert.IsTrue(result);
            }

        }


        [TestMethod]
        public void TestListPrivateField()
        {

            int seedCount = MaxSize;
            var origin = new List<TestClassLists>();
            for (int seed = 0; seed < seedCount; seed++)
            {
                origin.Add(GenerateClassList(seed, seedCount));
            }

            var destination = new List<TestClassLists>();
            for (int seed = 0; seed < seedCount; seed++)
            {
                destination.Add(this.PackUnpack(origin[seed], BPOPoliticsReadDepth.Private, BPOPoliticsMemberTypes.Field));
            }


            origin = new List<TestClassLists>();
            for (int seed = 0; seed < seedCount; seed++)
            {
                origin.Add(GenerateClassList(seed, seedCount));
            }
            for (int seed = 0; seed < seedCount; seed++)
            {
                var oItem = origin[seed];
                var dItem = destination[seed];
                var result = oItem.Equals(dItem);
                Assert.IsTrue(result);
            }

        }
        #endregion [======== TestList ========]


        #region ======== TestList and Simple ========
        [TestMethod]
        public void TestListSinglePublicPropery()
        {
            int seedCount = MaxSize;
            var originStarted = Stopwatch.StartNew();

            var origin = new List<TestClassListSimple>();
            for (int seed = 0; seed < seedCount; seed++)
            {
                origin.Add(GenerateClassListSimple(seed, seedCount));
            }

            originStarted.Stop();

            var originEnded = originStarted.ElapsedMilliseconds;
            var originMedia = new decimal(originEnded) / seedCount;

            var destinStarted = Stopwatch.StartNew();
            var destination = new List<TestClassListSimple>();
            long rawSize = 0;
            for (int seed = 0; seed < seedCount; seed++)
            {
                long itemLeng;
                destination.Add(this.PackUnpack(origin[seed], BPOPoliticsReadDepth.Public, BPOPoliticsMemberTypes.Property, out itemLeng));
                rawSize += itemLeng;
            }
            destinStarted.Stop();
            var destinEnded = destinStarted.ElapsedMilliseconds;
            var destinMedia = new decimal(destinEnded) / seedCount;

            origin = new List<TestClassListSimple>();
            for (int seed = 0; seed < seedCount; seed++)
            {
                origin.Add(GenerateClassListSimple(seed, seedCount));
            }

            for (int seed = 0; seed < seedCount; seed++)
            {
                var oItem = origin[seed];
                var dItem = destination[seed];

                Assert.IsTrue(oItem.Equals(dItem));
            }

            rawSize = rawSize;
            originMedia = originMedia;
            originEnded = originEnded;

            destinMedia = destinMedia;
            destinEnded = destinEnded;


            Console.WriteLine("****************************************************");
            Console.WriteLine("Public time: " + originEnded + "ms");

        }

        [TestMethod]
        public void TestListSinglePrivateField()
        {
            int seedCount = MaxSize;
            var originStarted = Stopwatch.StartNew();

            var origin = new List<TestClassListSimple>();
            for (int seed = 0; seed < seedCount; seed++)
            {
                origin.Add(GenerateClassListSimple(seed, seedCount));
            }

            originStarted.Stop();
            var originEnded = originStarted.ElapsedMilliseconds;
            var originMedia = new decimal(originEnded) / seedCount;

            var destinStarted = Stopwatch.StartNew();
            var destination = new List<TestClassListSimple>();
            long rawSize = 0;
            for (int seed = 0; seed < seedCount; seed++)
            {
                long itemLeng;
                destination.Add(this.PackUnpack(origin[seed], BPOPoliticsReadDepth.Private, BPOPoliticsMemberTypes.Field, out itemLeng));
                rawSize += itemLeng;
            }
            destinStarted.Stop();
            var destinEnded = destinStarted.ElapsedMilliseconds;
            var destinMedia = new decimal(destinEnded) / seedCount;

            origin = new List<TestClassListSimple>();
            for (int seed = 0; seed < seedCount; seed++)
                origin.Add(GenerateClassListSimple(seed, seedCount));
            
            for (int seed = 0; seed < seedCount; seed++)
            {
                var oItem = origin[seed];
                var dItem = destination[seed];

                Assert.IsTrue(oItem.Equals(dItem));
            }

            var rawMbSize = rawSize / (1024 * 2);
            originMedia = originMedia;
            originEnded = originEnded;

            destinMedia = destinMedia;
            destinEnded = destinEnded;


            Console.WriteLine("****************************************************");
            Console.WriteLine("Private time: " + originEnded + "ms");
        }
        #endregion [======== TestList and Simple ========]

        #region ======== TestDictionary ========
        [TestMethod]
        public void TestDictionaryPublicPropery()
        {

            int seedCount = MaxSize;
            var origin = new List<TestClassDictionary>();
            for (int seed = 0; seed < seedCount; seed++)
                origin.Add(GenerateClassDictionary(seed, seedCount));
            

            var destination = new List<TestClassDictionary>();
            for (int seed = 0; seed < seedCount; seed++)
            {
                destination.Add(this.PackUnpack(origin[seed], BPOPoliticsReadDepth.Public, BPOPoliticsMemberTypes.Property));
            }


            origin = new List<TestClassDictionary>();
            for (int seed = 0; seed < seedCount; seed++)
                origin.Add(GenerateClassDictionary(seed, seedCount));

            for (int seed = 0; seed < seedCount; seed++)
            {
                var oItem = origin[seed];
                var dItem = destination[seed];
                var result = oItem.Equals(dItem);
                Assert.IsTrue(result);
            }

        }

        [TestMethod]
        public void TestDictionaryPrivateField()
        {

            int seedCount = MaxSize;
            var origin = new List<TestClassDictionary>();
            for (int seed = 0; seed < seedCount; seed++)
            {
                origin.Add(GenerateClassDictionary(seed, seedCount));
            }

            var destination = new List<TestClassDictionary>();
            for (int seed = 0; seed < seedCount; seed++)
            {
                destination.Add(this.PackUnpack(origin[seed], BPOPoliticsReadDepth.Private, BPOPoliticsMemberTypes.Field));
            }


            origin = new List<TestClassDictionary>();
            for (int seed = 0; seed < seedCount; seed++)
            {
                origin.Add(GenerateClassDictionary(seed, seedCount));
            }
            for (int seed = 0; seed < seedCount; seed++)
            {
                var oItem = origin[seed];
                var dItem = destination[seed];
                var result = oItem.Equals(dItem);
                Assert.IsTrue(result);
            }

        }
        #endregion [======== TestDictionary ========]


        #region ======== TestDictionary ========
        [TestMethod]
        public void TestBDictionaryPublicPropery()
        {

            int seedCount = MaxSize;
            var origin = new List<TestClassBFriendlyDictionary>();
            for (int seed = 0; seed < seedCount; seed++)
                origin.Add(GenerateClassFriendlyDictionary(seed, seedCount));


            var destination = new List<TestClassBFriendlyDictionary>();
            for (int seed = 0; seed < seedCount; seed++)
            {
                destination.Add(this.PackUnpack(origin[seed], BPOPoliticsReadDepth.Public, BPOPoliticsMemberTypes.Property));
            }


            origin = new List<TestClassBFriendlyDictionary>();
            for (int seed = 0; seed < seedCount; seed++)
                origin.Add(GenerateClassFriendlyDictionary(seed, seedCount));

            for (int seed = 0; seed < seedCount; seed++)
            {
                var oItem = origin[seed];
                var dItem = destination[seed];
                var result = oItem.Equals(dItem);
                Assert.IsTrue(result);
            }

        }

        [TestMethod]
        public void TestBDictionaryPrivateField()
        {

            int seedCount = MaxSize;
            var origin = new List<TestClassBFriendlyDictionary>();
            for (int seed = 0; seed < seedCount; seed++)
                origin.Add(GenerateClassFriendlyDictionary(seed, seedCount));


            var destination = new List<TestClassBFriendlyDictionary>();
            for (int seed = 0; seed < seedCount; seed++)
            {
                destination.Add(this.PackUnpack(origin[seed], BPOPoliticsReadDepth.Private, BPOPoliticsMemberTypes.Field));
            }


            origin = new List<TestClassBFriendlyDictionary>();
            for (int seed = 0; seed < seedCount; seed++)
                origin.Add(GenerateClassFriendlyDictionary(seed, seedCount));

            for (int seed = 0; seed < seedCount; seed++)
            {
                var oItem = origin[seed];
                var dItem = destination[seed];
                var result = oItem.Equals(dItem);
                Assert.IsTrue(result);
            }

        }
        #endregion [======== TestDictionary ========]

        #region ######## PRIVATE ########



        private TestClassSimple GenerateClassSimple(int seed)
        {
            var result = new TestClassSimple
            {
                DateTime = new DateTime().AddSeconds(seed),
                Decimal = 10 + seed,
                Double = 100 + seed,
                Int = 1000 + seed,
                String = $"StringValue({seed})"
            };

            return result;
        }

        private TestClassLists GenerateClassList(int seed, int count)
        {
            var result = new TestClassLists();

            for (int i = 0; i < count; i++)
            {
                result.ListDateTime.Add(new DateTime().AddSeconds(seed + count));
                result.ListDecimal.Add(10 + seed + count);
                result.ListDouble.Add(100 + seed + count);
                result.ListInt.Add(1000 + seed + count);
                result.ListString.Add($"StringValue({seed}+{count})");
            }


            return result;
        }
        private TestClassDictionary GenerateClassDictionary(int seed, int count)
        {
            var result = new TestClassDictionary();
            for (int i = 0; i < count; i++)
            {
                result.DictionaryDateTime.Add(i + seed, new DateTime().AddSeconds(seed + count));
                result.DictionaryDecimal.Add(i + seed, 10 + seed + count);
                result.DictionaryDouble.Add(i + seed, 100 + seed + count);
                result.DictionaryInt.Add(i + seed, 1000 + seed + count);
                result.DictionaryString.Add(i + seed, $"StringValue({seed}+{i})");
                result.DictionarySimple.Add(i + seed, this.GenerateClassSimple(seed + i));
            }


            return result;
        }
        
        private TestClassBFriendlyDictionary GenerateClassFriendlyDictionary(int seed, int count)
        {
            var result = new TestClassBFriendlyDictionary();
            for (int i = 0; i < count; i++)
            {
                result.BFriendlyDictionaryDateTime.Add(i + seed, new DateTime().AddSeconds(seed + count));
                result.BFriendlyDictionaryDecimal.Add(i + seed, 10 + seed + count);
                result.BFriendlyDictionaryDouble.Add(i + seed, 100 + seed + count);
                result.BFriendlyDictionaryInt.Add(i + seed, 1000 + seed + count);
                result.BFriendlyDictionaryString.Add(i + seed, $"StringValue({seed}+{i})");
                result.BFriendlyDictionarySimple.Add(i + seed, this.GenerateClassSimple(seed + i));
            }


            return result;
        }
        private TestClassListSimple GenerateClassListSimple(int seed, int count)
        {
            var result = new TestClassListSimple();
            result.SingleSimple = this.GenerateClassSimple(seed);
            result.SinlgeList = this.GenerateClassList(seed, count);
            int multiplicator = 10000 * count;
            for (int i = 0; i < count; i++)
            {

                var simple = this.GenerateClassSimple(seed + multiplicator);
                result.ListSimple.Add(simple);

                var list = this.GenerateClassList(seed + multiplicator, count);
                result.ListList.Add(list);
            }

            return result;

        }

        private T PackUnpack<T>(T element, BPOPoliticsReadDepth depth, BPOPoliticsMemberTypes type)
        {
            var context = new BPOContext();
            context.Politics.MemberType = type;
            context.Politics.ReadDepth = depth;
            var packed = BPOTools.ToPortableObject(element, context);
            var unpacked = BPOTools.FromPortableObject(packed, context);

            return (T)unpacked;
        }

        private T PackUnpack<T>(T element, BPOPoliticsReadDepth depth, BPOPoliticsMemberTypes type, out long rawSize)
        {
            var context = new BPOContext();
            context.Politics.MemberType = type;
            context.Politics.ReadDepth = depth;
            var packed = BPOTools.ToPortableObject(element, context);
            rawSize = packed.LongLength;
            var unpacked = BPOTools.FromPortableObject(packed, context);

            return (T)unpacked;
        }
        #endregion [######## PRIVATE ########]
    }
}
