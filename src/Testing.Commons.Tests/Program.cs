using NUnitLite;
using System;
using System.Reflection;

namespace Testing.Commons.Tests.core
{
    class Program
    {
        static void Main(string[] args)
        {
          var writter = new ExtendedTextWrapper(Console.Out);
          new AutoRun(typeof(Program).GetTypeInfo().Assembly).Execute(args, writter, Console.In);
        }
    }
}
