namespace WordFinder;
public class WordFinder
{
    private char[,] _leftToRightMatrix, _topToBottomMatrix;
    ///<summary>Param: Matrix is not null, not empty and the strings are equal size</summary>>
    public WordFinder(IEnumerable<string> matrix)
    {
        _leftToRightMatrix= new char[matrix.Count(),matrix.First().Length];
        _topToBottomMatrix= new char[matrix.First().Length, matrix.Count()];
        var i = 0;
        var matrixLength = matrix.Count();
        foreach (var str in matrix)
        {
            var j = 0;
            var strLength = str.Length;
            foreach (var ch in str)
            {
                _leftToRightMatrix[i,j] = ch;
                _topToBottomMatrix[j,i] = ch;
                j++;
            }
            i++;
        }
    }
    
    /// <summary>Finds the words in the initialized matrix.
    /// Param: wordstream is not null, not empty and the strings are equal size, case is compared as is.
    /// </summary>>

    public IEnumerable<string> Find(IEnumerable<string> wordstream)
    {
        var found = new Dictionary<string, int>();
        var distinctWordstream = wordstream.Distinct();
        Parallel.ForEach(distinctWordstream, word =>
        {
            //On the matrix we count matches both Vertically and Horizontally, even if its 1 char word it will be matched
            var leftMatches = Find(word, _leftToRightMatrix);
            var rightMatches = Find(word,  _topToBottomMatrix);
            var totalMatches = leftMatches + rightMatches;
            
            if (totalMatches>0)
            {
                lock (found)
                {
                    if (!found.ContainsKey(word))
                        found.Add(word, totalMatches);
                    else
                        found[word] += totalMatches;
                }
            }
        });
        //Assuming we want top 10 distinct values of the words with most number of matches, after that is alphabetically ordered
        return found.OrderByDescending(x => x.Value).ThenBy(x=>x.Key).Take(10).Select(x => x.Key).ToList();
    }

    private int Find(string word,  char[,] matrix)
    {
        var matches = 0;
        if (word.Length > matrix.GetLength(1))
            return matches;
        else
        {
            
            var chars = word.ToCharArray();
            for (var i = 0; i < matrix.GetLength(0); i++)
            {
                var foundCnt = 0;
                for (var j = 0; j < matrix.GetLength(1); j++)
                {
                    if (matrix[i, j] == chars[foundCnt])
                        foundCnt++;
                    else if (matrix[i, j] == chars[0])
                        foundCnt = 1;
                    else
                        foundCnt = 0;

                    if (foundCnt == word.Length)
                    {
                        matches++;
                        foundCnt = 0;
                    }
                    
                    //if pending to be found greater than size of the dimension just abort it
                     if(chars.Length - foundCnt>matrix.GetLength(1)-j)
                         break;
                }
            }
        }
        return matches;
    }
}