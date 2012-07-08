using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ambiguous
{
   public static class Globals
    {
       public static string ReverseStr(string str)
       {
           char[] c = str.ToCharArray();
           Array.Reverse(c);
           return new string(c);
       }
    }
}
