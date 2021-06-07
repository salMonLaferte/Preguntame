using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preguntame
{
    public class SubTheme
    {
        List<Question> questions;
        List<SubTheme> children;
        String name;
        String TAG;
        int index;

        public SubTheme()
        {
            questions = new List<Question>();
            children = new List<SubTheme>();
            name = "unknown";
            TAG = "NOT";
            index = 1;
        }

        public SubTheme(string name, string TAG)
        {
            questions = new List<Question>();
            children = new List<SubTheme>();
            this.name = name;
            this.TAG = TAG;
            this.index = 1;
        }

        public String getName()
        {
            return name;
        }

        public String getTag()
        {
            return TAG;
        }

        public bool isTagOnThisTheme( string TAG )
        {
            if (this.TAG == TAG)
                return true;
            else
            {
                foreach (SubTheme t in children)
                {
                    if (isTagOnThisTheme(TAG))
                        return true;
                }
            }
            return false;   
        }

        public void addChild(SubTheme t) 
        {
            children.Add(t);
            t.index = children.Count;
        }

        public SubTheme GetChildWithTAG( string TAG)
        {
            if (this.TAG == TAG)
                return this;
            else
            {
                foreach(SubTheme t in children)
                {
                    SubTheme r = t.GetChildWithTAG(TAG);
                    if (r != null)
                        return r;
                }
            }
            return null;
        }

        public void AddQuestion(Question q)
        {
            questions.Add(q);
        }

    }
}
