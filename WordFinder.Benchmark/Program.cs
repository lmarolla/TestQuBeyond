
using BenchmarkDotNet.Running;
using WordFinder.Benchmark;

var summary = BenchmarkRunner.Run<WordFinderBenchmark>();