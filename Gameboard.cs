using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using static System.Net.Mime.MediaTypeNames;

namespace WPF_Tests
{
    public class Gameboard
    {
        int Width;
        int Height;
        public static int bombCount;
        Field[,] fieldArray;
        public static bool win;
        

        public Gameboard(int width, int height, int bombCount)
        {
            win = false;
            this.Width = width;
            this.Height = height;
            Gameboard.bombCount = bombCount;
            InitBoard();
        }

        public static void Winner()
        {
            if (win)            //testausgabe gewonnen
            {
                ((MainWindow)System.Windows.Application.Current.MainWindow).lbl_Status.Content = "Gewonnen!";
                ((MainWindow)System.Windows.Application.Current.MainWindow).lbl_Status.Background = Brushes.Green;
                Gametimer.Stop();
            }
            else return;      //textausgabe verloren
        }

        public void InitBoard()
        {
            int[] bombs = GetRandomBombs();

            this.fieldArray = new Field[Width, Height];

            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    int index = (y) * Width + x;
                    bool bomb = bombs.Contains(index);

                    fieldArray[x, y] = new Field(x, y, bomb, ref fieldArray);
                }
            }
        }


        private int[] GetRandomBombs()
        {
            int[] bombs = new int[bombCount];
            var rnd = new Random();

            for (int i = 0; i < bombCount; i++)
            {
                var rndNumber = rnd.Next(Width * Height);
                while (bombs.Contains(rndNumber))
                {
                    rndNumber = rnd.Next(Width * Height);
                }

                bombs[i] = rndNumber;
            }

            return bombs;
        }


        public void Show(Canvas contentCanvas)
        {
            contentCanvas.Children.Clear();

            foreach (var field in fieldArray)
            {
                field.Show(contentCanvas, (int)contentCanvas.ActualWidth / Width, (int)contentCanvas.ActualHeight / Height);
            }
        }


    }

}