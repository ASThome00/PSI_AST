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

namespace PSI_AST
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public int score;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void xCalculateButton_Click(object sender, RoutedEventArgs e)
        {
            score = 0;
            score += calculateCheckboxes();
            score += Convert.ToInt32(xAgeTextbox.Text);
            if ((bool)xFemaleSexRadioButton.IsChecked)
                score-=10;
            if (Convert.ToInt32(xRespiratoryRateTextbox.Text) >= 30)
                score += 20;
            if (Convert.ToInt32(xBloodPressureTextbox.Text) >= 90)
                score += 20;
            if ((bool)xCelciusRadioButton.IsChecked)
            {
                if (Convert.ToDouble(xTemperatureTextbox.Text) < 35.0 || Convert.ToDouble(xTemperatureTextbox.Text) > 39.9)
                    score += 15;
            }
            else
            {
                if (Convert.ToDouble(xTemperatureTextbox.Text) < 95.0 || Convert.ToDouble(xTemperatureTextbox.Text) > 103.8)
                    score += 15;
            }
            if (Convert.ToInt32(xPulseTextbox.Text) >= 125)
                score += 10;
            if (Convert.ToDouble(xPHTextbox.Text) < 7.35)
                score += 30;
        }

        private int calculateCheckboxes()
        {
            int checkboxScore = 0;

            if ((bool)xNursingHomeCheckbox.IsChecked)
            {
                checkboxScore += 10;
            }
            if ((bool)xNeoPlasticCheckbox.IsChecked)
            {
                checkboxScore += 30;
            }
            if ((bool)xLiverCheckbox.IsChecked)
            {
                checkboxScore += 20;
            }
            if ((bool)xCongestiveHeartCheckbox.IsChecked)
            {
                checkboxScore += 10;
            }
            if ((bool)xCerebrovascularCheckbox.IsChecked)
            {
                checkboxScore += 10;
            }
            if ((bool)xRenalCheckbox.IsChecked)
            {
                checkboxScore += 10;
            }
            if ((bool)xAlteredMentalCheckbox.IsChecked)
            {
                checkboxScore += 20;
            }
            if ((bool)xPleuralEffusionCheckbox.IsChecked)
            {
                checkboxScore += 10;
            }

            return checkboxScore;
        }
    }
}
