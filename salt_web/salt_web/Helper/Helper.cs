using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace salt_web.Helper
{
    public static class Helper
    {
        public static bool TryParseGuid(this string s, out Guid guid)
        {
            try
            {
                guid = new Guid(s);
                return true;
            }
            catch
            {
                guid = Guid.Empty;
                return false;
            }
        }
    }
}