# SortedMergedQueue
Simple implementation of sorted merged queue in C#

### Description

Class that takes several *IEnumerable* objects as input. Each of those inputs must provide sequence of *IComaparbe* objects that come in ascending sorted order.

***SortedMergedQueue*** class itself is *IEnumerable* that merges coming sequences and provides sequence that contains objects sorted across all inputs.

#### Exceptions

- when no inputs provided;
- when all inputs are empty;
- when an input contains data that is not sorted

> [!IMPORTANT]
>
> Not thread-safe!

