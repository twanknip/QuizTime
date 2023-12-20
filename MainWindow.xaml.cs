using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using static System.Net.Mime.MediaTypeNames;

namespace QuizTime
{
    public class QuizQuestion
    {
        public string QuestionText { get; set; }
        public string ImagePath { get; set; }
        public List<string> Options { get; set; }
        public char CorrectOption { get; set; }
    }

    public partial class MainWindow : Window
    {
        private int secondsRemaining = 10;
        private DispatcherTimer timer;
        private List<QuizQuestion> quizQuestions;
        private int currentQuestionIndex = 0;

        public MainWindow()
        {
            InitializeComponent();
            InitializeTimer();
            LoadQuizQuestions();
            LoadCurrentQuestion();
        }

        private void InitializeTimer()
        {
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
        }

        private void LoadQuizQuestions()
        {
            // Voeg hier je quizvragen toe, inclusief tekst, afbeeldingspad, opties en het juiste antwoord.
            quizQuestions = new List<QuizQuestion>
            {
                new QuizQuestion
                {
                    QuestionText = "Vraag 1: Zet hier vraag in",
                    ImagePath = "NederlandVlag.png",
                    Options = new List<string> { "test", "test1", "test2", "test3" },
                    CorrectOption = 'A'
                },
                // Voeg hier meer vragen toe zoals het bovenstaande voorbeeld
            };
        }

        private void LoadCurrentQuestion()
        {
            if (currentQuestionIndex < quizQuestions.Count)
            {
                QuizQuestion currentQuestion = quizQuestions[currentQuestionIndex];

                // Laad de vraagtekst
                questionTextBlock.Text = currentQuestion.QuestionText;

                // Laad de afbeelding
                image.Source = new BitmapImage(new Uri(currentQuestion.ImagePath, UriKind.Relative));

                // Laad de antwoorden
                for (int i = 0; i < currentQuestion.Options.Count; i++)
                {
                    if (i < answersStackPanel.Children.Count)
                    {
                        if (answersStackPanel.Children[i] is RadioButton radioButton)
                        {
                            radioButton.Content = $"{(char)('A' + i)}. {currentQuestion.Options[i]}";
                            radioButton.IsEnabled = true;
                            radioButton.IsChecked = false;
                        }
                    }
                }

                // Reset de timer
                secondsRemaining = 10;
                UpdateTimerText();
                timer.Start();
            }
            else
            {
                MessageBox.Show("Gefeliciteerd! Je hebt alle vragen beantwoord.");
                // Voeg hier eventuele andere logica toe wanneer alle vragen zijn beantwoord.
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            secondsRemaining--;

            if (secondsRemaining >= 0)
            {
                UpdateTimerText();
            }
            else
            {
                timer.Stop();
                // Vervang MessageBox.Show door aangepast dialoogvenster
                CustomMessageBox customMessageBox = new CustomMessageBox("Tijd is op! Je kunt geen antwoord meer selecteren.", "Waarschuwing");
                customMessageBox.ShowDialog();

                // Schakel de radiobuttons uit na het verstrijken van de tijd
                DisableRadioButtons();

                // Ga door naar de volgende vraag
                currentQuestionIndex++;
                LoadCurrentQuestion();
            }
        }

        private void UpdateTimerText()
        {
            timeTextBlock.Text = $"Tijd: {secondsRemaining}s";
        }

        private void DisableRadioButtons()
        {
            foreach (UIElement element in answersStackPanel.Children)
            {
                if (element is RadioButton radioButton)
                {
                    radioButton.IsEnabled = false;
                }
            }
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            timer.Stop();
            DisableRadioButtons();

            RadioButton selectedRadioButton = (RadioButton)sender;
            char selectedOption = selectedRadioButton.Content.ToString()[0];

            if (selectedOption == quizQuestions[currentQuestionIndex].CorrectOption)
            {
                MessageBox.Show("Goed antwoord!");
                // Voeg hier eventuele andere logica toe voor een correct antwoord.
            }
            else
            {
                MessageBox.Show("Fout antwoord.");
                // Voeg hier eventuele andere logica toe voor een fout antwoord.
            }

            // Ga door naar de volgende vraag
            currentQuestionIndex++;
            LoadCurrentQuestion();
        }

        // Voeg hier andere methoden toe voor het beheren van vragen en antwoorden, bijvoorbeeld om vragen toe te voegen, te bewerken of te verwijderen.

        private void NextQuestionButton_Click(object sender, RoutedEventArgs e)
        {
            // Ga naar de volgende vraag
            currentQuestionIndex++;
            LoadCurrentQuestion();
        }
    }
}
