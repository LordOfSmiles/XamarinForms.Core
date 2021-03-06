﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Xamarin.Core.Extensions
{
    public static class CollectionExtensions
    {
        public static int IndexOf(this IEnumerable self, object obj)
        {
            int index = -1;

            var enumerator = self.GetEnumerator();
            enumerator.Reset();
            int i = 0;
            while (enumerator.MoveNext())
            {
                if (enumerator.Current == obj)
                {
                    index = i;
                    break;
                }

                i++;
            }

            return index;
        }

        public static void AddRange<T>(this ICollection<T> oc, IEnumerable<T> collection)
        {
            if (collection == null)
                throw new ArgumentNullException(nameof(collection));

            foreach (var item in collection)
            {
                oc.Add(item);
            }
        }

        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> array, Action<T> act)
        {
            foreach (var i in array)
            {
                act(i);
            }

            return array;
        }

        public static IEnumerable<T> ForEach<T>(this IEnumerable arr, Action<T> act)
        {
            return arr.OfType<T>().ForEach<T>(act);
        }

        public static IEnumerable<TResult> ForEach<T, TResult>(this IEnumerable<T> array, Func<T, TResult> func)
        {
            var list = new List<TResult>();

            foreach (var i in array)
            {
                var obj = func(i);
                if (obj != null)
                    list.Add(obj);
            }

            return list;
        }


    }
}

