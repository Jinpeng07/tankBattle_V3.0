using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankBattle
{
    internal class NotMoveThing:GameObject
    {
        public NotMoveThing(int x,int y, Image img)
        {
            this.X = x;
            this.Y = y;
            this.Img = img;
        }

        private Image img;
        public Image Img 
        {
            get
            {
                return img;
            } 
            set
            {
                img = value;
                Width = img.Width;
                Height = img.Height;
            } 
        }

        protected override Image GetImg()
        {
            return Img;
        }


    }
}
