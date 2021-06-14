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
        static QButtonState qButtonState = QButtonState.GetQuestion;
        public MainWindow()
        {
            Dictionary<String, List<Question>> materias = new Dictionary<string, List<Question>>();
            Data.ReadData();
            InitializeComponent();
            
        }

        private void Pregunta_Click(object sender, RoutedEventArgs e)
        {
            if(qButtonState == QButtonState.GetQuestion)
            {
                DisplayQuestion();
                qButtonState = QButtonState.VerifyQuestion;
                Pregunta.Content = "Revisar respuesta";
                return;
            }
            if(qButtonState == QButtonState.VerifyQuestion)
            {
                CheckAnswers();
                qButtonState = QButtonState.GetQuestion;
                Pregunta.Content = "Siguiente pregunta";
                return;
            }
            
        }

        private void Opciones_Click(object sender, RoutedEventArgs e)
        {
            Opciones op = new Opciones();
            op.Show();
        }

        enum QButtonState
        {
            GetQuestion = 0,
            VerifyQuestion = 1,
        }

        private void CheckAnswers()
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
                if (rightOnes.Count == 1)
                    textToShow += "La respuesta correcta es: \n";
                else
                    textToShow += "Las respuestas correctas son: \n";
                foreach(CheckBox op in rightOnes)
                {
                    textToShow += op.Content + "\n";
                }
                Data.questionAnswered(false);
            }
            else
            {
                textToShow += "¡Respuesta correcta! \n";
                foreach (CheckBox op in rightOnes)
                {
                    textToShow += op.Content + "\n";
                }
                Data.questionAnswered(true);
            }
            QuestionText.Text = textToShow;

        }

        private void DisplayQuestion()
        {
            Question q = Data.GetQuestion();
            List<QuestionOption> options = q.GenerateAndGetListOfOptions(Settings.rightAnswers, Settings.wrongAnswers);
            QuestionText.Text = q.GetQuestionText();
            OptionsPanel.Children.Clear();
            optionsBoxes.Clear();
            for (int i = 0; i < options.Count; i++)
            {
                CheckBox box = new CheckBox();
                box.Content = Settings.GetCharacterForOption(i) + " )" + options[i].GetContent();
                box.HorizontalAlignment = HorizontalAlignment.Left;
                box.VerticalAlignment = VerticalAlignment.Top;
                Thickness t = new Thickness(60, 20, 0, 0);
                box.Margin = t;
                OptionsPanel.Children.Add(box);
                optionsBoxes.Add(box, options[i].IsRight());
               
            }
        }

    }
}
