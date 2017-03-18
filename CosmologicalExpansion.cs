using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GalacticMotionsWithExpansion
{
    class CosmologicalExpansion
    {
         /* Omega_M and Omega_L - constants corresponding to
          * the amount of matter in the universe and the cosmological constant */

        // method that gives the value of the scale factor
        public double Parameter_A(double Omega_M, double Omega_L, double t)
        {
            double f1 = Math.Sqrt(1 + Omega_L / Omega_M) * Math.Sinh((3 / 2) * Math.Sqrt(Omega_L) * t);
            double f2 = Math.Sqrt(Omega_L / Omega_M) * Math.Cosh((3 / 2) * Math.Sqrt(Omega_L) * t);
            double F = Math.Pow(f1 + f2, 2 / 3);//expression in brackets
            return (Math.Pow((Omega_M / Omega_L), 2 / 3) * F);
        }

        //The method that gives the value of da/dt
        public double Parameter_At(double Omega_M, double Omega_L, double t)
        {
            //Numerator
            double F1 = Math.Sqrt(1 + (Omega_M / Omega_L)) * Math.Cosh((3 / 2) * Math.Sqrt(Omega_L) * t) + Math.Sinh((3 / 2) * Math.Sqrt(Omega_L) * t);
            //Denominator
            double F2 = Math.Exp((1 / 3) * Math.Log(Math.Sqrt(1 + (Omega_L / Omega_M)) * Math.Sinh((3 / 2) * Math.Sqrt(Omega_L) * t) +
                Math.Sqrt((Omega_L / Omega_M)) * Math.Cosh((3 / 2) * Math.Sqrt(Omega_L) * t)));
            //  The final value of a(t)
            return (Math.Exp((2 / 3) * Math.Log(Omega_M / Omega_L)) * Math.Sqrt(Omega_L) * F1 / F2);
        }
    }
}
