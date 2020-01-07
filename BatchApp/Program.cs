using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatchApp
{
    /// <summary>
    /// cmd /c %AZ_BATCH_APP_PACKAGE_WHIZLABSAPP%\\BatchApp.exe
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("This is a batch job");
        }
    }
}
