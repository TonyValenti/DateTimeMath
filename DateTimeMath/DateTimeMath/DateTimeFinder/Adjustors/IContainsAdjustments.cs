using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DateTimeMath.Search {
    public interface IContainsAdjustments {
        List<DateTimeAdjuster> Adjustments { get; }
    }

      public static partial class IContainsAdjustmentsExtensions {

        public static T Adjustments<T>(this IContainsAdjustments This) where T : DateTimeAdjuster, new() {
            var ret = default(T);

            ret = This.Adjustments.OfType<T>().FirstOrDefault();

            if (ret == null) {
                ret = new T();
                This.Adjustments.Add(ret);
            }

            return ret;
        }


    }
}
