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
        public static int wrongAnswers = 6;
        public static int rightAnswers = 1;

        public static char GetCharacterForOption(int num)
        {
            return optionCharacters[num];
        }
    }
}
