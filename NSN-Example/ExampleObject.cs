﻿using NSN.Core;

namespace NSN.Example
{
    public abstract class IExampleObject
    {
        public abstract Person Person { get; set; }
        public abstract string HelloWorld();
        public abstract double FiveAndAHalf();
        public abstract double GetBmi();
        public abstract Person GetPerson();
        public abstract Person SetAge(int age);
        public abstract Person SetPerson(int age, int height, double weight, string firstName, string lastName);
        public abstract ExampleObject This();
    }
    public class ExampleObject: IExampleObject
    {
        public override Person Person { get; set; }
        public override string HelloWorld()
        {
            return "<h1>Hello World!</h1>";
        }
        public override double FiveAndAHalf()
        {
            return 5.5;
        }
        public override double GetBmi()
        {
            return Person.Bmi();
        }
        public override Person GetPerson()
        {
            return Person;
        }
        public override Person SetAge(int age)
        {
            Person.Age = age;
            return Person;
        }
        public override Person SetPerson(int age, int height, double weight, string firstName, string lastName)
        {
            Person.Age = age;
            Person.Height = height;
            Person.Weight = weight;
            Person.FirstName = firstName;
            Person.LastName = lastName;
            return Person;
        }
        public override ExampleObject This()
        {
            return this;
        }
        public Person GetTheirPerson(string uri)
        {
            return ServiceInfo.Invoke<IExampleObject,Person>(uri, "GetPerson");
        }
        public Person SetTheirPerson(string uri, int age, int height, double weight, string firstName, string lastName)
        {
            ServiceInfo serviceInfo = new ServiceInfo(uri, GetType().GetMethod("SetPerson"));
            return serviceInfo.Invoke<Person>(age, height, weight, firstName, lastName);
        }
    }
    public class Person
    {
        public int Age { get; set; }
        public int Height { get; set; }
        public double Weight { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public float Bmi()
        {
            return (float)(Weight * 704.7) / (Height * Height);
        }
    }
}
