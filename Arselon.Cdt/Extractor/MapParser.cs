using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arselon.Cdt.Extractor
{
    public class MapParser 
    {
        IEnumerator<string> _enumerator;
        int[] _columns;

        public string Current => _enumerator.Current;
        public bool IsEmpty => Current == string.Empty;

        public MapParser(string[] lines)
        {
            _enumerator = lines.AsEnumerable<string>().GetEnumerator();
            _enumerator.MoveNext();
        }

        public MapParser(IEnumerator<string> enumerator)
        {
            _enumerator = enumerator;
        }

        public void SkipTo(string line)
        {
            SkipWhile(s => s != line);
            MoveNext();
        }

        public void SkipWhile(Func<string, bool> predicate)
        {
            while (predicate(_enumerator.Current))
                if (!_enumerator.MoveNext())
                    break;
        }

        public bool Test(string s)
        {
            if (Current != s)
                return false;

            MoveNext();
            return true;
        }

        public void Require(string s)
        {
            if (_enumerator.Current != s)
                throw new Exception();
            
            _enumerator.MoveNext();
        }

        public void Require(string[] columns)
        {
            var terms = GetTerms();
            for (int i = 0; i < terms.Length; i++)
                if (terms[i] != columns[i])
                    throw new Exception("Invalid Data");
        }

        public void RequireEmpty()
        {
            Require("");            
        }

        public bool MoveNext()
        {
            return _enumerator.MoveNext();
        }

        public string[] GetTerms()
        {
            var result = new List<string>();
            var line = Current;
            var h = _columns[0];
            var l = line.Length;
            foreach (var t in _columns.Skip(1))
            {
                if (h >= l)
                    break;

                var term = line.Substring(h, Math.Min(l - h, t - h));
                result.Add(term.Trim());
                h = t;
            }

            var n = _columns.Length - result.Count - 1;
            for (int i = 0; i < n; i++)
                result.Add(string.Empty);

            MoveNext();
            return result.ToArray();
        }

        public void SetColumnLine(string columnLine)
        {
            var columns = new List<int>();
            var length = columnLine.Length;
            var lc = ' ';
            for (int i = 0; i < length; i++)
            {
                var c = columnLine[i];
                if (c == '-' && lc == ' ')
                    columns.Add(i);
                lc = c;
            }
            columns.Add(int.MaxValue);
            _columns = columns.ToArray();
        }

        public void BeginTable(string[] headers, string columnLine)
        {
            SetColumnLine(columnLine);
            Require(headers);
            Require(columnLine);
        }
    }
}
