using System;
using System.Collections.Generic;
using System.Linq; 
using System.Text;
using System.Threading.Tasks;

namespace Generator_of_Password
{
    class Program
    {
        static void sayhi()
            {
                Console.WriteLine("what are doing now?");
            }

        static void func(int x)
            {
                Console.WriteLine(x * 2);
            }

        static int Sum(int x, int y)
            {
            return x + y;
            }

        static int Pow(int x, int y = 2)
        {
            int result = 1;
            for (int i = 0; i < y; i++)
            {
                result *= x;
            }
            return result;
        }



        static string generatePswd(int length)
        {
            const string validChars = "text-for-generator";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            while (0 < length--)
            {
                res.Append(validChars[rnd.Next(validChars.Length)]);
            }
            return res.ToString();
        }


        static int Area(int h,int w)
        {
            return h * w;
        }


        class Customiz
        {
            public int sqr(int x)
            {
                int result = x * x;
                return result;
            }

           public void Print(int x)
            {
                Console.WriteLine(x);
            }



        }

        static void Main(string[] args)
        {
            Customiz sdq = new Customiz();

            sdq.Print(25);

            /*int res = Sum(12,11);
              Console.WriteLine(res); 
              int length = Convert.ToInt32((Console.ReadLine()));
              string pass = generatePswd(length);
              Console.WriteLine(pass);

              Custom nt = new Custom();
              Console.WriteLine(nt.sqr(5));

              sayhi();
              func(29);
             
            Console.WriteLine(Pow(12));
            Console.WriteLine(Pow(11,21));
            */
            int resul = Area(h:3, w:5);

            Console.WriteLine(resul);
            Console.ReadLine();

            Custom cs = new Custom();
            cs.Print(95);

            Student sn = new Student("David");
            sn.Speak();

        }
    }
}
