using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlyFetch
{
    static class Helpers
    {
        public static T Cast<T>(T typeHolder, Object x)
        {
            return (T)x;
        }
    }
}
