﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Diffie_Hellman_OD;

namespace Diffie_Hellman_OD.Tests
{
    [TestClass]
    public class DiffieHellmanTests
    {
        [TestMethod]
        public void Encrypt_Decrypt()
        {
            string text = "Hello World!";

            using (var bob = new DiffieHellman())
            {
                using (var alice = new DiffieHellman())
                {
                    // Bob uses Alice's public key to encrypt his message.
                    byte[] secretMessage = bob.Encrypt(alice.PublicKey, text);

                    // Alice uses Bob's public key and IV to decrypt the secret message.
                    string decryptedMessage = alice.Decrypt(bob.PublicKey, secretMessage, bob.IV);
                }
            }
        }
    }
}
