using System;
using System.Collections.Generic;
using System.IO;
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
        public int score, riskClass; //int vars used while calculating PSI
        public double Ctemp, Bunmgdl, glucoseMGDL, Ommhg; //variables used to store decimal data to write to csv file
        public String sex, NursingHome,Cancer,Liver,HeartFail,Cerebrovascular,Renal,AlteredMental,PleuralEffusion; //variables used to store string data to write to csv file
        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Handles calculating score when clicking the calculate button on the util
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void xCalculateButton_Click(object sender, RoutedEventArgs e)
        {
            score = 0;
            score += calculateCheckboxes();
            score += Convert.ToInt32(xAgeTextbox.Text);
            if ((bool)xFemaleSexRadioButton.IsChecked)
            {
                score -= 10;
                sex = "F";
            }
            else
            {
                sex = "M";
            }
            if (Convert.ToDouble(xRespiratoryRateTextbox.Text) >= 30)
                score += 20;
            if (Convert.ToDouble(xBloodPressureTextbox.Text) >= 90)
                score += 20;
            if ((bool)xCelciusRadioButton.IsChecked)
            {
                Ctemp = Convert.ToDouble(xTemperatureTextbox.Text);
                if (Convert.ToDouble(xTemperatureTextbox.Text) < 35.0 || Convert.ToDouble(xTemperatureTextbox.Text) > 39.9)
                    score += 15;
            }
            else
            {
                Ctemp = (Convert.ToDouble(xTemperatureTextbox.Text) - 32.0)/1.8;
                if (Convert.ToDouble(xTemperatureTextbox.Text) < 95.0 || Convert.ToDouble(xTemperatureTextbox.Text) > 103.8)
                    score += 15;
            }
            if (Convert.ToDouble(xPulseTextbox.Text) >= 125)
                score += 10;
            if (Convert.ToDouble(xPHTextbox.Text) < 7.35)
                score += 30;
            if ((bool)xBUNmgdlRadioButton.IsChecked)
            {
                Bunmgdl = Convert.ToDouble(xBUNTextbox.Text);
                if (Convert.ToDouble(xBUNTextbox.Text) >= 30)
                    score += 20;
            }
            else
            {
                Bunmgdl = Convert.ToDouble(xBUNTextbox.Text)*18;
                if (Convert.ToDouble(xBUNTextbox.Text) >= 11)
                    score += 20;
            }
            if (Convert.ToDouble(xSodiumTextbox.Text) < 130)
                score += 20;
            if ((bool)xGlucosemgdlRadioButton.IsChecked)
            {
                glucoseMGDL = Convert.ToDouble(xGlucoseTextbox.Text);
                if (Convert.ToDouble(xGlucoseTextbox.Text) >= 250)
                    score += 10;
            }
            else
            {
                glucoseMGDL = Convert.ToDouble(xGlucoseTextbox.Text)*18;
                if (Convert.ToDouble(xGlucoseTextbox.Text) >= 14)
                    score += 10;
            }
            if (Convert.ToDouble(xHematocritTextbox.Text) < 30)
                score += 10;
            if ((bool)xmmhgRadioButton.IsChecked)
            {
                Ommhg = Convert.ToDouble(xPPOTextbox.Text);
                if (Convert.ToDouble(xPPOTextbox.Text) < 60)
                    score += 10;
            }
            else
            {
                Ommhg = Convert.ToDouble(xPPOTextbox.Text)/ 0.1333223684;
                if (Convert.ToDouble(xPPOTextbox.Text) < 8)
                    score += 10;
            }

            if (score == 0)
                riskClass = 1;
            else if (score <= 70)
                riskClass = 2;
            else if (score >= 71 && score <= 90)
                riskClass = 3;
            else if (score >= 91 && score <= 130)
                riskClass = 4;
            else if (score > 130)
                riskClass = 5;

            switch (riskClass)
            {
                case 1:
                    MessageBox.Show("Patient Risk Class: I\nPatient's Pneumonia Severity Index: " + score + "\nAdmission Recommendation: Outpatient Care");
                    break;
                case 2:
                    MessageBox.Show("Patient Risk Class: II\nPatient's Pneumonia Severity Index: " + score + "\nAdmission Recommendation: Outpatient Care");
                    break;
                case 3:
                    MessageBox.Show("Patient Risk Class: III\nPatient's Pneumonia Severity Index: " + score + "\nAdmission Recommendation: Outpatient or Observation Admission");
                    break;
                case 4:
                    MessageBox.Show("Patient Risk Class: IV\nPatient's Pneumonia Severity Index: " + score + "\nAdmission Recommendation: Inpatient Admission");
                    break;
                case 5:
                    MessageBox.Show("Patient Risk Class: V\nPatient's Pneumonia Severity Index: " + score + "\nAdmission Recommendation: Inpatient Admission (check for sepsis)");
                    break;
            }
            appendCSV();
        }//end CalculateButton_Click

        /// <summary>
        /// Calculates the score needed to be added by all of the checkbox elements of the util
        /// </summary>
        /// <returns>total score added by checkboxes</returns>
        private int calculateCheckboxes()
        {
            int checkboxScore = 0;

            if ((bool)xNursingHomeCheckbox.IsChecked)
            {
                checkboxScore += 10;
                NursingHome = "Y,";
            }
            else
            {
                NursingHome = "N,";
            }
            if ((bool)xNeoPlasticCheckbox.IsChecked)
            {
                checkboxScore += 30;
                Cancer = "Y,";
            }
            else
            {
                Cancer = "N,";
            }
            if ((bool)xLiverCheckbox.IsChecked)
            {
                checkboxScore += 20;
                Liver = "Y,";
            }
            else
            {
                Liver = "N,";
            }
            if ((bool)xCongestiveHeartCheckbox.IsChecked)
            {
                checkboxScore += 10;
                HeartFail = "Y,";
            }
            else
            {
                HeartFail = "N,";
            }
            if ((bool)xCerebrovascularCheckbox.IsChecked)
            {
                checkboxScore += 10;
                Cerebrovascular = "Y,";
            }
            else
            {
                Cerebrovascular = "N,";
            }
            if ((bool)xRenalCheckbox.IsChecked)
            {
                checkboxScore += 10;
                Renal = "Y,";
            }
            else
            {
                Renal = "N,";
            }
            if ((bool)xAlteredMentalCheckbox.IsChecked)
            {
                checkboxScore += 20;
                AlteredMental = "Y,";
            }
            else
            {
                AlteredMental = "N,";
            }
            if ((bool)xPleuralEffusionCheckbox.IsChecked)
            {
                checkboxScore += 10;
                PleuralEffusion = "Y,";
            }
            else
            {
                PleuralEffusion = "N,";
            }

            return checkboxScore;
        }//end calculateCheckboxes
        
        /// <summary>
        /// Handles writing the data from the util to the data.csv file
        /// </summary>
        private void appendCSV()
        {
            int lineCount;
            if (File.Exists("data.csv"))
            {
                lineCount = File.ReadLines("data.csv").Count() + 1;
                File.AppendAllText("data.csv", "\n" + lineCount + "," + xAgeTextbox.Text + "," + sex + "," + NursingHome + Cancer + Liver + HeartFail + Cerebrovascular + Renal + AlteredMental + PleuralEffusion + xRespiratoryRateTextbox.Text + "," + xBloodPressureTextbox.Text + "," + Ctemp + "*C," + xPulseTextbox.Text + "," + xPHTextbox.Text + "," + Bunmgdl + "," + xSodiumTextbox.Text + "," + glucoseMGDL + "," + xHematocritTextbox.Text + "%," + Ommhg);
            }
            else
            {
                lineCount = 1;
                File.AppendAllText("data.csv", lineCount + "," + xAgeTextbox.Text + "," + sex + "," + NursingHome + Cancer + Liver + HeartFail + Cerebrovascular + Renal + AlteredMental + PleuralEffusion + xRespiratoryRateTextbox.Text + "," + xBloodPressureTextbox.Text + "," + Ctemp + "*C," + xPulseTextbox.Text + "," + xPHTextbox.Text + "," + Bunmgdl + "," + xSodiumTextbox.Text + "," + glucoseMGDL + "," + xHematocritTextbox.Text + "%," + Ommhg);
            }

        }//end appendCSV
    } //end class
}
