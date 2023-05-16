using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

namespace App_spice
{
    class EnemyGhost : Character
    {
        private Random _random;
        private bool _runningAway;


        public EnemyGhost(double positionX, double positionY, Image image, double width, double height, Random random, Canvas canvas) : 
                                                                            base (positionX, positionY, image, width, height, random, canvas)
        {
            _random = random;
            Speed = random.Next(2, 6);
            _runningAway = false;
        }

        public override void Move(double setLeft, double setTop)
        {
            if (!_runningAway)
            {
                // Check left/right borders
                if (setLeft < 0 && PositionX + setLeft <= 0)
                    Speed *= -1;
                    
                else if (setLeft > 0 && PositionX + Width >= 1150)
                    Speed *= -1;
                if (_random.Next(0, 10000) <= 2)
                    _runningAway = true;
            }
            else
            {
                setLeft = 0;
                setTop = -7;
            }
            if (PositionY + Height < 0)
                IsDead = true;

            base.Move(setLeft, setTop);
        }

        public override Shot Shoot()
        {
            Image arrowImage = new Image();
            arrowImage.Source = new BitmapImage(new Uri("ms-appx:///Assets/Bomb3.png"));
            Canvas.SetLeft(arrowImage, -100);
            Shot shot = new Shot(PositionX + 2, PositionY, arrowImage, 40, 60, MyCanvas, false);
            IsArrow = true;
            return shot;
        }
    }
}
