using System;
using NSN.Core;

namespace NSN.Example
{
    public interface IExampleObject
    {
        Person Person { get; set; }
        string HelloWorld();
        double FiveAndAHalf();
        double GetBmi();
        Person GetPerson();
        Person SetAge(int age);
        Person SetPerson(int age, int height, double weight, string firstName, string lastName);
        ExampleObject This();
    }
    public class ExampleObject: IExampleObject
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
        public ExampleObject This()
        {
            return this;
        }
        public Person GetTheirPerson(string uri)
        {
            Console.Read();
            return ServiceObject.Invoke<IExampleObject, Person>(uri, p => GetPerson());
        }
        public Person SetTheirPerson(string uri, int age, int height, double weight, string firstName, string lastName)
        {
            Console.WriteLine(age+"\t|"+ height + "\t|" + weight + "\t|" + firstName + "\t|" + lastName);
            return ServiceObject
                .Invoke<IExampleObject, Person>
                    (uri, "SetPerson", age, height, weight, firstName, lastName);
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
