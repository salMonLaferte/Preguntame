using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preguntame
{
    /// <summary>
    /// Provides methods to modify the subtheme tree and access subtheme info.
    /// </summary>
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

        /// <summary>
        /// Return true if the subtheme has the subtheme with the especified TAG as a child.  
        /// </summary>
        /// <param name="TAG"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Add a subtheme as a children of this subtheme.
        /// </summary>
        /// <param name="t"></param>
        public void addChild(SubTheme t) 
        {
            children.Add(t);
        }

        /// <summary>
        /// Get the child in the entire subtree with the especified tag, return null if a child with that TAG is not found.
        /// </summary>
        /// <param name="TAG"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Set name of the subtheme.
        /// </summary>
        /// <param name="name"></param>
        public void setName(string name)
        {
            this.name = name;
        }

        /// <summary>
        /// Sums a question to the counter.
        /// </summary>
        public void plusQuestion()
        {
            questionFreq++;
        }

        /// <summary>
        /// Get the theme name of the subtheme with the especified TAG, returns "desconocido" if is TAG is not found
        /// </summary>
        /// <param name="TAG"></param>
        /// <returns></returns>
        public static string GetThemeName(string TAG)
        {
            return Data.GetThemeName(TAG);
        }
    }
}
