using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;


namespace WPF_Tests
{
    public class Field
    {
        int x, y;
        int width;
        int height;
        bool clicked;
        bool flagged;
        bool bomb;
        Field[,] fieldArray;
        Button btn;


        public void SetBTNToClicked(string content)
        {
            btn.Content = content;
            btn.Background = Brushes.LightGray;
            clicked = true;
        }

        public Field(int x, int y, bool bomb, ref Field[,] fieldArray)
        {
            this.width = this.height = 20;
            this.x = x;
            this.y = y;
            this.bomb = bomb;
            this.clicked = this.flagged = false;
            this.fieldArray = fieldArray;

        }

        public void Show(Canvas ContentCanvas, int width, int height)
        {
            this.width = width;
            this.height = height;


            btn = new Button();
            btn.Name = "btn" + x.ToString() + y.ToString();
            btn.PreviewMouseDown += new MouseButtonEventHandler(Click);
            btn.Width = width;
            btn.Height = height;
            btn.Background = Brushes.Gray;

            Canvas.SetTop(btn, y * height);
            Canvas.SetLeft(btn, x * width);

            ContentCanvas.Children.Add(btn);
        }

        public void Click(object sender, MouseButtonEventArgs e)
        {

            if (e.ChangedButton == MouseButton.Right)
            {
                int bombFlaggedCount = 0;
                int flaggedCounter = 0;
                int cwidth = fieldArray.GetLength(0) - 1;
                int cheight = fieldArray.GetLength(1) - 1;

                if (clicked) return;

                if (flagged) btn.Content = "";
                else btn.Content = "🚩";

                flagged = !flagged;

                for (int a = 0; a <= cwidth; a++)
                {
                    for (int b = 0; b <= cheight; b++)
                    {
                        if (fieldArray[a, b].bomb == true && fieldArray[a, b].flagged == true) bombFlaggedCount++;
                        if (fieldArray[a, b].flagged == true) flaggedCounter++;

                    }
                }
                if ((bombFlaggedCount == Gameboard.bombCount) && (Gameboard.bombCount == flaggedCounter)) Gameboard.win = true ;
                Gameboard.Winner();
            }
            if (e.ChangedButton == MouseButton.Left)
            {
                if (bomb == true)
                {
                    clicked = true;
                    btn.Content = "boom";
                    if (clicked == true) btn.Background = Brushes.Red;
                    ((MainWindow)System.Windows.Application.Current.MainWindow).lbl_Status.Content = "Verloren!";
                    ((MainWindow)System.Windows.Application.Current.MainWindow).lbl_Status.Background =Brushes.Red;
                    Gametimer.Stop();
                }
                else
                {
                    CheckNeighbor();
                    Gameboard.Winner();
                    
                }
            }
        }



        //public void CheckNeighbor_backup(int x,int y)
        //{
        //    if (fieldArray[x, y].clicked == false)
        //    {
        //        int count = 0;
        //        int cwidth = fieldArray.GetLength(0) - 1;                                 //with angepasst ans arrayraster
        //        int cheight = fieldArray.GetLength(1) - 1;                                //height angepasst an arrayraster


        //        if (x > 0)
        //            if (fieldArray[x - 1, y].bomb == true) count++;         //links daneben
        //        if (x < cwidth)
        //            if (fieldArray[x + 1, y].bomb == true) count++;         //rechts daneben
        //        if (y > 0)
        //            if (fieldArray[x, y - 1].bomb == true) count++;         //nach oben
        //        if (y < cheight)
        //            if (fieldArray[x, y + 1].bomb == true) count++;         //nach unten
        //        if (x > 0 && y > 0)
        //            if (fieldArray[x - 1, y - 1].bomb == true) count++;     //links oben
        //        if (x > 0 && y < cheight)
        //            if (fieldArray[x - 1, y + 1].bomb == true) count++;     //links unten
        //        if (y > 0 && x < cwidth)
        //            if (fieldArray[x + 1, y - 1].bomb == true) count++;     //rechts oben
        //        if (y < cheight && x < cwidth)
        //            if (fieldArray[x + 1, y + 1].bomb == true) count++;     //rechts unten

        //        if (count == 0)
        //        {
        //            fieldArray[x, y].SetBTNToClicked("");                   //graues Feld ohne Zahl

        //            if ((x + 1) < fieldArray.GetLength(0)) CheckNeighbor(x + 1, y);
        //            if ((y + 1) < fieldArray.GetLength(1)) CheckNeighbor(x, y + 1);
        //            if ((x - 1) >= 0) CheckNeighbor(x - 1, y);
        //            if ((y - 1) >= 0) CheckNeighbor(x, y - 1);

        //        }
        //        else
        //        {
        //            fieldArray[x, y].SetBTNToClicked(count.ToString());     //graues feld mit zahl
        //        }

        //    }
        //}

        public void CheckNeighbor()
        {
            if (clicked == false)
            {
                int count = 0;
                int cwidth = fieldArray.GetLength(0) - 1;                                 //with angepasst ans arrayraster
                int cheight = fieldArray.GetLength(1) - 1;                                //height angepasst an arrayraster


                if (x > 0)
                    if (fieldArray[x - 1, y].bomb == true) count++;         //links daneben
                if (x < cwidth)
                    if (fieldArray[x + 1, y].bomb == true) count++;         //rechts daneben
                if (y > 0)
                    if (fieldArray[x, y - 1].bomb == true) count++;         //nach oben
                if (y < cheight)
                    if (fieldArray[x, y + 1].bomb == true) count++;         //nach unten
                if (x > 0 && y > 0)
                    if (fieldArray[x - 1, y - 1].bomb == true) count++;     //links oben
                if (x > 0 && y < cheight)
                    if (fieldArray[x - 1, y + 1].bomb == true) count++;     //links unten
                if (y > 0 && x < cwidth)
                    if (fieldArray[x + 1, y - 1].bomb == true) count++;     //rechts oben
                if (y < cheight && x < cwidth)
                    if (fieldArray[x + 1, y + 1].bomb == true) count++;     //rechts unten

                if (count == 0)
                {
                    this.SetBTNToClicked("");                   //graues Feld ohne Zahl

                    if ((x + 1) < fieldArray.GetLength(0)) fieldArray[x+1,y].CheckNeighbor();
                    if ((y + 1) < fieldArray.GetLength(1)) fieldArray[x,y+1].CheckNeighbor();
                    if ((x - 1) >= 0) fieldArray[x-1,y].CheckNeighbor();
                    if ((y - 1) >= 0) fieldArray[x,y-1].CheckNeighbor();


                }
                else
                {
                    this.SetBTNToClicked(count.ToString());     //graues feld mit zahl
                }

                //Gewinnbedingung abfragen

                int arrayFieldNumber = fieldArray.GetLength(0) * fieldArray.GetLength(1);
                int clickedCount = 0;

                for (int a = 0; a <= cwidth; a++)
                {
                    for (int b = 0; b <= cheight; b++)
                    {
                        if (fieldArray[a,b].clicked==true) clickedCount++;
                    }
                }
                if (clickedCount == arrayFieldNumber - Gameboard.bombCount) Gameboard.win = true;
            }
        }
    }
}
