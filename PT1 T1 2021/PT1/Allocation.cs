using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PT1
{
    /// <summary>
    /// Object type to store and manipulate the details of a allocation retrieved from the cff file.
    /// </summary>
    public class Allocation
    {
        // CLASS MEMBER VARIABLES

        /// <summary>
        /// Member Variable which stores the id of the allocation as int.
        /// </summary>
        private int allocationID;

        /// <summary>
        /// Member Variable which stores the allocation map as a 2D int array.
        /// </summary>
        private int[,] allocationMap;

        /// <summary>
        /// Member Variable which stores the numbers of errors as int.
        /// </summary>
        private int errors;
        
        /// <summary>
        /// Member Variable which stores the errors in a ArrayList.
        /// </summary>
        private ArrayList errorList = new ArrayList();

        /// <summary>
        /// Member Variable which stores a boolean value to see if the allocation is valid or not.
        /// </summary>
        private bool isValid;

        // CLASS PROPERTY METHODS

        /// <summary>
        /// Getter property to retrieve the allocationID.
        /// </summary>
        /// <returns>A positive integer value representing the ID of the allocation.</returns>
        public int getAllocationID()
        {
            return this.allocationID;
        }

        /// <summary>
        /// Setter property to change the allocationID.
        /// </summary>
        /// <param name="allocationID">A positive integer value representing the ID of the allocation.</param>
        public void setAllocationID(int allocationID)
        {
            this.allocationID = allocationID;
        }

        /// <summary>
        /// Getter property to retrieve the allocationMap.
        /// </summary>
        /// <returns>2D integer array which represents the task assignments for the processors in the allocation.</returns>
        public int[,] getAllocationMap()
        {
            return this.allocationMap;
        }

        /// <summary>
        /// Setter property to change the allocationMap.
        /// </summary>
        /// <param name="allocationMap">2D integer array which represents the task assignments for the processors in the allocation.</param>
        public void setAllocationMap(int[,] allocationMap)
        {
            this.allocationMap = allocationMap;
        }

        /// <summary>
        /// Getter property to retrieve the number of errors.
        /// </summary>
        /// <returns>A positive integer value representing the number of errors.</returns>
        public int getErrors()
        {
            return this.errors;
        }

        /// <summary>
        /// Setter property to change the number of errors.
        /// </summary>
        /// <param name="errors">A positive integer value representing the number of errors.</param>
        public void setErrors(int errors)
        {
            this.errors = errors;
        }

        /// <summary>
        /// Getter property to retrieve the error list.
        /// </summary>
        /// <returns>A ArrayList containing the errors in the file.</returns>
        public ArrayList getErrorList()
        {
            return this.errorList;
        }

        /// <summary>
        /// Getter property to retrieve the bool value representing the validity of the allocation.
        /// </summary>
        /// <returns>A boolean value representing the validity of the allocation.</returns>
        public bool isIsValid()
        {
            return this.isValid;
        }

        /// <summary>
        /// Setter property to change the value of the isValid member.
        /// </summary>
        /// <param name="isValid">A boolean value representing the validity of the allocation.</param>
        public void setIsValid(bool isValid)
        {
            this.isValid = isValid;
        }

        // CLASS CONSTRUCTORS

        /// <summary>
        /// Constructor method which assigns the created allocation object with a default ID and Map.
        /// </summary>
        public Allocation()
        {
            this.allocationID = -1;
            this.allocationMap = null;
        }

        // CLASS METHODS

        /// <summary>
        /// Method which prints the task assignment for the tasks and processors.
        /// </summary>
        /// <param name="processors">A positive integer value representing total number of processors for the allocation.</param>
        /// <param name="tasks">A positive integer value representing total number of tasks for the allocation.</param>
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

        /// <summary>
        /// Method used to add a error to the error list.
        /// </summary>
        /// <param name="error">A string value which contains the error.</param>
        public void AddErrorToList(string error)
        {
            errorList.Add(error);
        }

    }
}
