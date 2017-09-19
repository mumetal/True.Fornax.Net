using System;
using System.Collections.Generic;


namespace Fornax.Net.Util.Linq
{
    public static partial class XQueryable
    {
        private sealed class FunctorComparer<T> : IComparer<T>
        {
            private Comparison<T> comparison;

            public FunctorComparer(Comparison<T> comparison) {
                this.comparison = comparison;
            }

            public int Compare(T x, T y) {
                return this.comparison(x, y);
            }
        }

    }
}