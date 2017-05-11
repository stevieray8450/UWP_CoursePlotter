using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace App1
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    /// 

    public enum LetterGrade { A, Amin, Bmax, B, Bmin, Cmax, C, Cmin, Dmax, D, Dmin, F };

    public sealed partial class GPACalc : Page
    {
        public GPACalc()
        {
            this.InitializeComponent();
        }

        public void hypLinkPage2_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Faculty));
        }

        private void HyperlinkButton_Click_1(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));

        }

        private void HyperlinkButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(NewPage));

        }

        private void todo2_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(UserAuth));
        }

        private void invPage_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(InvPage));
        }

        private void GPACalcPage_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(GPACalc));
        }

        private void calcGPAbtn_Click(object sender, RoutedEventArgs e)
        {
            double[] hoursArray = new double[10];
            double[] scoreArray = new double[10];

            hoursArray[0] = ParseHours(hrs1.Text);
            hoursArray[1] = ParseHours(hrs2.Text);
            hoursArray[2] = ParseHours(hrs3.Text);
            hoursArray[3] = ParseHours(hrs4.Text);
            hoursArray[4] = ParseHours(hrs5.Text);
            hoursArray[5] = ParseHours(hrs6.Text);
            hoursArray[6] = ParseHours(hrs7.Text);
            hoursArray[7] = ParseHours(hrs8.Text);
            hoursArray[8] = ParseHours(hrs9.Text);
            hoursArray[9] = ParseHours(hrs10.Text);

            scoreArray[0] = ParseScore(grade1.SelectedIndex);
            scoreArray[1] = ParseScore(grade2.SelectedIndex);
            scoreArray[2] = ParseScore(grade3.SelectedIndex);
            scoreArray[3] = ParseScore(grade4.SelectedIndex);
            scoreArray[4] = ParseScore(grade5.SelectedIndex);
            scoreArray[5] = ParseScore(grade6.SelectedIndex);
            scoreArray[6] = ParseScore(grade7.SelectedIndex);
            scoreArray[7] = ParseScore(grade8.SelectedIndex);
            scoreArray[8] = ParseScore(grade9.SelectedIndex);
            scoreArray[9] = ParseScore(grade10.SelectedIndex);

            gpaResult.Text = CalcTotalGPA(hoursArray, scoreArray);

        }

        private double ParseHours(string hourTextBox)
        {
            if (String.IsNullOrEmpty(hourTextBox))
            {
                return 0;
            }
            else
            {
                return Double.Parse(hourTextBox);
            }
        }

        private double ParseScore(int letterGrade)
        {
            switch (letterGrade)
            {
                case (int)LetterGrade.A:
                    return 4.0;
                case (int)LetterGrade.Amin:
                    return 3.7;
                case (int)LetterGrade.Bmax:
                    return 3.33;
                case (int)LetterGrade.B:
                    return 3.0;
                case (int)LetterGrade.Bmin:
                    return 2.7;
                case (int)LetterGrade.Cmax:
                    return 2.3;
                case (int)LetterGrade.C:
                    return 2.0;
                case (int)LetterGrade.Cmin:
                    return 1.7;
                case (int)LetterGrade.Dmax:
                    return 1.3;
                case (int)LetterGrade.D:
                    return 1.0;
                case (int)LetterGrade.Dmin:
                    return 0.7;
                case (int)LetterGrade.F:
                    return 0.0;
                default:
                    return 0.0;
                }
            }

        private string CalcTotalGPA(double[] hrsEntered, double[] scoresEntered)
        {
            double totalHrs = 0;
            double totalGradePoints = 0;

            for(int i = 0; i < hrsEntered.Length - 1; i++)
            {
                totalHrs += hrsEntered[i];
                totalGradePoints += scoresEntered[i] * hrsEntered[i];
            }

            double totalGPA = totalGradePoints / totalHrs;

            return Math.Round(totalGPA, 2).ToString();
        }


    }
    }
