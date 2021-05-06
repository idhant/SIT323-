using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PT1
{
    class Allocation
    {
        private int allocationID;
        
        private int[,] allocationMap;

        public int getAllocationID()
        {
            return allocationID;
        }

        public void setAllocationMap(int[,] allocationMap)
        {
            this.allocationMap = allocationMap;
        }

        public void setAllocationID(int allocationID)
        {
            this.allocationID = allocationID;
        }




        public Allocation()
        {
            this.allocationID = -1;
            this.allocationMap = null;
        }

    }
}
