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

There must be methods that return Get, such as below.

```C#
public Get HelloWorld()
{
    return new Get("<h1>Hello World!</h1>");
}
```

This will send the string "\<h1\>Hello World!\</h1\>" to the browser to be displayed.

This isn't just limited to simple strings. You can also send objects.

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
public Get GetPerson()
{
    return new Get(Person);
}
```
How about results of functions?
```C#
public Get GetBmi()
{
    return new Get(Person.Bmi());
}
```
