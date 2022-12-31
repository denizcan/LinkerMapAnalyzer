using System;
namespace Arselon.Cdt
{
    public class CdtUtility
    {

        const string _numbers = "0123456789";

        public static string DemangleCppName(string name)
        {
            string result = string.Empty;
            int l = name.Length;
            for (int t = 0; t < l; t++)
            {
                if (_numbers.Contains(name[t]))
                {
                    var h = t;
                    do
                    {
                        t++;
                    } while ((t < l) && _numbers.Contains(name[t]));
                    var n = int.Parse(name.Substring(h, t - h));
                    var part = name.Substring(t, n);
                    t += n - 1;
                    if (result != string.Empty)
                        result += ".";
                    result += part;
                }
            }

            return result;
        }

        public static string DemangleTiName(string name)
        {
            return name.StartsWith("_Z") ? DemangleCppName(name) : name;
        }
    }
}
