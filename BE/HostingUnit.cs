using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class HostingUnit
    {
        public readonly int HostingUnitKey;
        private Host Owner { set; get; }
        private string HostingUnitName { set; get; }
        private bool[,] diary;
        public HostingUnit()
        {
            diary = new bool [12,31];
            Configurations.hostUnitKey ++; 
            HostingUnitKey= Configurations.hostUnitKey; 
        }
        public override string ToString()
        {
            string s = "";
            s += string.Format("Hosting UnitKey: {0}\n", HostingUnitKey)
                + string.Format("Owner: {0}\n", Owner)
                + string.Format("Hosting Unit Name: {0}\n", HostingUnitName);
            bool start = false;
            for (int i = 0; i < 12; i++)
            {
                for (int j = 0; j < 31; j++)
                {
                    if (start == false && diary[i, j] == true)
                    {
                        s += string.Format("start period: {0}/{1}\n", j + 1, i + 1);
                        start = true;
                    }
                    if (start == true && diary[i, j] == false)
                    {
                        s += string.Format("end period: {0}/{1}\n", j + 1, i + 1);
                        start = false;
                    }

                }
            }
            return s;
        }
    }
}
