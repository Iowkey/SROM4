using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class Program
    {

        /*-------------------------------------_____---------------------------
        --------------------l----------/\-----l     ) ---- /\------|    | ----
        --------------------l     --  /  \ ---l_____) ----/  \-----|____| ----
        --------------------l     -- /----\ --l      )---/----\----     | ------
        --------------------l_____--/      \--l______)--/      \--------| ------
        */

        public static int[] BinToArr(string a)
        {
            while (a.Length < 239)
                a = "0" + a;
            int[] Arr = new int[a.Length];
            for (int i = 0; i < a.Length; i++)
            {
                Arr[i] = Convert.ToInt32(a.Substring(i, 1), 2);
            }
            //Array.Resize(ref Arr, 239);
            return Arr;
        }

        public static string ArrToBin(int[] a)
        {
            var binstr = "";
            for (int i = 0; i < a.Length; i++)
            {
                binstr = binstr + Convert.ToString(a[i]);
            }
            return binstr;
        }

        public static int[] NBAddition(int[] a, int[] b)
        {
            //int greatest_length = Math.Max(a.Length, b.Length);
            //Array.Resize(ref a, greatest_length);
            //Array.Resize(ref b, greatest_length);
            //int commonlen = 239;
            int[] c = new int[a.Length];
            for (int i = 0; i < a.Length; i++)
            {
                c[i] = a[i] ^ b[i];
            }

            return c;
        }

        public static int[] Square(int[] a)
        {
            return LoopShiftR(a, 1);
        }

        public static string Trace(int[] a)
        {
            int res = a[0];
            for (int i = 1; i < a.Length; i++)
            {
                res = res + a[i];
                if (res == 2)
                    res = 0;
            }
            return Convert.ToString(res);
        }

        public static int[,] MulMatrix()
        {
            int mod = 2 * 239 + 1;
            int[,] lambda = new int[239, 239];
            int[] twomod = new int[239];

            for (int i = 1; i < 239; i++)
                twomod[i] = (twomod[i - 1] * 2) % mod;
            twomod[0] = 1;

            int twoi, twoj;
            for (int i = 0; i < 239; i++)
            {
                twoi = twomod[i];
                for (int j = 0; j < 239; j++)
                {
                    twoj = twomod[j];
                    if ((((twoi - twoj) + mod) % mod) == 1 || ((twoi + twoj) % mod) == 1 || (((-twoi - twoj) + mod) % mod) == 1 || ((twoj - twoi + mod) % mod) == 1)
                    {
                        lambda[i, j] = 1;
                    }
                    else
                        lambda[i, j] = 0;
                }
            }
            return lambda;
        }

        public static int[] NBMultiplication(int[] a, int[] b)
        {
            int[] res = new int[239];
            int[,] Matrix = MulMatrix();
            for (int z = 0; z < 239; z++)
            {
                int[] temp = new int[239];
                for (int j = 0; j < 239; j++)
                {
                    for (int i = 0; i < 239; i++)
                        temp[j] = (temp[j]) ^ (a[i] & Matrix[i, j]);
                }
                int lambdael = 0;
                for (int i = 0; i < a.Length; i++)
                    lambdael = (lambdael) ^ (temp[i] & b[i]);
                res[z] = lambdael;
                a = LoopShiftL(a, 1);
                b = LoopShiftL(b, 1);
            }
            return res;
        }

        public static int[] Inversed(int[] a)
        {
            int[] b = a;
            int k = 1;
            int[] m = new int[8];
            m[0] = 1;
            m[1] = 1;
            m[2] = 1;
            m[3] = 0;
            m[4] = 1;
            m[5] = 1;
            m[6] = 1;
            m[7] = 0;
            //Array.Reverse(m);
            //int t = m[6];
            //int[] c = new int[a.Length];
            for (int i = 1; i < 8; i++)
            {
                b = NBMultiplication(Power(b, k), b);
                k = 2 * k;
                if(m[i] == 1)
                {
                    b = NBMultiplication(Square(b), a);
                    k += 1;
                }
            }
            return Square(b);
        }

        public static int[] Power(int[] a, int k)
        {
            int[] Beta = a;
            Beta = LoopShiftR(Beta, k);
            return Beta;
        }
            
        public static string GlueOne(int m)
        {
            string temp = "";
            while (temp.Length != m)
                temp += "1";
            return temp;
        }

        public static int [] NBPower(int[] a, int[] n)
        {
            string one = GlueOne(239);
            int[] result = BinToArr(one);

            for (var i = a.Length - 1; i >= 0; i--)
            {
                if (n[i] == 1)
                {
                    result = NBMultiplication(result, a);
                }
                a = LoopShiftR(a, 1);
            }
            return result;
        }

        public static int[] LoopShiftR(int[] a, int n)
        {
            int[] b = new int[a.Length];
            int i = 0;
            int k = n;
            while(i < b.Length)
            {
                b[i] = a[(a.Length - k) % a.Length];
                i++;
                k--;
            }
            return b;
        }

        public static int[] LoopShiftL(int[] a, int n)
        {
            int[] b = new int[a.Length];
            int i = 0;
            int k = n;
            while(i < b.Length)
            {
                b[i] = a[k % b.Length];
                i++;
                k++;
            }
            return b;
        }

        
           

        public static string GetBin()
        {
            Console.Write("\nEnter an element: ");
            string bin = Console.ReadLine();
            if (bin.Length < 239)
                /*for(int i = 0; i < (239 - bin.Length); i++)
                {
                    bin = "0" + bin;
                }*/
                while (bin.Length != 239)
                    bin = "0" + bin;
            return bin;
        }


        static void Main(string[] args)
        {
            string bin1 = GetBin();
            string bin2 = GetBin();

            Console.Write("\n" + bin1);
            Console.Write("\n" + bin2);

            int[] Arr1 = BinToArr(bin1);
            int[] Arr2 = BinToArr(bin2);
            var ResultAdd = NBAddition(Arr1, Arr2);
            Console.Write("\nAddition: " + ArrToBin(ResultAdd));
            Console.Write("\nLength: " + ArrToBin(ResultAdd).Length);
            //var ResultMul = NBMultiplication(Arr1, Arr2);
            //Console.Write("\nMultiplication: " + ArrToBin(ResultMul));
            //int[,] test = MulMatrix();
            //for (int k = 0; k < 3; k++)
            //{
            //    for(int l = 0; l < 3; l++)
            //    {
            //        Console.Write(test[k, l] + " ");
            //    }
            //    Console.Write("\n");
            //}
            //Console.Write("\n" + (77 % 13));
            //int[] a = new int[6];
            //for (int i = 0; i < 6; i++)
            //{
            //    Console.Write("Enter an element for array: ");
            //    string b = Console.ReadLine();
            //    a[i] = Convert.ToInt16(b);
            //}
            //a = LoopShiftL(a, 0);
            //for(int j = 0; j < a.Length; j++)
            //{

            //    Console.Write("\n" + a[j]);
            //}



            //Console.Write("\n " + LoopShiftL(a, 3));
            //Console.Write("\n " + LoopShiftL(Arr1, 0));
            //Console.Write("\n " + LoopShiftL(Arr1, 3));
            //Console.Write("Enter an element for loop: ");
            //string test = Console.ReadLine();
            //int[] test1 = BinToArr(test);
            //int[] ResultTest = LoopShift(test1, 4);
            //Console.Write("\nLoopShift: " + ArrToBin(ResultTest));

            //int[,] testarr = new int[239, 239];
            // testarr = MulMatrix();
            // Console.Write("\nMatrix: " + testarr);
            //var a = "100000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000001000000000000111";
            //Console.Write(a.Length);


            //int[] Arr1 = BinToArr(bin1);
            //int[] Arr2 = BinToArr(bin2);

            //int[] ResultMul = Multiplication(Arr1, Arr2);
            //Console.Write("\nResult: " + ArrToBin(ResultMul));
            //int[] ResultAdd = Addition(Arr1, Arr2);
            //Console.Write("\n Addition result: " + ArrToBin(ResultAdd));
            //string hex1 = GetNumbers();
            //string hex2 = GetNumbers();

            //ulong[] Arr2 = NumToArr(hex2);
            //ulong[] Arr1 = NumToArr(hex1);

            //ulong[] ResultNOK = NOK(Arr1, Arr2);
            //Console.Write("\n NOK result: " + ShowResult(ResultNOK));
            //ulong[] Result = LongAddition(Arr1, Arr2);
            //Console.Write("\nAddition: " + ShowResult(Result));

            //ulong[] ResultSub = LongSub(Arr1, Arr2);
            //Console.Write("\nSubstraction: " + ShowResult(ResultSub));

            //Console.Write("\nResult: " + LongCmp(hex1, hex2));

            //Console.Write("\nLongMulOneDigit")
            //ulong[] ResultPow = LongPowerWindow(hex1, hex2);
            //ulong[] ResultMul = LongMul(Arr1, Arr2);
            //ulong[] ResultDiv = LongDiv(Arr1, Arr2);
            //Console.Write("\nPower: " + ShowResult(ResultPow));
            //Console.Write("\nDivision: " + ShowResult(ResultDiv));
            //Console.Write("\nMultiplication: " + ShowResult(ResultMul));
            Console.Write("\nPress any key..");
            Console.ReadKey();

        }
    }
}




















