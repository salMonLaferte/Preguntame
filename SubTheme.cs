using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preguntame
{
    public class SubTheme
    {
        List<SubTheme> children;
        String name;
        String TAG;
        int questionFreq;

        public SubTheme()
        {
            children = new List<SubTheme>();
            name = "unknown";
            TAG = "NOT";
            questionFreq = 1;
        }

        public SubTheme( string TAG)
        {
            children = new List<SubTheme>();
            name = TAG;
            this.TAG = TAG;
            questionFreq = 1;
        }

        public SubTheme(string TAG, string name)
        {
            children = new List<SubTheme>();
            this.name = name;
            this.TAG = TAG;
            questionFreq = 1;
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

        public void setName(string name)
        {
            this.name = name;
        }

        public void plusQuestion()
        {
            questionFreq++;
        }

        public static string GetThemeName(string TAG)
        {
            return Data.GetThemeName(TAG);
        }
    }
}
