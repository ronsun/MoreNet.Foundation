using BenchmarkDotNet.Running;
using MoreNet.Foundation.Benchmark.Extensions;

namespace MoreNet.Foundation.PerformanceTests
{
    public class Program
    {
        static void Main(string[] args)
        {
            BenchmarkRunner.Run<StringExtensionsBenchmark>();
        }
    }
}
