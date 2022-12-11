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
    internal class GameObjectManager
    {
        private static List<NotMoveThing> wallList = new List<NotMoveThing>();
        private static List<NotMoveThing> stellList = new List<NotMoveThing>();
        private static NotMoveThing boss ;
        private static MyTank myTank;
        private static List<EnemyTank> tankLists = new List<EnemyTank>();
        private static List<Bullet> bulletLists = new List<Bullet>();
        private static List<Explosion> explosion = new List<Explosion>();

        private static int enemyBornSpeed = 150;
        private static int enemyBornCount = 60;
        private static int startPlayAdd = 120;
        private static int fpsCount = 0;
        private static Point[] points = new Point[3];
        #region 检查碰撞
        public static NotMoveThing isCollidedWall(Rectangle rt)
        {
            foreach (NotMoveThing wall in wallList)
            {
                if (wall.GetRectangle().IntersectsWith(rt))
                {
                    return wall;
                }
            }
            return null;
        }
        public static NotMoveThing isCollidedStell(Rectangle rt)
        {
            foreach (NotMoveThing wall in stellList)
            {
                if (wall.GetRectangle().IntersectsWith(rt))
                {
                    return wall;
                }
            }
            return null;
        }
        public static bool isCollidedBoss(Rectangle rt)
        {
            return boss.GetRectangle().IntersectsWith(rt);
        }

        public static bool isMytankCollidedEnemyTank(Rectangle rt)
        {
            foreach (EnemyTank tank in tankLists)
            {
                if (tank.GetRectangle().IntersectsWith(rt))
                {
                    return true;
                }
            }
            return false;
        }
        public static bool isEnemyTankCollidedMytank(Rectangle rt)
        {
            if (myTank.GetRectangle().IntersectsWith(rt))
            {
                return true;
            }
            return false;
        }

        //public static bool isEnemyTankCollidedEnemyTank(Rectangle rt)
        //{
        //    foreach (EnemyTank tank in tankLists)
        //    {
        //        if (tank.GetRectangle().IntersectsWith(rt))
        //        {
        //            return true;
        //        }
        //    }
        //    return false;
        //}

        public static MyTank isCollidedMyTank(Rectangle rt)
        {
            if( myTank.GetRectangle().IntersectsWith(rt)) return myTank;
            else return null;
        }
        public static EnemyTank IsCollidedEnemyTank(Rectangle rect)
        {
            foreach(EnemyTank tank in tankLists)
            {
                if (tank.GetRectangle().IntersectsWith(rect))
                {
                    return tank;
                }
            }
            return null;
        }
        # endregion



        public static void Start()
        {
            points[0].X = 0; points[0].Y = 0;
            points[1].X = 7*30; points[1].Y = 0;
            points[2].X = 14*30; points[2].Y = 0;
        }

        //Update方法
        public static void Update()
        {
            foreach (NotMoveThing nm in wallList)
            {
                nm.Update();
            }
            foreach (NotMoveThing s in stellList)
            {
                s.Update();
            }
            foreach(EnemyTank tank in tankLists)
            {
                //int index = tankLists.IndexOf(tank);
                //if(index == 1)
                //{
                //    for (int i = 1; i < tankLists.Count; i++)
                //    {
                //        if (isEnemyTankCollidedEnemyTank(tankLists[i].GetRectangle()))
                //        {
                //            tank.ChangeDirection();
                //        }
                //    }
                //}else if (index == tankLists.Count)
                //{
                //    for (int i = 1; i < tankLists.Count-1; i++)
                //    {
                //        if (isEnemyTankCollidedEnemyTank(tankLists[i].GetRectangle()))
                //        {
                //            tank.ChangeDirection();
                //        }
                //    }
                //}
                //else 
                //{
                //    for (int i = 0; i < index; i++)
                //    {
                //        if (isEnemyTankCollidedEnemyTank(tankLists[i].GetRectangle()))
                //        {
                //            tank.ChangeDirection();
                //        }
                //    }
                //    for (int i = index+1; i < tankLists.Count; i++)
                //    {
                //        if (isEnemyTankCollidedEnemyTank(tankLists[i].GetRectangle()))
                //        {
                //            tank.ChangeDirection();
                //        }
                //    }
                //}
                tank.Update();
            }
            foreach (Bullet bullet in bulletLists)
            {
                bullet.Update();
            }
            foreach(Explosion exp in explosion)
            {
                exp.Update();
            }
            boss.Update();
            myTank.Update();
            CheckAndDestoryExplosion();
            EnemyBorn();
            CheckAndDestoryBUllet();

        }

        public static void EnemyBorn()
        {
            enemyBornCount++;
            fpsCount++;
            if (enemyBornCount < enemyBornSpeed) return;
            Random rd = new Random();
            int index = rd.Next(0,3);
            Point position = points[index];
            if(fpsCount >= startPlayAdd)
            {
                SoundManager.PlayAdd();
            }
            int enemyType = rd.Next(1,5);
            switch (enemyType)
            {
                case 1:
                    CreateEnemyaTank1(position.X,position.Y);
                    break;
                case 2:
                    CreateEnemyaTank2(position.X, position.Y);
                    break;
                case 3:
                    CreateEnemyaTank3(position.X, position.Y);
                    break;
                case 4:
                    CreateEnemyaTank4(position.X, position.Y);
                    break;
            }
            enemyBornCount = 0;
        }

        public static void CreateEnemyaTank1(int x,int y)
        {
            EnemyTank tank = new EnemyTank(x, y, 2, Resources.enemyJiUp1, Resources.enemyJiDown1, Resources.enemyJiLeft1, Resources.enemyJiRight1);
            tankLists.Add(tank);
        }
        public static void CreateEnemyaTank2(int x, int y)
        {
            EnemyTank tank = new EnemyTank(x, y, 2, Resources.EnemyJiUp2, Resources.EnemyJiDown2, Resources.EnemyJiLeft2, Resources.EnemyJiRight2);
            tankLists.Add(tank);
        }
        public static void CreateEnemyaTank3(int x, int y)
        {
            EnemyTank tank = new EnemyTank(x, y, 4, Resources.enemyJiUp3, Resources.enemyJiDown3, Resources.enemyJiLeft3, Resources.enemyJiRight3);
            tankLists.Add(tank);
        }
        public static void CreateEnemyaTank4(int x, int y)
        {
            EnemyTank tank = new EnemyTank(x, y, 1, Resources.enemyJiUp4, Resources.enemyJiDown4, Resources.enemyJiLeft4, Resources.enemyJiRight4);
            tankLists.Add(tank);
        }

        public static void CreateBullet(int x, int y, Direction dir,Tag tag)
        {
            Bullet bullet = new Bullet(x, y, 8, dir, tag);
            bulletLists.Add(bullet);
        }
        //public static void DestoryBullet(Bullet bullet)
        //{
        //    //IsDestory = true;
        //    //bulletLists.Remove(bullet);
        //}
        public static void CreateEnemyBullet(int x, int y, Direction dir, Tag tag, bool IsEnemy)
        {
            Bullet bullet = new Bullet(x, y, 8, dir, tag,true);
            bulletLists.Add(bullet);
        }
        public static void CheckAndDestoryBUllet()
        {
            List<Bullet> needToDestory = new List<Bullet>();
            foreach(Bullet bullet in bulletLists)
            {
                if (bullet.IsDestory == true)
                {
                    needToDestory.Add(bullet);
                }
            }
            foreach(Bullet bullet in needToDestory)
            {
                bulletLists.Remove(bullet);
            }
        }

        public static void CheckAndDestoryExplosion()
        {
            List<Explosion> needToDestory = new List<Explosion>();
            foreach (Explosion exp in explosion)
            {
                if (exp.IsNeedDestory)
                {
                    needToDestory.Add(exp);
                }
            }
            foreach (Explosion exp in needToDestory)
            {
                explosion.Remove(exp);
            }
        }

        public static void DestoryWall(NotMoveThing wall)
        {
            wallList.Remove(wall);
        }
        public static void DestoryEnemyTank(EnemyTank enemyTank)
        {
            tankLists.Remove(enemyTank);
        }

        public static void CreateMap()
        {
            CreateWall(1,1,5, Resources.beidaiku3,wallList);
            CreateWall(3, 1,5, Resources.beidaiku3, wallList);
            CreateWall(5, 1,4, Resources.beidaiku3, wallList);
            CreateWall(7, 1,3, Resources.beidaiku3, wallList);
            CreateWall(9, 1,4, Resources.beidaiku3, wallList);
            CreateWall(11, 1,5, Resources.beidaiku3, wallList);
            CreateWall(13, 1, 5, Resources.beidaiku3, wallList);

            CreateWall(2, 7, 1, Resources.beidaiku3, wallList);
            CreateWall(3, 7, 1, Resources.beidaiku3, wallList);
            CreateWall(4, 7, 1, Resources.beidaiku3, wallList);
            CreateWall(6, 7, 1, Resources.beidaiku3, wallList);
            CreateWall(7, 6, 2, Resources.beidaiku3, wallList);
            CreateWall(8, 7, 1, Resources.beidaiku3, wallList);
            CreateWall(10, 7, 1, Resources.beidaiku3, wallList);
            CreateWall(11, 7, 1, Resources.beidaiku3, wallList);
            CreateWall(12, 7, 1, Resources.beidaiku3, wallList);

            CreateWall(1, 9, 5, Resources.beidaiku3, wallList);
            CreateWall(3, 9, 5, Resources.beidaiku3, wallList);
            CreateWall(5, 9, 3, Resources.beidaiku3, wallList);
            CreateWall(7, 10, 2, Resources.beidaiku3, wallList);
            CreateWall(9, 9, 3, Resources.beidaiku3, wallList);
            CreateWall(11, 9, 5, Resources.beidaiku3, wallList);
            CreateWall(13, 9, 5, Resources.beidaiku3, wallList);

            CreateWall(7, 5, 1, Resources.steel, stellList);
            CreateWall(0, 7, 1, Resources.steel, stellList);
            CreateWall(14, 7, 1, Resources.steel, stellList);

            //CreateWall(2, 7, 1, Resources.wall, walllList);
            CreateWall(6, 10, 1, Resources.beidaiku3, wallList);
            CreateWall(8, 10, 1, Resources.beidaiku3, wallList);

            CreateWall(6, 13, 2, Resources.beidaiku3, wallList);
            CreateWall(8, 13, 2, Resources.beidaiku3, wallList);
            CreateWall(7, 13, 1, Resources.beidaiku3, wallList);

            //CreateWall(7, 14, 1, Resources.Boss, bossList);
            CreateBoss(7, 14, Resources.kunkun);
        }
        private static void CreateBoss(int x, int y, Image img)
        {
            int xPosition = x * 30;
            int yPosition = y * 30;
            boss = new NotMoveThing(xPosition, yPosition, img);
        }
        private static void CreateWall(int x, int y, int count, Image img, List<NotMoveThing> list)
        {
            int xPosition = x*30;
            int yPosition = y*30;
            for (int i = yPosition; i < yPosition+ 30*count; i += 15)
            {
                NotMoveThing wall1 = new NotMoveThing(xPosition, i, img);
                NotMoveThing wall2 = new NotMoveThing(xPosition+15, i, img);
                list.Add(wall1);
                list.Add(wall2);
            }
        }

        public static void CreateMyTank()
        {
            int x = 5 * 30;
            int y = 14 * 30;

            myTank = new MyTank(x,y,3);
        }

        public static void CreateExplosion(int x,int y)
        {
            Explosion exp = new Explosion(x, y);
            explosion.Add(exp);
        }


        public static void DrawMyTank()
        {
            myTank.DrawSelf();
        }

        public static void keyDown(KeyEventArgs args)
        {
            myTank.keyDown(args);
            //if (args.KeyCode == Keys.Enter && GameFramework.gameState == GameState.GameOver)
            //{
            //    GameFramework.gameState = GameState.Running;
            //    Start();
            //    CreateMap();
            //    CreateMyTank();
            //    Update();
            //}
        }

        public static void keyUp(KeyEventArgs args)
        {
            myTank.keyUp(args);
        }
    }
}
