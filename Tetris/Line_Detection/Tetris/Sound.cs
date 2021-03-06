﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    class Sound
    {
        public IrrKlang.ISound sound = null;
        public IrrKlang.ISoundEngine AudioEngine = null;
        public IrrKlang.ISoundSource source = null;
        public float volume = 0.005f;


        public Sound(String music)
        {
            AudioEngine = new IrrKlang.ISoundEngine();
            source = AudioEngine.AddSoundSourceFromFile(music);

        }

        public void play()
        {
            sound = AudioEngine.Play2D(source, false, false, false);
        }

        public void pause()
        {
            sound.Paused = true;
        }

        public void playLoop()
        {
            sound = AudioEngine.Play2D(source, true, false, false);
        }

        public void gameOver()
        {

        }

    }
}
