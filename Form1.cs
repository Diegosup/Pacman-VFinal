using System;
using System.Drawing;
using System.Windows.Forms;


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection.Emit;

namespace Pacman
{
    public partial class Form1 : Form
    {
        Bitmap bmp;
        Graphics g;
        byte[,] level = Class1.map0;
        byte[,] NewLevel1 = Class1.N0;//Guardar mapas para que no se borren 
        int coins = 0;
        int bonus = 0;
        int time,trespawn = 0;
        int xpos, ypos, count;
        int PGxpos, PGypos, RGxpos, RGypos, BGxpos,BGypos,YGxpos,YGypos;
        int NPCmove=0;
        int ranNum,ranNum1,ranNum2,ranNum3 = 0;
        
        bool goright, goleft, goup, godown;
        bool PGrespawn = true;
        bool RGrespawn = true;
        bool BGrespawn = true;
        bool YGrespawn = true;
        bool firstTime,supreme = false;

    

        public Form1()
        {
            InitializeComponent();
            bmp = new Bitmap(250, 250);
            PCT_CANVAS.Image = bmp;
            firstTime = true;
            DrawMap(level,supreme);
            coins = countCoins(Class1.map0);
            ranNum = generateRandomNumber();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        public int generateRandomNumber()
        {
            Random random = new Random();
            int randomSign = random.Next(2) == 0 ? -1 : 1;
            int randomNumber = randomSign * 1;
            return randomNumber;
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            
            
            if (NPCmove < 7)
            {
                ranNum1 = generateRandomNumber();
                if(PGrespawn)
                    Move(PGxpos, PGypos, PGxpos + ranNum, PGypos, 3);
                if (RGrespawn)
                    Move(RGxpos, RGypos, RGxpos, RGypos - ranNum1,4);
                if (YGrespawn)
                    Move(YGxpos, YGypos, YGxpos - ranNum2, YGypos, 5);
                if (BGrespawn)
                    Move(BGxpos, BGypos, BGxpos , BGypos + ranNum3, 6);

            }
            if (NPCmove > 7 && NPCmove < 14)
            {
                ranNum3 = generateRandomNumber();
                if (PGrespawn)
                    Move(PGxpos, PGypos, PGxpos , PGypos - ranNum3, 3);
                if (RGrespawn)
                    Move(RGxpos, RGypos, RGxpos - ranNum2, RGypos, 4);
                if (YGrespawn)
                    Move(YGxpos, YGypos, YGxpos + ranNum1, YGypos, 5);
                if (BGrespawn)
                    Move(BGxpos, BGypos, BGxpos - ranNum, BGypos, 6);
            }
            if(NPCmove >14 && NPCmove <21)
            {
                ranNum2 = generateRandomNumber();
                if (PGrespawn)
                    Move(PGxpos, PGypos, PGxpos - ranNum2, PGypos, 3);
                if (RGrespawn)
                    Move(RGxpos, RGypos, RGxpos, RGypos + ranNum3, 4);
                if (YGrespawn)
                    Move(YGxpos, YGypos, YGxpos, YGypos + ranNum1, 5);
                if (BGrespawn)
                    Move(BGxpos, BGypos, BGxpos, BGypos - ranNum, 6);
            }
            if (NPCmove>21 && NPCmove <28)
            {
                if(NPCmove==27)
                {
                    ranNum = generateRandomNumber();                
                    NPCmove = 0;                 
                }
                if (PGrespawn)
                    Move(PGxpos, PGypos, PGxpos , PGypos + ranNum3, 3);
                if (RGrespawn)
                    Move(RGxpos, RGypos, RGxpos + ranNum1, RGypos , 4);
                if (YGrespawn)
                    Move(YGxpos, YGypos, YGxpos , YGypos - ranNum, 5);
                if (BGrespawn)
                    Move(BGxpos, BGypos, BGxpos + ranNum2, BGypos, 6);

            }
            NPCmove++;
            if (goleft == true)
            {
                Move(xpos, ypos, xpos - 1, ypos,2);
            }
            if (goright == true)
            {
                Move(xpos, ypos, xpos + 1, ypos,2);
            }
            if (goup == true)
            {
                Move(xpos, ypos, xpos, ypos - 1,2);
            }
            if (godown == true)
            {
                Move(xpos, ypos, xpos, ypos + 1, 2);
            }

            

        }

        //Resetear mapas
        private void restart(byte[,] currentMap, byte[,] newMap)
        {
            for (int x = 0; x < currentMap.GetLength(1); x++)
            {
                for (int y = 0; y < currentMap.GetLength(1); y++)
                {
                    currentMap[y, x] = newMap[y, x];
                }
            }
            //Resetear SCORE y dibujar de nuevo el mapa
            count = 0;
            bonus = 0;
            BONUS.Text= bonus.ToString();
            Score.Text = count.ToString();
            PCT_CANVAS.Image = bmp;
            firstTime = true;
            DrawMap(currentMap,supreme);

        }

        private int countCoins(byte[,] currentMap)
        {
            int result=0;
            for (int x = 0; x < currentMap.GetLength(1); x++)
            {
                for (int y = 0; y < currentMap.GetLength(1); y++)
                {
                    if (currentMap[x, y] == 8)
                    {
                        result++;
                    }

                }
            }
            return result;
        }
        private void Level1_Click(object sender, EventArgs e)
        {
            coins = 0;
            restart(level, Class1.N0);
            coins= countCoins(Class1.N0);

        }

       

        private void Level2_Click(object sender, EventArgs e)
        {
            coins = 0;
            bonus = 0;
            restart(level, Class1.N1);
            coins= countCoins(Class1.N1);
        }

        private void Level3_Click(object sender, EventArgs e)
        {
            coins = 0;
            bonus = 0;
            restart(level, Class1.N2);
            coins= countCoins(Class1.N2);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left:
                    goleft = true;
                    goright = false;
                    godown = false;
                    goup = false;
                    break;
                case Keys.Right:
                    goright = true;
                    goleft = false;
                    godown = false;
                    goup = false;

                    break;
                case Keys.Up:
                    goup = true;
                    goleft = false;
                    godown = false;
                    goright = false;
                    break;
                case Keys.Down:
                    godown = true;
                    goleft = false;
                    goright = false;
                    goup = false;
                    break;
                case Keys.Space:
                    break;
            }
        }

        private void DrawMap(byte[,] map,bool isEatable)
        {

            g = Graphics.FromImage(bmp);
            g.Clear(Color.FromArgb(0, 3, 92));
            for (int x = 0; x < map.GetLength(1); x++)
            {
                for (int y = 0; y < map.GetLength(1); y++)
                {
                    

                    if (map[y, x] == 10)
                    {
                        g.FillRectangle(new SolidBrush(Color.Black), x * 10, y * 10, 10, 10);
                        g.DrawImage(Resource1.Boost, x * 10, y * 10, 10, 10);
                    }

                    if (map[y, x] == 3 )
                    {
                        if (isEatable)
                        {
                            g.FillRectangle(new SolidBrush(Color.Black), x * 10, y * 10, 10, 10);
                            g.DrawImage(Resource1.GC, x * 10, y * 10, 10, 10);
                        }
                        else
                        {
                            g.FillRectangle(new SolidBrush(Color.Black), x * 10, y * 10, 10, 10);
                            g.DrawImage(Resource1.PG, x * 10, y * 10, 10, 10);
                        }
                        PGxpos = x;
                        PGypos= y;
                    }
                    
                    if (map[y, x] == 4 )
                    {
                        if (isEatable)
                        {
                            g.FillRectangle(new SolidBrush(Color.Black), x * 10, y * 10, 10, 10);
                            g.DrawImage(Resource1.GC, x * 10, y * 10, 10, 10);
                        }
                        else
                        {
                            g.FillRectangle(new SolidBrush(Color.Black), x * 10, y * 10, 10, 10);
                            g.DrawImage(Resource1.RG, x * 10, y * 10, 10, 10);
                        }
                        RGxpos = x;
                        RGypos = y;
                    }
                    if (map[y, x] == 5 )
                    {
                        if (isEatable)
                        {
                            g.FillRectangle(new SolidBrush(Color.Black), x * 10, y * 10, 10, 10);
                            g.DrawImage(Resource1.GC, x * 10, y * 10, 10, 10);
                        }
                        else
                        {
                            g.FillRectangle(new SolidBrush(Color.Black), x * 10, y * 10, 10, 10);
                            g.DrawImage(Resource1.YG, x * 10, y * 10, 10, 10);
                        }
                        YGxpos = x;
                        YGypos = y;
                    }
                    if (map[y, x] == 6 )
                    {
                        if (isEatable)
                        {
                            g.FillRectangle(new SolidBrush(Color.Black), x * 10, y * 10, 10, 10);
                            g.DrawImage(Resource1.GC, x * 10, y * 10, 10, 10);
                        }
                        else
                        {
                            g.FillRectangle(new SolidBrush(Color.Black), x * 10, y * 10, 10, 10);
                            g.DrawImage(Resource1.BG, x * 10, y * 10, 10, 10);
                        }
                        BGxpos = x;
                        BGypos = y;
                    }
                    if (map[y, x] == 0)
                    {
                        g.FillRectangle(new SolidBrush(Color.Black), x * 10, y * 10, 10, 10);
                    }
                    if (map[y, x] == 8)
                    {
                        g.FillRectangle(new SolidBrush(Color.Black), x * 10, y * 10, 10, 10);
                        g.FillRectangle(new SolidBrush(Color.FromArgb(255, 255, 0)), (float)x * 10f, (float)y * 10f, 5, 5); 

                    }
                    
                    if (map[y, x] == 2)
                    {
                        g.FillRectangle(new SolidBrush(Color.Black), x * 10, y * 10, 10, 10);
                        if (firstTime == true)
                        {
                            g.DrawImage(Resource2.pacmanright, x * 10, y * 10, 10, 10);
                            firstTime = false;
                        }


                        if (goright == true)
                            g.DrawImage(Resource2.pacmanright, x * 10, y * 10, 10, 10);

                        if (goleft == true)
                            g.DrawImage(Resource2.pacmanleft, x * 10, y * 10, 10, 10);

                        if (godown == true)
                            g.DrawImage(Resource2.pacmandown, x * 10, y * 10, 10, 10);

                        if (goup == true)
                            g.DrawImage(Resource2.pacmanup, x * 10, y * 10, 10, 10);

                        xpos = x;
                        ypos = y;
                    }
                    else
                        g.DrawRectangle(Pens.Gray, x * 10, y * 10, 10, 10);
                }
            }
            PCT_CANVAS.Invalidate();

        }
                

        private void Move(int oldx, int oldy, int newx, int newy,byte character)
        {
            int limy, limx, nextPos;
            
            limy = level.GetLength(1) - 1;
            limx = level.GetLength(0) - 1;
            if (newy > limy)
            {
                level[0, newx] = character;
                level[oldy, oldx] = 0;
            }
            else
            {
                if (newy < 0)
                {
                    level[limy, newx] = character;
                    level[oldy, oldx] = 0;
                }
                else
                {
                    if (newx > limx)
                    {
                        level[newy, 0] = character;
                        level[oldy, oldx] = 0;
                    }
                    else
                    {
                        if (newx < 0)
                        {
                            level[newy, limx] = character;
                            level[oldy, oldx] = 0;
                        }
                        else
                        {
                            
                            if (level[newy, newx] != 1) 
                            {
                                //Si el personaje es pacman
                                if (character == 2)
                                {
                                    //Contar Monedas
                                    if (level[newy, newx] == 8)
                                    {
                                        count = int.Parse(Score.Text);
                                        count++;
                                        Score.Text = count.ToString();
                                        //llega a comer todas las monedas
                                        if (count >= coins)
                                        {
                                            count= int.Parse(Score.Text);
                                            count++;
                                            Score.Text = count.ToString();
                                            timer1.Enabled = false;
                                            DialogResult dr = MessageBox.Show(" CONTINUE?", "YOU WON", MessageBoxButtons.YesNo);
                                            switch (dr)
                                            {
                                                case DialogResult.Yes:
                                                    Application.Restart();
                                                    break;
                                                case DialogResult.No:
                                                    Application.Exit();
                                                    break;
                                            }
                                        }
                                    }
                                    if (level[newy, newx] == 10)
                                    {
                                        supreme = true;
                                        time= 0;
                                    }
                                    if(time==45)
                                    {
                                        supreme = false;
                                    }
                                    if(trespawn==30)
                                    {
                                        PGrespawn = true;
                                        RGrespawn = true;
                                        YGrespawn = true;
                                        BGrespawn = true;
                                        trespawn= 0;

                                    }

                                    //Detectar colision con fantasma
                                    if (level[newy, newx] >= 3 && level[newy, newx] <= 6 && !supreme)
                                    {
                                        timer1.Enabled = false;
                                        DialogResult dr = MessageBox.Show(" CONTINUE?", "GAME OVER", MessageBoxButtons.YesNo);
                                        switch (dr)
                                        {
                                            case DialogResult.Yes:
                                                Application.Restart();
                                                break;
                                            case DialogResult.No:
                                                Application.Exit();
                                                break;
                                        }


                                    }

                                    //Bonus por comer fantasmas
                                    if (level[newy, newx] >= 3 && level[newy, newx] <= 6 && supreme)
                                    {
                                        bonus = int.Parse(BONUS.Text);
                                        bonus = bonus+ 10;
                                        BONUS.Text = bonus.ToString();
                                        if(level[newy, newx]==3)
                                            PGrespawn = false;
                                        if (level[newy, newx] == 4)
                                            RGrespawn = false;
                                        if (level[newy, newx] == 5)
                                            YGrespawn = false;
                                        if (level[newy, newx] == 6)
                                            BGrespawn = false;
                                    }
                                    if(!PGrespawn || !RGrespawn ||!YGrespawn||!BGrespawn)
                                    {
                                       trespawn++;
                                    }
                                    
                                    time++;
                                    level[oldy, oldx] = 0;
                                    level[newy, newx] = character;
                                }
                                //si el personaje es fantasma
                                if (character >= 3 && character <= 6)
                                {
                                    if (level[newy, newx] == 0)
                                    {
                                        level[oldy, oldx] = 0;
                                        level[newy, newx] = character;                                                                                                                                                          
                                    }
                                    if(level[newy, newx] == 8)
                                    {
                                        level[oldy, oldx] = 8;
                                        level[newy, newx] = character;
                                    }
                                    if (level[newy, newx] == 2 && !supreme)
                                    {
                                        timer1.Enabled = false;
                                        DialogResult dr = MessageBox.Show(" CONTINUE?", "GAME OVER", MessageBoxButtons.YesNo);
                                        switch (dr)
                                        {
                                            case DialogResult.Yes:
                                                Application.Restart();
                                                break;
                                            case DialogResult.No:
                                                Application.Exit();
                                                break;
                                        }
                                    }


                                }
                                
                            }
                        }

                    }
                }
            }


            DrawMap(level,supreme);
        }

        public void UpdateMap()
        {
            Graphics g = Graphics.FromImage(bmp);
            g.Clear(Color.Black);
            for (int x = 0; x < level.GetLength(0); x++)
            {
                for (int y = 0; y < level.GetLength(1); y++)
                {
                    if (level[y, x] == 2)
                    {
                        g.DrawImage(Resource1.pacman, x * 10, y * 10, 10, 10);

                    }
                }
            }
        }
    }
}