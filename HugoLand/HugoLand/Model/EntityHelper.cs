using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HugoLand.Model
{
    public static class EntityHelper
    {
        public static T FirstOrNull<T>(this IEnumerable<T> list, Func<T, bool> predicate) where T : class
        {
            foreach (T item in list)
                if (predicate(item))
                    return item;
            return null;
        }
    }
}
