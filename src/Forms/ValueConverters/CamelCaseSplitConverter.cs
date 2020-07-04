using System;
using ReactiveUI;

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

            return 0;
        }

        public bool TryConvert(object @from, Type toType, object conversionHint, out object result)
        {
            result = @from.ToString().SplitCamelCase();
            return true;
        }
    }
}