﻿using System;
using System.Collections.Generic;
using UTProject.Model;
using Xunit;

namespace UTProject
{

    [Trait("快速查找", "Precision")]
    public class PrecisionTest
    {
        TestModel model;
        PrecisionCache<string> Handler1;

        public PrecisionTest()
        {
            model = new TestModel();
            Handler1 = model.Model1.PrecisioTree();
        }

        [Fact(DisplayName = "精确查找测试1")]
        public void TestModel1()
        {
            foreach (var item in model.Model1)
            {
                Assert.Equal(item.Value, Handler1[item.Key]);
            }
        }

        [Fact(DisplayName = "空集合测试1")]
        public void TestModel2()
        {
            var model2= new TestModel();
            model2.Model1.Clear();
            var tempHandler = model2.Model1.PrecisioTree();
            Assert.Equal(default, tempHandler["1"]);
            Assert.Equal(default, tempHandler.GetKeys("1"));
            Assert.Equal(default, tempHandler.GetValue("1"));
        }

        [Fact(DisplayName = "精确查找树反向查找测试")]
        public void TestModel5()
        {

            Dictionary<string, string> dict = new Dictionary<string, string>();
            dict["a"] = "a";
            dict["b"] = "a";
            dict["c"] = "a";
            dict["d"] = "e";


            var handler = dict.PrecisioTree();
            foreach (var item in dict)
            {
                Assert.Equal(item.Value, handler[item.Key]);
            }


            var hashSet = new HashSet<string>(handler.GetKeys("a"));
            Assert.Equal(3, hashSet.Count);
            Assert.Contains("a", hashSet);
            Assert.Contains("b", hashSet);
            Assert.Contains("c", hashSet);

        }
    }
}
