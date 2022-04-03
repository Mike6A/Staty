using System;
using System.Collections.Generic;
using System.Linq;

namespace Staty.Extensions
{
    public static class LinqExtensions
    {
        public static IEnumerable<T> WhereIfNotNull<T>(this IEnumerable<T> source, Func<T, bool> predicate, object o) => 
            o == null ? source : source.Where(predicate);
    }
}