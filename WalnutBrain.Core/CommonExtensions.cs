using System.Linq;

namespace WalnutBrain
{
    public static class CommonExtensions
    {
        public static string AsFormat(this string format, params object[] args)
        {
            return string.Format(format, args);
        }

        public static bool In<T>(T item, params object[] set) => set.Any(p => Equals(item, p));
        
    }
}