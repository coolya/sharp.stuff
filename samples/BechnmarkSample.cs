using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace samples
{
    public class BechnmarkSample
    {
        public static void Run()
        {
            var bench = new Benchmark.Benchmark();

            bench.Add("first", () =>
                {
                    string result = string.Empty;

                    for (int i = 0; i < 100000; i++)
                    {
                        result += i.ToString();
                    }
                }).Add("second", ()  =>
                {
                    StringBuilder builder = new StringBuilder();

                    for (int i = 0; i < 100000; i++)
                    {
                        builder.Append(i.ToString());
                    }
                    builder.ToString();
                }).RunIncremental();        
        }
    }
}
