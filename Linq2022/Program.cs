// See https://aka.ms/new-console-template for more information

// Specify the data source.

using Humanizer;

var nums = Enumerable.Range(1, 1_000_000_000);

var seq = nums
    .Where(MyMath.IsPrime)
    .Skip(1_000_000)
    .Take(100);

foreach (var i in seq)
{
    Console.Write(i + " ");
}



//int[] scores = { 97, 92, 81, 60 };

//// Define the query expression.
//var scoreQuery = scores
//    .Where(MyMath.IsOdd)
//    .Select(s => s.ToWords()); 


//// Execute the query. LINQ uses deferred execution
//foreach (var i in scoreQuery)
//{
//    Console.Write(i + " ");
//}
// LINQ to Objects == in memory
// LINQ to Entities== in database with ORM / Entity Framework

var people = new Person[]
{
    new() { FirstName = "Shaul", LastName = "Niyazov" },
    new() { FirstName = "Sruly", LastName = "Brach", Birthday = new DateOnly(2001, 2, 28) },
};

var processedPeople =
    people.Where(p => p.FirstName.StartsWith("S"))
        .Select(p =>
            new
            {
                FullName = $"{p.LastName}, {p.FirstName}",
                Age = (p.Birthday == null)
                    ? (double?)null
                    : (DateTime.Now - ((DateOnly)p.Birthday).ToDateTime(TimeOnly.MinValue)).TotalDays / 365.25
            })
        .OrderBy(p => p.Age)
        .ThenBy(p => p.FullName);

foreach (var i in processedPeople)
{
    Console.WriteLine(i + " ");
}
public static class MyMath
{
    public static double MySqrt2(int x)
    {
        Console.WriteLine($"Called MySqrt with arg {x}");
        return Math.Sqrt(x);
    }

    public static bool IsPrime(int x)
    {
        for (int i = 2; i <= Math.Sqrt(x); i++)
            if (x % i == 0)
                return false;
        return true;
    }
    public static bool IsOdd(int x)
    {
        return x % 2 == 1;
    }

    public static Object AnonTypeFactory()
    {
        var x = new { AnimalName = "elephant", Fears = "Mice" };
        return x;
    }
}

class Person
{
    public String FirstName { get; set; }
    public String LastName { get; set; }
    public DateOnly? Birthday { get; set; }

    public override string ToString()
    {
        return $"{FirstName} {LastName} {Birthday}";
    }
}

