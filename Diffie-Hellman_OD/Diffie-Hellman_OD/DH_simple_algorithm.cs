using System;
using System.Collections.Generic;
using System.Text;

namespace Diffie_Hellman_OD
{
    class DH
    {
        private UInt32 p, g, x, y, kA, kB;
        public UInt32 X, Y;
        Random rand = new Random();

        public void Display()
        {
            Console.WriteLine("p: {0}", p);
            Console.WriteLine("g: {0}", g);
            Console.WriteLine("x: {0}", x);
            Console.WriteLine("y: {0}", y);
            Console.WriteLine("X: {0}", X);
            Console.WriteLine("Y: {0}", Y);
            Console.WriteLine("kA: {0}", kA);
            Console.WriteLine("kB: {0}", kB);
        }

        public DH()
        {
            p = 12853;
            g = 11491;
        }

        public void A1()
        {
            x = (UInt32)rand.Next(10000, 20000);
            X = (UInt32)powerLL(g, x);
        }

        public void B1()
        {
            y = (UInt32)rand.Next(10000, 20000);
            Y = (UInt32)powerLL(g, y);
        }

        public void kCheck()
        {
            kA = (UInt32)powerLL(Y, x);
            kB = (UInt32)powerLL(X, y);

            if (kA == kB)
            {
                Console.WriteLine("kA is equal to kB");
            }
            else
            {
                Console.WriteLine("kA is NOT equal to kB");
            }
        }

        private UInt64 powerLL(UInt64 x, UInt64 y)
        {
            ulong result = 1;
            while (y > 0)
            {
                if (y % 2 == 1)
                {
                    result = result * x % p;
                }
                y /= 2;
                x = x * x % p;
            }
            return result;
        }
    }
}
