using System;
using System.Collections;
using System.Collections.Generic;
using ReactiveUI;
using Showroom.Extensions;

namespace Showroom.ValueConverters
{
    public class CamelCaseSplitConverter : IBindingTypeConverter
    {
        public int GetAffinityForObjects(Type fromType, Type toType)
        {
            if (fromType == typeof(string))
            {
                return 100;
            }

            if (fromType == typeof(IEnumerable<string>))
            {
                return 100;
            }

            return 0;
        }

        public bool TryConvert(object @from, Type toType, object conversionHint, out object result)
        {
            result = string.Join(", ", (IEnumerable<string>)@from).SplitCamelCase().Trim();
            return true;
        }
    }
}