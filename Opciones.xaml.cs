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
            TotalOpt.Text = Data.settings.totalOptions.ToString();
            OptRight.Text = Data.settings.rightOptions.ToString();
            RandOptRight.IsChecked = Data.settings.randRightOptions;
            RandOptRight.UpdateLayout();
            OptionName.SelectedIndex = (int)Data.settings.rAnsMode;
            OptionName.UpdateLayout();
            RepeatMode.SelectedIndex = (int)Data.settings.repeatQuestions;
            foreach(KeyValuePair<string,bool> t in Data.settings.themeSelection)
            {
                CheckBox c = new CheckBox();
                c.Tag = t.Key;
                c.Content = "[" + t.Key + "] - "+ SubTheme.GetThemeName(t.Key);
                c.IsChecked = t.Value;
                ThemeSelection.Children.Add(c);
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            string error = SaveSettings();
            if (error == "")
            {
                Close();
            }
            else
                ErrorSaving.Text = error;
        }

        string SaveSettings()
        {
            int totalOp, rightOpt = 0;
            if (Int32.TryParse(TotalOpt.Text, out totalOp)       
                && RandOptRight.IsChecked !=null
                && OptionName.SelectedIndex !=-1
                && RepeatMode.SelectedIndex !=-1)
            {
                if (totalOp > 0)
                {
                    if (RandOptRight.IsChecked == true || Int32.TryParse(OptRight.Text, out rightOpt))
                    {
                        if (rightOpt < 0)
                            rightOpt = 0;
                        if (totalOp - rightOpt < 0)
                            return "El número de opciones totales no puede ser mayor que el número de opciones correctas.";
                        else {
                            Data.settings.ChangeSettings(totalOp, rightOpt, (bool)RandOptRight.IsChecked, (Settings.RightAnswerMode)OptionName.SelectedIndex, (Settings.RepeatQuestions)RepeatMode.SelectedIndex);
                            
                        }
                       
                    }
                    else
                    {
                        return "Por favor introduce un número válido para el total de opciones correctas por pregunta o marca aleatorio.";
                    }
                }
                else
                    return "El total de opciones por pregunta no puede ser igual o menor que cero.";
            }
            else
            {
                if (!Int32.TryParse(TotalOpt.Text, out totalOp))
                    return "Por favor introduce un número válido para el total de opciones.";
                if (OptionName.SelectedIndex != -1)
                    return "Por favor selecciona un modo de respuesta correcta.";
            }
            return "";
        }

        private void RandOptRight_Checked(object sender, RoutedEventArgs e)
        {
            OptRight.IsReadOnly = true;
            OptRight.Opacity = .5;
        }

        private void RandOptRight_Unchecked_1(object sender, RoutedEventArgs e)
        {
            OptRight.IsReadOnly = false;
            OptRight.Opacity = 1;
        }

        private void SaveThemesSelection_Click(object sender, RoutedEventArgs e)
        {
            Dictionary<string, bool> changes = new Dictionary<string, bool>();
            foreach (CheckBox c in ThemeSelection.Children)
            {
                changes.Add(c.Tag.ToString(), (bool)c.IsChecked);
            }
            Data.settings.ChangeThemeSelection(changes);
            Close();
        }


    }
}
