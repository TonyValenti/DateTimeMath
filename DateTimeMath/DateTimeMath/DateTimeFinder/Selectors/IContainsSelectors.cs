using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DateTimeMath.Search {
    public interface IContainsSelectors {
        List<DateTimeSelector> Selectors { get; }
    }

      public static class IContainsSelectorsExtensions {

        public static T Selectors<T>(this IContainsSelectors This) where T : DateTimeSelector, new() {
            var ret = default(T);

            ret = This.Selectors.OfType<T>().FirstOrDefault();

            if (ret == null) {
                ret = new T();
                This.Selectors.Add(ret);
            }

            return ret;
        }


    }
}
