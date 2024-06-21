using Xunit;

namespace WordFinder.Test;

public class WordFinderTests
{
    [Theory]
    [InlineData(new string[] { "abcgc", "fgwio", "chill", "pqnsd", "uvdxy" },
        new string[] { "chill", "cold", "snow", "wind" }, new string[] { "chill", "cold", "wind" })]
    [InlineData(new string[] { "abcgc", "fgwio", "chiPl", "pqnsd", "uvdxy" },
        new string[] { "chill", "cold", "snow", "wind" }, new string[] { "cold", "wind" })]
    [InlineData(new string[] { "abcgc", "fgwio", "aaaa", "pqnsd", "uvdxy" },
        new string[] { "chill", "cold", "snow", "wind" }, new string[] { })]
    [InlineData(new string[] { "aaaaaaaaaaa", "aaaaaaaaaaa", "aaaaaaaaaaa", "aaaaaaaaaaa", "aaaaaaaaaaa" },
        new string[] { "aaaa", "aaa", "aa", "a" }, new string[] { "a", "aa", "aaa", "aaaa" })]
    [InlineData(new string[] { "aaaaaaaaaaa", "bbbbbbbbbbb", "ccccccccccc", "ddddddddddd", "eeeeeeeeeee" },
        new string[] { "abc", "e", "ff", "cd", "cde", "cc", "ba" }, new string[] {"e", "abc","cd", "cde","cc" })]
    [InlineData(new string[]
        {
            "TRETHEMATRIXHASYOUPQRS", "FFOLLOWTHEWHITERABBITA", "AKQWERAYUAOPASDFGHJKLZ", "KPQRSTKVWKYZABCDEFGHIX",
            "ELMNOPERSEUVWXYZABCDEC", "UMQWERUYUUOPASDFGHJKLN", "PNOPQRPTUPWXYZABEFGELL", "NQWERTNUIOPASDFGHJKLZO",
            "EMNOPQESTUVWXYZABCDEFP", "OWERTYOIOPASDFGHJKLZXQ", "ASDFGHJKLZXCVBNMQWERTU"
        },
        new string[] { "WAKEUPNEO", "THEMATRIXHASYOU", "FOLLOWTHEWHITERABBIT", "MRANDERSON" },
        new string[] { "FOLLOWTHEWHITERABBIT","THEMATRIXHASYOU","WAKEUPNEO",  })]
    [InlineData(new string[] { "abcgc", "fgwio", "chill", "pqnsd", "uvdxy" },
        new string[] { "chill", "cold", "snow", "wind","abcgc", 
            "fgwio", "chill", "pqnsd", "uvdxy","afcpu", "bghqv", "cwind", "gilsx","coldy" 
        }, new string[] {  "abcgc", "afcpu","bghqv", "chill","cold", 
            "coldy", "cwind", "fgwio", "gilsx", "pqnsd"})]
    [InlineData(new string[] { "ab", "ba", "ab", "ba"},
        new string[] { "chill", "ab", "ba", "wind" }, new string[] { "ab", "ba" })]

    public void TestWordFinder(string[] matrix, string[] words, string[] expected)
    {
        var wordFinder = new WordFinder(matrix.ToList());
        var result = wordFinder.Find(words);
        Assert.Equal(expected.ToList(), result.ToList());
    }
}
