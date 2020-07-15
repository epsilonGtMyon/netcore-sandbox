using System;
using System.Collections.Generic;
using System.Text;

namespace Base64Study
{
    class Program
    {
        static void Main(string[] args)
        {
            //Enc();
            //Dec();
            Console.WriteLine("[end]");
        }

        private static void Enc()
        {
            List<string> rawStrings = new List<string>
            {
                "",
                "f",
                "fo",
                "foo",
                "foob",
                "fooba",
                "foobar",
            };

            foreach (string rawString in rawStrings)
            {
                byte[] b = Encoding.UTF8.GetBytes(rawString);
                string encoded = MyBase64.Encode(b);
                Console.WriteLine(encoded);
            }
        }

        private static void Dec()
        {

            List<string> encodedStrings = new List<string>
            {
                 "",
                 "Zg==",
                 "Zm8=",
                 "Zm9v",
                 "Zm9vYg==",
                 "Zm9vYmE=",
                 "Zm9vYmFy",
            };

            foreach (string ex in encodedStrings)
            {
                byte[] b = MyBase64.Decode(ex);

                string s = Encoding.UTF8.GetString(b);
                Console.WriteLine(s);
            }
        }
    }
}
