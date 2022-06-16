using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiAppDemo.Pages
{
    public class StringKeyValuPair
    {
        public String Key { get; set; }

        public String Value { get; set; }

        public override bool Equals(object obj)
        {
            return obj is StringKeyValuPair pair &&
                   Key == pair.Key;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Key);
        }
    }
}
