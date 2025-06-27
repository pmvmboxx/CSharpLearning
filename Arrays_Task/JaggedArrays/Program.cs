namespace JaggedArrays
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
        }

        #region ---===###$$$ Swap min&max rows JAGGED $$$###===---
        /// <returns>A row index with a max value.</returns>
        public static int GetMaxRowIndex(int[][] matrix, int rows, int cols)
        {
            int max = matrix[0][0];
            int maxRow = 0;

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    if (matrix[i][j] > max)
                    {
                        max = matrix[i][j];
                        maxRow = i;
                    }
                }
            }
            return maxRow;
        }

        /// <returns>A row index with a min value.</returns>
        public static int GetMinRowIndex(int[][] matrix, int rows, int cols)
        {
            int min = matrix[0][0];
            int minRow = 0;

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    if (matrix[i][j] < min)
                    {
                        min = matrix[i][j];
                        minRow = i;
                    }
                }
            }

            return minRow;
        }

        /// <summary>
        /// Swaps a row with a min value and a row with a max value in the matrix.
        /// </summary>
        public static void SwapRows(int[][] matrix, int row1, int row2)
        {
            if (row1 == row2)
            {
                return;
            }
            else
            {

            }

                int cols = matrix.GetLength(1);

            for (int j = 0; j < cols; j++)
            {
                int temp = matrix[row1][j];
                matrix[row1][j] = matrix[row2][j];
                matrix[row2][j] = temp;

            }
        }
        #endregion
    }
}
