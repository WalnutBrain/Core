using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalnutBrain.Configuration;

namespace PrimitiveTests
{
    public class A
    {
        public string Val1 { get; set; }
        public string Val2 { get; set; }
        public int[] Val3 { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var a1 = new A
            {
                Val1 = "123",
                Val2 = "456",
                Val3 = new [] { 1,2, 3, 9 }
            };

            var a2 = new A
            {
                Val1 = "789",
                Val2 = "765",
                Val3 = new[] { 1, 2, 3, 9 }
            };

            var section1 = new ConfigaratorSection<A>("test", a1, null, null);
            Console.WriteLine(section1.ToString());
            Console.WriteLine();

            section1.Value.Val2 = "0987";
            section1.Value.Val3 = new[] {1, 2, 3};

            Console.WriteLine(section1.ToString());
            Console.WriteLine();

            Console.WriteLine(section1.Diff());
            Console.WriteLine();

            var section2 = new ConfigaratorSection<A>("test", a2, null, null);
            Console.WriteLine(section2.ToString());
            Console.WriteLine();

            section2.Patch(section1.Diff());

            Console.WriteLine(section2.ToString());
            Console.WriteLine();

            Console.ReadKey();
        }
    }
}
