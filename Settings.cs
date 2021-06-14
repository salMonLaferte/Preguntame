using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preguntame
{
    public static class Settings
    {
        public static string optionCharacters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        public static int wrongAnswers = 3;
        public static int rightAnswers = 1;
        public static bool randWrongAnswers = false;
        public static bool randRightAnswers = false;

        public static RightAnswerMode rAnsMode = RightAnswerMode.MarkAll;

        public static char GetCharacterForOption(int num)
        {
            return optionCharacters[num];
        }

        public static void ChangeSettings( int wrongAnswers = 3, int rightAnswers = 1, bool randWrongAnswers = false,
            bool randRightAnswers = false, RightAnswerMode rAnsMode = RightAnswerMode.MarkAll)
        {
            Settings.wrongAnswers = wrongAnswers;
            Settings.rightAnswers = rightAnswers;
            Settings.randRightAnswers = randRightAnswers;
            Settings.randWrongAnswers = randWrongAnswers;
            Settings.rAnsMode = rAnsMode;
        }

        public enum RightAnswerMode
        {
            MarkAll = 0,
            MarkOne = 1
        }
    }
}
