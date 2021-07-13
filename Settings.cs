using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
namespace Preguntame
{
    public class Settings
    {
        [JsonInclude]
        public int totalOptions;
        public int rightOptions;
        public bool randRightOptions;
        public RightAnswerMode rAnsMode = RightAnswerMode.MarkAll;
        public Dictionary<string, bool> themeSelection;
        public RepeatQuestions repeatQuestions = RepeatQuestions.No;

        public static string optionCharacters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        public Settings()
        {
            totalOptions = 4;
            rightOptions = 1;
            randRightOptions = false;
            rAnsMode = RightAnswerMode.MarkAll;
            themeSelection = new Dictionary<string, bool>();
            repeatQuestions = RepeatQuestions.No;
        }

        /// <summary>
        /// Deletes themes wrote in settings that not exist in the current list of questions.
        /// </summary>
        public void DeleteUnusedThemeKeys   ()
        {
            List<string> toRemove = new List<string>();
            foreach(KeyValuePair<string, bool> t in themeSelection)
            {
                if (!Data.GetThemes().ContainsKey(t.Key))
                {
                    toRemove.Add(t.Key);
                }
            }
            foreach(string s in toRemove)
            {
                themeSelection.Remove(s);
            }
        }

        /// <summary>
        /// Returns the character to identify a question option.
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public char GetCharacterForOption(int num)
        {
            return optionCharacters[num];
        }

        /// <summary>
        /// Change and save settings.
        /// </summary>
        /// <param name="totalOptions"></param>
        /// <param name="rightAnswers"></param>
        /// <param name="randRightAnswers"></param>
        /// <param name="rAnsMode"></param>
        /// <param name="repeat"></param>
        public void ChangeSettings(int totalOptions = 4, int rightAnswers = 1,
            bool randRightAnswers = false, RightAnswerMode rAnsMode = RightAnswerMode.MarkAll, RepeatQuestions repeat = RepeatQuestions.No)
        {
            this.totalOptions = totalOptions;
            this.rightOptions = rightAnswers;
            this.randRightOptions = randRightAnswers;
            this.rAnsMode = rAnsMode;
            if (this.repeatQuestions == RepeatQuestions.No && repeat == RepeatQuestions.Si 
                || this.repeatQuestions == RepeatQuestions.SoloRespondidasMal && repeat == RepeatQuestions.Si)
                Data.SelectAllAvailableQuestions();
            this.repeatQuestions = repeat;
            Data.WriteSettings();
        }
        
        /// <summary>
        /// Change and saves settings.
        /// </summary>
        /// <param name="changes"></param>
        public void ChangeThemeSelection(Dictionary<string, bool> changes)
        {
            foreach (KeyValuePair<string, bool> k in changes)
            {
                if (themeSelection.ContainsKey(k.Key))
                {
                    themeSelection[k.Key] = k.Value;
                }
            }
            Data.WriteSettings();
            Data.SelectAllAvailableQuestions();

        }


        public enum RightAnswerMode
        {
            MarkAll = 0,
            MarkOne = 1
        }

        public enum RepeatQuestions
        {
            No = 0,
            SoloRespondidasMal = 1,
            Si = 2
        }

    }
}
