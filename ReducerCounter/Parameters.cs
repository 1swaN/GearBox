using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReducerCounter
{
    public class Parameters
    {
        // переменные, вызываемые по всей программе

        // исходные переменные
        public static double ExPower;
        public static int ExCircles;

        // к методу EdPower
        public static double edPower;

        // к методу KPDCounting
        public static double nu1;
        public static double nu2;
        public static double nu3;
        public static double nu4;
        public static double KPD;
        public static int n;
        public static double u;

        // к методу AllNumber
        public static int Per;
        public static int Klin;
        public static double Freq;
        public static double PerPr;
        public static int PerPr1;
        public static int LeadingChervVal;
        public static double difference;

        // к методу KinematicCounter
        public static double om3;
        public static double om2;
        public static double om1;
        public static double T3;
        public static double T2;
        public static double T1;

        // к методу ZubchatieWheels
        public static int timeOfWork = 30000;
        public static double outsideTemp = 20;
        public static int ha;
        public static double c;
        public static int alpha;
        public static int SIGMA_Ha;
        public static int SIGMA_F;
        public static int SIGMA_Ha1;
        public static int SIGMA_F1;

        // к методу AllowedTensions
        public static int n1;
        public static double Vs;
        public static double N2;
        public static double Khe;
        public static double Kfe;
        public static double Nhe2;
        public static double Nfe2;
        public static double SIGMA_Ha2;
        public static int type;

        // к методу InterAxial
        public static int z1;
        public static int z2;
        public static double Aw;

        // к методу Module
        public static double m;
        public static double q;
        public static double X;
        public static int Type;
        public static double angleGammaRadians;
        public static double angleGamma;
        public static double Yba;
        public static double Ybd;
        public static double Khbeta;
        public static int d1;
        public static int d2;
        public static double dw1;

        //к методу TensionsCheck
        public static double sigma_n;
        public static double percentage;
        public static double z2v;
        public static double Ft2;
        public static double Yf;
        public static double Sigma_Fa;

        // к методу Warmth
        public static double V;
        public static double VS;
        public static double f;
        public static double ro;
        public static double KPDcherv;
        public static double P1;
        public static double P2;
        //public static double S;
        public static double A;
        public static double t;

        // к методу GeometrySizes
        public static int da1;
        public static int da2;
        public static int daM2;
        public static double b1;
        public static double b2;

        // к методу Powers
        public static double Fa1;
        public static double Ft1;
        public static double Fr;
    }
}
