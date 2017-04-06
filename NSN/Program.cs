﻿using System;
using NSN.Core;

namespace NSN.Example
{
    class Program
    {
        class ExampleObject
        {
            public Person Person { get; set; }
            public Get HelloWorld()
            {
                return new Get()
                {
                    Payload = "<h1>Hello World!</h1>"
                };
            }

            public Get GetPerson()
            {
                return new Get(Person);
            }

            public Get GetBmi()
            {
                return new Get(Person.Bmi());
            }
        }

        class Person
        {
            public int Age { get; set; }
            public int Height { get; set; }
            public float Weight { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }

            public float Bmi()
            {
                return (float) (Weight * 704.7) / (Height * Height);
            }
        }

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


            var core = new Runner(example, "http://localhost:8080");
            core.Start();
            Console.Read();
            core.Stop();
        }
    }
}