using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Configuration;
using System.Text;
using System.Threading.Tasks;
using TankBattle.Properties;

namespace TankBattle
{
    internal class Explosion : GameObject
    {
        private int playSpeed = 1;//单位是帧
        private int playCount = -1;
        public bool IsNeedDestory { get; set; }
        public Bitmap[] bmpArray = new Bitmap[]
        {
            Resources.EXP1,
            Resources.EXP2,
            Resources.EXP3,
            Resources.EXP4,
            Resources.EXP5,
        };
        private int index;

        public Explosion(int x,int y)
        {
            foreach (Bitmap bmp in bmpArray)
            {
                bmp.MakeTransparent(Color.Black);
            }
            this.X = x - bmpArray[0].Width / 2;
            this.Y = y - bmpArray[0].Height / 2;
            IsNeedDestory= false;
        }

        protected override Image GetImg()
        {
            if (index > 4) index = 4;
            return bmpArray[index];
        }

        public override void Update()
        {
            playCount++;
            index = playCount  / playSpeed;
            if(index>4) IsNeedDestory = true;
            base.Update();
        }
    }
}
