# What's new in C# 7.0?
C# Version 7.0 was released on March 2017. 

## Out variables
We all know the `out` keyword causes arguments to be passed by reference. The syntax for `out` is greatly improved in the new version

```csharp
static void Main(string[] args)
{
    int height, weight;
    OutMethod(out height, out weight);
    Console.WriteLine($"Height:{height}, Weight:{weight}");
    //Height:180, Weight:90
}

static void OutMethod(out int height, out int weight)
{
    height = 180;
    weight = 90;
}
```
Now lets see the new way of writting the same code

```csharp
static void Main(string[] args)
{
    OutMethod(out int height, out int weight);
    WriteLine($"Height:{height}, Weight:{weight}");
    //Height:180, Weight:90
}
```
In above code the variables are created at `OutMethod()` 

## Pattern matching
Pattern matching is used for checking the conpatiblity of a type. The `is' operator/keyword is available since the first version of C#. Now in version 7.0 it is used to check if an object is compatible with a specific type

```csharp
static void Main(string[] args)
{
    object objA = null;
    object objB = 30;

    Print(objA);//It is null
    Print(objB);//It is not null
}

static void Print(object obj)
{
    if (obj == null)
    {
        Console.WriteLine("It is null");
    }
    else
    {
        Console.WriteLine("It is not null");
    }
}
```
Lets see the how the same can be implemented in new version
```csharp
static void Print(object obj)
{
    if (obj is null)
    {
        Console.WriteLine("It is null");
    }
    else
    {
        Console.WriteLine("It is not null");
    }
}
```
Here if you notice `is` keyword is used at the conditional block

### Type conversion with `is`
```csharp
static void Main(string[] args)
{
    object objA = null;
    object objB = 30;

    ConvertAndPrint(objA);
    ConvertAndPrint(objB);//Converted to Integer with value 30
}
static void ConvertAndPrint(object obj)
{
    //Traditional way
    if (obj != null)
    {
        if (obj.GetType() == typeof(int))
        {
            int i = (int)obj;
            Console.WriteLine($"Converted to Integer with value {i}");
        }
    }
}
```
Lets see how `ConvertAndPrint()` is rewritten with new version of C#

```csharp
static void ConvertAndPrint(object obj)
{
    if (!(obj is null) && obj is int i)
    {
        Console.WriteLine($"Converted to Integer with value {i}");
    }
}
```
Here variable `obj` is converted to int with the `is` keyword

### Including clause with `is` keyword 
```csharp
static void Main(string[] args)
{
    object objA = 55;
    object objB = 30;

    ConvertAndPrintWithCondition(objA);
    //It is an integer greater than 40
    
    ConvertAndPrintWithCondition(objB);
    //It is an integer less than 40
}

static void ConvertAndPrintWithCondition(object obj)
{
    if (obj != null)
    {
        switch (obj.GetType().ToString())
        {
            case "System.Int32":
                int i = (int)obj;
                if (i > 40)
                {
                    Console.WriteLine("It is an integer greater than 40");
                }
                else
                {
                    Console.WriteLine("It is an integer less than 40");
                }
                break;
            default:
                Console.WriteLine("Not an int type");
                break;
        }
    }
}
```
Now lets see how the same can be done in new way

```csharp
static void ConvertAndPrintWithCondition(object obj)
{
    if (!(obj is null))
    {
        switch (obj)
        {
            case int j when j > 40:
                Console.WriteLine("It is an integer greater than 40");
                break;
            case int j when j <= 40:
                Console.WriteLine("It is an integer less than 40");
                break;
            default:
                Console.WriteLine("Not an int type");
                break;
        }
    }
}
```

## Tuples

```csharp
static void Main(string[] args)
{
    var res = GetCircleDetails(2m);
    Console.WriteLine($"Radius:{res.Item1}, Diameter:{res.Item2}, Area:{res.Item3}, Circumference:{res.Item4}");
    //Radius:2, Diameter:4, Area:5.72, Circumference:5.72    
}
static (decimal, decimal, decimal, decimal) GetCircleDetails(decimal radius)
{
    decimal diameter = 2 * radius;
    decimal area = 1.43m * radius * radius;
    decimal circumference = 2 * radius * 1.43m;
    return (radius, diameter, area, circumference);
}
```
The above code can rewritten like show below

```csharp
static void Main(string[] args)
{
    var res = GetCircleDetails(2m);
    Console.WriteLine($"Radius:{res.Item1}, Diameter:{res.Item2}, Area:{res.Item3}, Circumference:{res.Item4}");
    //Radius:2, Diameter:4, Area:5.72, Circumference:5.72

    Console.WriteLine($"Radius:{res.radius}, Diameter:{res.diameter}, Area:{res.area}, Circumference:{res.circumference}");
    //Radius:2, Diameter:4, Area:5.72, Circumference:5.72
}

static (decimal radius, decimal diameter, decimal area, decimal circumference) GetCircleDetails(decimal radius)
{
    decimal diameter = 2 * radius;
    decimal area = 1.43m * radius * radius;
    decimal circumference = 2 * radius * 1.43m;
    return (radius, diameter, area, circumference);
}
```
Now the `GetCircleDetails()` return's named parameters so tuple can be accessed with name's as well. The `res.Item1` can be accessed with `res.radius` as well

### Deconstruction
There may be situations where you don't to retrieve all the tuple properties so for those properties just include/use `_` like shown in the below sample 
```csharp
static void Main(string[] args)
{
    (decimal r3, _, decimal a3, _) = GetCircleDetailsNew(2m);
    
    Console.WriteLine($"Radius:{r3}, Area:{a3}");
    //Radius:2, Area:5.72
}
```
## Local functions
Local function is actually a function inside a function which cannot be accessed from outside. Similar to variables created inside a function
```csharp
static void Main(string[] args)
{
    DateTime[] dobs = { new DateTime(2012, 1, 22), new DateTime(2013, 1, 3) };
    PrintAge(dobs);
}

static void PrintAge(DateTime[] dates)
{
    var since = new DateTime(2015, 1, 1);
    foreach (var date in dates)
    {
        Console.WriteLine($"Age on 2015-03-25 is {GetAge(date)} for DOB {date}");
    }

    int GetAge(DateTime dob)
    {
        var yearDiff = since.Year - dob.Year;
        return yearDiff;
    }
}
```
Here in the above example `PrintAge()` method's  local function is `GetAge()` which is inside the `PrintAge()` method's scope.

### Digit separators
A digit separator is one or more underscore ( `_` ) characters added inside of a numeric literal to enhance readability of code. The digit separator can be used with decimal, float and double types

```csharp
int a = 1_234_5_67;
decimal b = 1_2.34_56m;
Console.WriteLine(a);//1234567
Console.WriteLine(b);//12.34567
```

## Binary literals
Using the 0b prefix, a binary literal can be defined
```csharp
int a = 0b1;
int b = 0b1000_0;
Console.WriteLine(0b1);//1
Console.WriteLine(0b10000);//16
```
## Ref returns and locals
Perviously returning functions with `ref` was not possible. With new version you can. Lets see the code below

```csharp
static void Main(string args[])
{
    int[] numberArray = { 20, 40, 60, 90 };
    ref int refInt = ref RefDemo(numberArray);
    
    Console.WriteLine(refInt);//20
    Console.WriteLine(numberArray[0]);//20

    numberArray[0] = 55555;
    Console.WriteLine(refInt);//55555
}
static ref int RefDemo(int[] numberArray)
{
    return ref numberArray[0];
}
```
## Expression bodied constructors, finalizers, getters & setters
C# version 6 introduced expression bodied methods now this feature got extended to constructors, finalizer, getters and setters as well

```csharp
public class Person
{
    public string Name { get; set; }

    private DateTime _dob;
    public DateTime DOB 
    { 
        //getter
        get => _dob;

        //setter
        set => _dob = value;
    }

    //Constructor
    Person(string name) => Name = name;

    //Finalizer
    ~Person() => Console.Write("finalizers");
}
```