using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParallelExamples
{
    public class OptimalParallelism
    {
        public static int[,] MatrixMultiply(int[,] a, int[,] b, int degreeOfParallelism)
        {
            // [... argument validation not shown ...]
            int aRows = a.GetUpperBound(0) + 1;
            int aCols = a.GetUpperBound(1) + 1;
            int bRows = b.GetUpperBound(0) + 1;
            int bCols = b.GetUpperBound(1) + 1;
            int[,] result = new int[bRows, aCols];
            ParallelOptions options = new ParallelOptions
            {
                MaxDegreeOfParallelism = degreeOfParallelism
            };
            Parallel.For(0, bRows, options, row =>
            {
                for (int col = 0; col < aCols; col++)
                    for (int k = 0; k < aRows; k++)
                        result[row, col] += a[k, col] * b[row, k];
            });
            return result;
        }
    }
}
