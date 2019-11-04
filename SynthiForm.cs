using NAudio.Wave;
using Synthi.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Synthi
{
    public partial class SynthiForm : Form
    {
        private List<Sound> SList = new List<Sound>();
        private List<Sound> SeqNotes = new List<Sound>();
        private List<PictureBox> SeqVisNotes = new List<PictureBox>();
        private bool SEQ = false;
        private bool SeqMouseDown = false;
        private bool SeqResize = false;
        private bool SeqMove = false;
        private bool SeqPlaying = false;
        private int SeqNoteCount = 0;
        public SynthiForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.Height = 500;
        }

        private async void ShowSeqBTN_Click(object sender, EventArgs e)
        {
            if (SEQ == false)
            {
                SEQ = true;
                ShowSeqBTN.Text = "/\\";
                while (this.Height != 900)
                { this.Height += 25; }
            }
            else if (SEQ == true)
            {
                SEQ = false;
                ShowSeqBTN.Text = "\\/";
                while (this.Height != 500)
                { this.Height -= 25; }
            }
            await Task.Delay(3);
        }

        private async void KeyboardKeyDown(object sender, MouseEventArgs e)
        {
            Button KEY = (Button)sender;
            MakeSound(KEY, AmpTB.Value, FreqTB.Value, ReleaseTB.Value, DecayTB.Value, VolumeTB.Value, AttackTB.Value);
            await Task.Delay(3);
        }

        private async void MakeSound(Button KEY, int AMP, int FREQ, int REL, int DEC, int VOL, int ATK)
        {
            bool check = false;
            if (KEY.Text.EndsWith("#"))
            { KEY.BackColor = Color.Gray; }
            else
            { KEY.BackColor = Color.LightGray; }
            foreach (Sound SS in SList)
            {
                if (SS.KEYY == KEY) { check = true; SS.FREQQ = FREQ ; SS.PLAYS(REL, ATK, (float)AMP, DEC, (float)VOL); }
            }
            if (check == false)
            {
                Sound S = new Sound(KEY, (float)AMP, (float)FREQ, REL, (float)VOL);
                SList.Add(S);
                S.PLAYS(REL, ATK, (float)AMP, DEC, (float)VOL);
            }
            await Task.Delay(3);
        }

        private async void KeyboardPCKeyDown(object sender, KeyEventArgs e)
        {
            if (PCKeysCB.Checked)
            {
                Button Returner = null;
                if (e.KeyCode == Keys.Y) { Returner = C4; }
                            if (e.KeyCode == Keys.S) { Returner = CS4; }
                if (e.KeyCode == Keys.X) { Returner = D4; }
                            if (e.KeyCode == Keys.D) { Returner = DS4; }
                if (e.KeyCode == Keys.C) { Returner = E4; }
                if (e.KeyCode == Keys.V) { Returner = F4; }
                            if (e.KeyCode == Keys.G) { Returner = FS4; }
                if (e.KeyCode == Keys.B) { Returner = G4; }
                            if (e.KeyCode == Keys.H) { Returner = GS4; }
                if (e.KeyCode == Keys.N) { Returner = A4; }
                            if (e.KeyCode == Keys.J) { Returner = AS4; }
                if (e.KeyCode == Keys.M) { Returner = B4; }

                if (e.KeyCode == Keys.Oemcomma) { Returner = C5; }
                            if (e.KeyCode == Keys.L) { Returner = CS5; }
                if (e.KeyCode == Keys.OemPeriod) { Returner = D5; }
                            if (e.KeyCode == Keys.Oemtilde) { Returner = DS5; }
                if (e.KeyCode == Keys.OemMinus) { Returner = E5; }
                if (e.KeyCode == Keys.Q) { Returner = F5; }
                            if (e.KeyCode == Keys.D2) { Returner = FS5; }
                if (e.KeyCode == Keys.W) { Returner = G5; }
                            if (e.KeyCode == Keys.D3) { Returner = GS5; }
                if (e.KeyCode == Keys.E) { Returner = A5; }
                            if (e.KeyCode == Keys.D4) { Returner = AS5; }
                if (e.KeyCode == Keys.R) { Returner = B5; }

                if (e.KeyCode == Keys.T) { Returner = C6; }
                            if (e.KeyCode == Keys.D6) { Returner = CS6; }
                if (e.KeyCode == Keys.Z) { Returner = D6; }
                            if (e.KeyCode == Keys.D7) { Returner = DS6; }
                if (e.KeyCode == Keys.U) { Returner = E6; }
                if (e.KeyCode == Keys.I) { Returner = F6; }
                            if (e.KeyCode == Keys.D9) { Returner = FS6; }
                if (e.KeyCode == Keys.O) {Returner = G6; }
                            if (e.KeyCode == Keys.D0) { Returner = GS6; }
                if (e.KeyCode == Keys.P) { Returner = A6; }
                            if (e.KeyCode == Keys.OemOpenBrackets) { Returner = AS6; }
                if (e.KeyCode == Keys.Oem1) { Returner = B6; }
                if (Returner != null) { MakeSound(Returner, AmpTB.Value, FreqTB.Value, ReleaseTB.Value, DecayTB.Value, VolumeTB.Value, AttackTB.Value); }
            }
            await Task.Delay(3);
        }

        private async void KeyboardPCKeyUp(object sender, KeyEventArgs e)
        {
            foreach(Sound S in SList)
            {
                if (e.KeyCode.ToString() == S.KEYY.Tag.ToString() && S.IsPlaying) { S.IsPlaying = false; S.STOPS();
                    if (S.KEYY.Text.Contains("#"))
                    { S.KEYY.BackColor = Color.DimGray; }
                    else
                    { S.KEYY.BackColor = Color.Silver; }
                }
            }
            await Task.Delay(3);
        }

        private async void KeyboardKeyUp(object sender, MouseEventArgs e)
        {
            Button K = (Button)sender;
            foreach (Sound S in SList)
            {
                if (K == S.KEYY && S.IsPlaying) { S.IsPlaying = false; S.STOPS();
                    if (S.KEYY.Text.Contains("#"))
                    { S.KEYY.BackColor = Color.DimGray; }
                    else
                    { S.KEYY.BackColor = Color.Silver; }
                }
            }
            await Task.Delay(3);
        }

        private async void SliderMoved(object sender, EventArgs e)
        {
            TrackBar TB = (TrackBar)sender;
            if(TB.Name == "FreqTB")
            {
                float Returner = 440.00f;
                if (FreqTB.Value > 5)
                { Returner += ((FreqTB.Value - 5) * 16); }
                if (FreqTB.Value < 5)
                { Returner -= Math.Abs((FreqTB.Value - 5) * 16); }
                FreqVisLabel.Text = Returner.ToString();
            }
            if (TB.Name == "AmpTB") { AmpVisLabel.Text = (AmpTB.Value * 10).ToString(); }
            if (TB.Name == "DecayTB") { DecayVisLabel.Text = (DecayTB.Value * 10).ToString(); }
            if (TB.Name == "VolumeTB") { VolumeVisLabel.Text = ((float)VolumeTB.Value * 10f).ToString(); }
            if (TB.Name == "AttackTB") { AttackVisLabel.Text = (AttackTB.Value * 10).ToString(); }
            if (TB.Name == "ReleaseTB") { ReleaseVisLabel.Text = (ReleaseTB.Value * 10).ToString(); }

            await Task.Delay(3);
        }

        private async void SeqMouseDClick(object sender, MouseEventArgs e)
        {
            MouseEventArgs E = (MouseEventArgs)e;
            Panel P = (Panel)sender;
            bool placed = false;
            int seqer = 0;
            int YY = 0;
            int XX = 0;
            int NOTE = 0;

            while (!placed)
            {
                if (E.Location.Y >= seqer)
                {
                    if (E.Location.Y <= (seqer + 10))
                    {
                        YY = seqer;
                        placed = true;
                    }
                }
                if (!placed)
                { NOTE += 1; }
                seqer += 10;
            }

            seqer = 0;
            placed = false;

            while (!placed)
            {
                if (E.Location.X >= seqer)
                {
                    if (E.Location.X <= (seqer + 20))
                    {
                        XX = seqer;
                        placed = true;
                    }
                }
                seqer += 20;
            }

            SeqMouseDown = true;
            PictureBox PB = new PictureBox();
            PB.Parent = P;
            PB.Height = 10;
            PB.Width = 20;
            PB.Location = new Point(XX, YY);
            PB.BackColor = Color.Aquamarine;
            PB.BorderStyle = BorderStyle.FixedSingle;
            PB.Tag = NoteTxt(PB);
            PB.Show();
            PB.MouseDown += new MouseEventHandler(RemoveSeqNote);
            PB.MouseMove += new MouseEventHandler(SeqMouseChange);
            PB.MouseDown += new MouseEventHandler(SeqResizeMoveStart);
            PB.MouseMove += new MouseEventHandler(SeqResizeMove);
            PB.MouseUp += new MouseEventHandler(SeqResizeMoveEnd);
            SeqVisNotes.Add(PB);
            Sound S = new Sound(null, (float)AmpTB.Value, (float)FreqTB.Value, ReleaseTB.Value, (float)VolumeTB.Value);
            S.KNAME = PB.Tag.ToString();
            S.PARENTP = PB;
            SeqNotes.Add(S);
            SeqMouseDown = false;
            SeqNoteCount += 1;
            await Task.Delay(3);
        }

        private String NoteTxt(PictureBox PB)
        {
            int LOC = PB.Location.Y;
            String Returner = "";
            if (LOC == 0) { Returner = "B6"; PB.BackColor = Color.Aquamarine; }
            if (LOC == 10) { Returner = "AS6"; PB.BackColor = Color.Aqua; }
            if (LOC == 20) { Returner = "A6"; PB.BackColor = Color.Aquamarine; }
            if (LOC == 30) { Returner = "GS6"; PB.BackColor = Color.Aqua; }
            if (LOC == 40) { Returner = "G6"; PB.BackColor = Color.Aquamarine; }
            if (LOC == 50) { Returner = "FS6"; PB.BackColor = Color.Aquamarine; }
            if (LOC == 60) { Returner = "F6"; PB.BackColor = Color.Aqua; }
            if (LOC == 70) { Returner = "E6"; PB.BackColor = Color.Aquamarine; }
            if (LOC == 80) { Returner = "DS6"; PB.BackColor = Color.Aqua; }
            if (LOC == 90) { Returner = "D6"; PB.BackColor = Color.Aquamarine; }
            if (LOC == 100) { Returner = "CS6"; PB.BackColor = Color.Aqua; }
            if (LOC == 110) { Returner = "C6"; PB.BackColor = Color.Aquamarine; }
            if (LOC == 120) { Returner = "B5"; PB.BackColor = Color.Aquamarine; }
            if (LOC == 130) { Returner = "AS5"; PB.BackColor = Color.Aqua; }
            if (LOC == 140) { Returner = "A5"; PB.BackColor = Color.Aquamarine; }
            if (LOC == 150) { Returner = "GS5"; PB.BackColor = Color.Aqua; }
            if (LOC == 160) { Returner = "G5"; PB.BackColor = Color.Aquamarine; }
            if (LOC == 170) { Returner = "FS5"; PB.BackColor = Color.Aquamarine; }
            if (LOC == 180) { Returner = "F5"; PB.BackColor = Color.Aqua; }
            if (LOC == 190) { Returner = "E5"; PB.BackColor = Color.Aquamarine; }
            if (LOC == 200) { Returner = "DS5"; PB.BackColor = Color.Aqua; }
            if (LOC == 210) { Returner = "D5"; PB.BackColor = Color.Aquamarine; }
            if (LOC == 220) { Returner = "CS5"; PB.BackColor = Color.Aqua; }
            if (LOC == 230) { Returner = "C5"; PB.BackColor = Color.Aquamarine; }
            if (LOC == 240) { Returner = "B4"; PB.BackColor = Color.Aquamarine; }
            if (LOC == 250) { Returner = "AS4"; PB.BackColor = Color.Aqua; }
            if (LOC == 260) { Returner = "A4"; PB.BackColor = Color.Aquamarine; }
            if (LOC == 270) { Returner = "GS4"; PB.BackColor = Color.Aqua; }
            if (LOC == 280) { Returner = "G4"; PB.BackColor = Color.Aquamarine; }
            if (LOC == 290) { Returner = "FS4"; PB.BackColor = Color.Aquamarine; }
            if (LOC == 300) { Returner = "F4"; PB.BackColor = Color.Aqua; }
            if (LOC == 310) { Returner = "E4"; PB.BackColor = Color.Aquamarine; }
            if (LOC == 320) { Returner = "DS4"; PB.BackColor = Color.Aqua; }
            if (LOC == 330) { Returner = "D4"; PB.BackColor = Color.Aquamarine; }
            if (LOC == 340) { Returner = "CS4"; PB.BackColor = Color.Aqua; }
            if (LOC == 350) { Returner = "C4"; PB.BackColor = Color.Aquamarine; }
            return Returner;
        }

        private async void RemoveSeqNote(object sender, MouseEventArgs e)
        {
            PictureBox P = (PictureBox)sender;
            Sound SS = null;
            MouseEventArgs E = (MouseEventArgs)e;
            if (E.Button == MouseButtons.Right)
            { 
                foreach (Sound S in SeqNotes)
                {
                    if (S.PARENTP == P)
                    {
                        SS = S;
                    }
                }
                SeqNotes.Remove(SS);
                P.Dispose(); SeqNoteCount -= 1;
            }
            await Task.Delay(3);
        }

        private async void SeqMouseChange(object sender, MouseEventArgs e)
        {
            PictureBox P = (PictureBox)sender;
            MouseEventArgs E = (MouseEventArgs)e;
            if (E.Location.X >= P.Width - 5 && E.Location.X <= P.Width && !SeqMouseDown)
            { Cursor.Current = Cursors.SizeWE; SeqMove = false; SeqResize = true; }
            else if (E.Location.X <= P.Width - 5 && E.Location.X >= 0 && !SeqMouseDown)
            { SeqMove = true; SeqResize = false; }
            await Task.Delay(3);
        }

        private async void SeqResizeMoveStart(object sender, MouseEventArgs e)
        {
            PictureBox P = (PictureBox)sender;
            MouseEventArgs E = (MouseEventArgs)e;
            if (E.Button == MouseButtons.Left && SeqResize)
            {
                Cursor.Current = Cursors.SizeWE;
                SeqMouseDown = true;
            }
            if (E.Button == MouseButtons.Left && SeqMove)
            {
                Cursor.Current = Cursors.SizeAll;
                SeqMouseDown = true;
            }
            await Task.Delay(3);
        }

        private async void SeqResizeMove(object sender, MouseEventArgs e)
        {
            PictureBox P = (PictureBox)sender;
            if (SeqMouseDown && SeqResize)
            {
                if (e.Location.X >= P.Width)
                {
                    P.Width += 20;
                }
                if (e.Location.X <= P.Width - 8 && P.Width != 20)
                {
                    P.Width -= 20;
                }
            }
            if (SeqMouseDown && SeqMove)
            {
                if (e.Location.X > P.Width && P.Location.X + P.Width != SeqPanel.Width)
                {
                    P.Location = new Point(P.Location.X + 20, P.Location.Y);
                }
                if (e.Location.X < 0 && P.Location.X != 0)
                {
                    P.Location = new Point(P.Location.X - 20, P.Location.Y);
                }

                if (e.Location.Y > P.Height && P.Location.Y + P.Height != SeqPanel.Height)
                {
                    P.Location = new Point(P.Location.X, P.Location.Y + 10);
                }
                if (e.Location.Y < 0 && P.Location.Y != 0)
                {
                    P.Location = new Point(P.Location.X, P.Location.Y - 10);
                }
                P.Tag = NoteTxt(P);
                foreach (Sound S in SeqNotes)
                {
                    if (S.PARENTP == P)
                    {
                        S.KNAME = P.Tag.ToString();
                    }
                }

            }
            await Task.Delay(3);
        }

        private async void SeqResizeMoveEnd(object sender, MouseEventArgs e)
        {
            SeqMouseDown = false;
            SeqResize = false;
            SeqMove = false;
            await Task.Delay(3);
        }

        private async void SeqPlayBtn_Click(object sender, EventArgs e)
        {
            SeqProgBar.Maximum = SeqPanel.Width;
            int limit = 0;
            SeqPlaying = true;
            while (SeqPlaying)
            {
                if (SeqNoteCount == 0)
                {
                    limit = 40;
                }
                if (SeqNoteCount > 0)
                {
                    limit = 0;
                    foreach (PictureBox PB in SeqVisNotes)
                    {
                        if (SeqPlayBar.Location.X == PB.Location.X)
                        {
                            foreach (Sound S in SeqNotes)
                            {
                                if (S.PARENTP == PB)
                                {
                                    S.PLAYS(ReleaseTB.Value, AttackTB.Value, (float)AmpTB.Value, DecayTB.Value, (float)VolumeTB.Value);
                                }
                            }
                        }
                        else if (SeqPlayBar.Location.X == PB.Location.X + PB.Width)
                        {
                            foreach (Sound S in SeqNotes)
                            {
                                if (S.PARENTP == PB)
                                {
                                    S.STOPS();
                                    S.IsPlaying = false;
                                }
                            }
                        }
                        if (limit < PB.Location.X + PB.Width)
                        { limit = PB.Location.X + (PB.Width + 20); }
                    }
                }
                SeqPlayBar.Location = new Point(SeqPlayBar.Location.X + 1, 0);
                SeqProgBar.PerformStep();
                if (SeqPlayBar.Location.X >= limit)
                {
                    SeqPlayBar.Location = new Point(0, 0);
                    SeqProgBar.Value = 0;
                }
                await Task.Delay(1);
            }
        }

        private async void SeqPauseBtn_Click(object sender, EventArgs e)
        {
            SeqPlaying = false;
            foreach (Sound S in SeqNotes)
            {
                S.STOPS();
                S.IsPlaying = false;
            }
            await Task.Delay(3);
        }

        private async void SeqStopBtn_Click(object sender, EventArgs e)
        {
            SeqPlaying = false;
            foreach (Sound S in SeqNotes)
            {
                S.STOPS();
                S.IsPlaying = false;
            }
            SeqPlayBar.Location = new Point(0, 0);
            await Task.Delay(3);
        }
    }
}
