# NSN - An quicker way for connecting simple classes and objects to the internet

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

This will send the string "<h1>Hello World!</h1>" to the browser to be displayed.
