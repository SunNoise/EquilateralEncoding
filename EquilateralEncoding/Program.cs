using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquilateralEncoding
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Number of Categories: ");
            int n = int.Parse(Console.ReadLine());
            Console.Write("Lowest Range: ");
            double l = double.Parse(Console.ReadLine());
            Console.Write("Highest Range: ");
            double h = double.Parse(Console.ReadLine());

            var eq = new Equilateral(n, h, l);
            int y = 0;
            foreach(var a in eq.Result)
            {
                var line = new StringBuilder();
                line.Append(String.Format("{0}:", y));
                for(int x = 0; x < a.Length;x++)
                {
                    if (x > 0)
                        line.Append(',');
                    line.Append(a[x]);
                }
                Console.WriteLine(line);
                y++;
            }
            Console.ReadKey();
        }
    }

    public class Equilateral
    {
        public double[][] Result { get; }

        public Equilateral(int n, double high = 1, double low = -1)
        {
            Result = new double[n][]; // n - 1
            for (int i = 0; i < n; i++)
            {
                Result[i] = new double[n - 1];
            }

            //seed for the first two categories
            Result[0][0] = -1;
            Result[1][0] = 1.0;

            for (int k = 2; k < n; k++)
            {
                double r = k;
                double f = Math.Sqrt(r * r - 1.0) / r;
                var s = -1.0 / r;
                for (int i = 0; i < k; i++)
                {
                    Result[i][k - 1] = s;
                    for (int j = 0; j < k - 1; j++)
                    {
                        Result[i][j] *= f;
                        Result[k][j] = 0.0;
                    }
                }
                Result[k][k - 1] = 1.0;
            }

            if (low == -1 && high == 1)
                return;

            //scale to other ranges different than (-1,1)
            for (int row = 0; row < Result.GetLength(0); row++)
            {
                for (int col = 0; col < Result[0].GetLength(0); col++)
                {
                    const double min = -1;
                    const double max = 1;
                    Result[row][col] = ((Result[row][col] - min) / (max - min))
                                       * (high - low) + low;
                }
            }
        }
    }
}
