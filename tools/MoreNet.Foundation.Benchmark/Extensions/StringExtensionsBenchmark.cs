using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using MoreNet.Foundation.Extensions;

namespace MoreNet.Foundation.Benchmark.Extensions
{
    [SimpleJob(RuntimeMoniker.Net462, baseline: true)]
    [SimpleJob(RuntimeMoniker.NetCoreApp31)]
    [SimpleJob(RuntimeMoniker.Net60)]
    public class StringExtensionsBenchmark : IBenchmark
    {
        // To prevent out of memory exception, the length is set to smaller value.
        private static int longStringLength = int.MaxValue / 20;
        private static string longString = new string('a', longStringLength);

        [Benchmark]
        public void Baseline()
        {
            longString.Mask(0, 1, null, '*');
            longString.Mask(0, longStringLength, null, '*');
            longString.Mask(longStringLength - 1, 1, null, '*');
        }

        [Benchmark]
        public void Candidate()
        {
            longString.Mask(0, 1, null, '*');
            longString.Mask(0, longStringLength, null, '*');
            longString.Mask(longStringLength - 1, 1, null, '*');
        }
    }
}
