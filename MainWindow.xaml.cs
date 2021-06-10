using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;

namespace Preguntame
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static Dictionary<CheckBox, bool> optionsBoxes = new Dictionary<CheckBox, bool>();
        public MainWindow()
        {
            Dictionary<String, List<Question>> materias = new Dictionary<string, List<Question>>();
            bool studyAll = false;
            bool deleteOnApeareance = false;
            int aciertos = 0;
            int errores = 0;

            Data.ReadData();
            InitializeComponent();
            
        }

        private void Pregunta_Click(object sender, RoutedEventArgs e)
        {
            Question q = Data.GetQuestion();
            List<QuestionOption> options = q.GenerateAndGetListOfOptions(Settings.rightAnswers, Settings.wrongAnswers);
            QuestionText.Text = q.GetQuestionText();
            OptionsPanel.Children.Clear();
            optionsBoxes.Clear();
            for(int i=0; i< options.Count; i++)
            {
                CheckBox box = new CheckBox();
                box.Content = Settings.GetCharacterForOption(i) + " )" + options[i].content;
                box.HorizontalAlignment = HorizontalAlignment.Left;
                box.VerticalAlignment = VerticalAlignment.Top;
                Thickness t = new Thickness(60,20, 0, 0);
                box.Margin = t;
                OptionsPanel.Children.Add(box);
                optionsBoxes.Add(box, options[i].isRight);
            }
        }


        private void Verificar_Click(object sender, RoutedEventArgs e)
        {
            List<CheckBox> wrongOnes = new List<CheckBox>();
            List<CheckBox> rightOnes = new List<CheckBox>();
            foreach (KeyValuePair<CheckBox, bool> op in optionsBoxes)
            {
                if (op.Value != op.Key.IsChecked)
                    wrongOnes.Add(op.Key);
                if (op.Value)
                    rightOnes.Add(op.Key);
            }
            string textToShow = "";
            if (wrongOnes.Count > 0)
            {
                textToShow += "Respuesta incorrecta.";
                if (Settings.rightAnswers == 1)
                    textToShow += "La respuesta correcta es: \n";
                else
                    textToShow += "Las respuestas correctas son: \n ";
                foreach(CheckBox op in rightOnes)
                {
                    textToShow += op.Content + "\n";
                }
            }
            else
            {
                textToShow += "¡Respuesta correcta! \n";
                foreach (CheckBox op in rightOnes)
                {
                    textToShow += op.Content + "\n";
                }
            }
            QuestionText.Text = textToShow;

        }
    }
}
