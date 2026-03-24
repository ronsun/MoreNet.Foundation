namespace MoreNet.Foundation.Benchmark
{
    public interface IBenchmark
    {
        void Baseline();

        void Candidate();
    }
}
