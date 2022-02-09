using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Twerkle.AppLogic;
using TwerkleWepPagePort.Models;
using TwerkleWepPagePort.Services;

namespace TwerkleWepPagePort.Pages
{
    [BindProperties]
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly ITwerkleService _twerkleService;

        public bool showError = false;

        public bool isWinner = false;
        public bool loseGame = false;

        public WordAttempts wordAttempts { get; set; } = new WordAttempts();

        public IndexModel(ILogger<IndexModel> logger, ITwerkleService twerkleService)
        {
            _logger = logger;
            _twerkleService = twerkleService;
        }


        public void OnGet()
        {
            wordAttempts.WordOfDay = _twerkleService.GetWordOfDay().Result;
        }

        [HttpPost]
        public IActionResult OnPostGuess()
        {
            BindModel();
            
            var guess = wordAttempts.Attempts[wordAttempts.CurrentAttempt -1].Letter1 +
                wordAttempts.Attempts[wordAttempts.CurrentAttempt - 1].Letter2 +
                wordAttempts.Attempts[wordAttempts.CurrentAttempt - 1].Letter3 +
                wordAttempts.Attempts[wordAttempts.CurrentAttempt - 1].Letter4 +
                wordAttempts.Attempts[wordAttempts.CurrentAttempt - 1].Letter5;

            AppLogicHandler appLogic = new AppLogicHandler(wordAttempts.WordOfDay.ToUpper());


            if (guess.Length < 5)
            {
                TempData["UserMessage"] = "Please Enter 5 Letters";
            }
            else
            {
                guess = guess.ToUpper();
                appLogic.CheckAttempt(guess);

                if(appLogic.wordToGuess == guess)
                {
                    //win
                    appLogic.winCondition = true;
                    isWinner = true;
                    return Page();
                }
                if(wordAttempts.CurrentAttempt == wordAttempts.TotalAttempts)
                {
                    //lose
                    loseGame = true;
                    return Page();
                }

                foreach (var item in appLogic.correctLetterAndPosition)
                {
                    wordAttempts.rightLetterAndPosition.Add(new LetterAndPosition(){
                        Letter = item.Value,
                        Position = item.Key,
                        Color = "green"
                    });
                }

                foreach (var item in appLogic.correctLetterNotPosition)
                {
                    wordAttempts.rightLetterNotPosition.Add(new LetterAndPosition(){
                        Letter = item.Value,
                        Position = item.Key,
                        Color = "yellow"
                    });
                }
                
            }



            BuildCorrectLetterAndPositionGrid(appLogic);
            BuildCorrectLetterNotPositionGrid(appLogic);

            UpdateCustomKeyboard(appLogic);

            wordAttempts.CurrentAttempt++;

            return Page();
        }

        private void BindModel()
        {
            var dict = Request.Form.ToDictionary(x => x.Key, x => x.Value.ToString());

            int currentAttempt = 0;
            string currentAttemptString;
            string letterString = "";
            dict.TryGetValue("wordAttempts.CurrentAttempt", out currentAttemptString);
            currentAttempt = int.Parse(currentAttemptString);

            wordAttempts.CurrentAttempt = currentAttempt;

            dict.TryGetValue("wordAttempts.WordOfDay", out letterString);
            wordAttempts.WordOfDay = letterString;

            for (int currentAttemptIndex = 0; currentAttemptIndex < currentAttempt; currentAttemptIndex++)
            {
                dict.TryGetValue($"wordAttempts.Attempts[{currentAttemptIndex}].Letter1", out letterString);
                wordAttempts.Attempts[currentAttemptIndex].Letter1 = letterString;

                dict.TryGetValue($"wordAttempts.Attempts[{currentAttemptIndex}].Letter2", out letterString);
                wordAttempts.Attempts[currentAttemptIndex].Letter2 = letterString;

                dict.TryGetValue($"wordAttempts.Attempts[{currentAttemptIndex}].Letter3", out letterString);
                wordAttempts.Attempts[currentAttemptIndex].Letter3 = letterString;

                dict.TryGetValue($"wordAttempts.Attempts[{currentAttemptIndex}].Letter4", out letterString);
                wordAttempts.Attempts[currentAttemptIndex].Letter4 = letterString;

                dict.TryGetValue($"wordAttempts.Attempts[{currentAttemptIndex}].Letter5", out letterString);
                wordAttempts.Attempts[currentAttemptIndex].Letter5 = letterString;

                for (int idx = 0; idx < wordAttempts.rightLetterAndPosition.Count; idx++)
                {
                    dict.TryGetValue($"wordAttempts.rightLetterAndPosition[{idx}].Letter", out letterString);
                    wordAttempts.rightLetterAndPosition[idx].Letter = letterString;

                    dict.TryGetValue($"wordAttempts.rightLetterAndPosition[{idx}].Color", out letterString);
                    wordAttempts.rightLetterAndPosition[idx].Color = letterString;

                    dict.TryGetValue($"wordAttempts.rightLetterAndPosition[{idx}].Position", out letterString);
                    wordAttempts.rightLetterAndPosition[idx].Position = int.Parse(letterString);

                    if (wordAttempts.Attempts[currentAttemptIndex].Letter1 == wordAttempts.rightLetterAndPosition[idx].Letter)
                    {
                        wordAttempts.Attempts[currentAttemptIndex].Letter1Color = "green";
                    }
                    if (wordAttempts.Attempts[currentAttemptIndex].Letter2 == wordAttempts.rightLetterAndPosition[idx].Letter)
                    {
                        wordAttempts.Attempts[currentAttemptIndex].Letter2Color = "green";
                    }
                    if (wordAttempts.Attempts[currentAttemptIndex].Letter3 == wordAttempts.rightLetterAndPosition[idx].Letter)
                    {
                        wordAttempts.Attempts[currentAttemptIndex].Letter3Color = "green";
                    }
                    if (wordAttempts.Attempts[currentAttemptIndex].Letter4 == wordAttempts.rightLetterAndPosition[idx].Letter)
                    {
                        wordAttempts.Attempts[currentAttemptIndex].Letter4Color = "green";
                    }
                    if (wordAttempts.Attempts[currentAttemptIndex].Letter5 == wordAttempts.rightLetterAndPosition[idx].Letter)
                    {
                        wordAttempts.Attempts[currentAttemptIndex].Letter5Color = "green";
                    }
                }

                for (int idx = 0; idx < wordAttempts.rightLetterNotPosition.Count; idx++)
                {
                    dict.TryGetValue($"wordAttempts.rightLetterNotPosition[{idx}].Letter", out letterString);
                    wordAttempts.rightLetterNotPosition[idx].Letter = letterString;

                    dict.TryGetValue($"wordAttempts.rightLetterNotPosition[{idx}].Color", out letterString);
                    wordAttempts.rightLetterNotPosition[idx].Color = letterString;

                    dict.TryGetValue($"wordAttempts.rightLetterNotPosition[{idx}].Position", out letterString);
                    wordAttempts.rightLetterNotPosition[idx].Position = int.Parse(letterString);

                    if (wordAttempts.Attempts[currentAttemptIndex].Letter1 == wordAttempts.rightLetterNotPosition[idx].Letter)
                    {
                        wordAttempts.Attempts[currentAttemptIndex].Letter1Color = "yellow";
                    }
                    if (wordAttempts.Attempts[currentAttemptIndex].Letter2 == wordAttempts.rightLetterNotPosition[idx].Letter)
                    {
                        wordAttempts.Attempts[currentAttemptIndex].Letter2Color = "yellow";
                    }
                    if (wordAttempts.Attempts[currentAttemptIndex].Letter3 == wordAttempts.rightLetterNotPosition[idx].Letter)
                    {
                        wordAttempts.Attempts[currentAttemptIndex].Letter3Color = "yellow";
                    }
                    if (wordAttempts.Attempts[currentAttemptIndex].Letter4 == wordAttempts.rightLetterNotPosition[idx].Letter)
                    {
                        wordAttempts.Attempts[currentAttemptIndex].Letter4Color = "yellow";
                    }
                    if (wordAttempts.Attempts[currentAttemptIndex].Letter5 == wordAttempts.rightLetterNotPosition[idx].Letter)
                    {
                        wordAttempts.Attempts[currentAttemptIndex].Letter5Color = "yellow";
                    }
                }
            }


            ReBindKeyboard(dict);
           

        }

        private void ReBindKeyboard(Dictionary<string, string> dict)
        {
            string letterString;
            var keysCount = dict.Keys.Where(x => x.Contains("keyboardUI")).Count() / 2;
            for (int i = 0; i < keysCount; i++)
            {
                dict.TryGetValue($"wordAttempts.keyboardUI[{i}].Letter", out letterString);
                wordAttempts.keyboardUI[i].Letter = letterString;

                dict.TryGetValue($"wordAttempts.keyboardUI[{i}].Color", out letterString);
                wordAttempts.keyboardUI[i].Color = letterString;
            }
        }

        private void UpdateCustomKeyboard(AppLogicHandler appLogic)
        {
            foreach(var item in appLogic.blackListLetters)
            {
                var keyboardKey = wordAttempts.keyboardUI.FirstOrDefault(x => x.Letter.ToUpper() == item.ToUpper());
                if(keyboardKey != null)
                {
                    keyboardKey.Color = "grey";
                    if (!wordAttempts.blackListKeys.Contains(keyboardKey.Letter))
                    {
                        wordAttempts.blackListKeys.Add(keyboardKey.Letter);
                    }
                }
                
            }

            foreach (var item in appLogic.correctLetterAndPosition)
            {
                var keyboardKey = wordAttempts.keyboardUI.FirstOrDefault(x => x.Letter.ToUpper() == item.Value.ToUpper());
                if (keyboardKey != null)
                {
                    keyboardKey.Color = "green";
                    if (!wordAttempts.greenListKeys.Contains(keyboardKey.Letter))
                    {
                        wordAttempts.greenListKeys.Add(keyboardKey.Letter);
                    }
                }
            }

            foreach (var item in appLogic.correctLetterNotPosition)
            {
                var keyboardKey = wordAttempts.keyboardUI.FirstOrDefault(x => x.Letter.ToUpper() == item.Value.ToUpper());
                if (keyboardKey != null)
                {
                    keyboardKey.Color = "yellow";
                    if (!wordAttempts.yellowListKeys.Contains(keyboardKey.Letter))
                    {
                        wordAttempts.yellowListKeys.Add(keyboardKey.Letter);
                    }
                }
            }
        }

        private void BuildCorrectLetterAndPositionGrid(AppLogicHandler appLogic)
        {
            if (appLogic.correctLetterAndPosition.Count > 0)
            {
                var letter = "";
                //parse all guesses already
                for(int currentAttemptIndex = 0; currentAttemptIndex < wordAttempts.CurrentAttempt; currentAttemptIndex++)
                {
                    appLogic.correctLetterAndPosition.TryGetValue(0, out letter);
                    if (wordAttempts.Attempts[currentAttemptIndex].Letter1 == letter)
                    {
                        wordAttempts.Attempts[currentAttemptIndex].Letter1Color = "green";
                    }

                    appLogic.correctLetterAndPosition.TryGetValue(1, out letter);
                    if (wordAttempts.Attempts[currentAttemptIndex].Letter2 == letter)
                    {
                        wordAttempts.Attempts[currentAttemptIndex].Letter2Color = "green";
                    }

                    appLogic.correctLetterAndPosition.TryGetValue(2, out letter);
                    if (wordAttempts.Attempts[currentAttemptIndex].Letter3 == letter)
                    {
                        wordAttempts.Attempts[currentAttemptIndex].Letter3Color = "green";
                    }

                    appLogic.correctLetterAndPosition.TryGetValue(3, out letter);
                    if (wordAttempts.Attempts[currentAttemptIndex].Letter4 == letter)
                    {
                        wordAttempts.Attempts[currentAttemptIndex].Letter4Color = "green";
                    }

                    appLogic.correctLetterAndPosition.TryGetValue(4, out letter);
                    if (wordAttempts.Attempts[currentAttemptIndex].Letter5 == letter)
                    {
                        wordAttempts.Attempts[currentAttemptIndex].Letter5Color = "green";
                    }
                }
                
            }
        }
        public void BuildCorrectLetterNotPositionGrid(AppLogicHandler appLogic)
        {
            if (appLogic.correctLetterNotPosition.Count > 0)
            {
                for (int currentAttemptIndex = 0; currentAttemptIndex < wordAttempts.CurrentAttempt; currentAttemptIndex++)
                {
                    var letter = "";
                    appLogic.correctLetterNotPosition.TryGetValue(0, out letter);
                    if (wordAttempts.Attempts[currentAttemptIndex].Letter1 == letter)
                    {
                        wordAttempts.Attempts[currentAttemptIndex].Letter1Color = "yellow";
                    }

                    appLogic.correctLetterNotPosition.TryGetValue(1, out letter);
                    if (wordAttempts.Attempts[currentAttemptIndex].Letter2 == letter)
                    {
                        wordAttempts.Attempts[currentAttemptIndex].Letter2Color = "yellow";
                    }

                    appLogic.correctLetterNotPosition.TryGetValue(2, out letter);
                    if (wordAttempts.Attempts[currentAttemptIndex].Letter3 == letter)
                    {
                        wordAttempts.Attempts[currentAttemptIndex].Letter3Color = "yellow";
                    }

                    appLogic.correctLetterNotPosition.TryGetValue(3, out letter);
                    if (wordAttempts.Attempts[currentAttemptIndex].Letter4 == letter)
                    {
                        wordAttempts.Attempts[currentAttemptIndex].Letter4Color = "yellow";
                    }

                    appLogic.correctLetterNotPosition.TryGetValue(4, out letter);
                    if (wordAttempts.Attempts[currentAttemptIndex].Letter5 == letter)
                    {
                        wordAttempts.Attempts[currentAttemptIndex].Letter5Color = "yellow";
                    }
                }
            }
        }
    }
}