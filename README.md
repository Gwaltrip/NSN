# NSN - An quicker way of using NancyFx for simple classes and objects

How quick is it? Quick.

If you have an object already, all you have to do is to pass it into a new NSN Runner.

```C#
var core = new Runner(object, "http://localhost:8080");
core.Start();
Console.Read();
core.Stop();
```
