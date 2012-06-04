using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace Benchmark
{
    public class Benchmark
    {
        List<Tuple<string, Action>> steps;

        public Benchmark()
        {
            steps = new List<Tuple<string, Action>>();
        }

        public Benchmark Add(string name, Action toBenchmark)
        {
            steps.Add(Tuple.Create(name, toBenchmark));
            return this;
        }

        public void Run()
        {
            var times = RunInternal();

            PrintHeader();
            
            foreach (var item in times)
            {
                Console.WriteLine(string.Format("\t{0}\t\t\t\t\t\t{1}", item.Key, item.Value / (double)TimeSpan.TicksPerMillisecond));
            }

            PrintFooter();

        }

        public void RunIncremental()
        {
            var times = RunInternal();
            double baseline = -1, current;

            PrintHeader();

            foreach (var item in times)
            {
                current = item.Value / (double)TimeSpan.TicksPerMillisecond;

                Console.WriteLine(string.Format("\t{0}\t\t\t{2}\t\t{1}", item.Key, current, baseline == -1 ? "\t" : "\t" + (current / (baseline * 0.01)).ToString("0.####") + "%"));

                if (baseline == -1)
                    baseline = current;
            }

            PrintFooter();
        }

        static void PrintFooter()
        {
            Console.WriteLine("===============================================================================");
        }

        static void PrintHeader()
        {
            Console.WriteLine("===============================================================================");
            Console.WriteLine("\tBenchmark\t\t\t\t\tms");
            Console.WriteLine("===============================================================================");
        }

        Dictionary<string,long> RunInternal()
        {
            var times = new Dictionary<string,long>(steps.Count);
            var sw = new Stopwatch();

            foreach (var step in steps)
            {
                sw.Restart();

                step.Item2();

                sw.Stop();
                times.Add(step.Item1, sw.ElapsedTicks);
            }

            return times;
        }
    }
}
