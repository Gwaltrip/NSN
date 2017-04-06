using System;
using NSN.Core;
using NSN.Core.Modules;

namespace NSN.Example
{
    class Program
    {
        class ExampleObject
        {
            public Person Person { get; set; }
            public string HelloWorld()
            {
                return "<h1>Hello World!</h1>";
            }

            public double FiveAndAHalf()
            {
                return 5.5;
            }

            public double GetBmi()
            {
                return Person.Bmi();
            }

            public Person GetPerson()
            {
                return Person;
            }

            public Person SetAge(int age)
            {
                Person.Age = age;
                return Person;
            }

            public Person SetPerson(int age, int height, double weight, string firstName, string lastName)
            {
                Person.Age = age;
                Person.Height = height;
                Person.Weight = weight;
                Person.FirstName = firstName;
                Person.LastName = lastName;
                return Person;
            }
        }

        class Person
        {
            public int Age { get; set; }
            public int Height { get; set; }
            public double Weight { get; set; }
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

            var runner = new Runner(example, "http://localhost:8080");
            runner.Start();
            Console.Read();
            runner.Stop();
        }
    }
}
