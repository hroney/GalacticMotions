using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GalacticMotionsWithExpansion
{
    class GalacticPositions
    {
        CosmologicalExpansion NewExpansion = new CosmologicalExpansion();

        static Random random = new Random();
        public static int galacticNumber = 250;

        // galactic position
        private float[] position = new float[3];
        private float[] extraposition = new float[3];
        // size
        private float _size;
        // lifetime
        private float _lifeTime;

        private float d = new float();
        private float[] sum = new float[3];

        // acceleration of the galaxy
        private float[] acceleration = new float[3];

        private float M, M_mid;

        double Omega_M = 0.31;
        double Omega_L = 0.69;
        double dt = 0.0001;

        // velocity
        private float[] velocity = new float[3];

        // Time interval for galaxy activation
        private float LastTime = 0;

        // class constructor
        public GalacticPositions(float x, float y, float z, float size, float lifeTime, float start_time)
        {
            _size = size;
            _lifeTime = lifeTime;

            LastTime = start_time;

            
                //coordinates randomly
                position[0] = random.Next(-10, 10);
                position[1] = random.Next(-10, 10);
                position[2] = random.Next(-10, 10);

                //velocities randomly
                for(int i=0; i<3; i++)
                {
                    int a = 0;
                    while(a == 0)
                    {
                        a = random.Next(-500, 500);
                    }
                    velocity[i] = a;
                }

                M = random.Next(400, 500);

                extraposition[0] = position[0];
                extraposition[1] = position[1];
                extraposition[2] = position[2];
                
        }

        // Method for solving the equations
        public void SystemOfEquations(double Omega_M, double Omega_L, double dt)
        {
            /* N - это количество частиц(max количество не должно превышать 1 000 000 000);
             * x0, y0, z0 - это начальные значения точек (задается руками); 
             * vx0, vy0, vz0 - начальное значение скоростей (задается руками);
             * ax0, ay0, az0 - непосредственно вычисляется;
             * Sum - слогаемое в правых частях.
             * l - размер окна*/
             
            
            //a(t) and da/dt
            double A = NewExpansion.Parameter_A(Omega_M, Omega_L, dt);
            double At = NewExpansion.Parameter_At(Omega_M, Omega_L, dt);

            
                extraposition[0] = position[0];
                extraposition[1] = position[1];
                extraposition[2] = position[2];

                /* Find the initial acceleration to find velocities etc. and doing it with the next iteration method */
                acceleration[0] = (float)(-(2 / A) * At * velocity[0] - 3 * Omega_M / (8 * Math.PI * Math.Pow(A, 3) * M_mid) * sum[0]);
                acceleration[1] = (float)(-(2 / A) * At * velocity[1] - 3 * Omega_M / (8 * Math.PI * Math.Pow(A, 3) * M_mid) * sum[1]);
                acceleration[2] = (float)(-(2 / A) * At * velocity[2] - 3 * Omega_M / (8 * Math.PI * Math.Pow(A, 3) * M_mid) * sum[2]);

                /* Iteration method for finding coordinates and velocities */
                position[0] += velocity[0] * (float)dt;
                position[1] += velocity[1] * (float)dt;
                position[2] += velocity[2] * (float)dt;
                //velocities
                velocity[0] += acceleration[0] * (float)dt;
                velocity[1] += acceleration[1] * (float)dt;
                velocity[2] += acceleration[2] * (float)dt;
              
        }

        // Function for setting acceleration acting on the galaxy
        public void SetAcceleration(float x, float y, float z)
        {
            acceleration[0] = x;
            acceleration[1] = y;
            acceleration[2] = z;
        }

        public void SetD(float x)
        {
            d = x;
        }

        public void SetSum(float x, float y, float z)
        {
            sum[0] = x;
            sum[1] = y;
            sum[2] = z;
        }

        public float GetPosition(int i)
        {
            return position[i];
        }

        public float GetMass()
        {
            return M;
        }

        // getting the galaxy size
        public float GetSize()
        {
            return _size;
        }


        //Set the average value of the mass
        public void SetAverMass(float x)
        {
            M_mid = x;
        }

        // Update the position of the galaxy
        public void UpdatePosition(float timeNow)
        {
            SystemOfEquations(Omega_M, Omega_L, dt);
            
            /* define the time difference from the last update of
             * the position of the galaxy. (Because the timer may not be fixed) */
            float dTime = timeNow - LastTime;
            _lifeTime -= dTime;
        }

        // Check if the galaxy lifetime has not finished yet
        public bool isLife()
        {
            if (_lifeTime > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // Obtaining the coordinates of a galaxy
        public float GetPositionX()
        {
            return position[0];
        }
        public float GetPositionY()
        {
            return position[1];
        }
        public float GetPositionZ()
        {
            return position[2];
        }

        // Obtaining old coordinates of the galaxy
        public float GetOldPositionX()
        {
            return extraposition[0];
        }
        public float GetOldPositionY()
        {
            return extraposition[1];
        }
        public float GetOldPositionZ()
        {
            return extraposition[2];
        }

    }
}
