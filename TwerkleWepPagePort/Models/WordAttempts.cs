using System.ComponentModel.DataAnnotations;

namespace TwerkleWepPagePort.Models
{
    public class WordAttempts
    {
        public List<Attempt> Attempts = new List<Attempt>();

        public readonly int TotalAttempts = 5;
        public int CurrentAttempt = 1;
        public string WordOfDay = "";

        public List<KeysUI> keyboardUI = new List<KeysUI>();

        public List<LetterAndPosition> rightLetterAndPosition { get; set; } = new List<LetterAndPosition>();
        public List<LetterAndPosition> rightLetterNotPosition { get; set; } = new List<LetterAndPosition>();

        public List<string> blackListKeys = new List<string>();
        public List<string> greenListKeys = new List<string>();
        public List<string> yellowListKeys = new List<string>();



        public WordAttempts()
        {
            InitializeGame();
            KeysUI keysUI = new KeysUI();
            keyboardUI = keysUI.BuildKeyBoard();
        }

        public void InitializeGame()
        {
            for (int i = 0; i < TotalAttempts; i++)
            {
                Attempts.Add(new Attempt());
                
            }
        }
    }

    public class LetterAndPosition
    {
        public string Letter { get; set; }
        public int Position { get; set; }
        public string Color { get; set; }
    }
    public class KeysUI
    {
        public string Letter { get; set; }
        public string Color { get; set; }
        public int Sort { get; set; }

        public List<KeysUI> BuildKeyBoard()
        {
            List<KeysUI> keys = new List<KeysUI>();
            keys.Add(new KeysUI()
            {
                Letter = "Q",
                Sort = 1
            }) ;
            keys.Add(new KeysUI()
            {
                Letter = "W",
                Sort = 1
            });
            keys.Add(new KeysUI()
            {
                Letter = "E",
                Sort = 1
            });
            keys.Add(new KeysUI()
            {
                Letter = "R",
                Sort = 1
            });
            keys.Add(new KeysUI()
            {
                Letter = "T",
                Sort = 1
            });
            keys.Add(new KeysUI()
            {
                Letter = "Y",
                Sort = 1
            });
            keys.Add(new KeysUI()
            {
                Letter = "U",
                Sort = 1
            });
            keys.Add(new KeysUI()
            {
                Letter = "I",
                Sort = 1
            });
            keys.Add(new KeysUI()
            {
                Letter = "O",
                Sort = 1
            });
            keys.Add(new KeysUI()
            {
                Letter = "P",
                Sort = 1
            });

            keys.Add(new KeysUI()
            {
                Letter = "A",
                Sort = 1
            });
            keys.Add(new KeysUI()
            {
                Letter = "S",
                Sort = 1
            });
            keys.Add(new KeysUI()
            {
                Letter = "D",
                Sort = 1
            });
            keys.Add(new KeysUI()
            {
                Letter = "P",
                Sort = 1
            });
            keys.Add(new KeysUI()
            {
                Letter = "F",
                Sort = 1
            });
            keys.Add(new KeysUI()
            {
                Letter = "G",
                Sort = 1
            });
            keys.Add(new KeysUI()
            {
                Letter = "H",
                Sort = 1
            });
            keys.Add(new KeysUI()
            {
                Letter = "J",
                Sort = 1
            });
            keys.Add(new KeysUI()
            {
                Letter = "K",
                Sort = 1
            });
            keys.Add(new KeysUI()
            {
                Letter = "L",
                Sort = 1
            });
            keys.Add(new KeysUI()
            {
                Letter = "Z",
                Sort = 1
            });
            keys.Add(new KeysUI()
            {
                Letter = "X",
                Sort = 1
            });
            keys.Add(new KeysUI()
            {
                Letter = "C",
                Sort = 1
            });
            keys.Add(new KeysUI()
            {
                Letter = "V",
                Sort = 1
            });
            keys.Add(new KeysUI()
            {
                Letter = "B",
                Sort = 1
            });
            keys.Add(new KeysUI()
            {
                Letter = "N",
                Sort = 1
            });
            keys.Add(new KeysUI()
            {
                Letter = "M",
                Sort = 1
            });



            return keys;
        }
    }

    public class Attempt
    {
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use letters only please")]

        public string Letter1 { get; set; }
        public string Letter1Color { get; set; } = "";

        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use letters only please")]

        public string Letter2 { get; set; }
        public string Letter2Color { get; set; } = "";

        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use letters only please")]

        public string Letter3 { get; set; }
        public string Letter3Color { get; set; } = "";

        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use letters only please")]

        public string Letter4 { get; set; }
        public string Letter4Color { get; set; } = "";

        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use letters only please")]

        public string Letter5 { get; set; }
        public string Letter5Color { get; set; } = "";
    }
}
