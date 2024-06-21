using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xunit;

namespace WordFinder;

public class WordFinderTests
{
    [Theory]
    [InlineData(new string[] { "abcgc","fgwio","chill","pqnsd","uvdxy" },
        new string[] { "chill", "cold","snow", "wind" }, new string[]{"chill","cold","wind"})]
    [InlineData(new string[] { "abcgc","fgwio","chiPl","pqnsd","uvdxy" },
        new string[] { "chill", "cold","snow", "wind" }, new string[]{"cold","wind"})]
    [InlineData(new string[] { "abcgc","fgwio","aaaa","pqnsd","uvdxy" },
        new string[] { "chill", "cold","snow", "wind" }, new string[]{})]
    [InlineData(new string[] { "aaaaaaaaaaa","aaaaaaaaaaa","aaaaaaaaaaa","aaaaaaaaaaa","aaaaaaaaaaa" },
        new string[] { "aaaa", "aaa","aa", "a" }, new string[]{"a","aa","aaa","aaaa"})]
    [InlineData(new string[] { "aaaaaaaaaaa","bbbbbbbbbbb","ccccccccccc","ddddddddddd","eeeeeeeeeee" },
        new string[] { "abc", "e","ff", "cd","cde","cc","ba" }, new string[]{"abc","e","cd","cde","cc"})]
    public void TestWordFinder(string[] matrix, string[] words, string[] expected)
    {
        var wordFinder = new WordFinder(matrix.ToList());
        var result =wordFinder.Find(words);
        //we dont care about the order on comparing as the words matched equal number of times can be returned in diff order
        CollectionAssert.AreEqual(expected.Order().ToList(),result.Order().ToList());
    }
}
