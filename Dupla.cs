using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preguntame
{
    public class Dupla
    {
        public int key;
        public int priority;

        public Dupla(int key, int priority)
        {
            this.key = key;
            this.priority = priority;
        }

        public int CompareTo(object obj)
        {
            Dupla d = obj as Dupla;
            if (this.priority < d.priority)
            {
                return -1;
            }
            if (this.priority > d.priority || d == null)
            {
                return 1;
            }
            return 0;
        }
    }
}
