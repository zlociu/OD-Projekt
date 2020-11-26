using System;
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
                    using(var adamB = new DiffieHellman())
                    {
                        using(var adamA = new DiffieHellman())
                        {
                            // Bob uses what he thinks is Alice's public key to encrypt his message but is using a key provided by Adam instead.
                            byte[] secretMessage = bob.Encrypt(adamB.PublicKey, text);

                            // Adam recieves the message from Bob and decrypts it
                            string decryptedMessage = adamB.Decrypt(bob.PublicKey, secretMessage, bob.IV);
                            decryptedMessage += " Ugly World!";

                            // Adam uses Alice's public key to encrypt the changed message
                            byte[] changedMessage = adamA.Encrypt(alice.PublicKey, decryptedMessage);

                            // Alice uses what she thinks is Bob's public key and IV to decrypt the altered message.
                            string alteredMessage = alice.Decrypt(adamA.PublicKey, changedMessage, adamA.IV);
                        }
                    }
                }
            }
        }
    }
}
