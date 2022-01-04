﻿using System;
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
using System.Windows.Threading;

namespace werkplekleren_project_galgje
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 


    public partial class MainWindow : Window
    {        
        private StringBuilder stringBuilderOutputTekst;
        private StringBuilder stringBuilderGeheimWoord;
        private StringBuilder stringBuilderJuisteLetters;
        private StringBuilder stringBuilderFouteLetter;
        string woordInAsteriksAlsString;
        int aantalLevens = 10;
        char[] woordInAsteriks;
        char geradenLetter;
        bool geraden;
        int secondenAftellen;
        string geradenWoord;
        DispatcherTimer timer = new DispatcherTimer();
        private string[] galgjeWoorden = new string[]
        {
            "grafeem",
            "tjiftjaf",
            "maquette",
            "kitsch",
            "pochet",
            "convocaat",
            "jakkeren",
            "collaps",
            "zuivel",
            "cesium",
            "voyant",
            "spitten",
            "pancake",
            "gietlepel",
            "karwats",
            "dehydreren",
            "viswijf",
            "flater",
            "cretonne",
            "sennhut",
            "tichel",
            "wijten",
            "cadeau",
            "trotyl",
        };
        Random randomWoordGenerator = new Random();
        public MainWindow()
        {
            InitializeComponent();
            stringBuilderOutputTekst = new StringBuilder();
            stringBuilderGeheimWoord = new StringBuilder();
            stringBuilderJuisteLetters = new StringBuilder();
            stringBuilderFouteLetter = new StringBuilder();            
            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = new TimeSpan(0, 0, 1);            
        }
        private void VerbergWoordButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            StartSpel();
            timer.Start();
        }
        private void VerbergWoordButton_MouseEnter(object sender, MouseEventArgs e)
        {
            Cursor = Cursors.Hand;
            VerbergWoordButton.Background = Brushes.Black;
            VerbergWoordButton.Foreground = Brushes.Maroon;
            VerbergWoordButton.BorderBrush = Brushes.Red;
        }

        private void VerbergWoordButton_MouseLeave(object sender, MouseEventArgs e)
        {
            Cursor = Cursors.Arrow;
            VerbergWoordButton.Background = Brushes.Maroon;
            VerbergWoordButton.Foreground = Brushes.Black;
            VerbergWoordButton.BorderBrush = Brushes.Black;
        }
        private void RaadButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            timer.Start();
            VerloopVanHetSpel();
            secondenAftellen = Convert.ToInt32(secondeningeven.Text);
            seconden.Text = secondenAftellen.ToString();
            InputTextBox.Text = string.Empty;
            
        }

        private void RaadButton_MouseEnter(object sender, MouseEventArgs e)
        {
            Cursor = Cursors.Hand;
            RaadButton.Background = Brushes.Black;
            RaadButton.Foreground = Brushes.Maroon;
            RaadButton.BorderBrush = Brushes.Red;
        }

        private void RaadButton_MouseLeave(object sender, MouseEventArgs e)
        {
            Cursor = Cursors.Arrow;
            RaadButton.Background = Brushes.Maroon;
            RaadButton.Foreground = Brushes.Black;
            RaadButton.BorderBrush = Brushes.Black;
        }
        private void NieuwSpel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            AllesVoorEenNieuwSpel();
        }
        private void NieuwSpel_MouseEnter(object sender, MouseEventArgs e)
        {
            Cursor = Cursors.Hand;
            NieuwSpel.Background = Brushes.Black;
            NieuwSpel.Foreground = Brushes.Maroon;
            NieuwSpel.BorderBrush = Brushes.Red;
        }

        private void NieuwSpel_MouseLeave(object sender, MouseEventArgs e)
        {
            Cursor = Cursors.Arrow;
            NieuwSpel.Background = Brushes.Maroon;
            NieuwSpel.Foreground = Brushes.Black;
            NieuwSpel.BorderBrush = Brushes.Black;
        }
        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Close();
        }
        private void spelerLabel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            RandomWoordGenerator();
            StartSpel();
            timer.Start();
            infoTijd.Visibility = Visibility.Collapsed;
            VWBX_Image.Visibility = Visibility.Visible;
            VWBX_NieuwSpel.Visibility = Visibility.Visible;
            VWBX_RaadButton.Visibility = Visibility.Visible;
            VWBX_Output.Visibility = Visibility.Visible;
            VWBX_Input.Visibility = Visibility.Visible;
            VWBX_speler.Visibility = Visibility.Collapsed;
            VWBX_spelers.Visibility = Visibility.Collapsed;
            VWBX_Secondeningeven.Visibility = Visibility.Collapsed;
            VWBX_seconden.Visibility = Visibility.Visible;
        }

        private void speler_MouseEnter(object sender, MouseEventArgs e)
        {
            Cursor = Cursors.Hand;
            speler.Background = Brushes.Black;
            speler.Foreground = Brushes.Maroon;
            speler.BorderBrush = Brushes.Red;
        }

        private void speler_MouseLeave(object sender, MouseEventArgs e)
        {
            Cursor = Cursors.Arrow;
            speler.Background = Brushes.Maroon;
            speler.Foreground = Brushes.Black;
            speler.BorderBrush = Brushes.Black;
        }

        //methodes               
        private void StartSpel()
        {
            stringBuilderGeheimWoord.Append(InputTextBox.Text);
            AanmakenWoordInAsterix();
            InputTextBox.Text = string.Empty;            
            VWBX_RaadButton.Visibility = Visibility.Visible;
            VWBX_VerbergWoord.Visibility = Visibility.Collapsed;          
            StringBuilderOutput();
            secondenAftellen = Convert.ToInt32(secondeningeven.Text);
        }
        private void timer_Tick(object sender, EventArgs e)
        {
            secondenAftellen--;
            seconden.Text = secondenAftellen.ToString();
            TimerTellerOpNul();
            TellerVeranderdVanKleurBijLageWaarde();
        }
        /// <summary>
        /// wanneer het aantal levens op 0 staat zal de raadbuuton uit staan en er een tekst geprint worden.
        /// </summary>
        private void VerloopVanHetSpel()
        {
            
            if (aantalLevens != 1)
            {
                IsHetGeradenWoordofLetterJuist();
            }
            else
            {
                timer.Stop();
                OutputTextBlock.Text = $"Sorry maar al je levens zijn op het woord dat we zochten was {stringBuilderGeheimWoord}";
                RaadButton.IsEnabled = false;                               
            }
            
            GetImage();
        }

        /// <summary>
        /// <code>
        /// In deze methode worden beide stringBuilders terug leeggemaakt en vernieuwd.
        /// De tekst in de ouputbox terug gereset naar oorspronkelijk gewenste tekst
        ///  en input tekst box gecleared.
        ///  De raad knop wordt terug hidden en de verbergWoordButton is terug enabled.
        ///  ook het aantal levens staat terug op 10.
        ///  </code>
        /// </summary>
        private void AllesVoorEenNieuwSpel()
        {
            InputTextBox.Text = "";
            OutputTextBlock.Text = "Geheim woord ingeven:";
            stringBuilderGeheimWoord.Clear();
            stringBuilderOutputTekst.Clear();
            stringBuilderJuisteLetters.Clear();
            stringBuilderFouteLetter.Clear();
            VWBX_VerbergWoord.Visibility = Visibility.Visible;
            VWBX_RaadButton.Visibility = Visibility.Collapsed;
            aantalLevens = 10;
            seconden.Text = "10";
            secondenAftellen = 10;
            timer.Stop();
            RaadButton.IsEnabled = true;
        }
        private void AanmakenWoordInAsterix()
        {
            woordInAsteriks = new char[stringBuilderGeheimWoord.Length];
            for (int i = 0; i < stringBuilderGeheimWoord.Length; i++)
            {
                woordInAsteriks[i] = '*';
                woordInAsteriksAlsString = new string(woordInAsteriks);
            }            
        }

        /// <summary>
        /// Hier kijken we of de geraden letter in het woord staat. Bij zowel ja als neen word de stringBuilder aangepast en gaat het aantal levens met 1 omlaag.
        /// </summary>
        private void BoolAanmakenOmTeKijkenOfLetterInGeheimWoordStaat()
        {
            string woordConverrsieNaarString = stringBuilderGeheimWoord.ToString();
            geradenLetter = Convert.ToChar(InputTextBox.Text);
            geraden = woordConverrsieNaarString.Contains(geradenLetter);

        }

        /// <summary>
        /// De ouput wordt hier gevuld en aangepast met de weergegeven en ingegeven waardes tekst.
        /// </summary>
        private void StringBuilderOutput()
        {           
            
            OutputTextBlock.Text = ($"{aantalLevens} levens \n\r " +
                                    $"Juiste Letters: {stringBuilderJuisteLetters}\n\r" +
                                    $"Foute Letters: {stringBuilderFouteLetter}\n\r" +
                                    $"{woordInAsteriksAlsString}");          
        }
        private void IsGeradenLetterJuist()
        {
            BoolAanmakenOmTeKijkenOfLetterInGeheimWoordStaat();
            StringBuilderOutput();

            if (geraden)
            {
                if (stringBuilderJuisteLetters.Length == 0)
                {
                    stringBuilderJuisteLetters.Append(InputTextBox.Text);
                }
                else
                {
                    stringBuilderJuisteLetters.Append(", " + InputTextBox.Text);
                }
                
                LetterPlaatsenInWoordInAsterix();
            }
            else
            {
                if (stringBuilderFouteLetter.Length == 0)
                {
                    stringBuilderFouteLetter.Append(InputTextBox.Text);
                }
                else
                {
                    stringBuilderFouteLetter.Append(", " + InputTextBox.Text);
                }
                aantalLevens = aantalLevens - 1;
            }
            StringBuilderOutput();
        }

        private void LetterPlaatsenInWoordInAsterix()
        {
            BoolAanmakenOmTeKijkenOfLetterInGeheimWoordStaat();
            
                for (int j = 0; j < stringBuilderGeheimWoord.Length; j++)
                {
                    if (geradenLetter == stringBuilderGeheimWoord[j])
                    {
                        woordInAsteriks[j] = geradenLetter;                        
                    }
                }
            woordInAsteriksAlsString = new string(woordInAsteriks);
            
        }

        /// <summary>
        /// <code>
        /// Hier kijken we wanneer er bij input een woord word geraden of deze gelijk is aan het geheime woord
        /// indien ja, dan word een overwinningstekst geprint.
        /// Indien nee, dan word er een tekst geprint met het foute woord + het aantal levens je nog hebt
        /// </code>
        /// </summary>
        private void IsHetGeradenWoordJuist()
        {
            geradenWoord = InputTextBox.Text;

            if (geradenWoord == stringBuilderGeheimWoord.ToString())
            {
                RaadButton.IsEnabled = false;
                OutputTextBlock.Text = $"Joepie het antwoord is inderdaad: {geradenWoord}";
                timer.Stop();
            }
            else if (geradenWoord != stringBuilderGeheimWoord.ToString())
            {
                aantalLevens = aantalLevens - 1;
                OutputTextBlock.Text = $"{geradenWoord} is niet het woord dat we zochten, maar geef niet op je hebt nog {aantalLevens} levens.";
                timer.Stop();
            }
            
        }

        /// <summary>
        /// hier catchen we op de FormatException indien er woorden worden geraden in plaats van letters
        /// </summary>
        private void IsHetGeradenWoordofLetterJuist()
        {
            try
            {
                IsGeradenLetterJuist();                
            }            
            catch (FormatException)
            {
                IsHetGeradenWoordJuist();
            }
            if (woordInAsteriksAlsString == stringBuilderGeheimWoord.ToString())
            {
                OutputTextBlock.Text = $"Joepie het antwoord is inderdaad: {stringBuilderGeheimWoord.ToString()}";
                timer.Stop();
            }
        }

        private void TimerTellerOpNul()
        {
            StringBuilderOutput();

            if (secondenAftellen == 0)
            {
                aantalLevens--;
                secondenAftellen = Convert.ToInt32(secondeningeven.Text) + 1;
            }
            GetImage();
            StringBuilderOutput();
        }

        private void TellerVeranderdVanKleurBijLageWaarde()
        {
            if (secondenAftellen <= 3)
            {
                seconden.Foreground = Brushes.Red;
            }
            else
            {
                seconden.Foreground = Brushes.Black;
            }
        }

        private void GetImage()
        {
            switch (aantalLevens)
            {
                case 10: galg.Source = new BitmapImage(new Uri("/images/galg/hangman part10.png", UriKind.Relative));
                    break;
                case 9: galg.Source = new BitmapImage(new Uri("/images/galg/hangman part1.png", UriKind.Relative));
                    break;
                case 8: galg.Source = new BitmapImage(new Uri("/images/galg/hangman part2.png", UriKind.Relative));
                    break;
                case 7: galg.Source = new BitmapImage(new Uri("/images/galg/hangman part3.png", UriKind.Relative));
                    break;
                case 6: galg.Source = new BitmapImage(new Uri("/images/galg/hangman part4.png", UriKind.Relative));
                    break;
                case 5: galg.Source = new BitmapImage(new Uri("/images/galg/hangman part5.png", UriKind.Relative));
                    break;
                case 4: galg.Source = new BitmapImage(new Uri("/images/galg/hangman part6.png", UriKind.Relative));
                    break;
                case 3: galg.Source = new BitmapImage(new Uri("/images/galg/hangman part7.png", UriKind.Relative));
                    break;
                case 2: galg.Source = new BitmapImage(new Uri("/images/galg/hangman part8.png", UriKind.Relative));
                    break;
                case 1: galg.Source = new BitmapImage(new Uri("/images/galg/hangman part9.png", UriKind.Relative));
                    break;
                default: galg.Source = new BitmapImage(new Uri("/images/galg/hangman part10.png", UriKind.Relative));
                    break;
            }
        }    
        private void RandomWoordGenerator()
        {
            int randomWoord = randomWoordGenerator.Next(galgjeWoorden.Length);
            stringBuilderGeheimWoord.Append(galgjeWoorden[randomWoord]);
        }
        private void IngegevenSeconden()
        {
            secondenAftellen = Convert.ToInt32(secondeningeven.Text);
        }

       
    }
}
