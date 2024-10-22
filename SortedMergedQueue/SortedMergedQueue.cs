using System.Collections;

namespace SortedMergedQueue;

/// <summary>
/// Takes a list of sorted inputs and merges them into a single sorted queue.
/// </summary>
/// <typeparam name="T">Enumerable of sortable elements</typeparam>
public sealed class SortedMergedQueue<T> : IEnumerable<T> where T : IComparable<T>
{
    private readonly List<InputIterator<T>> _iterators = new();
    private readonly SortedMergedQueueIterator<T> _iterator;

    /// <summary>
    /// Creates a new instance of SortedMergedQueue with the given inputs.
    /// </summary>
    /// <param name="inputs">Collection of inputs</param>
    /// <exception cref="ArgumentException">When no inputs given</exception>
    public SortedMergedQueue(params IEnumerable<T>[] inputs)
    {
        foreach (var input in inputs)
            _iterators.Add(new InputIterator<T>(input.GetEnumerator()));
        
        if (_iterators.Count == 0)
            throw new ArgumentException("At least one input is required");
        
        _iterator = new SortedMergedQueueIterator<T>(_iterators);
    }

    public IEnumerator<T> GetEnumerator()
    {
        while (_iterator.MoveNext())
            yield return _iterator.Current;
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}

internal class SortedMergedQueueIterator<T> : IEnumerator<T> where T : IComparable<T>
{
    private readonly SortedList<T, InputIterator<T>> _sortedIterators = new();

    public T Current { get; private set; }

    public SortedMergedQueueIterator(List<InputIterator<T>> inputIterators)
    {
        foreach (var iterator in inputIterators.Where(i => i.MoveNext()))
            _sortedIterators.Add(iterator.Current, iterator);
        
        if (_sortedIterators.Count == 0)
            throw new ArgumentException("At least one input is required");
        
        Current = _sortedIterators.First().Key;
    }
    
    public bool MoveNext()
    {
        if (_sortedIterators.Count == 0) return false;
        
        var minIterator = _sortedIterators.First();
        
        Current = minIterator.Key;
        
        if (!minIterator.Value.MoveNext())
            _sortedIterators.Remove(minIterator.Key);
        else
        {
            _sortedIterators.Remove(minIterator.Key);
            _sortedIterators[minIterator.Value.Current] = minIterator.Value;
        }
        
        return true;
    }

    public void Reset()
    {
        throw new NotImplementedException();
    }

    object IEnumerator.Current => Current;

    public void Dispose() { }
}

internal class InputIterator<T>(IEnumerator<T> enumerator) : IEnumerator<T> where T : IComparable<T>
{
    private T? _prevMin;
    
    public T Current { get; private set; }

    public bool MoveNext()
    {
        var gotNext = enumerator.MoveNext();

        if (!gotNext) return false;
        
        if (_prevMin is not null && _prevMin.CompareTo(enumerator.Current) > 0)
            throw new InvalidOperationException("Input is not sorted");
        
        _prevMin = enumerator.Current;
        Current = enumerator.Current;
        
        return true;
    }

    public void Reset()
    {
        throw new NotImplementedException();
    }

    object IEnumerator.Current => Current;

    public void Dispose() { }
}