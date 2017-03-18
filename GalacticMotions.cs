using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Tao.OpenGl;
using Tao.FreeGlut;
using Tao.Platform.Windows;
using Tao.DevIl;

/* Class for visualisation of galactic motions together with cosmological expansion */
namespace GalacticMotionsWithExpansion
{
    class GalacticMotions
    {
        // galactic position
        private float[] position = new float[3];
        // acceleration
        private float _acceleration;
        // max number of galactics
        private int maxGalaxiesNumber = 10000;
        // Currently set number of galaxies
        private int galacticsNow;

        private int maxTime = 1000000;
        //Galactic size
        private double size = 0.1;

        Random random = new Random();

        

        // activated
        private bool isStart = false;

        // Array of galaxies based on the previously created class
        private GalacticPositions[] GalacticArray;

        // Display list for drawing galaxies created
        private bool isDisplayList = false;
        // Display list number for rendering
        private int[] DisplayListNom = new int[11];

        /* Constructor of the class: coordinates are transferred to it, 
         * where the start of the motion should occur, acceleration and the number of galaxies */
        public GalacticMotions(float x, float y, float z, float acceleration, int galacticsCount)
        {
            position[0] = x;
            position[1] = y;
            position[2] = z;

            galacticsNow = galacticsCount;
            _acceleration = acceleration;

            // If the number of galaxies exceeds the maximum allowed
            if(galacticsCount > maxGalaxiesNumber)
            {
                galacticsCount = maxGalaxiesNumber;
            }

            // Create an array of galaxies of the required size
            GalacticArray = new GalacticPositions[galacticsCount];
           
        }

        // Function to update the position of galaxies
        public void SetNewPosition(float x, float y, float z)
        {
            position[0] = x;
            position[1] = y;
            position[2] = z;
        }

        // Setting a new value for the acceleration of galaxies
        public void SetNewAcceleration(float newAcceleration)
        {
            _acceleration = newAcceleration;
        }

        /* Create a display list to draw a galaxy,
         * since To draw even a small polygon such a number of times is very expensive*/
        private void CreateDisplayList(int a)
        {
            // generation of the display list
            DisplayListNom[a] = Gl.glGenLists(a);

            // Start creating a list
            Gl.glNewList(DisplayListNom[a], Gl.GL_COMPILE);

            Gl.glPushMatrix();
            Gl.glEnable(Gl.GL_TEXTURE_2D);
            
            Gl.glBindTexture(Gl.GL_TEXTURE_2D, GalacticForm.mGlTextureObject[a]);
            Gl.glTranslated(0, 0, 0);
            Gl.glBegin(Gl.GL_QUADS);
            Glu.GLUquadric myEarth = Glu.gluNewQuadric();
            Glu.gluQuadricTexture(myEarth, Gl.GL_TRUE);
            //Glu.gluSphere(myEarth, 0.1, 32, 32);
            Glu.gluDisk(myEarth, 0.001, size, 32, 32);
            
            //Glut.glutSolidSphere(0.05, 32, 32);
            Glu.gluDeleteQuadric(myEarth);
            Gl.glPopMatrix();
            Gl.glDisable(Gl.GL_TEXTURE_2D);

            //Glut.glutSolidSphere(0.05, 32, 32);
            Gl.glEnd();

            Gl.glPushMatrix();

            // Finish drawing the galaxy
            Gl.glEndList();

            // Flag - display list is created
            isDisplayList = true;
        }

        

        // Start function
        public void MotionStart(float time_start)
        {
            // Initialize an instance of class Random
            Random random = new Random();

            // If the display list is not created - you need to create it
            if (!isDisplayList)
            {
                 for (int i = 0; i < 11; i++)
                     CreateDisplayList(i);

            }

            // for all galaxies
            for (int ax = 0; ax < galacticsNow; ax++)
            {
                // creating a galaxy
                GalacticArray[ax] = new GalacticPositions(position[0], position[1], position[2], 5.0f, maxTime, time_start);

                // Randomly generating the orientation of the acceleration vector for a given galaxy
                int direction_x = random.Next(1, 3);
                int direction_y = random.Next(1, 3);
                int direction_z = random.Next(1, 3);

                // If the number 2 is generated, then we replace it by -1. 
                if (direction_x == 2)
                    direction_x = -1;


                if (direction_y == 2)
                    direction_y = -1;


                if (direction_z == 2)
                    direction_z = -1;

                // Set the acceleration in the range from 5 to 100% (so the galaxies have different acceleration)
                float _acceleration_random = random.Next((int)_acceleration/20, (int)_acceleration);
               
                /* Set the acceleration of the galaxy, generating a random number once again. 
                 * Thus, the acceleration will be determined from 10 - to 100% of the received. 
                 * Here we apply the orientation for the acceleration vectors */
                GalacticArray[ax].SetAcceleration(_acceleration_random * ((float)random.Next(100, 1000) / 1000.0f) * direction_x, _acceleration_random * ((float)random.Next(100, 1000) / 1000.0f) * direction_y, _acceleration_random * ((float)random.Next(100, 1000) / 1000.0f) * direction_z);
            }

            //mass
            float m = 0;
            for (int i = 0; i < galacticsNow; i++)
            {
                m += GalacticArray[i].GetMass();
            }
            m /= galacticsNow;
            for (int i = 0; i < galacticsNow; i++)
            {
                GalacticArray[i].SetAverMass(m);
            }

            // activated
            isStart = true;
        }

        // calculation of the current position etc.
        public void Calculate(float time)
        {
            // If the button is activated
            if (isStart)
            
            {
                // using a loop to go through every galaxy
                for (int ax = 0; ax < galacticsNow; ax++)
                {
                    // If the galactic lifetime has not yet finished
                    if (GalacticArray[ax].isLife())
                    {
                        int i = ax; float d, s1=0, s2=0, s3=0;
                        for (int j = 0; j < galacticsNow; j++)
                        {
                            if (i != j)
                            {
                                d = (float)Math.Sqrt(Math.Pow((GalacticArray[i].GetOldPositionX() - GalacticArray[j].GetOldPositionX()), 2) + Math.Pow((GalacticArray[i].GetOldPositionY() - GalacticArray[j].GetOldPositionY()), 2) + Math.Pow((GalacticArray[i].GetOldPositionZ() - GalacticArray[j].GetOldPositionZ()), 2));
                                GalacticArray[ax].SetD(d);
                                s1 = s1 + GalacticArray[j].GetMass() * (GalacticArray[i].GetOldPositionX() - GalacticArray[j].GetOldPositionX()) / (float)Math.Pow(d + 0.01, 3);
                                s2 = s2 + GalacticArray[j].GetMass() * (GalacticArray[i].GetOldPositionY() - GalacticArray[j].GetOldPositionY()) / (float)Math.Pow(d + 0.01, 3);
                                s3 = s3 + GalacticArray[j].GetMass() * (GalacticArray[i].GetOldPositionZ() - GalacticArray[j].GetOldPositionZ()) / (float)Math.Pow(d + 0.01, 3);
                            }
                        }
                        GalacticArray[ax].SetSum(s1, s2, s3);
                        // update the galaxy position
                        GalacticArray[ax].UpdatePosition(time);

                        // saving the current matrix
                        Gl.glPushMatrix();
                        // obtain the galaxy size
                        float size = GalacticArray[ax].GetSize();

                        // transferring galaxy to necessary position
                        Gl.glTranslated(GalacticArray[ax].GetPositionX(), GalacticArray[ax].GetPositionY(), GalacticArray[ax].GetPositionZ());
                        
                        int aa;
                        if (GalacticForm.rbtn == 2)
                            aa = ((int)GalacticArray[ax].GetMass() % 10) + 1;
                        else aa = 4;

                        // If the tick is set on the form, then rotate the galaxy
                        if (GalacticForm.rotat == 1)
                        {
                            if (GalacticArray[ax].GetMass() % 2 == 1)
                                Gl.glRotated(GalacticArray[ax].GetMass() - 400 + GalacticForm.count * 1, 0.0f, 0.5f, 1.0f);
                            else
                                Gl.glRotated(GalacticArray[ax].GetMass() - 400 + GalacticForm.count * 1, 0.5f, 0.0f, 1.0f);
                        }
                        else
                            Gl.glRotated(GalacticArray[ax].GetMass() - 400 + GalacticForm.count * 1, 0.0f, 0.0f, 1.0f);
                        // scale it according to its size
                        Gl.glScalef(size, size, size);
                        // call the display list to draw galaxies from the video adapter cache
                        Gl.glCallList(DisplayListNom[aa]);
                        // return matrix
                        Gl.glPopMatrix();
                    }

                }
            }
        }

    }
}
