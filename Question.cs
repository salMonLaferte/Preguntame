using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preguntame
{
    public class Question
    {
        static string abc = "ABCDEFGHIJKLMNOPQRSTU";
        string question;
        string[] optionsRight;
        string[] optionsWrong;

        /// <summary>
        /// A list of options for the question with a determined number of right and wrong answers, has to be set with the
        /// function GenerateListOfOptions
        /// </summary>
        List<QuestionOption> qOption;

        /// <summary>
        /// The TAG of the theme wich question corresponds to.
        /// </summary>
        public string TAG;

        bool questionIsCorrupted = true;

        public Question(string question, string[] optionsRight, string[] optionsWrong, string TAG = "default")
        {
            qOption = new List<QuestionOption>();
            this.question = question;
            this.optionsRight = optionsRight;
            this.optionsWrong = optionsWrong;
            this.TAG = TAG;
            if (optionsRight.Length == 0)
            {
                this.questionIsCorrupted = false;
            }
        }
        
        /// <summary>
        /// Returns 0 if succesfully sets qOption with the specified parameters, -1 if not.
        /// If the number of required right answers is bigger than the number of right answers
        /// on the question data, it will take all the right answers for the options.
        /// </summary>
        /// <param name="rightAnswers">number of right answers, has to be =>1 </param>
        /// <param name="wrongAnswers">number of wrong answers</param>
        /// <returns></returns>
        int GenerateListOfOptions( int rightAnswers, int wrongAnswers)
        {
            if (rightAnswers <= 0)
                return -1;
            Random rand = new Random();
            List<string> rlist = optionsRight.ToList<string>();
            List<string> wlist = optionsWrong.ToList<string>();
            qOption.Clear();
            while(rlist.Count > 0 && qOption.Count < rightAnswers)
            {
                int index = rand.Next(rlist.Count);
                qOption.Add(new QuestionOption(true, rlist[index]));
                rlist.RemoveAt(index);
            }
            int wrongCounter = 0;
            while(wrongCounter < wrongAnswers && wlist.Count > 0)
            {
                int index = rand.Next(wlist.Count);
                qOption.Add(new QuestionOption(true, wlist[index]));
                wlist.RemoveAt(index);
                wrongCounter++;
            }
            return 0;
        }

        /// <summary>
        /// Returns true if the answer selected is right
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public bool CheckAnswer(int number)
        {
            return qOption[number].isRight;
        }

        /// <summary>
        /// Returns true if all the answers selected are right
        /// </summary>
        /// <param name="numbers"></param>
        /// <returns></returns>
        public bool CheckAnswers(int[] numbers)
        {
            for(int i=0; i<numbers.Length; i++)
            {
                if (!qOption[numbers[i]].isRight)
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Get the question text
        /// </summary>
        /// <returns></returns>
        public string GetQuestionText()
        {
            return question;
        }

        /// <summary>
        /// Generate a random set of options with the specified right and wrong answers 
        /// </summary>
        /// <returns></returns>
        public List<QuestionOption> GenerateAndGetListOfOptions( int rightAnswers, int wrongAnswers)
        {
            GenerateListOfOptions(rightAnswers, wrongAnswers);
            return qOption;
        }

    }
}
