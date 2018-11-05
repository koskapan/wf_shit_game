using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Game_
{
    public partial class Form1 : Form
    {
        //int StarsCount = 1000;
        int SplitPanelHeight = 100;
        Random X = new Random();
        Random Y = new Random();        
        SolidBrush p = new SolidBrush(Color.Gray);
        SolidBrush pr = new SolidBrush(Color.Red);
        public Label GameTimeLabel = new Label();
        int Seconds = 0;
        int Minutes = 0;
        int BattleShipX = 100;
        int BattleShipY = 100;
        int ResTime = 0;
        //int ShieldResTime = 0;
        public meteor Meteor;
        public laser Laser;
        bool failed = false;
        bool MeteorsExist = false;
        bool ShieldRestore = false;
        int Score = 0;
        int MeteorSpeed = 1;
        int LaserLvl = 1;
        int MaxLaserLvl = 3;
        int NRJrestoreVal = 1;
        int BSWidth = 106;
        int BSHeight = 139;
        int Razbros = 30;
        int BonusSize = 40;
        int LaserHeight = 9;
        int LaserWidth = 37;
        int BSExplodeStd = 0;
        //int MeteorDesState = 0;
        int BonusRate = 200;
        int NRJMax = 100;
        int BonusScore = 100;
        int NRJBonusCoast = 20;
        int DamageRange = 15;
        int i = 0;
        Random MCol = new Random();
        int BonusMeteor = 0;
        //bool done = false;
        //public meteor Ob;

        public class laser : PictureBox { }

        public class meteor : PictureBox { }

        public void Destroy(meteor met, EventArgs e)
        {
            
               try
                {
                    met.BackgroundImage = Image.FromFile("explode.jpg");
                    met.BackgroundImageLayout = ImageLayout.Stretch;
                    AsteroidDestroy.Enabled = true;
                }
                catch { };
                met.Dispose();
            
        }

        public void Damage(int Val)
        {
            //ShieldResTime = 0;
            if (progressBar2.Value > Val)
                //progressBar2.Value -= Val;
                Discharge(progressBar2, Val);
            else
            {
                int ost = Val - progressBar2.Value;
                //progressBar2.Value = 0;
                Discharge(progressBar2, progressBar2.Value);
                if (progressBar1.Value > Val)
                {
                    if (ost != 0)
                        //progressBar1.Value -= ost;
                        Discharge(progressBar1, ost);
                    else
                        //progressBar1.Value -= Val;
                        Discharge(progressBar1, Val);
                }
                else
                {
                    //progressBar1.Value = 0;
                    
                    Discharge(progressBar1, progressBar1.Value);
                    failed = true;
                    FailTimer.Enabled = true;
                    if (MessageBox.Show("Game Over \r\n your Score is: " + Score.ToString(), "Time:" + GameTimeLabel.Text, MessageBoxButtons.OK) == DialogResult.OK)
                    {
                        this.Dispose();
                        this.Close();
                    }
                    

                }
                ost = 0;
            }

        }

        public void Discharge(ProgressBar ob, int val)
        {
            while (val > 0)
            {
                ob.Value--;
                //System.Threading.Thread.Sleep(10);
                val--;
            }
        }

        public void AddLaser(int X, int Y)
        {
            Laser = new laser();
            Laser.Height = LaserHeight;
            Laser.Width = LaserWidth;
            Laser.Top = Y;
            Laser.Left = X - Laser.Width;
            splitContainer1.Panel2.Controls.Add(Laser);
            Laser.BackColor = Color.Red;
            try
            {
                Laser.BackgroundImage = Image.FromFile("blast.jpg");
                Laser.BackgroundImageLayout = ImageLayout.Stretch;
            }
            catch { };
        }

        public void Shoot()
        {
            
            for (int i = 0; i <= LaserLvl - 1; i++)
            {
                AddLaser(BattleShipX, BattleShip.Top + i*15);
                AddLaser(BattleShipX, BattleShip.Top+BattleShip.Height - i*15);
            }
            //NRJrestore.Enabled = false;
            ResTime = 0;
            LaserTime.Enabled = true;

            if (progressBar3.Value >= 10)
                //progressBar3.Value -= 10;
                Discharge(progressBar3, 10);
            else
            {
                //progressBar3.Value = 0;
                Discharge(progressBar3, progressBar3.Value);
            }
        }

        public void AddMeteor(int top, int left, int Size)
        {
            //Panel Meteor = new Panel();
            BonusMeteor = MCol.Next() % BonusRate;
            Meteor = new meteor(); 
            Meteor.Height = Meteor.Width = Size;

            switch (BonusMeteor)
            {
                case 3:
                    {
                        try
                        {
                            Meteor.BackgroundImage = Image.FromFile("weaponBonus.jpg");
                            Meteor.BackgroundImageLayout = ImageLayout.Stretch;
                        }
                        catch { };
                        Meteor.BackColor = Color.Blue;
                        Meteor.Height = Meteor.Width = BonusSize;
                        break;
                    }
                case 2:
                    {
                        try
                        {
                            Meteor.BackgroundImage = Image.FromFile("NRJ.jpg");
                            Meteor.BackgroundImageLayout = ImageLayout.Stretch;
                        }
                        catch { };
                        Meteor.BackColor = Color.Lime;
                        Meteor.Height = Meteor.Width = BonusSize;
                        break;
                    }
                default:
                    {
                        try
                        {
                            Meteor.BackgroundImage = Image.FromFile("meteor.jpg");
                            Meteor.BackgroundImageLayout = ImageLayout.Stretch;
                        }
                        catch { };
                        Meteor.BackColor = Color.Brown;
                        break;
                    }
            }
            splitContainer1.Panel2.Controls.Add(Meteor);
            Meteor.Top = top;
            Meteor.Left = left;
            MeteorTime.Enabled = true;
            Meteor.Visible = true;
            MeteorsExist = true;
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            panel1.Width = this.Width;
            panel1.Height = this.Height;
            panel1.Left = 0;
            panel1.Top = 0;
            button1.Left = (panel1.Width / 2) - button1.Width/2;
            button2.Left = (panel1.Width / 2) - button2.Width / 2;
            button1.Top = panel1.Height / 5;
            button2.Top = button1.Top + button1.Height + 10;
            panel1.BackColor = Color.Black;
            label4.Top = button2.Top + button2.Height + 10;
            label4.Left = (label4.Width / 2) - label4.Width / 2;
            progressBar3.Maximum = NRJMax;
            progressBar3.Value = NRJMax;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Dispose();
            this.Close();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            splitContainer1.Visible = true;
            GameTime.Enabled = true;
            splitContainer1.Height = this.Height;
            splitContainer1.Width = this.Width;
            splitContainer1.Left = 0;
            splitContainer1.Top = 0;
            splitContainer1.SplitterDistance = SplitPanelHeight;
            button3.Left = 0;
            button3.Top = 0;
            button3.Height = splitContainer1.SplitterDistance;
            button3.Width = splitContainer1.SplitterDistance;
            splitContainer1.Panel1.BackColor = Color.Gray;            
            /*for (int i = 0; i <= StarsCount - 1; i++)
            {
                Graphics gPanel = splitContainer1.Panel2.CreateGraphics();
                gPanel.FillEllipse(p, new Rectangle(X.Next() % splitContainer1.Width, Y.Next() % (splitContainer1.Height - splitContainer1.SplitterDistance), 2, 2));
                
            }*/
            progressBar1.Left = splitContainer1.Width - progressBar1.Width;
            progressBar1.Top = progressBar3.Top = 0;
            progressBar2.Height = progressBar1.Height = splitContainer1.SplitterDistance/2;
            progressBar2.Left = splitContainer1.Width - progressBar1.Width;
            progressBar2.Top = progressBar1.Height;
            progressBar3.Height = splitContainer1.SplitterDistance;
            progressBar3.Left = splitContainer1.Width - progressBar2.Width - progressBar3.Width;
            label3.Left = progressBar3.Left + progressBar3.Width / 2 - label3.Width / 2;
            label3.Top = progressBar3.Height / 2 - label3.Height / 2;
            label3.BackColor = progressBar3.ForeColor;
            label1.Left = progressBar2.Left + progressBar2.Width / 2 - label1.Width / 2;
            label2.Left = progressBar1.Left + progressBar1.Width / 2 - label2.Width / 2;
            label1.Top = progressBar2.Top + progressBar2.Height / 2 - label1.Height / 2;
            label2.Top = progressBar1.Top + progressBar1.Height / 2 - label2.Height / 2;
            label1.BackColor = progressBar2.ForeColor;
            label2.BackColor = progressBar1.ForeColor;
            GameTimeLabel.Font = new System.Drawing.Font("Monotype Corsiva", 36F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            GameTimeLabel.AutoSize = true;
            GameTimeLabel.Top = splitContainer1.SplitterDistance / 2 - GameTimeLabel.Height / 2;
            GameTimeLabel.Left = button3.Left + button3.Width + 30;
            GameTimeLabel.ForeColor = Color.Yellow;
            splitContainer1.Panel1.Controls.Add(GameTimeLabel);
            GameScore.Top = splitContainer1.SplitterDistance / 2 - GameScore.Height/2;
            GameScore.Left = GameTimeLabel.Left + GameTimeLabel.Width + 200;
            GameScore.AutoSize = true;
            GameScore.Font = new System.Drawing.Font("Monotype Corsiva", 36F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            GameScore.ForeColor = Color.Yellow;
            MeteorResp.Enabled = true;
            if (MeteorsExist) MeteorTime.Enabled = true;
            BattleShip.Width = BSWidth;
            BattleShip.Height = BSHeight;
            BattleShip.BackColor = Color.White;
            try
            {
                BattleShip.BackgroundImage = Image.FromFile("BS.jpg");
                BattleShip.BackgroundImageLayout = ImageLayout.Stretch;
            }
            catch { };
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            panel1.Visible = true;
            splitContainer1.Visible = false;
            GameTime.Enabled = false;
            MeteorResp.Enabled = false;
            MeteorTime.Enabled = false;
        }

        private void GameTime_Tick_1(object sender, EventArgs e)
        {
            if (Seconds < 59)
            {
                Seconds++;
                if ((Seconds == 20) || (Seconds == 40))
                {
                    MeteorSpeed++;
                    if (MeteorResp.Interval > 100) 
                        MeteorResp.Interval -= 100;
                    
                }
            }
            else
            {
                Seconds = 0;
                Minutes++;
                MeteorSpeed++;
                if (MeteorResp.Interval > 100)
                    MeteorResp.Interval -= 100;
            }
            GameTimeLabel.Text = Minutes.ToString() + ":" + Seconds.ToString();
            
        }

        private void splitContainer1_Panel2_MouseMove(object sender, MouseEventArgs e)
        {
            BattleShipX = e.X;
            BattleShipY = e.Y;
            BattleShip.Top = e.Y + 10;
            BattleShip.Left = e.X + 10;
           
        }

        private void splitContainer1_Panel2_MouseClick(object sender, MouseEventArgs e)
        {
            if (progressBar3.Value != 0)
            {
                if (e.Button == MouseButtons.Left)
                    Shoot();
                if (e.Button == MouseButtons.Right)
                {
                    if (progressBar2.Value < progressBar2.Maximum)
                        ShieldRestore = true;
                    else
                        ShieldRestore = false;
                }
            }
        }

        private void NRJrestore_Tick(object sender, EventArgs e)
        {
            if (progressBar3.Value < progressBar3.Maximum - NRJrestoreVal)
            {
                if (ResTime >= 100)
                    progressBar3.Value += NRJrestoreVal;
            }
            else
                ResTime = 0;
            
            if ((progressBar2.Value < progressBar2.Maximum) && (ShieldRestore))
            {
                //if (ShieldResTime >= 500)
                    if (progressBar3.Value > progressBar3.Minimum)
                    {
                        progressBar2.Value++;
                        progressBar3.Value--;
                        ResTime = 0;
                    }
                    else
                    {
                        ShieldRestore = false;
                        //ResTime = 0;
                        //ShieldResTime = 0;
                    }
            }
            else
                ShieldRestore = false;
                //ShieldResTime = 0;
        }

        private void TimeFORrestore_Tick(object sender, EventArgs e)
        {
            ResTime++;
            //ShieldResTime++;
            /*if (ResTime == 100)
            {
                NRJrestore.Enabled = true;
                ResTime = 0;
            }*/
        }

        private void ChangeNRJcolor_Tick(object sender, EventArgs e)
        {
            if (progressBar3.Value <= progressBar3.Maximum * 0.6)
            {
                if (progressBar3.Value <= progressBar3.Maximum * 0.3)
                {
                    progressBar3.ForeColor = Color.Red;
                    label3.BackColor = Color.Gray;
                }
                else
                {
                    progressBar3.ForeColor = Color.Orange;
                    label3.BackColor = progressBar3.ForeColor;
                }
            }
            else
            {
                progressBar3.ForeColor = Color.Lime;
                label3.BackColor = progressBar3.ForeColor;
            }

        }

        private void MeteorTime_Tick(object sender, EventArgs e)
        {

            foreach (Control control in splitContainer1.Panel2.Controls)
            {
                meteor tb = control as meteor;
                if (tb != null)
                {
                    if (tb.Left + tb.Width + 10 < splitContainer1.Width)
                        tb.Left += MeteorSpeed;
                    else
                        tb.Dispose();
                    if
                    (
                       (
                            (
                                (tb.Top <= BattleShip.Top)
                                ||
                                (tb.Top <= BattleShip.Top+BattleShip.Height)
                             )
                            &&
                            (
                                (tb.Top + Meteor.Height >= BattleShip.Top)
                                ||
                                (tb.Top + Meteor.Height >= BattleShip.Top+BattleShip.Height)
                            )
                       )
                        &&
                        (
                            (
                                (tb.Left <= BattleShip.Left)
                                ||
                                (tb.Left <= BattleShip.Left+BattleShip.Width)
                            )
                            &&
                            (
                                (tb.Left + Meteor.Width >= BattleShip.Left)
                                ||
                                (tb.Left + Meteor.Width >= BattleShip.Left+BattleShip.Width)
                             )
                        )
                    )
                    {
                        int Wid = tb.Width;
                        Destroy(tb, e);
                        if (!failed)
                        {
                                    
                            if (!tb.Disposing)
                            {
                                if (tb.BackColor == Color.Brown)
                                    Damage(Wid);
                                if (tb.BackColor == Color.Blue)
                                {
                                    if (LaserLvl < MaxLaserLvl) LaserLvl++;
                                    Score += BonusScore;
                                }
                                if (tb.BackColor == Color.Lime)
                                {
                                    progressBar3.Maximum += NRJBonusCoast;
                                    progressBar3.Value = progressBar3.Maximum;
                                    Score += BonusScore;
                                    NRJrestoreVal++;
                                }
                            }
                        }
                    }

                }
            }
            
        }

        private void MeteorResp_Tick(object sender, EventArgs e)
        {
            Random MY = new Random();
            Random Size = new Random();
            AddMeteor(splitContainer1.SplitterDistance + MY.Next() % (splitContainer1.Height - splitContainer1.SplitterDistance),0,10+Size.Next()%60);
            
        }

        private void LaserTime_Tick(object sender, EventArgs e)
        {
            
            foreach (Control control in splitContainer1.Panel2.Controls)
            {
                
                laser tb = control as laser;
                if (tb != null)
                {
                    if (tb.Left > 0)
                        tb.Left -= 10;
                    else
                        tb.Dispose();
                    foreach (Control control1 in splitContainer1.Panel2.Controls)
                    {
                        meteor tb1 = control1 as meteor;
                        if (tb1 != null)
                        {
                            if (MeteorsExist)
                                if
                                (
                                    (
                                        (tb1.Top-DamageRange <= tb.Top)
                                        &&
                                        (tb1.Top + tb1.Height + DamageRange >= tb.Top + tb.Height)

                                    )
                                     &&
                                    (
                                        (tb1.Left <= tb.Left)
                                        &&
                                        (tb1.Left + tb1.Width >= tb.Left)
                                    )
                                )
                                {
                                    tb.Dispose();
                                    if (tb1.Height > 20)
                                    {
                                        int x = tb1.Left;
                                        int y = tb1.Top;
                                        int h = tb1.Height;
                                        Destroy(tb1, e);
                                        Random Ry = new Random();
                                        Random Rx = new Random();
                                        if (tb1.BackColor == Color.Brown)
                                        if (tb1.Height < 50)
                                        {
                                            AddMeteor(y + Ry.Next() % Razbros, x + Rx.Next() % Razbros, h / 2);
                                            AddMeteor(y + h + Ry.Next() % Razbros, x + Rx.Next() % Razbros, h / 2);
                                        }
                                        else
                                        {

                                            AddMeteor(y + Ry.Next() % Razbros, x + Rx.Next() % Razbros, h / 2);
                                            AddMeteor(y - h + Ry.Next() % Razbros, x + Rx.Next() % Razbros, h / 2);
                                            AddMeteor(y + h + Ry.Next() % Razbros, x + Rx.Next() % Razbros, h / 2);
                                        }
                                    }
                                    else
                                    {
                                        Destroy(tb1, e);
                                        Score += tb1.Width;
                                    }

                                }
                        }
                    }
                }
            }
        }
        
        private void timer1_Tick(object sender, EventArgs e)
        {
            GameScore.Text = Score.ToString();
            
        }

        private void FailTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                switch (BSExplodeStd)
                {
                    case 0:
                        {
                            try
                            {
                                BattleShip.BackgroundImage = Image.FromFile("explode.jpg");
                                BattleShip.BackgroundImageLayout = ImageLayout.Stretch;
                                
                            }
                            catch { };
                            break;
                        }
                    case 1:
                        {
                            try
                            {
                                BattleShip.BackgroundImage = Image.FromFile("explodePt2.jpg");
                                BattleShip.BackgroundImageLayout = ImageLayout.Stretch;

                            }
                            catch { };
                            break;
                        }
                    case 2:
                        {
                            try
                            {
                                BattleShip.BackgroundImage = Image.FromFile("explodePt3.jpg");
                                BattleShip.BackgroundImageLayout = ImageLayout.Stretch;

                            }
                            catch { };
                            break;
                        }
                    case 3:
                        {
                            try
                            {
                                BattleShip.BackgroundImage = Image.FromFile("explodePt4.jpg");
                                BattleShip.BackgroundImageLayout = ImageLayout.Stretch;

                            }
                            catch { };
                            break;
                        }
                    case 4:
                        {
                            try
                            {
                                BattleShip.Dispose();

                            }
                            catch { };
                            break;
                        }

                }

            }
            catch
            { };
            BSExplodeStd++;
        }

        private void AsteroidDestroy_Tick(object sender, EventArgs e)
        {
            //done = true;
            //AsteroidDestroy.Enabled = false;
            
            /*try
            {
                switch (MeteorDesState)
                {
                    case 0:
                        {
                            try
                            {
                                Ob.BackgroundImage = Image.FromFile("explode.jpg");
                                Ob.BackgroundImageLayout = ImageLayout.Stretch;
                            }
                            catch { }
                            break;
                        }
                    case 1:
                        {

                            Ob.Dispose();

                            break;
                        }
                }
                MeteorDesState++;
            }
            catch { }*/
        }


        /*private void AsteroidDestroy_Tick(object sender, EventArgs e, meteor Ob)
        {
            try
            {
                switch (MeteorDesState)
                {
                    case 0:
                        {
                            try
                            {
                                Ob.BackgroundImage = Image.FromFile("explode.jpg");
                                Ob.BackgroundImageLayout = ImageLayout.Stretch;
                            }
                            catch { }
                            break;
                        }
                    case 1:
                        {

                            Ob.Dispose();

                            break;
                        }
                    case 2:
                        {
                            AsteroidDestroy.Enabled = false;
                            MeteorDesState = 0;
                            break;
                        }
                }
                MeteorDesState++;
            }
            catch { }
        }*/
    }
}
