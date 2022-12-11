using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TankBattle.Properties;

namespace TankBattle
{
    internal class MyTank : MoveThing
    {
        public MyTank(int x,int y,int speed)
        {
            this.X = x;
            this.Y = y;
            OriginalX = x;
            OriginalY = y;
            this.Speed = speed;
            BitmapUp = Resources.kunUp;
            BitmapDown = Resources.kunDown;
            BitmapLeft = Resources.kunLeft;
            BitmapRight = Resources.kunRight;
            this.Dir = Direction.Up;
            HP = 1;
        }

        public static bool IsMoving { get; set; }
        public int HP { get; set; }
        private int OriginalX { get; set; }
        private int OriginalY { get; set; }
        public void keyDown(KeyEventArgs args)
        {
            switch (args.KeyCode)
            {
                case Keys.W:
                    Dir = Direction.Up;
                    IsMoving = true;
                    break;
                case Keys.S:
                    Dir = Direction.Down;
                    IsMoving = true;
                    break;
                case Keys.D:
                    Dir = Direction.Right;
                    IsMoving = true;
                    break;
                case Keys.A:
                    Dir = Direction.Left;
                    IsMoving = true;
                    break;
                case Keys.Space:
                    Attack();
                    break;
            }
        }
        private void Attack()
        {
            SoundManager.PlayFire();
            int x = this.X;
            int y = this.Y;
            switch (Dir)
            {
                case Direction.Up:
                    x = x + Width/2;
                    break;
                case Direction.Down:
                    x = x + Width/ 2;
                    y += Height;
                    break;
                case Direction.Left:
                    y = y + Height/ 2;
                    break;
                case Direction.Right:
                    x +=  Width;
                    y = y + Height/ 2;
                    break;
            }
            GameObjectManager.CreateBullet(x,y,Dir,Tag.MyTank);
        }
        public void keyUp(KeyEventArgs args)
        {
            switch (args.KeyCode)
            {
                case Keys.W:
                    IsMoving = false;
                    break;
                case Keys.S:
                    IsMoving = false;
                    break;
                case Keys.D:
                    IsMoving = false;
                    break;
                case Keys.A:
                    IsMoving = false;
                    break;
            }
        }

        public override void Update()
        {
            MoveCheck();

            Move();
            base.Update();
        }

        public void Move()
        {
            if (IsMoving == false)
            {
                return;
            }

            switch (Dir)
            {
                case Direction.Up:
                    Y -= Speed;
                    break;
                case Direction.Down:
                    Y += Speed;
                    break;
                case Direction.Right:
                    X += Speed;
                    break;
                case Direction.Left:
                    X -= Speed;
                    break;
            }
        }

        public void MoveCheck()
        {

            #region 边界检查
            if (Dir == Direction.Up)
            {
                if (Y - Speed < 0)
                {
                    IsMoving = false;return;
                }
            }else if(Dir == Direction.Down)
            {
                if(Y+Speed+Height > 450)
                {
                    IsMoving = false; return;
                }
            }else if(Dir == Direction.Left)
            {
                if (X - Speed < 0)
                {
                    IsMoving = false; return;
                }
            }else if(Dir == Direction.Right)
            {
                if (X + Speed + Width > 450)
                {
                    IsMoving = false; return;
                }
            }
            #endregion
            Rectangle rect = GetRectangle();
            switch (Dir)
            {
                case Direction.Up:
                    rect.Y -=Speed;
                    break;
                case Direction.Down:
                    rect.Y +=Speed;
                    break;
                case Direction.Left:
                    rect.X -=Speed;
                    break;
                case Direction.Right:
                    rect.X +=Speed;
                    break;
            }
            if (GameObjectManager.isCollidedWall(rect) != null)
            {
                IsMoving = false;return;
            }
            if (GameObjectManager.isCollidedStell(rect) != null)
            {
                IsMoving = false; return;
            }
            if (GameObjectManager.isCollidedBoss(rect))
            {
                IsMoving = false; return;
            }
            if (GameObjectManager.isMytankCollidedEnemyTank(rect))
            {
                IsMoving = false; return;
            }
        }

        public void TakeDamage()
        {
            HP--;
            if (HP == 0)
            {
                X = OriginalX;
                Y = OriginalY;
                Dir = Direction.Up;
                HP = 1;
            }
        }
    }
}
