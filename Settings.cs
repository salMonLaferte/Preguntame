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
        public bool dontRepeatQuestions;

        public static string optionCharacters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        public Settings()
        {
            totalOptions = 4;
            rightOptions = 1;
            randRightOptions = false;
            rAnsMode = RightAnswerMode.MarkAll;
            themeSelection = new Dictionary<string, bool>();
            dontRepeatQuestions = true;
        }

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

        public char GetCharacterForOption(int num)
        {
            return optionCharacters[num];
        }

        public void ChangeSettings(int totalOptions = 4, int rightAnswers = 1,
            bool randRightAnswers = false, RightAnswerMode rAnsMode = RightAnswerMode.MarkAll, bool dontRepeatQuestions = true)
        {
            this.totalOptions = totalOptions;
            this.rightOptions = rightAnswers;
            this.randRightOptions = randRightAnswers;
            this.rAnsMode = rAnsMode;
            if (this.dontRepeatQuestions && !dontRepeatQuestions)
                Data.SelectAllAvailableQuestions();
            this.dontRepeatQuestions = dontRepeatQuestions;
            Data.WriteSettings();
        }

        public enum RightAnswerMode
        {
            MarkAll = 0,
            MarkOne = 1
        }

        public void ChangeThemeSelection(Dictionary<string,bool> changes)
        {
            foreach( KeyValuePair<string, bool> k in changes)
            {
                if (themeSelection.ContainsKey(k.Key))
                {
                    themeSelection[k.Key] = k.Value;
                }
            }
            Data.WriteSettings();
            Data.SelectAllAvailableQuestions();

        }
    }
}
