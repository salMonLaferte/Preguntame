using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preguntame
{
    public static class Data
    {
        static List<SubTheme> themes = new List<SubTheme>();
        static List<Question> questionlist = new List<Question>();
        static int questionCount = 0;

        public static void AddTheme( string parentTAG, string name, string TAG)
        {
            if (parentTAG == "")
                themes.Add(new SubTheme(name, TAG));
            SubTheme n = new SubTheme(name, TAG);
            foreach(SubTheme t in themes)
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
            string text = System.IO.File.ReadAllText(@"C:\Users\Dr ID\Documents\pregu.txt");
            int f = 0;
            while (f < text.Length)
            {
                int s = text.IndexOf('{', f);
                if (s < 0)
                    break;
                f = text.IndexOf('}', s);
                int start_p = text.IndexOf('[', s);
                int end_p = text.IndexOf(']', s);
                string texto = text.Substring(start_p + 1, end_p - start_p - 1);
                start_p = text.IndexOf('[', end_p) + 1;
                end_p = text.IndexOf(']', start_p);
                List<String> opciones = new List<String>();
                while (true)
                {
                    int sep = text.IndexOf('|', start_p);
                    if (sep < 0 || sep > f)
                    {
                        opciones.Add(text.Substring(start_p, end_p - start_p - 1));
                        start_p = end_p + 1;
                        break;
                    }
                    opciones.Add(text.Substring(start_p, sep - start_p - 1));
                    start_p = sep + 1;
                }
                start_p = text.IndexOf('[', end_p);
                end_p = text.IndexOf(']', start_p);
                string theme = text.Substring(start_p + 1, end_p - start_p - 1);
                start_p = text.IndexOf('[', end_p);
                end_p = text.IndexOf(']', start_p);
                string answer = text.Substring(start_p + 1, end_p - start_p - 1);
                string[] wrong = { "yolo" };
                Question pr = new Question(texto, wrong, opciones.ToArray(),  theme);
                Data.AddQuestion(pr);
            }
        }

        public static Question GetQuestion()
        {
            Random rand = new Random(Guid.NewGuid().GetHashCode());
            return questionlist[rand.Next(questionlist.Count)];
        }
        
        /*public static SubTheme GetRandomTheme()
        {
            Random rand = new Random();
            int value = rand.Next(themes.Count);
        }*/

    }
}
