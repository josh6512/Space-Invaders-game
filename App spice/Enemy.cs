using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

namespace App_spice
{
    class Enemy
    {

        public Image[] Enemies { get; set; }
        Canvas _cnv;

        public Enemy(Canvas cnv)
        {
            _cnv = cnv;
            BitmapImage BI = new BitmapImage(new Uri("ms-appx:///Assets/goast.png"));
            Enemies = new Image[10];
            int space = 78;
            for (int i = 0; i < Enemies.Length; i++)
            {
                Enemies[i] = new Image();
                Enemies[i].Source = BI;
                Enemies[i].Width = 50;
                Enemies[i].Height = 50;
                Enemies[i].SetValue(Canvas.LeftProperty, space);
                Enemies[i].SetValue(Canvas.TopProperty, 100);

                space += 78;

                _cnv.Children.Add(Enemies[i]);

                MoveEnemies();

            }

        }


        private void MoveEnemies()
        {
            DispatcherTimer Timer = new DispatcherTimer();
            Timer.Interval = new TimeSpan(0, 0, 0, 0, 500);
            Timer.Tick += Timer_Tick;
            Timer.Start();

        }



        private void Timer_Tick(object sender, object e)
        {
            for (int i = 0; i < Enemies.Length; i++)
            {
               
                if (Canvas.GetLeft(Enemies[0]) < 0)
                {
                    Enemies[i].SetValue(Canvas.LeftProperty, Canvas.GetLeft(Enemies[i]) + 10);
                }

            }

            for (int i = 0; i < Enemies.Length; i++)
            {
                
                if (Canvas.GetLeft(Enemies[9]) >= 1150)
                {
                    Enemies[i].SetValue(Canvas.LeftProperty, Canvas.GetLeft(Enemies[i]) - 10);

                }
            }   

         



        }

    }
}
