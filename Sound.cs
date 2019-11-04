using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Synthi
{
    class Sound
    {
        public PictureBox PARENTP;
        private WaveOut waveOut;
        public bool IsPlaying = false;
        public Button KEYY;
        public String KNAME;
        public float AMPP, FREQQ, VOLL, RELL, ATKK;
        private SineWave sineWaveProvider = new SineWave();

        public Sound(Button KEY, float AMP, float FREQ, int REL, float VOL)
        {
            KEYY = KEY;
            try { KNAME = KEY.Name; } catch (Exception) { KNAME = "C4"; }
            AMPP = (float)AMP * 10f;
            FREQQ = FREQ;
            VOLL = VOL;
            RELL = REL;
            sineWaveProvider.Frequency = FrequencySelect(KNAME, FREQQ);
            sineWaveProvider.Amplitude = (float)AMP + 0.5f;
            waveOut = new WaveOut();
            waveOut.Volume = VOL / 1000f;
            waveOut.Init(sineWaveProvider);
        }

        public async void PLAYS(int REL, int ATK, float AMP, int DEC, float VOL)
        {
            if (!IsPlaying)
            {
                AMPP = (float)AMP * 10f;
                VOLL = VOL;
                RELL = REL;
                sineWaveProvider.Frequency = FrequencySelect(KNAME, FREQQ);
                AMPP = (float)AMP * 10f;
                ATKK = ATK;
                sineWaveProvider.Amplitude = (float)AMPP + 0.5f;
                waveOut.Volume = 0;
                waveOut.Play();
                IsPlaying = true;
                if (ATKK != 0)
                {
                    while (waveOut.Volume < VOL / 1000f && IsPlaying)
                    {
                        try
                        {
                            waveOut.Volume = waveOut.Volume + (ATKK / 1000f);
                            await Task.Delay(100);
                        }
                        catch (Exception) { }
                    }
                }
                if (DEC != 10)
                {
                    await Task.Delay((DEC * 100) + 100);
                    if (IsPlaying) { STOPS(); }
                }
            }
            await Task.Delay(1);
        }

        public async void STOPS()
        {
            if (RELL != 0)
            {
                for (int i = 0; i < RELL; i++)
                {
                    try
                    {
                        waveOut.Volume = waveOut.Volume - RELEASER((int)RELL);
                        await Task.Delay(100);
                    }
                    catch (Exception) { }
                }
            }
            waveOut.Stop();
            await Task.Delay(1);
        }

        private float FrequencySelect(string KEY, float FREQ)
        {
            float Returner = 0;

            switch(KEY)
            {
                case "C4": Returner = 130.81f; break;
                    case "CS4": Returner = 138.59f; break;
                case "D4": Returner = 146.83f; break;
                    case "DS4": Returner = 155.56f; break;
                case "E4": Returner = 164.81f; break;
                case "F4": Returner = 174.61f; break;
                    case "FS4": Returner = 185.00f; break;
                case "G4": Returner = 196.00f; break;
                    case "GS4": Returner = 207.65f; break;
                case "A4": Returner = 220.00f; break;
                    case "AS4": Returner = 233.08f; break;
                case "B4": Returner = 246.94f; break;

                case "C5": Returner = 261.63f; break;
                    case "CS5": Returner = 277.18f; break;
                case "D5": Returner = 293.66f; break;
                    case "DS5": Returner = 311.13f; break;
                case "E5": Returner = 329.63f; break;
                case "F5": Returner = 349.23f; break;
                    case "FS5": Returner = 369.99f; break;
                case "G5": Returner = 392.00f; break;
                    case "GS5": Returner = 415.30f; break;
                case "A5": Returner = 440.00f; break;
                    case "AS5": Returner = 466.16f; break;
                case "B5": Returner = 493.88f; break;

                case "C6": Returner = 532.25f; break;
                    case "CS6": Returner = 554.37f; break;
                case "D6": Returner = 587.33f; break;
                    case "DS6": Returner = 622.25f; break;
                case "E6": Returner = 659.25f; break;
                case "F6": Returner = 698.46f; break;
                    case "FS6": Returner = 739.99f; break;
                case "G6": Returner = 783.99f; break;
                    case "GS6": Returner = 830.61f; break;
                case "A6": Returner = 880.00f; break;
                    case "AS6": Returner = 932.33f; break;
                case "B6": Returner = 987.77f; break;

            }
            if (FREQ > 5)
            { Returner += ((FREQ - 5) * 16); }
            if (FREQ < 5)
            { Returner -= Math.Abs((FREQ - 5) * 16); }
            return Returner;
        }

        private float RELEASER(int REL)
        {
            float Returner = 1.0f;
            switch (REL)
            {
                case 1: Returner = 0.01f; break;
                case 2: Returner = 0.009f; break;
                case 3: Returner = 0.008f; break;
                case 4: Returner = 0.007f; break;
                case 5: Returner = 0.006f; break;
                case 6: Returner = 0.005f; break;
                case 7: Returner = 0.004f; break;
                case 8: Returner = 0.003f; break;
                case 9: Returner = 0.002f; break;
                case 10:Returner = 0.001f; break;
            }
            return Returner;
        }
    }
}
