using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preguntame
{
    /// <summary>
    /// Provides methods to access the info for a questionOption.
    /// </summary>
    public class QuestionOption
    {
        private string content;
        private bool isRight;
        private string img;


        public QuestionOption(bool isRight, string content, string img="")
        {
            this.content = content;
            this.isRight = isRight;
            this.img = img;
        }

        /// <summary>
        /// Get the string of the question option.
        /// </summary>
        /// <returns></returns> 
        public string GetContent()
        {
            return content;
        }

        /// <summary>
        /// Get if the option is right. 
        /// </summary>
        /// <returns></returns>
        public bool IsRight()
        {
            return isRight;
        }

        /// <summary>
        /// Gets the image name for the option.
        /// </summary>
        /// <returns></returns>
        public string GetImgName()
        {
            return img;
        }

    }
}
