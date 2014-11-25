using System;
using System.Collections.Generic;
using System.Text;
using System.Media;

namespace HugoWorld
{
    /// <summary>
    /// Sounds is a static class for any other part of the program to use to play the sounds.
    /// </summary>
    public static class Sounds
    {
        private static SoundPlayer _eat = new SoundPlayer(@"gamedata\eat.wav");
        private static SoundPlayer _pickup = new SoundPlayer(@"gamedata\pickup.wav");
        private static SoundPlayer _fight = new SoundPlayer(@"gamedata\fight.wav");
        private static SoundPlayer _kiss = new SoundPlayer(@"gamedata\kiss.wav");
        private static SoundPlayer _magic = new SoundPlayer(@"gamedata\magic.wav");
        private static SoundPlayer _start = new SoundPlayer(@"gamedata\start.wav");

        static Sounds()
        {
            //preload the sounds on construction.
            _eat.Load();
            _pickup.Load();
            _fight.Load();
            _kiss.Load();
            _magic.Load();
            _start.Load();
        }


        public static void Eat()
        {
            _eat.Play();
        }

        public static void Pickup()
        {
            _pickup.Play();
        }

        public static void Fight()
        {
            _fight.Play();
        }

        public static void Kiss()
        {
            _kiss.Play();
        }

        public static void Magic()
        {
            _magic.Play();
        }

        public static void Start()
        {
            _start.Play();
        }

    }
}
