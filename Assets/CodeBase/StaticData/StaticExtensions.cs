using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeBase.StaticData
{
    public static class StaticExtensions
    {
        public static int ChangeIndex(this int index, IEnumerable<Object> list)
        {
            bool notLastIndex = index < (list.Count() - 1);
            int newIndex = index;

            if (notLastIndex)
                newIndex++;
            else
                newIndex = 0;

            return newIndex;
        }
    }
}