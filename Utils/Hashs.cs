using System.Security.Cryptography;
using System.Text;

namespace MauiAppDemo.Utils;

internal class Hashs
{
    public static string Sha256(string sult, string data)
    {
        var bytes = Encoding.UTF8.GetBytes($"{sult}\t{data}");

        SHA256 sha256 = SHA256.Create();
        for (int i = 0; i < 256; i++)
        {
            bytes = sha256.ComputeHash(bytes);
        }

        StringBuilder result = new StringBuilder();
        foreach (var b in bytes)
        {
            result.Append(string.Format("{0:X2}", b));
        }

        return result.ToString();
    }
}
