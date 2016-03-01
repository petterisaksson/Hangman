using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace gemensamHangman
{
    
    class Program
    {        
              
        static string usedLetters;               
        static bool gameContinues;
        static Word word;
           
        
        static void Main(string[] args)        
        {            

            bool keepPlaying = true;
            while (keepPlaying)
            {
                //Intro

                Player player = new Player();
                word = new Word();
                ProgramStart();
                player.Name = GetPlayerName();                
                Welcome(player.Name);                            
                DifficultySetup();

                //Spelrundan 
                while (gameContinues)
                {
                    DrawGame(player.Life);
                    char tryedLetter = GuessedLetter();

                    if (word.CheckLetter(tryedLetter) == true)
                    {
                        Console.WriteLine("\n                     BRA! " + tryedLetter + " fanns i det hemliga ordet!");
                        
                    }
                    else
                    {
                        player.Damage();
                        Console.WriteLine("\n                   Tyvärr fanns inte  " + tryedLetter + "  i det hemliga ordet\n\r");
                        
                    }

                    if (word.IsComplete())
                    {

                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("\n\r                              Det hemliga ordet är");
                        Console.ResetColor();
                        Console.WriteLine("                                    [" + word.SecretWord + "]");
                        Console.WriteLine("\n\r                GRATTIS!! du lyckades lista ut det rätta ordet");
                        gameContinues = false;
                    }  
                    if(player.Life <= 0)
                    {
                        string gameOver = File.ReadAllText("../../../gubbe/gameover.txt");
                        Console.WriteLine(gameOver);
                        Console.WriteLine("\n\rDet rätta ordet var:" + word.SecretWord);
                        gameContinues = false;
                    }                                   

                }
                //Spelrundan över, spela igen?
                HighScore.AddNewScore(player.Name, player.Life);
                PrintHighScoreList();
                keepPlaying = AskPlayAgain();
                Console.Clear();
            }
            //Progammet slut            
            GameEnd();            
            Console.ReadLine();
        }
          private static void ProgramStart()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("                                 ~HANGMAN 0.5~");            
            Console.ResetColor();
            gameContinues = true;
            
                    }
        private static string GetPlayerName()
        {                        
            string playerName;
            do
            {
                Console.Write("                     Skriv in ditt namn (minst 3 tecken):\n\r                                 ");
                playerName = Console.ReadLine();
            }
            while (playerName.Length < 3);            
            Console.Clear();
            return playerName;

        }
        private static void Welcome(string name)
        {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("                   Välkommen " + name.ToUpper() + " till HANGMAN-spelet!");
                Console.ResetColor();
                Console.WriteLine("\n\r Detta är ett spel som går ut på att försöka lista ut ett dolt ord. Genom att\n\r välja en bokstav från A - Ö kan man ta reda på om det dolda ordet innehåller\n\r just den bokstaven. För varje fel chansad bokstav ritas en del av av en\n\r hängande gubbe ut. Man har alltid 10 försök på sig innan man blir \"hängd\".");               
            
        }

        private static void DifficultySetup()
        {

            Console.WriteLine();
            Console.WriteLine(" (1) Easy");
            Console.WriteLine(" (2) Medium");
            Console.WriteLine(" (3) Hard");
            
            Console.Write("\n\r Välj svårighetsgrad genom att skriva in siffra: ");
            int difficulty = int.Parse(Console.ReadLine());

            word.SetDifficulty(difficulty);
            
            Console.Clear();
           
        }
        private static void DrawGame(int life)
        {
            string dude = File.ReadAllText("../../../gubbe/" + (10 - life) + ".txt");
            
            Console.WriteLine(dude);

            ConsoleColor color;
            if(life <=3)
            {
                color = ConsoleColor.Red;
            }
            else if(life <= 6)
            {
                color = ConsoleColor.Yellow;
            }
            else
            {
                color = ConsoleColor.Green;
            }

            Console.ForegroundColor = color;
            Console.WriteLine("\n                           <Du har " + life + "/10 försök kvar>");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("                              Det hemliga ordet är");
            Console.ResetColor();
            Console.WriteLine("                                    [" + word.ShownWord + "]");
          
            
            //Lägg Array här
        }
        private static char GuessedLetter()
        {
            string guess;

            do
            {
                Console.Write("\n\r                        Välj en bokstav: ");
                guess = Console.ReadLine().ToLower();
            }
            while (guess == "");
            char tryedLetter = guess[0];
            
            return tryedLetter;

        }
       
        private static bool AskPlayAgain()
        {
            Console.Write("\n\r                          Vill du spela igen? (J/N)\n\r                                     ");
            string inPut = Console.ReadLine().ToLower();
            bool answer = false;
            switch (inPut)
            {
                case "j":                    
                case "ja":
                    answer = true;
                    break;
                case "n":
                case "nej":
                    answer = false;
                    break;
            }
            return answer;
            

        }
        
        private static void GameEnd()
        {
            Console.WriteLine("Avslutar spelet... press [ENTER]");
        }
        static void WriteCenterLine(string text)
        {
            int width = Console.WindowWidth;
            int fill = (width - text.Length) / 2;
            string spaces = new string(char.Parse(" "), fill);
            Console.WriteLine(spaces + text);
        }
        static void PrintHighScoreList()
        {
            string[] scores = HighScore.ListScore();

            foreach (string scoreLine in scores)
            {
                int counter = 1;
                Console.WriteLine(counter + ". " + scoreLine);
                counter++;
            }
        }
    }
}
