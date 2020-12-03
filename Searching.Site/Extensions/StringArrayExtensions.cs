using Examine;
using Examine.Search;
using System.Collections.Generic;

namespace Searching.Site.Extensions
{
    public static class StringArrayExtensions
    {
        public static IExamineValue[] Fuzzy(this string[] terms)
        {
            if (terms == null) return null;

            List<IExamineValue> values = new List<IExamineValue>();
            foreach(var item in terms)
            {
                values.Add(item.Fuzzy());
            }
            return values.ToArray();
        }

        public static IExamineValue[] Boost(this string[] terms, float boost)
        {
            if (terms == null) return null;

            List<IExamineValue> values = new List<IExamineValue>();
            foreach(var item in terms)
            {
                values.Add(item.Boost(boost));
            }
            return values.ToArray();
        }
    }
}