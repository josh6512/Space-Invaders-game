using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace App_spice
{
    class GameEntity
    {
        private double _positionX;
        private double _positionY;
        private Image _myImage;
        private double _width;
        private double _height;
        private Canvas _myCanvas;
        private bool _isDead;
        private double _speed;

        public GameEntity(double positionX, double positionY, Image image, double width, double height, Canvas canvas)
        {
            _myCanvas = canvas;
            _positionX = positionX;
            _positionY = positionY;
            _width = width;
            _height = height;
            _isDead = false;
            MyImage = image;
        }


        public double Height
        {
            get { return _height; }
            set { _height = value; }
        }

        public double Width
        {
            get { return _width; }
            set { _width = value; }
        }

        public double Speed
        {
            get { return _speed; }
            set { _speed = value; }
        }

        public bool IsDead
        {
            get { return _isDead; }
            set { _isDead = value; }
        }


        public Canvas MyCanvas
        {
            get { return _myCanvas; }
            set { _myCanvas = value; }
        }

        public Image MyImage
        {
            get { return _myImage; }
            set
            {
                _myImage = value;
                _myImage.Width = _width;
                _myImage.Height = _height;
                _myImage.Stretch = Stretch.Uniform;
            }
        }

        public double PositionX
        { 
            get { return _positionX; }
            set { _positionX = value; }
        }

        public double PositionY
        {
            get { return _positionY; }
            set { _positionY = value; }
        }

        public virtual void Move( double setLeft, double setTop)
        {
            _positionX += setLeft;
            _positionY += setTop;
            Canvas.SetLeft(_myImage, _positionX);
            Canvas.SetTop(_myImage, _positionY);
        }

        
        
    }
}
