using System.Collections.Generic;
using System.Linq;

namespace JWLibrary
{
    public static class StaticUtils
    {
        public static string ToString<T>(this List<T> list)
        {
            var newList = new List<string>();
            foreach (var item in list)
            {
                newList.Add(string.Format("{0}", item));
            }

            return newList.Aggregate((a, b) => a + ", " + b);
        }
    }
}