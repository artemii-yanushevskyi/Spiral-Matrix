using System;
using System.Threading; // make pauses

namespace Spiral
{
    class Program
    {
        static void Main(string[] args)
        {
            string templatePath = "assets/tables.tex";
            TeXDocument content = new TeXDocument();

            Console.WriteLine("\"Spiral Matrix\"");

            int n;
            // n = int.Parse(Console.ReadLine());
            n = 30;

            for (int i = 0; i < n; i++)
            {
                var watch = System.Diagnostics.Stopwatch.StartNew();
                // Thread.Sleep(1);
                // the code that you want to measure comes here

                SpiralMatrix matrix = new SpiralMatrix(i);

                string mqty = matrix.Output();

                watch.Stop();

                var elapsedMs = watch.ElapsedMilliseconds;

                content.Insert("% Insertion requested from 'Program.cs'\n"
                          + mqty);
                content.Insert($"\\texttt{{Elapsed Milliseconds {elapsedMs}}}\n");

            }

            TeXDocument doc = new TeXDocument(templatePath);

            doc.InsertInPlace(content);

            doc.Compile();
        }
    }

    class SpiralMatrix
    {
        private readonly int size; // field, can't be modified 

        public const int defaultSize = 20; // not a field, non-modifiable
        //reference as SpiralMatrix.defaultSize

        public int Size
        {
            get { return this.size; }
        }

        public int GetSize()
        {
            return this.size;
        }


        public SpiralMatrix()
        {
            this.size = defaultSize;
        }

        public SpiralMatrix(int n)
        {
            this.size = n; // can't modify this field further
        }


        public void ConsoleOutput()
        {
            for (int i = 1; i <= size; i++)
            {
                for (int j = 1; j <= size; j++)
                {
                    LocalValue localValue = new LocalValue(i, j, size);

                    Console.Write(localValue.Value + "  ");
                }

                Console.Write("\n");
            }
        }

        public string Output()
        {
            string res;
            res = "\\[\\smqty(\n";

            for (int i = 1; i <= size; i++)
            {
                for (int j = 1; j <= size; j++)
                {
                    LocalValue localValue = new LocalValue(i, j, size);

                    res += localValue.Value + " & ";
                }
                res = res.Remove(res.Length - 2, 2);
                res += "\\\\\n";
            }
            res += ")\\]";
            return res;
        }
    }

    class LocalValue
    {
        private readonly int value;
        private readonly int[] coordinates = new int[2];
        private readonly int mainMatrixSize;

        public LocalValue(int i, int j, int n)
        {
            coordinates[0] = i;
            coordinates[1] = j;
            mainMatrixSize = n;

            value = CalculateLocalValue();
        }

        public int Value
        {
            get { return value; }
        }

        //~LocalValue()
        //{
        //    Console.WriteLine("Garbage collector deleted element" +
        //                      " on a position" +
        //                      $" ({coordinates[0]},{coordinates[1]}" +
        //                      $" with value {value}");
        //}

        private int CalculateLocalValue()
        {
            int n = this.mainMatrixSize;
            int i = this.coordinates[0];
            int j = coordinates[1]; // "this" can be omited

            int[] circleArray = { i, j, n - i + 1, n - j + 1 };
            int circle = Min(circleArray);
            int circleMatrixSize = n - 2 * (circle - 1);

            int diagonal = n * n - circleMatrixSize * circleMatrixSize + 1;

            int distance;

            if (circle == i) // top
                distance = j - circle;
            else if ((n - circleMatrixSize) / 2 + 1 == j) // left
                distance = 3 * circleMatrixSize - 2 - 1 + 
                    (n - i - (n - circleMatrixSize) / 2);
            else if (j == (n - circleMatrixSize) / 2 + circleMatrixSize) //right
                distance = circleMatrixSize - 1 + 
                    (i - (n - circleMatrixSize) / 2 - 1);
            else // bottom
                distance = 2 * circleMatrixSize - 1 - 1 + 
                    (circleMatrixSize + (n - circleMatrixSize) / 2 - j);

            int localValue = diagonal + distance;
            return localValue;
        }

        static int Min(int[] narray)
        {
            int min = narray[0];
            foreach (int n in narray)
            {
                if (min > n) min = n;
            }
            return min;
        }

    }

    // class MinArray : Array

}

static func()
{
    fasd
}
