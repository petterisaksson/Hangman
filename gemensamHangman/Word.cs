using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace gemensamHangman
{
    class Word
    {

        string secretWord;
        string shownWord;

        public string SecretWord
        {
            get { return secretWord.ToUpper(); }
            //private set { }
        }
        public string ShownWord
        {
            get { return shownWord.ToUpper(); }
        }
       
        public void SetDifficulty(int difficulty)
        {
            string fileName = "";

            switch (difficulty)
            {
                case 1:
                    fileName = "easy.txt";
                    break;
                case 2:
                    fileName = "medium.txt";
                    break;
                case 3:
                    fileName = "hard.txt";
                    break;
                default:
                    fileName = "easy.txt";
                    break;
            }
            string[] lines = File.ReadAllLines("../../../Ordlista/" + fileName);
            Random random = new Random();
            int pick = random.Next(lines.Length);
            secretWord = lines[pick];

            shownWord = new string('_', secretWord.Length);

        }
        public bool CheckLetter(char tryedLetter)
        {
            char[] letters = shownWord.ToCharArray();
            bool match = false;

            for (int i = 0; i < secretWord.Length; i++)
            {
                if (secretWord[i] == tryedLetter)
                {
                    letters[i] = secretWord[i];
                    match = true;
                }
            }
            string word = "";
            for (int i = 0; i < letters.Length; i++)
            {
                word += letters[i];
            }
            shownWord = word;
            
            return match;
        }
        public bool IsComplete()
        {
            bool isDone = (secretWord == shownWord);
            return isDone;
        }

    }
}
