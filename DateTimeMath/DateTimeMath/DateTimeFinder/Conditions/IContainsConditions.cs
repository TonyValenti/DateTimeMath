using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DateTimeMath.Search {
    public interface IContainsConditions {
        List<DateTimeCondition> Conditions { get; }
    }

    public static class IContainsConditionsExtensions {

        public static T Conditions<T>(this IContainsConditions This) where T : DateTimeCondition, new() {
            var ret = default(T);

            ret = This.Conditions.OfType<T>().FirstOrDefault();

            if (ret == null) {
                ret = new T();
                This.Conditions.Add(ret);
            }

            return ret;
        }


    }

}
