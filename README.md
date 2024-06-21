OBJECTIVE:

Presented with a character matrix and a large stream of words, your task is to create a Class
that searches the matrix to look for the words from the word stream. Words may appear
horizontally, from left to right, or vertically, from top to bottom. In the example below, the word
stream has four words and the matrix contains only three of those words ("chill", "cold" and
"wind"):

The search code must be implemented as a class with the following interface:
public class WordFinder
{
public WordFinder(IEnumerable<string> matrix) {
...
}
public IEnumerable<string> Find(IEnumerable<string> wordstream)
{ ...
}
}
The WordFinder constructor receives a set of strings which represents a character matrix. The
matrix size does not exceed 64x64, all strings contain the same number of characters. The
"Find" method should return the top 10 most repeated words from the word stream found in the
matrix. If no words are found, the "Find" method should return an empty set of strings. If any
word in the word stream is found more than once within the stream, the search results
should count it only once
Due to the size of the word stream, the code should be implemented in a high performance
fashion both in terms of efficient algorithm and utilization of system resources. Where possible,
please include your analysis and evaluation.


Solution:

Created a WordFinder class which performs the Find on a stream, some considerations
- matrix is not null not empty
- stream is not null not empty, each word has at least 1 char
- returns top 10 being matched, among = number of matches the order is alphabetical

Solution is organized as:
- WordFinder: the actual WordFinder api
- WordFinder.Test: unit tests using Theory and 10 test cases
- WordFinder.Benchmark: benchmark using BenchmarkDotNet (https://benchmarkdotnet.org/), with large test cases.

Benchmark:
Tried the following scenarios 

-Actual solution
| Method        | Mean       | Error     | StdDev    | Median     | Allocated |
|-------------- |-----------:|----------:|----------:|-----------:|----------:|
| TestScenario1 |   6.790 us | 0.0661 us | 0.0586 us |   6.774 us |  10.52 KB |
| TestScenario2 | 141.253 us | 2.7368 us | 5.3379 us | 138.686 us |  45.66 KB |
| TestScenario3 | 490.407 us | 6.2711 us | 5.2366 us | 491.596 us |  101.6 KB |

-Removed break on word greater than remaining: By removing the code below, throws similar results, it is slightly slower and memory allocated slightly lower.
<img width="685" alt="image" src="https://github.com/lmarolla/TestQuBeyond/assets/74197657/a6df3d1f-29be-4743-9f72-11ff3f6d3f80">

| Method        | Mean       | Error     | StdDev    | Allocated |
|-------------- |-----------:|----------:|----------:|----------:|
| TestScenario1 |   6.870 us | 0.0361 us | 0.0301 us |  10.68 KB |
| TestScenario2 | 153.536 us | 3.0051 us | 5.9318 us |  45.65 KB |
| TestScenario3 | 508.914 us | 9.0221 us | 7.9979 us | 100.95 KB |

-Removed Parallel for each: by replacing with a regular foreach, in the first case which is the sample matrix of the exercise its actually faster.
But on Scenario 2 and 3 with a large matrix and a large word stream it is much slower than the alternative. 
Allocated memory increases in parallel foreach but again it's the first scenario where the parallel might not be useful and we could be overengineering the solution
A possible improvement of the algo could be to decide dynamically which strategy to follow and do a normal for based on the size of the matrix and words
<img width="543" alt="image" src="https://github.com/lmarolla/TestQuBeyond/assets/74197657/0f8800f1-b202-4f2a-ae19-5977154bbba6">


| Method        | Mean         | Error     | StdDev    | Allocated |
|-------------- |-------------:|----------:|----------:|----------:|
| TestScenario1 |     2.384 us | 0.0079 us | 0.0070 us |   1.76 KB |
| TestScenario2 |   756.692 us | 0.8356 us | 0.7816 us |  19.94 KB |
| TestScenario3 | 3,128.868 us | 3.2189 us | 3.0110 us |  74.05 KB |
