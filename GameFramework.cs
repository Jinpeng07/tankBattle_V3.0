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
    enum GameState
    {
        Running,
        GameOver
    }
    internal class GameFramework
    {
        public static Graphics g;
        public static GameState gameState = GameState.Running;
        public static void Start()
        {
            SoundManager.InitialSound();
            GameObjectManager.Start();
            GameObjectManager.CreateMap();
            GameObjectManager.CreateMyTank();
            SoundManager.PlayStart();
        }

        public static void Update()
        {
            if(gameState ==GameState.Running)
            {
                GameObjectManager.Update();
            }
            else if(gameState == GameState.GameOver)
            {
                GameOverUpdate();
            }
        }

        public static void ChangeToGameOver()
        {
            gameState = GameState.GameOver;
        }

        public static void GameOverUpdate()
        {
            int x = 450/2- Resources.GameOver.Width / 2;
            int y = 450/2- Resources.GameOver.Height / 2;
            g.DrawImage(Resources.zhiyin,x,y);
        }
    }
}
