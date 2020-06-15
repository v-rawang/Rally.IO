using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rally.Framework.Nuclide
{
    class MathUtility
    {
        public static int SolveLeastSquareCoefficient(double[] XData, double[] YData, out double A, out double B, out double C, double N = 1e-13)
        {
            A = 0;
            B = 0;
            C = 0;

            double[] x = XData; //{ 0.00, 0.056, 0.112, 0.168, 0.224, 0.280, 0.336, 0.392, 0.448, 0.504, 0.560, 0.616, 0.672, 0.728, 0.784, 0.84, 0.896, 0.952, 0.1008, 0.1064, 1.12 };
            double[] y = YData; //{ 0.00, 1.66, 3.31, 4.96, 6.6, 8.22, 9.82, 11.4, 12.94, 14.43, 15.86, 17.22, 18.5, 19.69, 20.79, 21.79, 22.7, 23.53, 24.25, 24.87, 25.4 };
            double a, b, c, m1, m2, m3, z1, z2, z3;
            a = b = c = 0;
            double sumx = 0, sumx2 = 0, sumx3 = 0, sumx4 = 0, sumy = 0, sumxy = 0, sumx2y = 0;

            for (int i = 0; i < XData.Length; i++)
            {
                sumx += x[i];
                sumy += y[i];
                sumx2 += System.Math.Pow(x[i], 2); //pow(x[i], 2);
                sumxy += x[i] * y[i];
                sumx3 += System.Math.Pow(x[i], 3);//pow(x[i], 3);
                sumx2y += System.Math.Pow(x[i], 2) * y[i];//pow(x[i], 2) * y[i];
                sumx4 += System.Math.Pow(x[i], 4);//pow(x[i], 4);
            }

            do {
                m1 = a;
                a = (sumx2y - sumx3 * b - sumx2 * c) / sumx4;
                z1 = (a - m1) * (a - m1);
                m2 = b;
                b = (sumxy - sumx * c - sumx3 * a) / sumx2;
                z2 = (b - m2) * (b - m2);
                m3 = c;
                c = (sumy - sumx2 * a - sumx * b) / 42;
                z3 = (c - m3) * (c - m3);
            }
            while ((z1 > N) || (z2 > N) || (z3 > N));
            //printf("a=%9.6f,\nb=%9.6f,\nc=%9.6f\n", a, b, c);
            //printf("拟合方程为   y=%9.6fx*x+%9.6fx+%9.6f", a, b, c);

            A = a;
            B = b;
            C = c;

            return 0;
        }

        /// <summary>
        /// 曲线平滑
        /// </summary>
        /// <param name="N"></param>
        /// <param name="A"></param>
        /// <param name="B"></param>
        /// <param name="C"></param>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        /// <returns></returns>
        public static int CurveSmooth(int N, double A, double B, double C, out double[] X, out double[] Y)
        {
            X = new double[N];
            Y = new double[N];

            for (int i = 0; i < N; i++)
            {
                X[i] = i;
                Y[i] = System.Math.Pow(X[i], 2) * A + X[i] * B + C;
            }

            return 0;
        }

        public static double[] Compute(double[] InputX, double A, double B, double C)
        {
            double[] result = new double[InputX.Length];
            for (int i = 0; i < InputX.Length; i++)
            {
                result[i] = System.Math.Pow(InputX[i], 2) * A + InputX[i] * B + C;
            }

            return result;
        }

        /// <summary>
        /// 按点求重心
        /// </summary>
        /// <param name="InputData"></param>
        /// <param name="StartIndex"></param>
        /// <param name="EndIndex"></param>
        /// <param name="Distance"></param>
        /// <returns></returns>
        public static double[] CenterOfArea(double[] InputData, int StartIndex, int EndIndex, int Distance)
        {
            double[] resultData = null;

            if (InputData != null && StartIndex >= 0 && EndIndex >= 3 && InputData.Length >= StartIndex && InputData.Length >= EndIndex)
            {
                resultData = new double[EndIndex];

                for (int i = StartIndex; i <= EndIndex; i++)
                {
                    //if (Distance == 1)
                    //{
                    //    resultData[i] = (InputData[i - 1] + 2 * InputData[i] + InputData[i + 1]) / 4;
                    //}
                    //else if (Distance == 2)
                    //{
                    //    resultData[i] = (InputData[i - 2] + 4 * InputData[i - 1] + 6 * InputData[i] + 4 * InputData[i + 1] + InputData[i + 2]) / 16;
                    //}
                    //else
                    //{
                    //    resultData[i] = (InputData[i - 3] + 6 * InputData[i - 2] + 15 * InputData[i - 1] + 20 * InputData[i] + 15 * InputData[i + 1] + 6 * InputData[i + 2] + InputData[i + 3]) / 64;
                    //}

                    switch (Distance)
                    {
                        case 1:
                            resultData[i] = (InputData[i - 1] + 2 * InputData[i] + InputData[i + 1]) / 4;
                            break;
                        case 2:
                            resultData[i] = (InputData[i - 2] + 4 * InputData[i - 1] + 6 * InputData[i] + 4 * InputData[i + 1] + InputData[i + 2]) / 16;
                            break;
                        case 3:
                            resultData[i] = (InputData[i - 3] + 6 * InputData[i - 2] + 15 * InputData[i - 1] + 20 * InputData[i] + 15 * InputData[i + 1] + 6 * InputData[i + 2] + InputData[i + 3]) / 64;
                            break;
                        default:
                            resultData[i] = (InputData[i - 3] + 6 * InputData[i - 2] + 15 * InputData[i - 1] + 20 * InputData[i] + 15 * InputData[i + 1] + 6 * InputData[i + 2] + InputData[i + 3]) / 64;
                            break;
                    }
                }
            }

            return resultData;
        }


        public static double Gauss(int i, int m, int H) //高斯函数
        {
            double a = i;
            double b = H;
            double c = 4 * System.Math.Log(2) * (a / b) * (a / b);

            return System.Math.Exp(c * -1);
        }

        public static double SimilarGaussConstant(int m, int H) //类高斯常数
        {
            double sum = 0;
            for (int i = m*-1; i < m; i++)
            {
                sum += Gauss(i, m, H);
            }

            return sum / (2 * m + 1);
        }

        public static double SimilarGauss(int i, int m, int H) //类高斯函数
        {
            return Gauss(i, m, H) - SimilarGaussConstant(m, H);
        }

        public static double SimilarGauss2(int i, int m, int H) //类高斯函数平方
        {
            return SimilarGauss(i, m, H) * SimilarGaussConstant(m, H);
        }

        public static double IsPossiblePeak(int n, int j, int m, int H, double[] argA, int channelCount = 1024)
        {
            double a = 0;
            double b = 0;

            int t = n / channelCount;

            if (t == 0)
            {
                t = 1;
            }

            for (int i = m*-1; i <= m;i++ )
            {
                a += SimilarGauss(i, m, H) * argA[j + i * t];
                b += SimilarGauss2(i, m, H) * argA[j + i * t];
            }

            if (b != 0)
            {
                return a / System.Math.Sqrt(b);
            }
            else
            {
                return 0;
            }
        }

        public static void PeakSearch(int n, int m, int H, double[] argA, List<int> argB, double C, int channelCount = 1024)
        {
            double a = 0;
            int num = 0;
            bool first = true;
            bool iS = false;

            int t = n / channelCount;

            if (t == 0)
            {
                t = 1;
            }

            for (int i = m*t; i < n-m*t; i=i+t)
            {
                if (IsPossiblePeak(n,i,m,H, argA) >C)
                {
                    if (first == true)
                    {
                        first = false;
                        iS = true;
                        a = IsPossiblePeak(n, i, m, H, argA);
                        argB[num] = i;
                    }
                }
                else
                {
                    if (iS == true)
                    {
                        first = true;
                        iS = false;
                        int PD = argB[num];

                        for (int k = PD - t; k < PD; k++)
                        {
                            if (argA[k] <argA[k +1])
                            {
                                argB[num] = k + 1;
                            }
                        }

                        num++;
                    }
                }
            }
        }

        /// <summary>
        /// 计算置信度
        /// </summary>
        /// <param name="E0"></param>
        /// <param name="E1"></param>
        /// <param name="ETOL"></param>
        /// <returns></returns>
        public static double ComputeCredibility(double[] E0, double[] E1, double ETOL)
        {
            double T = E0.Length, n = E1.Length;

            double[] f = n == 0 ? new double[1] { 1 } : new double[E1.Length];
            double deltaE = 0;

            double credibility = n;

            for (int i = 0; i < E1.Length; i++)
            {
                deltaE = System.Math.Abs(E1[i] - E0[i]);
                f[i] = System.Math.Exp(-0.16 * System.Math.Pow(deltaE, 2) / ETOL);
            }

            for (int i = 0; i < f.Length; i++)
            {
                credibility *= f[i];
            }

            double h = 1 + 1.8 * (n / T - 1);

            credibility *= h;

            return credibility;
        }
    }
}
