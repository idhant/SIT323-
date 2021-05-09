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

        public int[,] getAllocationMap()
        {
            return this.allocationMap;
        }

        public Allocation()
        {
            this.allocationID = -1;
            this.allocationMap = null;
        }

        public void ReadAllocationMap(int processors, int tasks)
        {
            Console.WriteLine("Allocation Map Matrix: ");
            for (int processor = 0; processor < processors; processor++)
            {
                for(int task = 0; task < tasks; task++)
                {
                    Console.WriteLine("Processor: " + processor + " , Task Number: " + task + " , Assignment:  " + allocationMap[processor,task]);
                }
            }
        }

        public void ReadAllocationID()
        {
            Console.WriteLine("Allocation ID: " + allocationID);
        }

    }
}
