using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Twerkle.AppLogic
{
    public class AppLogicHandler
    {
        public string wordToGuess;

        public List<string> blackListLetters { get; set; } = new List<string>();


        public Dictionary<int, string> wordToGuessDict { get; set; } = new Dictionary<int, string>();

        public Dictionary<int, string> correctLetterNotPosition { get; set; } = new Dictionary<int, string>();
        public Dictionary<int, string> correctLetterAndPosition { get; set; } = new Dictionary<int, string>();
        public bool winCondition = false;

        public AppLogicHandler(string wordToGuess)
        {
            this.wordToGuess = wordToGuess;
            int i = 0;
            foreach (var l in wordToGuess.ToCharArray())
            {
                wordToGuessDict.Add(i, l.ToString());
                i++;
            }
        }
        public void InitializeGame()
        {
            
        }

        public void CheckAttempt(string attempt)
        {
            List<string> wordToGuessSplit = Regex.Split(wordToGuess, string.Empty).ToList();
            wordToGuessSplit = wordToGuessSplit.Where(s => !string.IsNullOrWhiteSpace(s)).ToList();

            List<string> attemptSplit = Regex.Split(attempt, string.Empty).ToList();
            attemptSplit = attemptSplit.Where(s => !string.IsNullOrWhiteSpace(s)).ToList();

            correctLetterNotPosition.Clear();




            //1 guessed letter at right position, 1 guessed letter at wrong position, 3 letters blacklisted.
            //var attemptSplit = attempt.ToCharArray();

            if (wordToGuess == attempt)
            {
                //word was guessed, Win.
                winCondition = true;
            }

            blackListLetters.AddRange(attemptSplit);
            blackListLetters = blackListLetters.Distinct().ToList();

            
            for (int i = 0; i < wordToGuess.Length; i++)
            {
                var letterToGuess = wordToGuessSplit[i];
                var letterOfAttempt = attemptSplit[i];

                if (letterToGuess == letterOfAttempt && !correctLetterAndPosition.ContainsKey(i))
                {
                    correctLetterAndPosition.Add(i, letterToGuess.ToString());
                    blackListLetters.Remove(letterOfAttempt);
                }
            }

            var wordToGuessCopy = string.Empty;
            wordToGuessCopy = wordToGuess;
            var Notmatching = wordToGuessDict.Except(correctLetterAndPosition).ToList();
            wordToGuess = string.Join("",Notmatching.Select(x => x.Value).ToList());

            for (int i = 0; i < attempt.Length; i++)
            {
                


                if (wordToGuess.Contains(attempt[i]))
                {
                    //letter exists in word to check.

                    //does index and position already exist in good list
                    var correctGuessExists = correctLetterAndPosition.FirstOrDefault(x => x.Key == i && x.Value == attempt[i].ToString());

                    if (correctGuessExists.Value is null)
                    {
                        //dont add it if it the letter is already captured in correctLetterAndPosition
                        correctLetterNotPosition.Add(i, attempt[i].ToString());
                        blackListLetters.Remove(attempt[i].ToString());
                    }

                }
            }

            wordToGuess = wordToGuessCopy;

        }
    }
}
