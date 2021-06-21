using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Preguntame
{
    public static class Data
    {
        static Dictionary<string,SubTheme> themes = new Dictionary<string,SubTheme>();
        static List<SubTheme> themeTree = new List<SubTheme>();
        static List<Question> questionlist = new List<Question>();
        static List<Question> availableQuestions = new List<Question>();
        static Dictionary<string, string> themesNames = new Dictionary<string, string>();

        static int cntSesionRightAns = 0;
        static int cntSesionWrongAns = 0;

        public static Settings settings = new Settings();

        public static void AddTheme(string parentTAG, string TAG)
        {
            if (themes.ContainsKey(TAG))
                return;
            SubTheme n = new SubTheme(TAG);
            themes.Add(TAG, n);
            if (parentTAG == "")
            {
                themeTree.Add(n);
                return;
            }
            foreach (SubTheme t in themeTree)
            {
                SubTheme parent = t.GetChildWithTAG(TAG);
                if (parent != null)
                {
                    parent.addChild(n);
                    return;
                }
            }
        }

        public static void AddQuestion(Question q)
        {
            if (!themes.ContainsKey(q.TAG))
                AddTheme("", q.TAG);
            else
                themes[q.TAG].plusQuestion();
            questionlist.Add(q);
        }

        public static void ReadData()
        {
            string text = "";
            string[] files = System.IO.Directory.GetFiles(System.AppDomain.CurrentDomain.BaseDirectory + "\\pregu", "*.txt", System.IO.SearchOption.TopDirectoryOnly);
            for (int i = 0; i < files.Length; i++)
            {
                if ( !files[i].Contains("names.txt"))
                    text += System.IO.File.ReadAllText(files[i]);
            }
            int f = 0;
            while (f < text.Length)
            {
                bool error = false;

                //Get the indexes where the data from the current questions begins and ends
                int s = text.IndexOf('{', f);
                if (s < 0)
                    break;
                f = text.IndexOf('}', s);

                //Get the text part from the question
                int start_p = text.IndexOf('[', s) + 1;
                int end_p = text.IndexOf(']', s);
                string texto = "";
                string[] rightOptions = { };
                string[] wrongOptions = { };
                string[] imgRight = { };
                string[] imgWrong = { };
                string theme = "";
                string img = "";

                if (start_p > f || end_p > f)
                    error = true;
                else
                {
                    texto = text.Substring(start_p, end_p - start_p);
                    string[] p = texto.Split('&');
                    if (p.Length > 1)
                    {
                        texto = p[0];
                        img = p[1];
                    }
                        
                }

                //Get an array of the wrong answers
                if (!error)
                {
                    start_p = text.IndexOf('[', end_p) + 1;
                    end_p = text.IndexOf(']', start_p);
                    if (start_p > f || end_p > f)
                        error = true;
                    else
                    {
                        string wrongOpStr = text.Substring(start_p, end_p - start_p);
                        wrongOptions = wrongOpStr.Split('|');
                        imgWrong = new string[wrongOptions.Length];
                        for(int i=0; i<wrongOptions.Length; i++)
                        {
                            string[] aux = wrongOptions[i].Split('&');
                            if(aux.Length > 1)
                            {
                                wrongOptions[i] = aux[0];
                                imgWrong[i] = aux[1];
                            }
                            else
                            {
                                wrongOptions[i] = aux[0];
                                imgWrong[i] = "";
                            }
                        }
                    }
                }

                //Get an array of the correct answers
                if (!error)
                {
                    start_p = text.IndexOf('[', end_p) + 1;
                    end_p = text.IndexOf(']', start_p);
                    if (start_p > f || end_p > f)
                        error = true;
                    else
                    {
                        string rightOpStr = text.Substring(start_p, end_p - start_p);
                        rightOptions = rightOpStr.Split('|');
                        imgRight = new string[rightOptions.Length];
                        for (int i = 0; i < rightOptions.Length; i++)
                        {
                            string[] aux = rightOptions[i].Split('&');
                            if (aux.Length > 1)
                            {
                                rightOptions[i] = aux[0];
                                imgRight[i] = aux[1];
                            }
                            else
                            {
                                rightOptions[i] = aux[0];
                                imgRight[i] = "";
                            }

                        }
                    }
                }

                //Get the TAG of the question
                if (!error)
                {
                    start_p = text.IndexOf('[', end_p) + 1;
                    end_p = text.IndexOf(']', start_p);
                    if (start_p > f || end_p > f)
                        error = true;
                    else
                    {
                        theme = text.Substring(start_p, end_p - start_p);
                    }
                }


                //Cronstuct the question
                if (!error)
                {
                    Question pr = new Question(texto,  rightOptions, wrongOptions, theme, img, imgWrong, imgRight);
                    Data.AddQuestion(pr);
                }

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
            if (availableQuestions.Count > 0)
            {
                int r = rand.Next(availableQuestions.Count);
                Question q = availableQuestions[r];
                if(settings.dontRepeatQuestions)
                    availableQuestions.RemoveAt(r);
                return q;
            }
                
            return null;
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
            string s = "Sesion actual:    Aciertos = " + cntSesionRightAns.ToString() + "   |    Errores = " + cntSesionWrongAns.ToString();
            if (settings.dontRepeatQuestions)
                s += "    |     Preguntas restantes = " + availableQuestions.Count.ToString();
            return s;
        }

        public static void WriteNames()
        {
            string names =  JsonSerializer.Serialize(themesNames);
            System.IO.File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "\\pregu\\names.txt", names);
        }

        public static void ReadNames()
        {
            if(System.IO.File.Exists(AppDomain.CurrentDomain.BaseDirectory + "\\pregu\\names.txt"))
            {
                string names = System.IO.File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "\\pregu\\names.txt");
                themesNames = JsonSerializer.Deserialize<Dictionary<string, string>>(names);
            }
            else
            {
                foreach(KeyValuePair<string, SubTheme> t in themes)
                {
                    themesNames.Add(t.Key, t.Key);
                }
                WriteNames();
            }
        }

        public static void Initialize()
        {
            ReadSettings();
            ReadData();
            ReadNames();
            SelectAllAvailableQuestions();
            settings.DeleteUnusedThemeKeys();
        }

        public static void SelectAllAvailableQuestions() {
            availableQuestions.Clear();
            foreach (KeyValuePair<string,bool> t in settings.themeSelection)
            {
                if (t.Value)
                {
                    foreach (Question q in questionlist)
                    {
                        if (q.TAG == t.Key)
                        {
                            availableQuestions.Add(q);
                        }
                    }
                }
                
            }
        }
        
        public static string GetThemeName(string TAG)
        {
            return themesNames[TAG];
        }

        public static Dictionary<string, SubTheme> GetThemes()
        {
            return themes;
        }

    }
}
