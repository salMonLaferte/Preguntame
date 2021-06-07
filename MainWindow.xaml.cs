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

namespace Preguntame
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
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
            q.GenerateListOfOptions(1, 3);
            List<QuestionOption> m = q.getOptions();
            MessageBox.Show(m[0].content);
        }
    }
}
