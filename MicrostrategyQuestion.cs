

class Program
{
    // M   M   J
    // Y A E S O
    // N   I   E
    static void Main(string[] args)
    {
        string s = "MYNAMEISJOE";

        var ans = convert(s, 5);
        Console.WriteLine(ans);
    }
    
    static string convert(string s, int rowNo)
    {
        List<List<char>> mat = new List<List<char>>();

        // add row number of lists first in matrix
        for(int rownumber = 0; rownumber < rowNo; rownumber++)
        {
            List<char> list = new List<char>();
            mat.Add(list);
        }

        int i = 0;
        int row = 0;
        while(i < s.Length)
        {
            while(i < s.Length && row < rowNo)
            {
                mat[row].Add(s[i]);
                i++;
                row++;
            }

            row = rowNo - 2;

            while(i<s.Length && row > 0)
            {
                mat[row].Add(s[i]);
                i++;
                row--;
            }
        }

        string ans = "";
        for(int matRow = 0; matRow < mat.Count; matRow++)
        {
            for(int col = 0; col < mat[matRow].Count; col++)
            {
                ans += mat[matRow][col];
            }
        }

        printMatrix(mat);

        return ans;
    }

    private static void printMatrix(List<List<char>> mat)
    {
        for (int matRow = 0; matRow < mat.Count; matRow++)
        {
            for (int col = 0; col < mat[matRow].Count; col++)
            {
                Console.Write(mat[matRow][col]);
            }
            Console.WriteLine();
        }
    }
}
