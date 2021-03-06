﻿using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Mathematics;
using BenchmarkDotNet.Order;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace BenchmarkProject
{

    [MemoryDiagnoser, MarkdownExporter, RPlotExporter]
    [MinColumn, MaxColumn, MeanColumn, MedianColumn]
    [GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory)]
    [Orderer(SummaryOrderPolicy.FastestToSlowest)]
    [RankColumn(NumeralSystem.Arabic)]
    [CategoriesColumn]
    public class BenchmarkTest
    {
        public readonly TestModel model;
        public readonly FuzzyCache<string> FuzzyHandler;
        public readonly HashCache<string, string> HashHandler;
        public readonly PrecisionCache<string> PrecisionHandler;
        public readonly Dictionary<string, string> DictHandler;
        public readonly ConcurrentDictionary<string, string> ConDictHandler;
        public BenchmarkTest()
        {
            model = new TestModel();
            FuzzyHandler = model.Model1.FuzzyTree();
            HashHandler = model.Model1.HashTree();
            PrecisionHandler = model.Model1.PrecisioTree();
            DictHandler = model.Model1;
            ConDictHandler = new ConcurrentDictionary<string, string>(model.Model1);
        }

        [Benchmark(Description = "哈希查找树")]
        public void TestHash()
        {
            var result = HashHandler["11"];
            result = HashHandler["2"];
        }

        [Benchmark(Description = "模糊查找树")]
        public void TestFuzzy()
        {
            var result = FuzzyHandler["11"];
            result = FuzzyHandler["2"];
        }

        [Benchmark(Description = "精确查找树")]
        public void TestPrecision()
        {
            var result = PrecisionHandler["11"];
            result = PrecisionHandler["2"];
        }

        [Benchmark(Description = "普通字典")]
        public void TestDict()
        {
            var result = DictHandler["11"];
            result = DictHandler["2"];
        }


        [Benchmark(Description = "并发字典")]
        public void TestConDict()
        {
            var result = ConDictHandler["11"];
            result = ConDictHandler["2"];
        }

    }
}
