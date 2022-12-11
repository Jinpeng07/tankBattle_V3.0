using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TankBattle.Properties;

namespace TankBattle
{
    internal class EnemyTank:MoveThing
    {
        Random random = new Random();

        public static int AttackSpeed { get; set; }
        private static int AttackCount = 0;
        public int ChangDirSpeed { get; set; }
        private int ChangDirCount = 0;
        public EnemyTank(int x, int y, int speed, Bitmap bmpUp, Bitmap bmpDown, Bitmap bmpLeft, Bitmap bmpRight)
        {
            this.X = x;
            this.Y = y;
            this.Speed = speed;
            BitmapUp = bmpUp;
            BitmapDown = bmpDown;
            BitmapLeft = bmpLeft;
            BitmapRight = bmpRight;
            this.Dir = Direction.Down;
            AttackSpeed = 20;
            ChangDirSpeed = 120;
        }
        public override void Update()
        {
            MoveCheck();
            Move();
            AttackCheck();
            AutoChangeDirrection();
            base.Update();
        }

        public void ChangeDirection()
        {
            while (true)
            {
                Direction dir = (Direction)random.Next(0, 4);
                if(dir == Dir)
                {
                    continue;
                }
                else
                {
                    Dir = dir;
                    break;
                }
            }
            MoveCheck();

        }

        private void AutoChangeDirrection()
        {
            ChangDirCount++;
            if (ChangDirCount < ChangDirSpeed) return;
            ChangDirCount = 0;
            ChangeDirection();
        }
        public void Move()
        {
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
                    ChangeDirection(); return;
                }
            }
            else if (Dir == Direction.Down)
            {
                if (Y + Speed + Height > 450)
                {
                    ChangeDirection(); return;
                }
            }
            else if (Dir == Direction.Left)
            {
                if (X - Speed < 0)
                {
                    ChangeDirection(); return;
                }
            }
            else if (Dir == Direction.Right)
            {
                if (X + Speed + Width > 450)
                {
                    ChangeDirection(); return;
                }
            }
            #endregion
            Rectangle rect = GetRectangle();
            switch (Dir)
            {
                case Direction.Up:
                    rect.Y -= Speed;
                    break;
                case Direction.Down:
                    rect.Y += Speed;
                    break;
                case Direction.Left:
                    rect.X -= Speed;
                    break;
                case Direction.Right:
                    rect.X += Speed;
                    break;
            }
            if (GameObjectManager.isCollidedWall(rect) != null)
            {
                ChangeDirection(); return;
            }
            if (GameObjectManager.isCollidedStell(rect) != null)
            {
                ChangeDirection(); return;
            }
            if (GameObjectManager.isCollidedBoss(rect))
            {
                ChangeDirection(); return;
            }
            if (GameObjectManager.isEnemyTankCollidedMytank(rect))
            {
                ChangeDirection(); return;
            }
            
        }
        private void Attack()
        {
            int x = this.X;
            int y = this.Y;
            switch (Dir)
            {
                case Direction.Up:
                    x = x + Width / 2;
                    break;
                case Direction.Down:
                    x = x + Width / 2;
                    y += Height;
                    break;
                case Direction.Left:
                    y = y + Height / 2;
                    break;
                case Direction.Right:
                    x += Width;
                    y = y + Height / 2;
                    break;
            }
            GameObjectManager.CreateEnemyBullet(x, y, Dir, Tag.EnemyTank,true);
        }

        private void AttackCheck()
        {
            AttackCount++;
            if (AttackCount < AttackSpeed) return;
            Attack();
            AttackCount = 0;
        }
    }
}
