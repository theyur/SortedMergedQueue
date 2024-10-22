using SortedMergedQueue;

namespace SortedMergedQueueTests;

public class UnitTest1
{
    [Fact]
    public void AllInputsAre_EvenlyOrdered_OfTheSameLength_ProvidesExpectedSequence()
    {
        var input1 = new[] { new Person("Alice", 20), new Person("Bob", 30), new Person("Charlie", 40) };
        var input2 = new[] { new Person("David", 25), new Person("Eve", 35), new Person("Frank", 45) };
        var input3 = new[] { new Person("Grace", 22), new Person("Hank", 32), new Person("Ivy", 42) };

        var sut = new SortedMergedQueue<Person>(input1, input2, input3);

        var expected = new[]
        {
            new Person("Alice", 20), new Person("Grace", 22), new Person("David", 25),
            new Person("Bob", 30), new Person("Hank", 32), new Person("Eve", 35),
            new Person("Charlie", 40), new Person("Ivy", 42), new Person("Frank", 45)
        };

        Assert.Equal(expected, sut);
    }

    [Fact]
    public void AllInputsAre_NotEvenlyOrdered_OfTheSameLength_ProvidesExpectedSequence()
    {
        var input1 = new[] { new Person("Alice", 20), new Person("David", 25), new Person("Charlie", 40) };
        var input2 = new[] { new Person("Bob", 30), new Person("Hank", 32), new Person("Eve", 35) };
        var input3 = new[] { new Person("Grace", 22), new Person("Ivy", 42), new Person("Frank", 45) };

        var sut = new SortedMergedQueue<Person>(input1, input2, input3);

        var expected = new[]
        {
            new Person("Alice", 20), new Person("Grace", 22), new Person("David", 25),
            new Person("Bob", 30), new Person("Hank", 32), new Person("Eve", 35),
            new Person("Charlie", 40), new Person("Ivy", 42), new Person("Frank", 45)
        };

        Assert.Equal(expected, sut);
    }
    
    [Fact]
    public void AllInputsAre_NotEvenlyOrdered_OfDifferentLength_ProvidesExpectedSequence()
    {
        var input1 = new[]
        {
            new Person("Alice", 20), new Person("David", 25), new Person("Hank", 32),
            new Person("Charlie", 40), new Person("Ivy", 42), new Person("Frank", 45)
        };
        var input2 = new[] { new Person("Bob", 30), new Person("Eve", 35) };
        var input3 = new[] { new Person("Grace", 22) };

        var sut = new SortedMergedQueue<Person>(input1, input2, input3);

        var expected = new[]
        {
            new Person("Alice", 20), new Person("Grace", 22), new Person("David", 25),
            new Person("Bob", 30), new Person("Hank", 32), new Person("Eve", 35),
            new Person("Charlie", 40), new Person("Ivy", 42), new Person("Frank", 45)
        };

        Assert.Equal(expected, sut);
    }
    
    [Fact]
    public void WhenNoInputsGiven_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() => new SortedMergedQueue<Person>());
    }
    
    [Fact]
    public void WhenAllInputsAreEmpty_ThrowsArgumentException()
    {
        var input1 = Array.Empty<Person>();
        var input2 = Array.Empty<Person>();
        var input3 = Array.Empty<Person>();

        Assert.Throws<ArgumentException>(() => new SortedMergedQueue<Person>(input1, input2, input3));
    }
    
    [Fact]
    public void WhenSomeInputsAreEmpty_ProvidesExpectedSequence()
    {
        var input1 = new[] { new Person("Alice", 20), new Person("Bob", 30), new Person("Charlie", 40) };
        var input2 = Array.Empty<Person>();
        var input3 = new[] { new Person("Grace", 22), new Person("Hank", 32), new Person("Ivy", 42) };

        var sut = new SortedMergedQueue<Person>(input1, input2, input3);

        var expected = new[]
        {
            new Person("Alice", 20), new Person("Grace", 22), new Person("Bob", 30),
            new Person("Hank", 32), new Person("Charlie", 40), new Person("Ivy", 42)
        };

        Assert.Equal(expected, sut);
    }
    
    [Fact]
    public void WhenInputIsNotSorted_ThrowsArgumentException()
    {
        var input1 = new[] { new Person("Bob", 30), new Person("Alice", 20), new Person("Charlie", 40) }; // Not sorted
        var input2 = new[] { new Person("Bob", 30), new Person("Hank", 32), new Person("Eve", 35) };
        var input3 = new[] { new Person("Grace", 22), new Person("Ivy", 42), new Person("Frank", 45) };
        
        Assert.Throws<ArgumentException>(() => new SortedMergedQueue<Person>(input1, input2, input3));
    }
}

public class Person(string name, int age) : IComparable<Person>
{
    public string Name { get; } = name;
    public int Age { get; } = age;

    public int CompareTo(Person? other) => Age.CompareTo(other?.Age);
}