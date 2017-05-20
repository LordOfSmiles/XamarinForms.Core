using System.Collections.Generic;

namespace XamarinForms.Core.Services
{
	public class LocalStringPool
	{
		private readonly Dictionary<string,string> StringPool = new Dictionary<string,string> ();

		public string GetOrCreate (string entry)
		{
			string result;

			if (!StringPool.TryGetValue (entry, out result)) {
				StringPool [entry] = entry;
				result = entry;
			}

			return result;
		}
	}


}

