using System.Collections.Generic;

namespace System.Linq
{
	public static class IEnumerable
	{
		/// <summary>
		/// Return Enumerable where action is not true
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="source"></param>
		/// <param name="predicate"></param>
		/// <returns></returns>
		public static IEnumerable<T> Reject<T>(this IEnumerable<T> source, Func<T, bool> predicate)
		{
			foreach(T element in source)
			{
				if(!predicate(element)) yield return element;
			}
		}
		
		/// <summary>
		/// Break a list of items into chunks of a specific size
		/// </summary>
		public static IEnumerable<IEnumerable<T>> Chunk<T>(this IEnumerable<T> source, int chunksize)
		{
			var enumerator = source.GetEnumerator();
			var arr = new List<T>(chunksize);
			while (enumerator.MoveNext())
			{
				arr.Add(enumerator.Current);
				if(arr.Count >= chunksize)
				{
					yield return arr;
					arr = new List<T>(chunksize);
				}
			}
		}
		
		/// <summary>
		/// Returns the first array that contains the object
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="source"></param>
		/// <param name="obj"></param>
		/// <returns></returns>
		public static IEnumerable<T> AssocFirstOrDefault<T>(this IEnumerable<IEnumerable<T>> source, T obj)
		{
			var outer = source.GetEnumerator();
			while(outer.MoveNext())
			{
				var inner = outer.Current.GetEnumerator();
				while(inner.MoveNext())
				{
					if(inner.Current.Equals(obj))
					{
						return outer.Current;
					}
				}
			}
			return null;
		}

		/// <summary>
		/// Remove all nulls
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="source"></param>
		/// <returns></returns>
		public static IEnumerable<T> Compact<T>(this IEnumerable<T> source)
			where T : class
		{
			foreach(var item in source)
			{
				if(item != null)
				{
					yield return item;
				}
			}
		}

		/// <summary>
		/// Run action over array x times
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="source"></param>
		/// <param name="action"></param>
		/// <param name="times">Times we should enumerate over the array</param>
		public static void Cycle<T>(this IEnumerable<T> source, Action<T> action, int times)
		{
			for(int i = 0; i < times; i++)
			{
				Enumerate(source, action);
			}
		}

		/// <summary>
		/// Infinate loop over Enumerable calling the action over each one
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="source"></param>
		/// <param name="action"></param>
		public static void Cycle<T>(this IEnumerable<T> source, Action<T> action)
		{
			while(true)
			{
				Enumerate(source, action);
			}
		}

		public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
		{
			Enumerate(source, action);
		}

		public static void ForEach<T>(this IEnumerable<T> source, Action<T, int> action)
		{
			var index = 0;
			var enumerator = source.GetEnumerator();
			while(enumerator.MoveNext())
			{
				action?.Invoke(enumerator.Current, index);
				index++;
			}
		}

		public static IEnumerable<T> Distinct<T>(this IEnumerable<T> source, Func<T, object> comparer)
		{
			var items = new HashSet<object>();
			foreach(var item in source)
			{
				var comparedItem = comparer?.Invoke(item);
				if(!items.Contains(comparedItem))
				{
					items.Add(comparedItem);
					yield return item;
				}
			}
		}

		private static void Enumerate<T>(IEnumerable<T> source, Action<T> action)
		{
			var enumerator = source.GetEnumerator();
			while(enumerator.MoveNext())
			{
				action?.Invoke(enumerator.Current);
			}
		}
	}
}