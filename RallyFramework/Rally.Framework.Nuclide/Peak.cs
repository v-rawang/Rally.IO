using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Rally.Framework.Nuclide
{
    class Peak
    {
        public static void Peaks(int[] data,int dataLim, out List<int> list)
        {
            List<int> li = new List<int>();
            List<int> outli = new List<int>();

            li = getPeaksIndex(oneDiff(data, dataLim));
            int Peak = -1;
            for (int i = 0; i < li.Count; i ++)
            {
                int start = 0;
                int end = data.Length - 1;
                if (li[i] - 50 > 0)
                    start = li[i] - 50;
                if (li[i] + 50 < data.Length)
                    end = li[i] + 50;

                Peak = SearPeakDifferential(start, end, 10, data);

                if (System.Math.Abs(Peak - li[i]) < 10 && !outli.Contains(Peak))
                    outli.Add(Peak);
            }
            list = outli;
        }

        //第一次寻峰（基本峰距为1）算法
        private static int[] oneDiff(int[] data,int dataLim)
        {
            int[] result = new int[data.Length];
            int tempValue = 0;
            int num = 0;
            int aValue = 0;
            for (int i = 0; i < data.Length; i++)
            {
                if (data[i] > 0)
                {
                    tempValue += data[i];
                    num++;
                }
            }
            if (num > 0) aValue = (int)(tempValue / num);

            if(dataLim<2*aValue)
                dataLim=2*aValue;

                if (data[0] > data[1] && data[0] > dataLim)
            {
                result[0] = 1;
            }
            else //if (data[0] < data[1])
            {
                result[0] = -1;
            }
            for (int i = 1; i < data.Length; i++)
            {
                int diff = data[i] - data[i - 1];
                if (diff > 0 && data[i] > dataLim)
                {
                    result[i] = 1;
                }
                else if (diff < 0)
                {
                    result[i] = -1;
                }
                else
                {
                    result[i] = result[i-1];
                }

            }
            return result;
        }


        private static List<int> getPeaksIndex(int[] sign)
        {
            List<int> data = new List<int>();
            if (sign[0]>0)
                data.Add(0);
            for (int j = 1; j < sign.Length; j++)
            {
                int diff = sign[j] - sign[j - 1];
                if (diff < 0)
                {
                    data.Add(j);
                }
            }
            return data;//相当于原数组的下标
        }

        public static DataTable GetDistinctSelf(DataTable SourceDt, int filedName)
        {
            for (int i = SourceDt.Rows.Count - 2; i > 0; i--)
            {
                DataRow[] rows = SourceDt.Select(string.Format("{0}='{1}'", SourceDt.Columns[filedName], SourceDt.Rows[i][filedName]));
                if (rows.Length > 1)
                {
                    SourceDt.Rows.RemoveAt(i);
                }
            }
            return SourceDt;

        }

        public static DataTable GetNuclide(DataTable SourceDt, int filedName)
        {
            DataTable dt = SourceDt.Clone();
            for (int i = SourceDt.Rows.Count - 2; i > 0; i--)
            {
                DataRow[] rows = SourceDt.Select(string.Format("{0}='{1}'", SourceDt.Columns[filedName], SourceDt.Rows[i][filedName]));
                if (rows.Length > 1)
                {
                    dt.Rows.Add(SourceDt.Rows[i].ItemArray);
                }
            }
            if (dt.Rows.Count > 0)
                return GetDistinctSelf(dt, filedName);
            else
                return SourceDt;

        }

        public static int SearPeakDifferential(int Beginch, int Endch, int fwhm, int[] differ)
        {

            int nmax = 0, nmin = 0, maxtemp, mintemp;// differ[Endch-Beginch+1], , temp

            maxtemp = differ[Beginch];
            mintemp = differ[Beginch];
            nmax = Beginch;
            nmin = Beginch;
            

            for (int j = Beginch + 1; j <= Endch; j++)
            {

                if (differ[j] < mintemp)
                {
                    mintemp = differ[j];
                    nmin = j;
                }

                if (differ[j] > maxtemp)
                {
                    maxtemp = differ[j];
                    nmax = j;
                }

            }

            if (System.Math.Abs(nmin - nmax) > fwhm)// 0.8 * && System.Math.Abs(nmin - nmax) < 3 * fwhm
                return nmax;

            else return 0;

        }

    }
}
