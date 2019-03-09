using BenchmarkDotNet.Running;

namespace BenchmarkApp
{
    class Program
    {
        static void Main(string[] args)
        {
            BenchmarkRunner.Run<Benchmark>();
        }
    }
}
