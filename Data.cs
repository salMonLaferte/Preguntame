using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace Preguntame
{
    public static class Data
    {
        static List<SubTheme> themes = new List<SubTheme>();
        static List<Question> questionlist = new List<Question>();
        static int cntSesionRightAns = 0;
        static int cntSesionWrongAns = 0;
        static int questionCount = 0;


        public static Settings settings = new Settings();

        public static void AddTheme(string parentTAG, string name, string TAG)
        {
            if (parentTAG == "")
                themes.Add(new SubTheme(name, TAG));
            SubTheme n = new SubTheme(name, TAG);
            foreach (SubTheme t in themes)
            {
                SubTheme parent = t.GetChildWithTAG(TAG);
                if (parent != null)
                {
                    parent.addChild(n);
                    return;
                }
            }
            themes.Add(n);
        }

        public static void AddQuestion(Question q)
        {
            foreach (SubTheme t in themes)
            {
                SubTheme parent = t.GetChildWithTAG(q.TAG);
                if (parent != null)
                {
                    parent.AddQuestion(q);
                    questionCount++;
                    questionlist.Add(q);
                    questionCount++;
                    return;
                }
            }
            AddTheme("", "Tema " + themes.Count.ToString(), q.TAG);
            questionlist.Add(q);
            questionCount++;
        }

        public static void ReadData()
        {
            string text = System.IO.File.ReadAllText(System.AppDomain.CurrentDomain.BaseDirectory + "\\pregu.txt");
            int f = 0;
            while (f < text.Length)
            {
                //Get the indexes where the data from the current questions begins and ends
                int s = text.IndexOf('{', f);
                if (s < 0)
                    break;
                f = text.IndexOf('}', s);

                //Get the text part from the question
                int start_p = text.IndexOf('[', s) + 1;
                int end_p = text.IndexOf(']', s);
                string texto = text.Substring(start_p, end_p - start_p);

                //Get an array of the correct answers
                start_p = text.IndexOf('[', end_p) + 1;
                end_p = text.IndexOf(']', start_p);
                string rightOpStr = text.Substring(start_p, end_p - start_p);
                string[] rightOptions = rightOpStr.Split('|');

                //Get an array of the wrong answers
                start_p = text.IndexOf('[', end_p) + 1;
                end_p = text.IndexOf(']', start_p);
                string wrongOpStr = text.Substring(start_p, end_p - start_p);
                string[] wrongOptions = wrongOpStr.Split('|');

                //Get the TAG of the question
                start_p = text.IndexOf('[', end_p) + 1;
                end_p = text.IndexOf(']', start_p);
                string theme = text.Substring(start_p, end_p - start_p);

                //Cronstuct the question
                Question pr = new Question(texto, wrongOptions, rightOptions, theme);
                Data.AddQuestion(pr);
            }
        }

        public static void QuestionAnswered(bool isRight)
        {
            if (isRight)
                cntSesionRightAns++;
            else
                cntSesionWrongAns++;

        }

        public static Question GetQuestion()
        {
            Random rand = new Random(Guid.NewGuid().GetHashCode());
            return questionlist[rand.Next(questionlist.Count)];
        }

        public static void WriteSettings()
        {
            var options = new JsonSerializerOptions { WriteIndented = true, IncludeFields = true };
            string s = JsonSerializer.Serialize(settings, options);
            System.IO.File.WriteAllText(System.AppDomain.CurrentDomain.BaseDirectory + "\\settings.txt", s);
        }

        public static void ReadSettings()
        {
            if (System.IO.File.Exists(System.AppDomain.CurrentDomain.BaseDirectory + "\\settings.txt"))
            {
                string s = System.IO.File.ReadAllText(System.AppDomain.CurrentDomain.BaseDirectory + "\\settings.txt");
                var options = new JsonSerializerOptions { WriteIndented = true, IncludeFields = true };
                settings = JsonSerializer.Deserialize<Settings>(s, options);
            }
        }

        public static string GetSesionInfo()
        {
            return "Sesion actual:    Aciertos = " + cntSesionRightAns.ToString() + "   |    Errores = " + cntSesionWrongAns.ToString();
        }
        
    }
}
