using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimitiveTests
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "AppData",
                typeof(Program).Assembly.GetName().Name));
            Console.ReadKey();
        }
    }
}
