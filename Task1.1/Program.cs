using System;

namespace Task1._1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }

    abstract class A
    {
        public string Prop1 { get; set; } = "aaa";
        public int Prop2
        {
            get
            {
                int prop2 = int.Parse(Console.ReadLine());
                if (prop2 < 0)
                {
                    prop2 = Math.Abs(prop2);
                }
                if (prop2 > 100)
                {
                    prop2 = int.Parse(prop2.ToString()[^2].ToString()) * 10 + int.Parse(prop2.ToString()[^1].ToString());
                }
                return prop2;
            }
            set => Prop2 = value;
        }

        public int Foo()
        {
            return 1;
        }

        public abstract void Foo(DateTime date);
    }

    class B : A
    {
        private double Prop1 { get; set; }

        public override void Foo(DateTime date)
        {
            throw new NotImplementedException();
        }
    }

    abstract class C : A
    {
        protected Guid Prop1 { get; set; }
    }

    class D : C
    {
        public E PropD1 { get; set; }
        public override void Foo(DateTime date)
        {
            throw new NotImplementedException();
        }
    }

    class E
    {
        public E PropE1 { get; set; }

        public void Bar()
        {

        }

        private void Bar(int size)
        {

        }
    }
}
