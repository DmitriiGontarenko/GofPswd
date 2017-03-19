using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator_of_Password
{
    class Custom
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

    class Person
    {
        protected int Age { get; set; }
        protected string Name { get; set; }
    }

    class Student : Person
    {
        public Student(string nm)
        {
            Name = nm;
        }

        public void Speak()
        {
            Console.WriteLine("Name" + Name);
        }
    }
}
