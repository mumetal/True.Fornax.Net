using System;
using System.IO;
using System.Text;
using Fornax.Net.Util.Security.Cryptography;

namespace Fornax.Psaximo
{
    class Program
    {
        static void Main(string[] args) {
            
            //Encoding code = DetectTextEncoding("count_1w.txt",out string text,100);
           // Console.WriteLine(code);
            //Console.WriteLine(text);

        }

        //Detect text encoding from file
        //overload to input stream ,out string text, int taster =1000
        //or better still use valurtuple to return (Encoding encoding & string text)
        //Always use value-tuples if needs be as well as normal methods
        private static Encoding DetectTextEncoding(string filename, out string text, int taster = 1000) {

            byte[] b = File.ReadAllBytes(filename);

            if (b.Length >= 4 && b[0] == 0x00 && b[1] == 0x00 && b[2] == 0xFE && b[3] == 0xFF) {
                text = Encoding.GetEncoding("utf-32BE").GetString(b, 4, b.Length - 4); return Encoding.GetEncoding("utf-32BE");
            } else if (b.Length >= 4 && b[0] == 0xFF && b[1] == 0xFE && b[2] == 0x00 && b[3] == 0x00) {
                text = Encoding.UTF32.GetString(b, 4, b.Length - 4); return Encoding.UTF32;
            } else if (b.Length >= 2 && b[0] == 0xFE && b[1] == 0xFF) {
                text = Encoding.BigEndianUnicode.GetString(b, 2, b.Length - 2); return Encoding.BigEndianUnicode;
            } else if (b.Length >= 2 && b[0] == 0xFF && b[1] == 0xFE) {
                text = Encoding.Unicode.GetString(b, 2, b.Length - 2); return Encoding.Unicode;
            } else if (b.Length >= 3 && b[0] == 0xEF && b[1] == 0xBB && b[2] == 0xBF) {
                text = Encoding.UTF8.GetString(b, 3, b.Length - 3); return Encoding.UTF8;
            } else if (b.Length >= 3 && b[0] == 0x2b && b[1] == 0x2f && b[2] == 0x76) {
                text = Encoding.UTF7.GetString(b, 3, b.Length - 3); return Encoding.UTF7;
            }

            if (taster == 0 || taster > b.Length) taster = b.Length;


            //checking for utf8 pattern - shift this to another method
            int i = 0;
            bool Isutf8 = false;

            while (i < taster - 4) {
                //0x7f
                if (b[i] <= 0x7f) { ++i; continue; }
                //0xc2,0xdf,0x80,0xc0
                if (b[i] >= 0xC2 && b[i] <= 0xDF && b[i + 1] >= 0x80 && b[i + 1] < 0xC0) { i += 2; Isutf8 = true; continue; }
                //0xe0,0xf0,0x80,0xc0
                if (b[i] >= 0xE0 && b[i] <= 0xF0 && b[i + 1] >= 0x80 && b[i + 1] < 0xC0 && b[i + 2] >= 0x80 && b[i + 2] < 0xC0) { i += 3; Isutf8 = true; continue; }
                //0xf0,0xf4,0x80,0xc0
                if (b[i] >= 0xE0 && b[i] <= 0xF0 && b[i + 1] >= 0x80 && b[i + 1] < 0xC0 && b[i + 2] >= 0x80 && b[i + 2] < 0xC0 && b[i + 3] >= 0x80 && b[i + 3] < 0xC0) { i += 4; Isutf8 = true; continue; }
                Isutf8 = false; break;
            }

            if (Isutf8) {
                text = Encoding.UTF8.GetString(b);
                return Encoding.UTF8;
            }

            double threshold = 0.1;
            int count = 0;
            for (int n = 0; n < taster; n += 2) if (b[n] == 0) count++;
            if (((double)count) / taster > threshold) { text = Encoding.BigEndianUnicode.GetString(b); return Encoding.BigEndianUnicode; }
            count = 0;
            for (int n = 1; n < taster; n += 2) if (b[n] == 0) count++;
            if (((double)count) / taster > threshold) { text = Encoding.Unicode.GetString(b); return Encoding.Unicode; }

            for (int n = 0; n < taster - 9; n++) {

                if (
                    ((b[n + 0] == 'c' || b[n + 0] == 'C') && (b[n + 1] == 'h' || b[n + 1] == 'H') && (b[n + 2] == 'a' || b[n + 2] == 'A') && (b[n + 3] == 'r' || b[n + 3] == 'R') && (b[n + 4] == 's' || b[n + 4] == 'S') && (b[n + 5] == 'e' || b[n + 5] == 'E') && (b[n + 6] == 't' || b[n + 6] == 'T') && (b[n + 7] == '=')) ||
                   ((b[n + 0] == 'e' || b[n + 0] == 'E') && (b[n + 1] == 'n' || b[n + 1] == 'N') && (b[n + 2] == 'c' || b[n + 2] == 'C') && (b[n + 3] == 'o' || b[n + 3] == 'O') && (b[n + 4] == 'd' || b[n + 4] == 'D') && (b[n + 5] == 'i' || b[n + 5] == 'I') && (b[n + 6] == 'n' || b[n + 6] == 'N') && (b[n + 7] == 'g' || b[n + 7] == 'G') && (b[n + 8] == '='))) {

                    if (b[n + 0] == 'c' || b[n + 0] == 'C') n += 8; else n += 9;
                    if (b[n] == '"' || b[n] == '\'') n++;
                    int oldn = n;
                    while (n < taster && (b[n] == '_' || b[n] == '-' || (b[n] >= '0' && b[n] <= '9') || (b[n] >= 'a' && b[n] <= 'z') || (b[n] >= 'A' && b[n] <= 'Z'))) { n++; }
                    byte[] nb = new byte[n - oldn];
                    Array.Copy(b, oldn, nb, 0, n - oldn);
                    try {
                        string internalenc = Encoding.ASCII.GetString(nb);
                        text = Encoding.GetEncoding(internalenc).GetString(b);
                        return Encoding.GetEncoding(internalenc);
                    } catch { break; }
                }


            }

            text = Encoding.Default.GetString(b);
            return Encoding.Default;
        }
    }
    public enum EnumLaStat : int { YES, NO }

    public static class LaStart
    {
        public static string GetStringValue(this EnumLaStat stat) {
            if (stat == 0) { return ".no"; } else if ((int)stat == 1) { return ".yes"; }
            return "null";

        }

    }

}

