using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects;
using Domain.Entities;

namespace DomainStorage.EFStorage
{
    public static class EFExtensionMethods
    {
        public static IQueryable<TSource> Include<TSource>(this IQueryable<TSource> source, string path)
        {
            var objectQuery = source as ObjectQuery<TSource>;
            return (objectQuery == null ? source : objectQuery.Include(path));
        }

    }
}
