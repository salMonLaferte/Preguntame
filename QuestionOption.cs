using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preguntame
{
    public class QuestionOption
    {
        private string content;
        private bool isRight;
        public QuestionOption(bool isRight, string content)
        {
            this.content = content;
            this.isRight = isRight;
        }

        /// <summary>
        /// Get the string of the question option
        /// </summary>
        /// <returns></returns> 
        public string GetContent()
        {
            return content;
        }

        /// <summary>
        /// Get if the option is right 
        /// </summary>
        /// <returns></returns>
        public bool IsRight()
        {
            return isRight;
        }
    }
}
