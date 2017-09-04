namespace CSharp7
{
    using System;
    using static System.Console;
    class Program
    {
        static void Main(string[] args)
        {
            //Out Variable................................
            int h1, w1;
            OutMethod(out h1, out w1);
            WriteLine($"Height:{h1}, Weight:{w1}");//Height:180, Weight:90

            OutMethod(out int newh1, out int neww1);
            WriteLine($"Height:{newh1}, Weight:{neww1}");//Height:180, Weight:90


            //Pattern Matching............................
            object objA = null;
            object objB = 30;

            Print(objA);
            Print(objB);

            ConvertAndPrint(objA);
            ConvertAndPrint(objB);

            object objC = 50;
            ConvertAndPrintWithCondition(objB);
            ConvertAndPrintWithCondition(objC);

            //Tuples................................
            var c1 = GetCircleDetails(2m);
            WriteLine($"Radius:{c1.Item1}, Diameter:{c1.Item2}, Area:{c1.Item3}, Circumference:{c1.Item4}");

            var c2 = GetCircleDetailsNew(2m);
            WriteLine($"Radius:{c2.Item1}, Diameter:{c2.Item2}, Area:{c2.Item3}, Circumference:{c2.Item4}");
            //Radius:2, Diameter:4, Area:5.72, Circumference:5.72

            WriteLine($"Radius:{c2.radius}, Diameter:{c2.diameter}, Area:{c2.area}, Circumference:{c2.circumference}");
            //Radius:2, Diameter:4, Area:5.72, Circumference:5.72

            //Tuple Deconstruction
            (decimal r1, decimal d1, decimal a1, decimal cl1) = GetCircleDetails(2m);
            WriteLine($"Radius:{r1}, Diameter:{d1}, Area:{a1}, Circumference:{cl1}");
            //Radius:2, Diameter:4, Area:5.72, Circumference:5.72

            (decimal r2, decimal d2, decimal a2, decimal cl2) = GetCircleDetailsNew(2m);
            WriteLine($"Radius:{r2}, Diameter:{d2}, Area:{a2}, Circumference:{cl2}");
            //Radius:2, Diameter:4, Area:5.72, Circumference:5.72

            //Discard with _
            (decimal r3, _, decimal a3, _) = GetCircleDetailsNew(2m);
            WriteLine($"Radius:{r3}, Area:{a3}");
            //Radius:2, Area:5.72

            //Digit Seperators.....................................
            WriteLine(1_234_5_67);//1234567
            WriteLine(1_2.34_56);//12.34567
            WriteLine(1.999_033_988_749_894_848_204_586_834_365_638_117_720_309_179m);//1.9990339887498948482045868344

            WriteLine(0b1);//1
            WriteLine(0b10000);//16

            //Method return ref................................
            int[] numberArray = { 22, 44, 50, 60 };
            if (!(numberArray is ValueType))
            {
                WriteLine("Is a Reference Type");
            }

            int refInt1 = RefDemo(numberArray);
            WriteLine(refInt1);//22
            WriteLine(numberArray[0]);//22
            numberArray[0] = 99999;
            WriteLine(refInt1);//22

            ref int refInt2 = ref RefDemo(numberArray);
            WriteLine(refInt2);//99999
            WriteLine(numberArray[0]);//99999
            numberArray[0] = 55555;
            WriteLine(refInt2);//55555

            //Local Function................................
            DateTime[] dobs = { new DateTime(2012, 1, 22), new DateTime(2013, 1, 3) };
            PrintAge(dobs);
        }

        static void OutMethod(out int height, out int weight)
        {
            height = 180;
            weight = 90;
        }

        static void Print(object obj)
        {
            //Traditional way
            if (obj == null)
            {
                WriteLine("It is null");
            }
            else
            {
                WriteLine("It is not null");
            }

            //Using Pattern matching
            if (obj is null)
            {
                WriteLine("It is null");
            }
            else
            {
                WriteLine("It is not null");
            }
        }

        static void ConvertAndPrint(object obj)
        {
            //Traditional way
            if (obj != null)
            {
                if (obj.GetType() == typeof(int))
                {
                    int i = (int)obj;
                    WriteLine($"Variable is not null & of Integer type with value {i}");
                }
            }


            //Using Pattern matching
            if (!(obj is null) && obj is int j)
            {
                WriteLine($"Variable is not null & of Integer type with value {j}");
            }
        }

        static void ConvertAndPrintWithCondition(object obj)
        {
            //Traditional way
            if (obj != null)
            {
                switch (obj.GetType().ToString())
                {
                    case "System.Int32":
                        int i = (int)obj;
                        if (i > 40)
                        {
                            WriteLine("It is an integer greater than 40");
                        }
                        else
                        {
                            WriteLine("It is an integer less than 40");
                        }
                        break;
                    default:
                        WriteLine("Not an integer type");
                        break;
                }
            }

            //Using Pattern matching
            if (!(obj is null))
            {
                switch (obj)
                {
                    case int j when j > 40:
                        WriteLine("It is an integer greater than 40");
                        break;
                    case int j when j <= 40:
                        WriteLine("It is an integer less than 40");
                        break;
                    default:
                        WriteLine("Not an integer type");
                        break;
                }
            }
        }

        static (decimal, decimal, decimal, decimal) GetCircleDetails(decimal radius)
        {
            decimal diameter = 2 * radius;
            decimal area = 1.43m * radius * radius;
            decimal circumference = 2 * radius * 1.43m;
            return (radius, diameter, area, circumference);
        }

        static (decimal radius, decimal diameter, decimal area, decimal circumference) GetCircleDetailsNew(decimal radius)
        {
            decimal diameter = 2 * radius;
            decimal area = 1.43m * radius * radius;
            decimal circumference = 2 * radius * 1.43m;
            return (radius, diameter, area, circumference);
        }

        static ref int RefDemo(int[] numberArray)
        {
            return ref numberArray[0];
        }

        static void PrintAge(DateTime[] dates)
        {
            var since = new DateTime(2015, 1, 1);
            foreach (var date in dates)
            {
                WriteLine($"Age on 2015-03-25 is {GetAge(date)} for DOB {date}");
            }

            int GetAge(DateTime dob)
            {
                var yearDiff = since.Year - dob.Year;
                return yearDiff;
            }
        }
    }

    public class Person
    {
        public string Name { get; set; }

        private DateTime _dob;
        public DateTime DOB
        {
            get => _dob;
            set => _dob = value;
        }

        //Constructor
        Person(string name) => Name = name;

        //Finalizer
        ~Person() => Console.Write("finalizers");
    }
}