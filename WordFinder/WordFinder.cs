namespace WordFinder;

public class WordFinder
{
    private char[,] _leftToRightMatrix, _topToBottomMatrix;
    
    ///<summary>Matrix is not null, not empty and the strings are equal size</summary>>
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
    /// wordstream is not null, not empty and the strings are equal size, case is compared as is.
    /// </summary>>
    public IEnumerable<string> Find(IEnumerable<string> wordstream)
    {
        var found = new Dictionary<string, int>();
        Parallel.ForEach(wordstream, word =>
        {
            if (Find(word,_leftToRightMatrix)|| Find(word,_topToBottomMatrix))
            {
                lock (found)
                {
                    if (!found.ContainsKey(word))
                        found.Add(word, 1);
                    else
                        found[word] += 1;
                }
            }
        });
        //Assuming we want top 10 distinct values, most found with each horizontal dimension of the matrix, Im not caring about repeated words within the same row
        return found.OrderByDescending(x => x.Value).Take(10).Select(x => x.Key).ToList();
    }

    private bool Find(string word, char[,] matrix)
    {
        if (word.Length > matrix.GetLength(0))
            return false;
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
                    else
                        foundCnt = 0;

                    if (foundCnt == word.Length)
                        return true;
                    
                    //if pending to be found greater than size of the dimension just abort it
                    if(chars.Length - foundCnt>matrix.GetLength(1)-j)
                        break;
                }
            }
        }
        return false;
    }
}