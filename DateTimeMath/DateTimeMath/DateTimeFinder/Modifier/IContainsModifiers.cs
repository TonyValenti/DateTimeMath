using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DateTimeMath.Search {
    public interface IContainsModifiers {
        List<DateTimeModifier> Modifiers { get; }
    }

      public static class ICompositeModifierExtensions {

        public static T Modifiers<T>(this IContainsModifiers This) where T : DateTimeModifier, new() {
            var ret = default(T);

            ret = This.Modifiers.OfType<T>().FirstOrDefault();

            if (ret == null) {
                ret = new T();
                This.Modifiers.Add(ret);
            }

            return ret;
        }


    }
}
