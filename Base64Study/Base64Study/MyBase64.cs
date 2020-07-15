using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Base64Study
{
    public class MyBase64
    {
        private static readonly char[] _base64EncodeTable = ("ABCDEFGHIJKLMNOPQRSTUVWXYZ" +
                                                        "abcdefghijklmnopqrstuvwxyz" +
                                                        "0123456789" +
                                                        "+/").ToCharArray();

        private static readonly IDictionary<char, int> _base64DecodeTable;

        static MyBase64()
        {
            _base64DecodeTable = new Dictionary<char, int>();
            for (int i = 0; i < _base64EncodeTable.Length; i++)
            {
                char c = _base64EncodeTable[i];

                _base64DecodeTable[c] = i;
            }
        }

        public static string Encode(byte[] source)
        {
            StringBuilder buf = new StringBuilder();

            for (int i = 0, n = source.Length; i < n; i = i + 3)
            {
                byte b1 = source[i];

                bool exb2 = i + 1 < n;
                byte b2 = exb2 ? source[i + 1] : (byte)0;

                bool exb3 = i + 2 < n;
                byte b3 = exb3 ? source[i + 2] : (byte)0;

                char c1 = _base64EncodeTable[b1 >> 2 & 0x3f];
                char c2 = _base64EncodeTable[(b1 << 4 | b2 >> 4) & 0x3f];
                char c3;
                if (exb2)
                {
                    c3 = _base64EncodeTable[(b2 << 2 | b3 >> 6) & 0x3f];
                }
                else
                {
                    c3 = '=';
                }

                char c4;
                if (exb3)
                {
                    c4 = _base64EncodeTable[b3 & 0x3f];
                }
                else
                {
                    c4 = '=';
                }

                buf.Append(c1).Append(c2).Append(c3).Append(c4);
            }

            return buf.ToString();
        }


        public static byte[] Decode(string source)
        {
            if (string.IsNullOrEmpty(source)) return new byte[0];

            using (MemoryStream m = new MemoryStream(source.Length / 4 * 3))
            {
                for (int i = 0, n = source.Length; i < n; i = i + 4)
                {
                    char c1 = source[i];
                    char c2 = source[i + 1];
                    char c3 = source[i + 2];
                    char c4 = source[i + 3];

                    bool padC3 = c3 == '=';
                    bool padC4 = c4 == '=';

                    int i1 = _base64DecodeTable[c1];
                    int i2 = _base64DecodeTable[c2];

                    //byte1
                    byte b1 = (byte)((i1 << 2) | (i2 >> 4));
                    m.WriteByte(b1);

                    //byte2
                    if (padC3)
                    {
                        continue;
                    }

                    int i3 = _base64DecodeTable[c3];

                    byte b2 = (byte)((i2 << 4) | i3 >> 2);
                    m.WriteByte(b2);

                    //byte3
                    if (padC4)
                    {
                        continue;
                    }

                    int i4 = _base64DecodeTable[c4];
                    byte b3 = (byte)((i3 << 6) | i4);
                    m.WriteByte(b3);
                }

                return m.ToArray();
            }
        }
    }
}
