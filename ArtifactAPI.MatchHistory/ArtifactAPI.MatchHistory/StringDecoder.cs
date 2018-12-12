using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtifactAPI.MatchHistory
{
    public class StringDecoder
    {
        public static object DecodeString(string s)
        {
            if (string.IsNullOrEmpty(s))
                return null;

            return "";
        }
    }
}
