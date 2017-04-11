using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using NSN.Core;

namespace NSN.Example
{
    class Program
    {
        static void Main(string[] args)
        {
            var example = new ExampleObject
            {
                Person = new Person()
                {
                    Age = 23,
                    Height = 68,
                    Weight = 201.5f,
                    FirstName = "Bob",
                    LastName = "Wallace"
                }
            };
            Console.WriteLine("What port do you want to use?");
            var runner = new Runner(example, "http://localhost:"+int.Parse(Console.ReadLine()));
            runner.Start();
            Console.Read();
            runner.Stop();
        }
    }
}
