using System;
using System.Collections;
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

        // error and validation related vars
        private int errors;
        private ArrayList errorList = new ArrayList();
        private bool isValid;

        public int getErrors()
        {
            return this.errors;
        }

        public void setErrors(int errors)
        {
            this.errors = errors;
        }

        public ArrayList getErrorList()
        {
            return this.errorList;
        }

        public void setErrorList(ArrayList errorList)
        {
            this.errorList = errorList;
        }

        public bool isIsValid()
        {
            return this.isValid;
        }

        public void setIsValid(bool isValid)
        {
            this.isValid = isValid;
        }

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

        public void AddErrorToList(string error)
        {
            errorList.Add(error);
        }

    }
}
