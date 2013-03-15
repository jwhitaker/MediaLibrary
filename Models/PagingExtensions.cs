using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MediaLibrary.Models
{
    public static class PagingExtensions
    {
        public static IEnumerable<TSource> Page<TSource>(this IEnumerable<TSource> source, int page, int pageSize)
        {
            return source.Skip((page - 1) * pageSize).Take(pageSize);
        }
    }
}