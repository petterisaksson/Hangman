using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace gemensamHangman
{
    class HighScore
    {

        static string scoreFile = "../../../highscore.txt";

        public static string [] ListScore()
        {
            return File.ReadAllLines(scoreFile);
        }
        public static void AddNewScore(string name, int score)
        {
           string[] oldFile = File.ReadAllLines(scoreFile);
           string[] newFile = new String[oldFile.Length + 1];
           string newScore = score + " " + name;

            oldFile.CopyTo(newFile, 0);

           newFile[newFile.Length - 1] = newScore;

            File.WriteAllLines(scoreFile, newFile);
        }
    }
}
