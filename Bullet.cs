using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TankBattle.Properties;

namespace TankBattle
{
    enum Tag
    {
        MyTank,
        EnemyTank
    }
    internal class Bullet : MoveThing
    {
        public Tag Tag { get; set; }
        public bool IsDestory { get; set; }
        public bool IsEnemy { get; set; }
        public Bullet(int x, int y, int speed,Direction dir,Tag tag)
        {
            IsDestory = false;
            this.X = x;
            this.Y = y;
            this.Speed = speed;
            BitmapUp = Resources.lanqiu;
            BitmapDown = Resources.lanqiu;
            BitmapLeft = Resources.lanqiu;
            BitmapRight = Resources.lanqiu;
            this.Dir = dir;
            this.Tag = tag;

            this.X -= Width / 2;
            this.Y -= Height / 2;
        }
        public Bullet(int x, int y, int speed, Direction dir, Tag tag, bool IsEnemy)
        {
            this.IsEnemy = IsEnemy;
            IsDestory = false;
            this.X = x;
            this.Y = y;
            this.Speed = speed;
            BitmapUp = Resources.jidan;
            BitmapDown = Resources.jidan;
            BitmapLeft = Resources.jidan2;
            BitmapRight = Resources.jidan2;
            this.Dir = dir;
            this.Tag = tag;

            this.X -= Width / 2;
            this.Y -= Height / 2;
        }

        public override void Update()
        {
            MoveCheck();
            Move();
            base.Update();
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
                if (Y + Height/2 +3 < 0)
                {
                    IsDestory = true;
                    return;
                }
            }
            else if (Dir == Direction.Down)
            {
                if (Y + Height / 2 - 3 > 450)
                {
                    IsDestory = true;
                    return;
                }
            }
            else if (Dir == Direction.Left)
            {
                if (X + Width / 2 -3 < 0)
                {
                    IsDestory = true;
                    return;
                }
            }
            else if (Dir == Direction.Right)
            {
                if (X + 3 + Width/2 > 450)
                {
                    IsDestory = true;
                    return;
                }
            }
            #endregion
            Rectangle rect = GetRectangle();
            //rect.X = X+Width/2;
            //rect.Y = Y+Height/2;
            rect.X = X;
            rect.Y = Y;
            rect.Width = 10;
            rect.Height = 10;

            int xExpolosion = this.X + Width/2;
            int yExpolosion = this.Y + Height/2;

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

            NotMoveThing wall =null;
            if ((wall = GameObjectManager.isCollidedWall(rect)) != null)
            {
                IsDestory = true;
                GameObjectManager.DestoryWall(wall);
                GameObjectManager.CreateExplosion(xExpolosion,yExpolosion);
                return;
            }
            if (GameObjectManager.isCollidedStell(rect) != null)
            {
                IsDestory = true;
                return;
            }
            if (GameObjectManager.isCollidedBoss(rect))
            {
                SoundManager.PlayHit();
                GameFramework.ChangeToGameOver(); return;
            }
            if(Tag == Tag.MyTank)
            {
                EnemyTank enemyTank = null;
                if ((enemyTank=GameObjectManager.IsCollidedEnemyTank(rect)) != null)
                {
                    SoundManager.PlayBlast();
                    IsDestory = true;
                    GameObjectManager.DestoryEnemyTank(enemyTank);
                    GameObjectManager.CreateExplosion(xExpolosion, yExpolosion);
                    return;
                }
            }else if(Tag == Tag.EnemyTank)
            {
                MyTank myTank = null;
                if((myTank= GameObjectManager.isCollidedMyTank(rect)) != null)
                {
                    SoundManager.PlayHit();
                    IsDestory = true;
                    GameObjectManager.CreateExplosion(xExpolosion, yExpolosion);
                    myTank.TakeDamage();
                }
                EnemyTank enemyTank = null;
                if ((enemyTank = GameObjectManager.IsCollidedEnemyTank(rect)) != null)
                {
                    IsDestory = true;
                    return;
                }
            }
        }
    }
}
