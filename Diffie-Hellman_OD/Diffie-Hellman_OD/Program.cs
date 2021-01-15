using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diffie_Hellman_OD
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("DH:");
            DH dh = new DH();
            dh.A1();
            dh.B1();
            dh.kCheck();
            dh.Display();
        }
    }
}
