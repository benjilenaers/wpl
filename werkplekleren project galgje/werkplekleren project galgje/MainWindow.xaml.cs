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
using System.Windows.Threading;
using Microsoft.VisualBasic;

namespace werkplekleren_project_galgje
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 


    public partial class MainWindow : Window
    {
        #region declaratie
        private StringBuilder stringBuilderOutputTekst;
        private StringBuilder stringBuilderGeheimWoord;
        private StringBuilder stringBuilderJuisteLetters;
        private StringBuilder stringBuilderFouteLetter;
        private StringBuilder stringBuilderTopscore;
        private StringBuilder stringBuilderTopscoreOpvragen;
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
        string naamSpeler;
        string naamHighscore;
        string highScoreString;
        List<string> highScoreList;
        List<char> hintLetter;
        Random randomHintLetterGenerator = new Random();
        char randomHintLetter;
        #endregion
        public MainWindow()
        {
            InitializeComponent();
            stringBuilderOutputTekst = new StringBuilder();
            stringBuilderGeheimWoord = new StringBuilder();
            stringBuilderJuisteLetters = new StringBuilder();
            stringBuilderFouteLetter = new StringBuilder();
            stringBuilderTopscore = new StringBuilder();
            stringBuilderTopscoreOpvragen = new StringBuilder();
            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = new TimeSpan(0, 0, 1);
            highScoreList = new List<string>();
            hintLetter = new List<char> { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };
        }
        #region knoppen
        private void VerbergWoordButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            StartSpel();
            timer.Start();
            Hintmenu.IsEnabled = true;
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
            secondenAftellen = Convert.ToInt32(secondenIngeven.Text);
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
        private void ImageCross_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Close();
        }
        private void ImageCross_MouseEnter(object sender, MouseEventArgs e)
        {
            Cursor = Cursors.Hand;
        }

        private void ImageCross_MouseLeave(object sender, MouseEventArgs e)
        {
            Cursor = Cursors.Arrow;
        }
        private void speler_MouseDown(object sender, MouseButtonEventArgs e)
        {
            VWBX_naamTextBox.Visibility = Visibility.Visible;
            VWBX_naamTextBlock.Visibility = Visibility.Visible;
            VWBX_StartKnop.Visibility = Visibility.Visible;
            VWBX_startImage.Visibility = Visibility.Collapsed;
            VWBX_speler.Visibility = Visibility.Collapsed;
            VWBX_spelers.Visibility = Visibility.Collapsed;
            VWBX_InfoNaam.Visibility = Visibility.Visible;
            VWBX_Secondeningeven.Visibility = Visibility.Visible;
            infoTijd.Visibility = Visibility.Visible;
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
        private void spelers_MouseDown(object sender, MouseButtonEventArgs e)
        {
            VWBX_naamTextBox.Visibility = Visibility.Visible;
            VWBX_naamTextBlock.Visibility = Visibility.Visible;
            VWBX_StartKnop2.Visibility = Visibility.Visible;
            VWBX_speler.Visibility = Visibility.Collapsed;
            VWBX_spelers.Visibility = Visibility.Collapsed;
            VWBX_startImage.Visibility = Visibility.Collapsed;
            VWBX_InfoNaam.Visibility = Visibility.Visible;
            VWBX_Secondeningeven.Visibility = Visibility.Visible;
            infoTijd.Visibility = Visibility.Visible;
        }

        private void spelers_MouseEnter(object sender, MouseEventArgs e)
        {
            Cursor = Cursors.Hand;
            spelers.Background = Brushes.Black;
            spelers.Foreground = Brushes.Maroon;
            spelers.BorderBrush = Brushes.Red;
        }

        private void spelers_MouseLeave(object sender, MouseEventArgs e)
        {
            Cursor = Cursors.Arrow;
            spelers.Background = Brushes.Maroon;
            spelers.Foreground = Brushes.Black;
            spelers.BorderBrush = Brushes.Black;
        }
        private void Startknop_MouseDown(object sender, MouseButtonEventArgs e)
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
            VWBX_startImage.Visibility = Visibility.Collapsed;
            VWBX_seconden.Visibility = Visibility.Visible;
            VWBX_naamTextBox.Visibility = Visibility.Collapsed;
            VWBX_naamTextBlock.Visibility = Visibility.Collapsed;
            VWBX_StartKnop.Visibility = Visibility.Collapsed;
            VWBX_InfoNaam.Visibility = Visibility.Collapsed;
            naamSpeler = naamInput.Text;
            TimerMenu.IsEnabled = false;
            Hintmenu.IsEnabled = true;
        }

        private void Startknop_MouseEnter(object sender, MouseEventArgs e)
        {
            Cursor = Cursors.Hand;
            Startknop.Background = Brushes.Black;
            Startknop.Foreground = Brushes.Maroon;
            Startknop.BorderBrush = Brushes.Red;
        }

        private void Startknop_MouseLeave(object sender, MouseEventArgs e)
        {
            Cursor = Cursors.Arrow;
            Startknop.Background = Brushes.Maroon;
            Startknop.Foreground = Brushes.Black;
            Startknop.BorderBrush = Brushes.Black;
        }
        private void Startknop2_MouseDown(object sender, MouseButtonEventArgs e)
        {
            infoTijd.Visibility = Visibility.Collapsed;
            VWBX_Image.Visibility = Visibility.Visible;
            VWBX_NieuwSpel.Visibility = Visibility.Visible;
            VWBX_Output.Visibility = Visibility.Visible;
            VWBX_Input.Visibility = Visibility.Visible;
            VWBX_speler.Visibility = Visibility.Collapsed;
            VWBX_spelers.Visibility = Visibility.Collapsed;
            VWBX_Secondeningeven.Visibility = Visibility.Collapsed;
            VWBX_startImage.Visibility = Visibility.Collapsed;
            VWBX_seconden.Visibility = Visibility.Visible;
            VWBX_naamTextBox.Visibility = Visibility.Collapsed;
            VWBX_naamTextBlock.Visibility = Visibility.Collapsed;
            VWBX_StartKnop.Visibility = Visibility.Collapsed;
            VWBX_InfoNaam.Visibility = Visibility.Collapsed;
            VWBX_VerbergWoord.Visibility = Visibility.Visible;
            VWBX_StartKnop2.Visibility = Visibility.Collapsed;
            naamSpeler = naamInput.Text;
            TimerMenu.IsEnabled = false;
        }

        private void Startknop2_MouseEnter(object sender, MouseEventArgs e)
        {
            Cursor = Cursors.Hand;
            Startknop2.Background = Brushes.Black;
            Startknop2.Foreground = Brushes.Maroon;
            Startknop2.BorderBrush = Brushes.Red;
        }

        private void Startknop2_MouseLeave(object sender, MouseEventArgs e)
        {
            Cursor = Cursors.Arrow;
            Startknop2.Background = Brushes.Maroon;
            Startknop2.Foreground = Brushes.Black;
            Startknop2.BorderBrush = Brushes.Black;
        }
        private void highScoreImage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MessageBoxResult HighscoresIedereen = MessageBox.Show(stringBuilderTopscore.ToString(), "Highscores", MessageBoxButton.OK);
        }

        private void highScoreImage_MouseEnter(object sender, MouseEventArgs e)
        {
            Cursor = Cursors.Hand;
        }

        private void highScoreImage_MouseLeave(object sender, MouseEventArgs e)
        {
            Cursor = Cursors.Arrow;
        }
        private void ok_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DynamischeTimer();
            VWBX_Secondeningeven.Visibility = Visibility.Collapsed;
            VWBX_OK.Visibility = Visibility.Collapsed;
        }

        private void ok_MouseEnter(object sender, MouseEventArgs e)
        {
            Cursor = Cursors.Hand;
            ok.Background = Brushes.Black;
            ok.Foreground = Brushes.Maroon;
            ok.BorderBrush = Brushes.Red;
        }

        private void ok_MouseLeave(object sender, MouseEventArgs e)
        {
            Cursor = Cursors.Arrow;
            ok.Background = Brushes.Maroon;
            ok.Foreground = Brushes.Black;
            ok.BorderBrush = Brushes.Black;
        }
        #endregion
        #region MenuKnoppen
        private void AfsluitenMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void NieuwspelMenu_Click(object sender, RoutedEventArgs e)
        {
            AllesVoorEenNieuwSpel();
            VWBX_StartKnop2.Visibility = Visibility.Collapsed;
            VWBX_StartKnop.Visibility = Visibility.Collapsed;
        }        
        private void TimerMenu_Click(object sender, RoutedEventArgs e)
        {
            VWBX_Secondeningeven.Visibility = Visibility.Visible;
            VWBX_OK.Visibility = Visibility.Visible;
        }
        private void Hintmenu_Click(object sender, RoutedEventArgs e)
        {
            HintMethode();
            MessageBoxResult hintMessageBox = MessageBox.Show("Deze letter staat NIET in het woord:" + randomHintLetter.ToString(), "Hint", MessageBoxButton.OK);
            Hintmenu.IsChecked = true;
        }
        #endregion
        #region keydown
        private void NaamHighscoreInputBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                HighScoresOpvragen();
            }
        }
        #endregion
        #region Methodes  
        #region spel
        /// <summary>
        /// <code>
        /// In deze methode word alles opgestart zoals de dynamische timer, het geheime woord zal aangemaakt worden in asterix tekens de raad knop wordt zichtbaar
        /// en de verbergwoord collapsed
        /// en de uitputStringbuilder wordt zichtbaar in de outputTextBox
        /// </code>
        /// </summary>
        private void StartSpel()
        {
            DynamischeTimer();
            stringBuilderGeheimWoord.Append(InputTextBox.Text);
            AanmakenWoordInAsterix();
            InputTextBox.Text = string.Empty;
            VWBX_RaadButton.Visibility = Visibility.Visible;
            VWBX_VerbergWoord.Visibility = Visibility.Collapsed;
            StringBuilderOutput();

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
        /// alle stringBuilders worden gereset behalve die voor de highscore
        /// layout word ook terug gereset naar het begin stadium
        /// ook de List word terug gereset
        /// </code>
        /// </summary>
        private void AllesVoorEenNieuwSpel()
        {
            InputTextBox.Text = "";
            OutputTextBlock.Text = "Geheim woord ingeven:";
            stringBuilderGeheimWoord.Clear();
            stringBuilderOutputTekst.Clear();
            stringBuilderJuisteLetters.Clear();
            stringBuilderFouteLetter.Clear();            
            VWBX_RaadButton.Visibility = Visibility.Collapsed;
            aantalLevens = 10;                     
            secondenIngeven.Text = "10";
            seconden.Text = secondenIngeven.Text;
            timer.Stop();
            RaadButton.IsEnabled = true;
            galg.Source = new BitmapImage(new Uri("/images/galg/hangman part10.png", UriKind.Relative));
            VWBX_Image.Visibility = Visibility.Collapsed;
            VWBX_NieuwSpel.Visibility = Visibility.Collapsed;
            VWBX_RaadButton.Visibility = Visibility.Collapsed;
            VWBX_Output.Visibility = Visibility.Collapsed;
            VWBX_Input.Visibility = Visibility.Collapsed;
            VWBX_speler.Visibility = Visibility.Visible;
            VWBX_spelers.Visibility = Visibility.Visible;
            VWBX_seconden.Visibility = Visibility.Collapsed;
            VWBX_VerbergWoord.Visibility = Visibility.Collapsed;
            VWBX_startImage.Visibility = Visibility.Visible;
            TimerMenu.IsEnabled = true;
            VWBX_Secondeningeven.Visibility = Visibility.Collapsed;
            VWBX_OK.Visibility = Visibility.Collapsed;
            hintLetter = new List<char> { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };
            Hintmenu.IsEnabled = false;
            Hintmenu.IsChecked = false;
        }
        #endregion
        #region timer
        private void timer_Tick(object sender, EventArgs e)
        {
            secondenAftellen--;
            seconden.Text = secondenAftellen.ToString();
            TimerTellerOpNul();
            TellerVeranderdVanKleurBijLageWaarde();
        }
        /// <summary>
        /// Wanneer de teller op 0 staat zal er een leven afgetrokken worden de achtergrond zal ook rood kleuren en de timer zal resetten
        /// </summary>
        private void TimerTellerOpNul()
        {
            StringBuilderOutput();

            if (secondenAftellen == 0)
            {
                aantalLevens--;
                secondenAftellen = Convert.ToInt32(secondenIngeven.Text) + 1;
                Background.Background = Brushes.Red;
            }
            else
            {
                //https://stackoverflow.com/questions/34562677/how-do-i-add-a-hex-code-to-brushes-on-c-sharp-wpf gevonden op deze site
                Background.Background = (Brush)(new BrushConverter().ConvertFrom("#222222"));
            }
            GetImage();
            StringBuilderOutput();
        }
        /// <summary>
        /// wanneer de timer aan het getal 3 of lager zit zal deze rood kleuren
        /// </summary>
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
        /// <summary>
        /// hier word gekeken of de waarde die word ingegeven in de timer textbox ook effectief een getal is en of deze van 5 tem 20 is ander defaultwaarde 10
        /// </summary>
        private void DynamischeTimer()
        {

            try
            {
                if (Convert.ToInt64(secondenIngeven.Text) > 20)
                {
                    throw new Exception(
                    "Het ingegeven getal is groter dan 20 en wordt terug op 10 gezet");
                }
                if (Convert.ToInt64(secondenIngeven.Text) < 5)
                {
                    throw new Exception(
                    "Het ingegeven getal is kleiner dan 5 en wordt terug op 10 gezet");
                }
                secondenAftellen = Convert.ToInt32(secondenIngeven.Text);
                seconden.Text = Convert.ToString(secondenAftellen);
            }
            catch (FormatException exception)
            {
                MessageBox.Show($"Er is iets fout gelopen: {exception.Message}", "Dit was geen getal", MessageBoxButton.OK, MessageBoxImage.Error);
                secondenAftellen = 10;

            }
            catch (Exception exception)
            {
                MessageBox.Show($"De input was niet correct: {exception.Message}", "Dit getal is te groot of te klein", MessageBoxButton.OK, MessageBoxImage.Warning);
                secondenAftellen = 10;
            }
        }
        #endregion       
        #region Woord
        /// <summary>
        /// Hier kijken we of de geraden letter in het woord staat. Bij zowel ja als neen word de stringBuilder aangepast en gaat het aantal levens met 1 omlaag.
        /// </summary>
        private void BoolAanmakenOmTeKijkenOfLetterInGeheimWoordStaat()
        {
            string woordConverrsieNaarString = stringBuilderGeheimWoord.ToString();
            geradenLetter = Convert.ToChar(InputTextBox.Text);
            geraden = woordConverrsieNaarString.Contains(geradenLetter);

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
        private void RandomWoordGenerator()
        {
            int randomWoord = randomWoordGenerator.Next(galgjeWoorden.Length);
            stringBuilderGeheimWoord.Append(galgjeWoorden[randomWoord]);
        }

        #endregion
        #region raden
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
                WanneerHintGeenHighScore();
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
                WanneerHintGeenHighScore();
            }
        }
        #endregion
        #region highscore
        private void ScoreBijhoudenSpeler()
        {
            stringBuilderTopscore.AppendLine($"{naamSpeler} - {aantalLevens} levens ({DateTime.Now.ToLongTimeString()})");
            highScoreString = ($"{naamSpeler} - {aantalLevens} levens ({DateTime.Now.ToLongTimeString()})");
            highScoreList.Add(highScoreString);
        }
        private void HighScoresOpvragen()
        {
            naamHighscore = NaamHighscoreInputBox.Text;

            for (int i = 0; i < highScoreList.Count; i++)
            {
                if (highScoreList[i].Contains(naamHighscore))
                {
                    stringBuilderTopscoreOpvragen.AppendLine(highScoreList[i]);
                }
            }

            MessageBoxResult highScoresOpvragenMessageBox = MessageBox.Show(stringBuilderTopscoreOpvragen.ToString(), "Highscores", MessageBoxButton.OK);

            if (highScoresOpvragenMessageBox == MessageBoxResult.OK)
            {
                stringBuilderTopscoreOpvragen.Clear();
            }
        }
        #endregion
        #region hint
        private void HintMethode()
        {
            string woordConverrsieNaarString = stringBuilderGeheimWoord.ToString();

            for (int i = 0; i < stringBuilderGeheimWoord.Length; i++)
            {
                for (int j = 0; j < hintLetter.Count; j++)
                {
                    if (woordConverrsieNaarString.Contains(hintLetter[j]))
                    {
                        hintLetter.RemoveAt(j);
                    };
                }


            }
            int randomHintLetterIndex = randomHintLetterGenerator.Next(hintLetter.Count);
            randomHintLetter = hintLetter[randomHintLetterIndex];
        }

        private void WanneerHintGeenHighScore()
        {
            if (Hintmenu.IsChecked == false)
            {
                ScoreBijhoudenSpeler();
            }
        }
        #endregion
        #region image
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
        #endregion
        #endregion
    }
}
