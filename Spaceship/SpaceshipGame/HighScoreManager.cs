using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spaceship.Content
{
    internal class HighScoreManager
    {
        private const string filePath = "highscore.txt";

        public static double LoadHighScore()
        {
            if (File.Exists(filePath))
            {
                string scoreString = File.ReadAllText(filePath);
                if (double.TryParse(scoreString, out double highScore))
                {
                    return highScore;
                }
            }

            return 0;
        }

        public static void SaveHighScore(double newHighScore)
        {
            File.WriteAllText(filePath, newHighScore.ToString());
        }
    }
}