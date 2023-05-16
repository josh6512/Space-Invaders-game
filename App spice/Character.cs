using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace App_spice
{
    class Character : GameEntity
    {
        private bool _isArrow;

        public Character(double positionX, double positionY, Image image, double width, double height, Random random, Canvas canvas) :
                                                                           base(positionX, positionY, image, width, height, canvas)
        {
            _isArrow = false;
        }


        public bool IsArrow
        {
            get { return _isArrow; }
            set { _isArrow = value; }
        }

        public virtual Shot Shoot() { return null; }
    }
}
