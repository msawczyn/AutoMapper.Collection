using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;

namespace AutoMapper.Collection.LinqToSQL
{
    public static class PersistanceExtensions
    {
        /// <summary>
        /// Create a persistance object for the <see cref="T:System.Data.Entity.DbSet`1"/> to have data persisted or removed from
        /// Uses static API's Mapper for finding TypeMap between classes
        /// </summary>
        /// <typeparam name="TSource">Source table type to be updated</typeparam>
        /// <param name="source">Table to be updated</param>
        /// <returns>Persistance object to Update or Remove data</returns>
        public static IPersistance Persist<TSource>(this Table<TSource> source)
            where TSource : class
        {
            return new Persistance<TSource>(source);
        }

        public static IEnumerable For<TSource>(this IQueryable<TSource> source, Type destType)
        {
            var forMethod = source.GetType().GetMethod("For").MakeGenericMethod(destType);
            var listType = typeof(List<>).MakeGenericType(destType);
            var forResult = forMethod.Invoke(source, new object[] { null });
            var enumeratedResult = Activator.CreateInstance(listType, forResult);
            return enumeratedResult as IEnumerable;
        }
    }
}