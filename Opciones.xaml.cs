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
using System.Windows.Shapes;

namespace Preguntame
{
    /// <summary>
    /// Lógica de interacción para Opciones.xaml
    /// </summary>
    public partial class Opciones : Window
    {
        public Opciones()
        {
            
            InitializeComponent();
            LoadSettings();
        }

        void LoadSettings()
        {
            OptWrong.Text = Settings.wrongAnswers.ToString();
            OptRight.Text = Settings.rightAnswers.ToString();
            RandOptRight.IsChecked = Settings.randRightAnswers;
            RandOptWrong.IsChecked = Settings.randWrongAnswers;
            OptionName.SelectedIndex = (int)Settings.rAnsMode;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (SaveSettings())
                Close();
        }

        bool SaveSettings()
        {
            int wrongOpt = 0;
            int rightOpt = 0;
            if (Int32.TryParse(OptWrong.Text, out wrongOpt)
                && Int32.TryParse(OptRight.Text, out rightOpt)
                && RandOptRight.IsChecked !=null
                && RandOptWrong.IsChecked !=null)
            {
                Settings.ChangeSettings(wrongOpt, rightOpt, (bool)RandOptWrong.IsChecked, (bool)RandOptRight.IsChecked);
            }
            else
            {
                return false;
            }
            return true;
        }

    }
}
