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
        Opciones optWindow;

        public MainWindow()
        {
            Dictionary<String, List<Question>> materias = new Dictionary<string, List<Question>>();
            Data.Initialize();
            InitializeComponent();
        }

        private void Pregunta_Click(object sender, RoutedEventArgs e)
        {
            if(qButtonState == QButtonState.GetQuestion)
            {
                qButtonState = QButtonState.VerifyQuestion;
                Pregunta.Content = "Revisar respuesta";
                DisplayQuestion();
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
            if (!Application.Current.Windows.OfType<Opciones>().Any())
            {
                Opciones opt = new Opciones();
                opt.Show();
                optWindow = opt;
            }
            else optWindow.Focus();
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
            bool somethingWrong = false;
            List<CheckBox> selectedOnes = new List<CheckBox>();
            foreach (KeyValuePair<CheckBox, bool> op in optionsBoxes)
            {
                if (op.Value != op.Key.IsChecked)
                    wrongOnes.Add(op.Key);
                if (op.Value == false && op.Key.IsChecked == true)
                    somethingWrong = true;
                if (op.Value)
                {
                    rightOnes.Add(op.Key);
                    if (op.Key.IsChecked == true)
                        selectedOnes.Add(op.Key);
                }
                if (op.Value)
                    op.Key.Foreground = Brushes.DarkGreen;
                else
                    op.Key.Foreground = Brushes.Red;
            }
            string textToShow = "";
            if (Data.settings.rAnsMode == Settings.RightAnswerMode.MarkAll)
            {
                if(wrongOnes.Count == 0)
                {
                    textToShow += "¡Respuesta correcta! \n";
                    foreach (CheckBox op in rightOnes)
                    {
                        textToShow += op.Content + "\n";
                    }
                    DisplayAnswerEvaluation(textToShow, true);
                    return;
                }
            }
            if (Data.settings.rAnsMode == Settings.RightAnswerMode.MarkOne)
            {
                if( selectedOnes.Count !=0 && !somethingWrong)
                {
                    textToShow += "¡Respuesta correcta! \n";
                    foreach( CheckBox s in selectedOnes)
                    {
                        textToShow += s.Content + "\n";
                    }
                        
                    if(wrongOnes.Count > 0)
                    {
                        textToShow += "Otras respuestas correctas son:\n";
                    }
                    foreach (CheckBox b in wrongOnes)
                    {
                        textToShow += b.Content + "\n";
                    }
                    DisplayAnswerEvaluation(textToShow, false);
                    return;
                }
            }
            if (rightOnes.Count == 0 )
            {
                if(wrongOnes.Count == 0)
                {
                    textToShow += "Respuesta correcta, todas las opciones eran incorrectas.";
                    DisplayAnswerEvaluation(textToShow, true);

                }
                else
                {
                    textToShow += "Respuesta incorrecta, todas las opciones eran incorrectas.";
                    DisplayAnswerEvaluation(textToShow, false);
                }
                QuestionText.Text = textToShow;
                return;
            }
            textToShow += "Respuesta incorrecta. ";
            if (rightOnes.Count == 1)
                textToShow += "La respuesta correcta es: \n";
            else
                textToShow += "Las respuestas correctas son: \n";
            foreach (CheckBox op in rightOnes)
            {
                textToShow += op.Content + "\n";
            }
            DisplayAnswerEvaluation(textToShow, false);
        }

        private void DisplayAnswerEvaluation(string message, bool isRight)
        {
            QuestionAnswered(isRight);
            QuestionText.Text = message;
            if (isRight)
                QuestionText.Foreground = Brushes.Green;
            else
                QuestionText.Foreground = Brushes.Red;
        }

        private void QuestionAnswered(bool isRight)
        {
            Data.QuestionAnswered(isRight);
            Status.Text = Data.GetSesionInfo();
        }

        private void DisplayQuestion()
        {
            QuestionText.Foreground = Brushes.Black;
            Question q = Data.GetQuestion();
            if(q == null)
            {
                QuestionText.Text = "No hay más preguntas que mostrar de acuerdo a los parametros seleccionados.";
                qButtonState = QButtonState.GetQuestion;
                Pregunta.Content = "Sesión finalizada";
                OptionsPanel.Children.Clear();
                return;
            }
            List<QuestionOption> options = q.GenerateAndGetListOfOptions(Data.settings.rightOptions, Data.settings.totalOptions);
            QuestionText.Text = q.GetQuestionText();
            string imgName = q.GetImageName();
            if(imgName != "" && System.IO.File.Exists(AppDomain.CurrentDomain.BaseDirectory + "\\pregu\\" + imgName))
            {
                QuestionText.ContentEnd.InsertLineBreak();
                QuestionText.ContentEnd.InsertLineBreak();
                Image img = new Image();
                img.Height = 200;
                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.UriSource = new Uri(AppDomain.CurrentDomain.BaseDirectory + "\\pregu\\" + imgName);
                bitmapImage.DecodePixelHeight = 200;
                bitmapImage.EndInit();
                img.Source = bitmapImage;
                InlineUIContainer uIContainer = new InlineUIContainer(img, QuestionText.ContentEnd);
            }
            OptionsPanel.Children.Clear();
            optionsBoxes.Clear();
            for (int i = 0; i < options.Count; i++)
            {
                CheckBox box = new CheckBox
                {
                    Content = Data.settings.GetCharacterForOption(i) + " )" + options[i].GetContent(),
                    HorizontalAlignment = HorizontalAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Top
                };
                Thickness t = new Thickness(60, 20, 0, 0);
                box.Margin = t;
                OptionsPanel.Children.Add(box);
                if (q.GetImageName() != "" && System.IO.File.Exists(AppDomain.CurrentDomain.BaseDirectory + "\\pregu\\" + options[i].GetImgName()))
                {
                    Image img = new Image();
                    img.Width = 100;
                    BitmapImage bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.UriSource = new Uri(AppDomain.CurrentDomain.BaseDirectory + "\\pregu\\" + options[i].GetImgName());
                    bitmap.DecodePixelWidth = 100;
                    bitmap.EndInit();
                    img.Source = bitmap;
                    OptionsPanel.Children.Add(img);
                    img.HorizontalAlignment = HorizontalAlignment.Left;
                    img.VerticalAlignment = VerticalAlignment.Top;
                    img.Margin = t;
                }


                optionsBoxes.Add(box, options[i].IsRight());
               
            }
        }

    }
}
