using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Tao.OpenGl;
using Tao.FreeGlut;
using Tao.Platform.Windows;
using Tao.DevIl;

namespace GalacticMotionsWithExpansion
{
    public partial class GalacticForm : Form
    {
        // отсчет времени
        float globalTime = 0;

        double alphaK = 0;
        static public int count = 0;
        static public int rbtn = 1;
        static public int rotat = 1;

        // массив с параметрами установки камеры
        private float[,] camera_date = new float[3, 7];

        // exemplar of the class GalacticMotions
        private GalacticMotions motionStart = new GalacticMotions(1, 10, 1, 30, GalacticPositions.galacticNumber);

        static public uint[] mGlTextureObject = new uint[11];

        public int[] imageId = new int[11];

        // form initialization
        public GalacticForm()
        {
            InitializeComponent();
            GalacticWindow.InitializeContexts();
        }

        // generation of random numbers
        Random random = new Random();

        // form loading
        private void Form1_Load(object sender, EventArgs e)
        {
            // initialization of OpenGL
            Glut.glutInit();
            Glut.glutInitDisplayMode(Glut.GLUT_RGB | Glut.GLUT_DOUBLE | Glut.GLUT_DEPTH);

            Il.ilInit();
            Il.ilEnable(Il.IL_ORIGIN_SET);
            
            Gl.glClearColor(255, 255, 255, 1);

            Gl.glViewport(0, 0, GalacticWindow.Width, GalacticWindow.Height);

            Gl.glMatrixMode(Gl.GL_PROJECTION);
            Gl.glLoadIdentity();


            Glu.gluPerspective(45, (float)GalacticWindow.Width / (float)GalacticWindow.Height, 0.1, 200);


            Gl.glMatrixMode(Gl.GL_MODELVIEW);
            Gl.glLoadIdentity();

            Gl.glEnable(Gl.GL_DEPTH_TEST);
            Gl.glEnable(Gl.GL_LIGHTING);
            Gl.glEnable(Gl.GL_LIGHT0);
            Gl.glEnable(Gl.GL_COLOR_MATERIAL);
            
            // installing initial values for comboBox elements
            comboBox1.SelectedIndex = 1;
            comboBox2.SelectedIndex = 0;
           
            // camera position 1:

            camera_date[0,0] = -3;
            camera_date[0,1] = 0;
            camera_date[0,2] = -20;
            camera_date[0,3] = 0;
            camera_date[0,4] = 1;
            camera_date[0,5] = 0;
            camera_date[0,6] = 0;

            // camera position 2:

            camera_date[1, 0] = -3;
            camera_date[2, 1] = 2;
            camera_date[1, 2] = -20;
            camera_date[1, 3] = 30;
            camera_date[1, 4] = 1;
            camera_date[1, 5] = 0;
            camera_date[1, 6] = 0;

            // camera position 3:

            camera_date[2, 0] = -3;
            camera_date[2, 1] = 2;
            camera_date[2, 2] = -20;
            camera_date[2, 3] = 30;
            camera_date[2, 4] = 1;
            camera_date[2, 5] = 1;
            camera_date[2, 6] = 0;

            loadImageBuff();

            // timer activation
            RenderTimer.Start();
        }

        private void loadImageBuff()
        {
            int width, height, bitspp;
            string url = "";
            // creating an image with indicator imageId
            Il.ilGenImages(4, imageId);
            // make the image current

            for (int i = 0; i < 11; i++)
            {
                Il.ilBindImage(imageId[i]);

                
                    if (i == 0) url = "back.jpg";
                    if (i == 1) url = "galaxy.jpg";
                    if (i == 2) url = "galaxy02.jpg";
                    if (i == 3) url = "galaxy03.jpg";
                    if (i == 4) url = "galaxy04.jpg";
                    if (i == 5) url = "galaxy05.jpg";
                    if (i == 6) url = "galaxy06.jpg";
                    if (i == 7) url = "galaxy07.jpg";
                    if (i == 8) url = "galaxy08.jpg";
                    if (i == 9) url = "galaxy09.jpg";
                    if (i == 10) url = "galaxy10.jpg";

                // loading it
                if (Il.ilLoadImage(url))
                {
                    // loded??
                    // saving the scales of image
                    width = Il.ilGetInteger(Il.IL_IMAGE_WIDTH);
                    height = Il.ilGetInteger(Il.IL_IMAGE_HEIGHT);

                    // determine the number of bits per pixel
                    bitspp = Il.ilGetInteger(Il.IL_IMAGE_BITS_PER_PIXEL);

                    switch (bitspp) // в зависимости оп полученного результата
                    {
                        // создаем текстуру используя режим GL_RGB или GL_RGBA
                        case 24:
                            mGlTextureObject[i] = MakeGlTexture(Gl.GL_RGB, Il.ilGetData(), width, height);
                            break;
                        case 32:
                            mGlTextureObject[i] = MakeGlTexture(Gl.GL_RGBA, Il.ilGetData(), width, height);
                            break;
                    }
                }
            }
            // очищаем память. А надо? ХЗ)
            Il.ilDeleteImages(4, imageId);

        }
        // creating texture
        private static uint MakeGlTexture(int Format, IntPtr pixels, int w, int h)
        {
            // идентификатор текстурного объекта
            uint texObject;

            // generating the texture object
            Gl.glGenTextures(1, out texObject);

            // устанавливаем режим упаковки пикселей
            Gl.glPixelStorei(Gl.GL_UNPACK_ALIGNMENT, 1);

            // создаем привязку к только что созданной текстуре
            Gl.glBindTexture(Gl.GL_TEXTURE_2D, texObject);

            // устанавливаем режим фильтрации и повторения текстуры
            Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_WRAP_S, Gl.GL_REPEAT);
            Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_WRAP_T, Gl.GL_REPEAT);
            Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_MAG_FILTER, Gl.GL_LINEAR);
            Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_MIN_FILTER, Gl.GL_LINEAR);
            Gl.glTexEnvf(Gl.GL_TEXTURE_ENV, Gl.GL_TEXTURE_ENV_MODE, Gl.GL_REPLACE);

            // creating RGB or RGBA texture
            switch (Format)
            {
                case Gl.GL_RGB:
                    Gl.glTexImage2D(Gl.GL_TEXTURE_2D, 0, Gl.GL_RGB, w, h, 0, Gl.GL_RGB, Gl.GL_UNSIGNED_BYTE, pixels);
                    break;

                case Gl.GL_RGBA:
                    Gl.glTexImage2D(Gl.GL_TEXTURE_2D, 0, Gl.GL_RGBA, w, h, 0, Gl.GL_RGBA, Gl.GL_UNSIGNED_BYTE, pixels);
                    break;
            }
            return texObject; // return object
        }

        // отклик таймера
        private void RenderTimer_Tick(object sender, EventArgs e)
        {
            //animating = true;
            count++;

            // отсчитываем время
            globalTime += (float)RenderTimer.Interval / 1000;
            // вызываем функцию отрисовки
            Gl.glPushMatrix();

            Draw();

        }

        // функция отрисвки сцены
        private void Draw()
        {
            // в зависимсоти от установленног оредима отрисовываем сцену в черном или белом цвете
            if (comboBox2.SelectedIndex == 0)
            {
                // цвет очистки окна
                Gl.glClearColor(255, 255, 255, 1);
            }
            else
            {
                Gl.glClearColor(0, 0, 0, 1);
 
            }
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT | Gl.GL_DEPTH_BUFFER_BIT);

            Gl.glLoadIdentity();

            // в зависимсоти от установленног оредима отрисовываем сцену в черном или белом цвете
            if (comboBox2.SelectedIndex == 0)
            {
                // цвет рисования
                Gl.glColor3d(0, 0, 0);
            }
            else
            {
                Gl.glColor3d(255,255,255);
            }

            Gl.glPushMatrix();

            // определяем установленную камеру
            int camera = comboBox1.SelectedIndex;

            // используем параметры для установленой камеры
            Gl.glTranslated(camera_date[camera, 0], camera_date[camera, 1], camera_date[camera, 2]);
            Gl.glRotated(camera_date[camera, 3], camera_date[camera, 4], camera_date[camera, 5], camera_date[camera, 6]);

                    Gl.glPushMatrix();

                // отрисовываем сеточную плоскость, которая нам будет напоминать где находится земля )
                    DrawMatrix(50);

            // выполняем просчет взрыва
                    motionStart.Calculate(globalTime);

                    Gl.glPopMatrix();

            Gl.glPopMatrix();
            Gl.glFlush();

            

            // refreshing the window
            GalacticWindow.Invalidate();
        }

        // функция для отрисовки матрицы
        private void DrawMatrix(int x)
        {
            float quad_size = 1;

            Gl.glLineWidth(1);
            // две последовательно линий после пересечения создадут "матрицу" чтобы мы могли понимать где назодится земля
            Gl.glBegin(Gl.GL_LINES);

            /*
            for (int ax = 0; ax < x+1; ax++)
            {
               Gl.glVertex3d(quad_size * ax, 0, 0);
               Gl.glVertex3d(quad_size * ax, 0, quad_size * x);
            }

            for (int bx = 0; bx < x+1; bx++)
            {
                Gl.glVertex3d(0, 0, quad_size * bx);
                Gl.glVertex3d(quad_size * x, 0, quad_size * bx);
            }*/
            Gl.glEnd();
            /*
            Gl.glLineWidth(3);
            Gl.glBegin(Gl.GL_LINES);
            Gl.glColor3d(255, 0, 0);
            Gl.glVertex3d(-100, 0, 0);
            Gl.glVertex3d(100, 0, 0);
            

            Gl.glColor3d(0, 255, 0);
            Gl.glVertex3d(0, -100, 0);
            Gl.glVertex3d(0, 100, 0);
            Gl.glColor3d(0, 0, 255);
            Gl.glVertex3d(0, 0, -100);
            Gl.glVertex3d(0, 0, 100);

            Gl.glEnd();*/

            /*Gl.glFlush();
            Gl.glColor3d(255, 0, 0);
            Gl.glRasterPos3d(10, 0, 0);
            Glut.glutBitmapString(Glut.GLUT_BITMAP_HELVETICA_18, "X");

            Gl.glColor3d(0, 255, 0);
            Gl.glRasterPos3d(0, 10, 0);
            Glut.glutBitmapString(Glut.GLUT_BITMAP_HELVETICA_18, "Y");

            Gl.glColor3d(0, 0, 255);
            Gl.glRasterPos3d(0, 0, 10);
            Glut.glutBitmapString(Glut.GLUT_BITMAP_HELVETICA_18, "Z");
            */

            if (comboBox2.SelectedIndex == 0)
            {
                // цвет рисования
                Gl.glColor3d(0, 0, 0);
            }
            else
            {
                Gl.glColor3d(255, 255, 255);
            }

            Gl.glEnd();
            Gl.glLineWidth(1);


            //Gl.glClear(Gl.GL_COLOR_BUFFER_BIT | Gl.GL_DEPTH_BUFFER_BIT);
            //Gl.glClearColor(0, 0, 0, 1);
            //очистка текущей матрицы 
            //Gl.glLoadIdentity();

            Gl.glPushMatrix();
            Gl.glEnable(Gl.GL_TEXTURE_2D);
            Gl.glBindTexture(Gl.GL_TEXTURE_2D, mGlTextureObject[0]);
            Gl.glBegin(Gl.GL_QUADS);
            Glu.GLUquadric back = Glu.gluNewQuadric();
            Glu.gluQuadricTexture(back, Gl.GL_TRUE);
            Glu.gluSphere(back, 50, 50, 50);
            Gl.glRotated(90, 0.0f, 1.0f, 1.0f);
            Glu.gluDeleteQuadric(back);
            Gl.glPopMatrix();
            Gl.glDisable(Gl.GL_TEXTURE_2D);

            Gl.glColor4d(255, 255, 255, 0.2);
            Gl.glLineWidth(1);
            Gl.glBegin(Gl.GL_LINES);
            
            Gl.glEnd();

            Gl.glFlush();
        }

        // method for working with button "Start"
        private void Start_Click(object sender, EventArgs e)
        {
            Random random = new Random();
            // installing the new initial coordinates for galaxies (randomly)
            motionStart.SetNewPosition(random.Next(1, 5), random.Next(1, 5), random.Next(1, 5));
            // random acceleration
            motionStart.SetNewAcceleration(random.Next(20,80));
            // and activating motion
            motionStart.MotionStart(globalTime);
            
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            rbtn = 2;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            rbtn = 1;
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            /*AnT.Width = ClientSize.Width - 215;
            AnT.Height = ClientSize.Height - 10;
            button1.Left = AnT.Width + 10;
            label1.Left = AnT.Width + 10;
            radioButton1.Left = AnT.Width + 10;
            radioButton2.Left = AnT.Width + 10;
            label3.Left = AnT.Width + 10;
            comboBox1.Left = AnT.Width + 10;
            comboBox2.Left = AnT.Width + 10;
            label2.Left = AnT.Width + 10;*/
        }

        // Method for working with checkbox
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            // if checked when enable rotation of the galaxies' axis
            if (checkBox1.Checked == true) rotat = 1;
            else rotat = 2;
        }
    }
}
