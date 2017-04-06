# NSN - Nancy Simple Notation
## An quicker way for connecting simple classes and objects to the internet

How quick is it? Quick.

If you have an object already, all you have to do is to pass it into a new NSN Runner.

```C#
var runner = new Runner(object, "http://localhost:8080");
runner.Start();
Console.Read();
runner.Stop();
```

Example of a few methods of an protentical object that can be sent to runner are below.

```C#
public string HelloWorld()
{
    return "<h1>Hello World!</h1>"
}
```

This will send the string "\<h1\>Hello World!\</h1\>" to the browser to be displayed.
```C#
public double FiveAndAHalf(){
    return 5.5;
}
```
This will send the double value 5.5 to the browser to be displayed.

This isn't just limited to simple strings and primitives. You can also send objects too!

```C#
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
```

A simple Get method in the object you are passing into the runner would look like this.
```C#
public Person GetPerson()
{
    return Person;
}
```
How about passing into functions?
```C#
public Person SetAge(int age)
{
    Person.Age = age;
    return Person;
}
```
