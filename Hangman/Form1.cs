using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hangman
{
    public partial class Form1 : Form
    {
        private string[] _dictionary;
        private const int _maxAllowedIncorrectGuesses = 6;
        private string _wordToGuess;
        private List<char> _guesses = new List<char>();
        private List<char> _incorrectGuesses = new List<char>();
        private bool _gameStarted = false;

        public Form1()
        {
            // initialize game
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            PopulateDictionary();
        }

        private void btnLetter_Click(object sender, EventArgs e)
        {
            if (!_gameStarted)
                return;

            Button clickedButton = sender as Button;
            if (clickedButton == null)
                return;

            //Blacks out selected letter
            clickedButton.BackColor = Color.Black;
            //Getting the letter just guessed
            char guessedLetter = Convert.ToChar(clickedButton.Text.ToLower());
            if (_guesses.Contains(guessedLetter))
                return;

            _guesses.Add(guessedLetter);

            UpdateIncorrectGuesses(guessedLetter);

            UpdateGuessedWordDisplay();

            if (CheckForLoss())
            {
                return;
            }

            CheckForWin();
        }

        private void btnNewGame_Click(object sender, EventArgs e)
        {
            //Reset counter txtGuesesLeft
            txtGuessesLeft.Text = Convert.ToString(_maxAllowedIncorrectGuesses);
            //Clear out txtIncorrectLetters
            txtIncorrectLetters.Clear();
            //Clear txtWordToGuess
            txtWordToGuess.Clear();
            //Clear guess lists
            _guesses.Clear();
            _incorrectGuesses.Clear();
            //Reset letters back to pink
            Color selectedColor = Color.MediumVioletRed;
            btnA.BackColor = selectedColor;
            btnB.BackColor = selectedColor;
            btnC.BackColor = selectedColor;
            btnD.BackColor = selectedColor;
            btnE.BackColor = selectedColor;
            btnF.BackColor = selectedColor;
            btnG.BackColor = selectedColor;
            btnH.BackColor = selectedColor;
            btnI.BackColor = selectedColor;
            btnJ.BackColor = selectedColor;
            btnK.BackColor = selectedColor;
            btnL.BackColor = selectedColor;
            btnM.BackColor = selectedColor;
            btnN.BackColor = selectedColor;
            btnO.BackColor = selectedColor;
            btnP.BackColor = selectedColor;
            btnQ.BackColor = selectedColor;
            btnR.BackColor = selectedColor;
            btnS.BackColor = selectedColor;
            btnT.BackColor = selectedColor;
            btnU.BackColor = selectedColor;
            btnV.BackColor = selectedColor;
            btnW.BackColor = selectedColor;
            btnX.BackColor = selectedColor;
            btnY.BackColor = selectedColor;
            btnZ.BackColor = selectedColor;

            UpdateHangman();
            GetRandomWord();
            _gameStarted = true;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private bool CheckForLoss()
        {
            //Check for Loss
            if (_incorrectGuesses.Count >= _maxAllowedIncorrectGuesses)
            {
                //Display message box that says Sorry! You Lose. 
                MessageBox.Show("Sorry! You Lose.\nThe word was: " + _wordToGuess);
                _gameStarted = false;
                return true;
            }
            return false;
        }

        private void CheckForWin()
        {
            //Check for win
            if (!txtWordToGuess.Text.Contains("*"))
            {
                //Display message box that says Congratulations! You Win. 
                MessageBox.Show("Congratulations!  You Win.");
                _gameStarted = false;
            }
        }

        private void GetRandomWord()
        {
            //Pick new word from dictionary
            Random random = new Random();
            int maxIndex = _dictionary.Length;
            int randomIndex = random.Next(0, maxIndex);
            _wordToGuess = _dictionary[randomIndex];
            //Make letters not visible, but have _ in place of each letter
            txtWordToGuess.Text = new string('*', _wordToGuess.Length);
        }

        private void PopulateDictionary()
        {
            try
            {
                _dictionary = File.ReadAllLines("WordsDictionary.txt");
            }
            catch (Exception)
            {
                MessageBox.Show("Unable to find WordsDictionary.txt file.", "Critical Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        } 

        private void UpdateHangman()
        {
            try
            {
                picHangman.Image = Image.FromFile("IncorrectGuess" + _incorrectGuesses.Count + ".png");
            }
            catch (Exception)
            {
                MessageBox.Show("Unable to find IncorrectGuess" + _incorrectGuesses.Count + ".png file.", "Critical Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateIncorrectGuesses(char guessedLetter)
        {
            
            //If it's not in the word to be guessed, add incorrect letter to the incorrect letter list
            if (!_wordToGuess.Contains(guessedLetter))
            {
                _incorrectGuesses.Add(guessedLetter);
                txtGuessesLeft.Text = (_maxAllowedIncorrectGuesses - _incorrectGuesses.Count).ToString();
                UpdateHangman();
            }

            var sortedIncorrectGuesses = from g in _incorrectGuesses
                                         orderby g
                                         select g;
            txtIncorrectLetters.Clear();
            foreach (char letter in sortedIncorrectGuesses)
            {
                txtIncorrectLetters.Text += letter + " ";
            }
        }

        private void UpdateGuessedWordDisplay()
        {
            txtWordToGuess.Clear();

            //If it is put the letter in the appropriate spot and replace _ in txtWordToGuess 
            for (int i = 0; i < _wordToGuess.Length; i++)
            {
                if (_guesses.Contains(_wordToGuess[i]))
                {
                    txtWordToGuess.Text += _wordToGuess[i];
                }
                else
                {
                    //If not, change incorrect guesses left 
                    //add to the incorrect letters list
                    //change image to next image
                    txtWordToGuess.Text += "*";
                }
            }
        }



        

    }
}
