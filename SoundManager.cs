using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TankBattle.Properties;

namespace TankBattle
{
    internal class SoundManager
    {

        private static SoundPlayer soundPlayer = new SoundPlayer();
        private static SoundPlayer addPlayer = new SoundPlayer();
        private static SoundPlayer blastPlayer = new SoundPlayer();
        private static SoundPlayer firePlayer = new SoundPlayer();
        private static SoundPlayer hitPlayer = new SoundPlayer();

        public static void InitialSound()
        {
            soundPlayer.Stream = Resources.music;
            addPlayer.Stream = Resources.jijiao;
            blastPlayer.Stream = Resources.lubulihainikunge;
            firePlayer.Stream = Resources.ji;
            hitPlayer.Stream = Resources.ganma;
        }
        public static void PlayStart()
        {
            soundPlayer.Play();
        }
        public static void PlayAdd()
        {
            addPlayer.Play();
        }
        public static void PlayBlast()
        {
            blastPlayer.Play();
        }
        public static void PlayFire()
        {
            firePlayer.Play();
        }
        public static void PlayHit()
        {
            hitPlayer.Play();
        }
    }
}
