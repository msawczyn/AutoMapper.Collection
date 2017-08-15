﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace AutoMapper.EntityFramework
{
    public static class Extensions
    {
        /// <summary>
        /// Create a persistance object for the <see cref="T:System.Data.Entity.DbSet`1"/> to have data persisted or removed from
        /// </summary>
        /// <typeparam name="TSource">Source table type to be updated</typeparam>
        /// <param name="source">DbSet to be updated</param>
        /// <param name="mapper">IMapper used to find TypeMap between classes</param>
        /// <returns>Persistance object to Update or Remove data</returns>
        public static IPersistance Persist<TSource>(this DbSet<TSource> source)
            where TSource : class
        {
            return new Persistance<TSource>(source);
        }

        /// <summary>
        /// Non Generic call for For
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <param name="destType"></param>
        /// <returns></returns>
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