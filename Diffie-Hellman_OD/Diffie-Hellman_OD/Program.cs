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

            DiffieHellman diffieHellman = new DiffieHellman();

            string text = "Hello world.";

            byte[] ciphered = diffieHellman.Encrypt(diffieHellman.PublicKey, text);

            Console.WriteLine(Encoding.Default.GetString(ciphered));
            Console.WriteLine(diffieHellman.Decrypt(diffieHellman.PublicKey, ciphered, diffieHellman.IV));

        }
    }
}
