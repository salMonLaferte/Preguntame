using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preguntame
{
    public class QuestionOption
    {
        public string content;
        public bool isRight;
        public QuestionOption(bool isRight, string content)
        {
            this.content = content;
            this.isRight = isRight;
        }
            
    }
}
