using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PT1
{
    /// <summary>
    /// Object type to store and manipulate the details of the .taff file.
    /// </summary>
    public class TaskAllocations
    {
        
        // CLASS MEMBER VARIABLES

        /// <summary>
        /// Member Variable which stores the lines from the taff file in a string array.
        /// </summary>
        private String[] taffFileLines;

        // error and validation related vars.

        /// <summary>
        /// Member Variable which stores the number of errors as a integer.
        /// </summary>
        private int errors;

        /// <summary>
        /// Member Variable which stores the errors in a Array List.
        /// </summary>
        private ArrayList errorList = new ArrayList();

        /// <summary>
        /// Member Variable which stores a boolean value to see if the taff file is valid or not.
        /// </summary>
        private bool isValid;

        // cff path and name related vars.

        /// <summary>
        /// Member Variable which stores the directory path of the taff file.
        /// </summary>
        private string dirPath;

        /// <summary>
        /// Member Variable which stores the name of the cff file obtained from the taff file.
        /// </summary>
        private string cffName;

        /// <summary>
        /// Member Variable which stores the directory path of the cff file.
        /// </summary>
        private string cffFilePath;

        // vars used to store the values of relevent things from the TAFF file

        /// <summary>
        /// Member Variable which stores the total number of allocations.
        /// </summary>
        private int totalAllocations;

        /// <summary>
        /// Member Variable which stores the total number of tasks in each allocation.
        /// </summary>
        private int totalTasks;

        /// <summary>
        /// Member Variable which stores the total number of processors for each allocation.
        /// </summary>
        private int totalProcessors;

        /// <summary>
        /// Member Variable which stores the the list of Allocation Type objects.
        /// </summary>
        private List<Allocation> allocationList = new List<Allocation>();

        // CLASS PROPERTY METHODS

        /// <summary>
        /// Getter property to retrieve the allocation list.
        /// </summary>
        /// <returns>A allocation list of type List{Allocation}. </returns>
        public List<Allocation> getAllocationList()
        {
            return this.allocationList;
        }

        /// <summary>
        /// Getter property to retrieve the bool value representing the validity of the taff file.
        /// </summary>
        /// <returns>A boolean value representing the validity of the taff file.</returns>
        public bool getIsValid()
        {
            return this.isValid;
        }

        /// <summary>
        /// Getter property to retrieve the directory path of the cff file.
        /// </summary>
        /// <returns>A string value which contains the path.</returns>
        public string getCffFilePath()
        {
            return this.cffFilePath;
        }

        /// <summary>
        /// Getter property to retrieve all the lines read from the taff file.
        /// </summary>
        /// <returns>A string array which cotains the lines.</returns>
        public String[] getTAFFFileLines()
        {
            return taffFileLines;
        }

        /// <summary>
        /// Getter property to retrieve the total number of tasks.
        /// </summary>
        /// <returns>A integer representing the total number of tasks.</returns>
        public int getTotalTasks()
        {
            return this.totalTasks;
        }

        /// <summary>
        /// Getter property to retrieve the total number of processors.
        /// </summary>
        /// <returns>A integer representing the total number of processors.</returns>
        public int getTotalProcessors()
        {
            return this.totalProcessors;
        }


        // CLASS CONSTRUCTORS
        public TaskAllocations()
        {

        }

        // CLASS METHODS

        /// <summary>
        /// Method to store the lines read from the taff file to the taffFilelines member variable.
        /// </summary>
        /// <param name="filename">The path of the taff file that needs to be read.</param>
        public void StoreTAFFLines(String filename)
        {
            this.taffFileLines = File.ReadAllLines(filename);
            Console.WriteLine("TAFF File lines copied to ArrrayList.");
        }

        /// <summary>
        /// Method to read the cff file name from the file lines of taff file.
        /// </summary>
        /// <param name="filename">The path of the taff file that needs to be read.</param>
        /// <returns></returns>
        public string ReadCFFFileName(String filename)
        {
            Console.WriteLine("Starting function to extract CFF Name from the TAFF File." + "\n");
            StreamReader streamReader = new StreamReader(filename);
            string line;
            string CFFname = null;
            using (streamReader)
            {
                while ((line = streamReader.ReadLine()) != null && (CFFname == null))
                {
                    string trimmedline = null;
                    trimmedline = line.Trim();
                    Console.WriteLine(trimmedline);

                    string regexStringFilenameRegex = "^FILENAME=";
                    string regexStringTrimQuotes = "\"";
                    string regexStringemptyText = "";

                    Regex regex = new Regex(regexStringFilenameRegex);
                    Regex trimQuotes = new Regex(regexStringTrimQuotes);

                    if (regex.IsMatch(trimmedline))
                    {
                        Console.WriteLine("CFF file name found, Extracting name... ");
                        CFFname = regex.Replace(trimmedline, regexStringemptyText);
                        String trimmedCFFname;
                        trimmedCFFname = trimQuotes.Replace(CFFname, regexStringemptyText);
                        CFFname = trimmedCFFname;
                        Console.WriteLine("CFF File Name Extracted: " + CFFname + " .");
                    }
                    else
                    {
                        Console.WriteLine("CFF file name not found, continuing search.....");
                    }
                }

            }
            Console.WriteLine("Ending function to extract CFF Name from the TAFF File." + "\n");
            this.cffName = CFFname;
            return CFFname;
        }

        /// <summary>
        /// Method to retrieve the path a file and storing that path in the member variable dirPath.
        /// </summary>
        /// <param name="file">File that path needs to be found.</param>
        public void GetDirectoryPath(string file)
        {
            FileInfo fileInfo = new FileInfo(file);
            string path = fileInfo.Directory.ToString();
            Console.WriteLine("CFF File Path: " + path + "");
            this.dirPath = path;
        }

        /// <summary>
        /// Method to read the path of the cff file.
        /// </summary>
        /// <returns>A string value containing the path of the cff file.</returns>
        public string ReadCFFFilePath()
        {
            string pathSeparator = "\\";
            this.cffFilePath = this.dirPath + pathSeparator + this.cffName;
            Console.WriteLine("Location of the CFF File is: " + this.cffFilePath);

            return this.cffFilePath;
        }

        /// <summary>
        /// Method to append the text of the taff file to the textBox in the application.
        /// </summary>
        /// <param name="filename">Path of the file that needs to be read to end.</param>
        /// <param name="textBox">Textbox to which lines are to be appended.</param>
        public void WriteTAFFFileData(String filename, TextBox textBox)
        {
            StreamReader streamReader = new StreamReader(filename);
            textBox.Text = streamReader.ReadToEnd();
            Console.WriteLine("TAFF File Data printed on screen.");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filelines"></param>
        /// <returns></returns>
        public bool ValidateTAFFFile(String[] filelines)
        {
            // Local initialisation of variables used to hold errors info and index of the line array.
            int index = 0;
            int errors = 0;
            ArrayList errorList = new ArrayList();

            Console.WriteLine("Validating TAFF File. " + "\n");

            // Looping till the file ends.
            while (index < filelines.Length)
            {
                //Console.WriteLine("Current Index: " + index);
                
                // initialising the trimmedline and trimming it
                string trimmedline = null;
                trimmedline = filelines[index].Trim();

                // Regex string intialisations
                string regexStringcomment = @"//.*";

                string regexStringConfigurationData = "CONFIGURATION-DATA";
                string regexStringCffFilename = "FILENAME=";
                string regexStringConfigurationDataEndpoint = "END-CONFIGURATION-DATA";
                
                string regexStringAllocationsData = "ALLOCATIONS";
                string regexStringAllocationsDataEndpoint = "END-ALLOCATIONS";
                string regexStringAllocationsDataCount = @"COUNT=\d";
                string regexStringAllocationsDataTasks = @"TASKS=\d";
                string regexStringAllocationsDataProcessors = @"PROCESSORS=\d";

                string regexStringAllocationData = "ALLOCATION";
                string regexStringAllocationDataEndpoint = "END-ALLOCATION";
                string regexStringAllocationDataID = @"ID=\d";
                string regexStringAllocationDataMap = @"MAP=.*";

                // Regex initialisations
                Regex comment = new Regex(regexStringcomment);

                Regex configurationData = new Regex(regexStringConfigurationData);
                Regex cffFilename = new Regex(regexStringCffFilename);
                Regex configurationDataEndpoint = new Regex(regexStringConfigurationDataEndpoint);

                Regex allocationsData = new Regex(regexStringAllocationsData);
                Regex allocationsDataEndpoint = new Regex(regexStringAllocationsDataEndpoint);
                Regex allocationsDataCount = new Regex(regexStringAllocationsDataCount);
                Regex allocationsDataTasks = new Regex(regexStringAllocationsDataTasks);
                Regex allocationsDataProcessors = new Regex(regexStringAllocationsDataProcessors);

                Regex allocationData = new Regex(regexStringAllocationData);
                Regex allocationDataEndpoint = new Regex(regexStringAllocationDataEndpoint);
                Regex allocationDataID = new Regex(regexStringAllocationDataID);
                Regex allocationDataMap = new Regex(regexStringAllocationDataMap);

                // Empty Line.
                if (trimmedline.Length == 0)
                {
                    //Console.WriteLine("Line: " + trimmedline + " : Empty Line -> Valid");
                }

                // Comment Line.
                else if (comment.IsMatch(trimmedline))
                {
                    //Console.WriteLine("Line: " + trimmedline + " : Comment Line -> Valid");
                }

                // Config Block
                // If block invalid add error to list and increment number of errors
                else if (configurationData.IsMatch(trimmedline))
                {
                    if (configurationDataEndpoint.IsMatch(trimmedline))
                    {
                        //Console.WriteLine("Line: " + trimmedline + " : END-CONFIGURATION -> Valid");
                    }

                    else
                    {
                        index = IncrementIndex(index, 1);
                        string filename = filelines[index].Trim();

                        if (cffFilename.IsMatch(filename))
                        {
                            //Console.WriteLine("Line: " + trimmedline + " : CONFIGURATION -> Valid");
                            //Console.WriteLine("Line: " + filename + " : CONFIGURATION Block -> Valid");
                        }
                        else
                        {
                            //Console.WriteLine("Line: " + trimmedline + " : CONFIGURATION Block -> Invalid");
                            errors++;
                            errorList.Add(trimmedline);
                            //Console.WriteLine("Errors Found: " + errors + " .");
                        }
                    }
                }

                // Allocations Block
                // If block valid, retrieve general allocations details
                // If block invalid add error to list and increment number of errors
                else if (allocationsData.IsMatch(trimmedline))
                {
                    if (allocationsDataEndpoint.IsMatch(trimmedline))
                    {
                        //Console.WriteLine("Line: " + trimmedline + " : END-ALLOCATIONS -> Valid");
                    }

                    else
                    {
                        index = IncrementIndex(index, 1);
                        string allocationCount = filelines[index].Trim();
                        index = IncrementIndex(index, 1);
                        string taskCount = filelines[index].Trim();
                        index = IncrementIndex(index, 1);
                        string processorCount = filelines[index].Trim();

                        if (allocationsDataCount.IsMatch(allocationCount) && allocationsDataTasks.IsMatch(taskCount) && allocationsDataProcessors.IsMatch(processorCount))
                        {
                            //Console.WriteLine("Line: " + allocationCount + " : ALLOCATIONS CONFIG Block -> Valid");
                            //Console.WriteLine("Line: " + taskCount + " : ALLOCATIONS CONFIG Block -> Valid");
                            //Console.WriteLine("Line: " + processorCount + " : ALLOCATIONS CONFIG Block -> Valid");
                            RetrieveAllocationsDetails(allocationCount, taskCount, processorCount);
                        }

                        else
                        {
                            if (!allocationsDataCount.IsMatch(allocationCount))
                            {
                                //Console.WriteLine("Line: " + allocationCount + " : ALLOCATIONS CONFIG Block -> Invalid");
                                errors++;
                                errorList.Add(allocationCount);
                                //Console.WriteLine("Errors Found: " + errors + " .");
                            }
                            if (!allocationsDataTasks.IsMatch(taskCount))
                            {
                                //Console.WriteLine("Line: " + taskCount + " : ALLOCATIONS CONFIG Block -> Valid");
                                errors++;
                                errorList.Add(taskCount);
                                //Console.WriteLine("Errors Found: " + errors + " .");
                            }
                            if (!allocationsDataProcessors.IsMatch(processorCount))
                            {
                                //Console.WriteLine("Line: " + processorCount + " : ALLOCATIONS CONFIG Block -> Valid");
                                errors++;
                                errorList.Add(processorCount);
                                //Console.WriteLine("Errors Found: " + errors + " .");
                            }
                        }
                    }

                }

                // Individual Allocation Block
                // If block valid, retrieve individual allocation details
                // If block invalid add error to list and increment number of errors
                else if (allocationData.IsMatch(trimmedline))
                {
                    if (allocationDataEndpoint.IsMatch(trimmedline))
                    {
                        //Console.WriteLine("Line: " + trimmedline + " : END-ALLOCATION -> Valid");
                    }

                    else
                    {
                        index = IncrementIndex(index, 1);
                        string allocationID = filelines[index].Trim();
                        index = IncrementIndex(index, 1);
                        string allocationMAP = filelines[index].Trim();

                        if (allocationDataID.IsMatch(allocationID) && allocationDataMap.IsMatch(allocationMAP))
                        {
                            //Console.WriteLine("Line: " + allocationID + " : ALLOCATION CONFIG Block -> Valid");
                            //Console.WriteLine("Line: " + allocationMAP + " : ALLOCATION CONFIG Block -> Valid");
                            this.allocationList.Add(RetrieveAllocationDetail(allocationID, allocationMAP));
                            
                        }

                        else
                        {
                            if (!allocationDataID.IsMatch(allocationID))
                            {
                                //Console.WriteLine("Line: " + allocationID + " : ALLOCATION CONFIG Block -> Invalid");
                                errors++;
                                errorList.Add(allocationID);
                                //Console.WriteLine("Errors Found: " + errors + " .");
                            }
                            if (!allocationDataMap.IsMatch(allocationMAP))
                            {
                                //Console.WriteLine("Line: " + allocationMAP + " : ALLOCATION CONFIG Block -> Invalid");
                                errors++;
                                errorList.Add(allocationMAP);
                                //Console.WriteLine("Errors Found: " + errors + " .");
                            }
                        }
                    }

                }

                // Invalid syntax line
                // Add error to list and increment number of errors
                else
                {
                    //Console.WriteLine("Line: " + trimmedline + " : Line doesn`t match TAFF Syntax -> Invalid");
                    errors++;
                    errorList.Add(trimmedline);
                    //Console.WriteLine("Errors Found: " + errors + " .");
                }

                // increment 1 index after each line/block check 
                index = IncrementIndex(index, 1);
            }

            Console.WriteLine("Ending Validation of TAFF file. " + "\n");

            // If errors are ther change the isValid to false and assign the local error related vars to global vars
            // Also return false
            if (errors > 0)
            {
                this.isValid = false;
                Console.WriteLine("The TAFF file is invalid.");
                Console.WriteLine("Number of errors found: " + errors + " .");

                Console.WriteLine("Errors are as follows: \n ");

                int errCount = 1;

                foreach (string error in errorList)
                {
                    Console.WriteLine("Error- " + errCount + " : " + error);
                    errCount++;
                }
                this.errors = errors;
                this.errorList = errorList;
                return false;
            }

            // Set isValid to true and return true.
            // Also print out the details of the file.
            else
            {
                Console.WriteLine("The TAFF file is valid. \n");
                this.isValid = true;

                Console.WriteLine("The details retrieved from the taff file are as follows: ");
                //Console.WriteLine("CFF File Name: " + cffName);
                //Console.WriteLine("CFF File Path: " + cffFilePath);
                Console.WriteLine("Allocation Details: ");
                Console.WriteLine("Number of Allocations: " + totalAllocations);
                Console.WriteLine("Number of Processors: " + totalProcessors);
                Console.WriteLine("Number of Tasks: " + totalTasks);
                Console.WriteLine("Individual Allocation Details: ");
                foreach (Allocation allocation in allocationList)
                {
                    Console.WriteLine("Allocation ID: " + allocation.getAllocationID());
                    allocation.ReadAllocationMap(totalProcessors, totalTasks);
                }

                return true;
            }

        }

        /// <summary>
        /// Method to retrieve the general allocation details from the tff file.
        /// </summary>
        /// <param name="allocationsCount">This contains the string which has the number of allocations.</param>
        /// <param name="allocationsTasks">This contains the string which has the number of tasks.</param>
        /// <param name="allocationsProcessors">This contains the string which has the number of processors.</param>
        public void RetrieveAllocationsDetails(string allocationsCount, string allocationsTasks, string allocationsProcessors)
        {
            // constant strings
            string emptyText = "";

            // Regex string init
            string regexStringAllocationsDataCount = @"COUNT=";
            string regexStringAllocationsDataTasks = @"TASKS=";
            string regexStringAllocationsDataProcessors = @"PROCESSORS=";

            // Regex init
            Regex allocationsDataCount = new Regex(regexStringAllocationsDataCount);
            Regex allocationsDataTasks = new Regex(regexStringAllocationsDataTasks);
            Regex allocationsDataProcessors = new Regex(regexStringAllocationsDataProcessors);

            this.totalAllocations = Int32.Parse(allocationsDataCount.Replace(allocationsCount, emptyText));
            this.totalTasks = Int32.Parse(allocationsDataTasks.Replace(allocationsTasks, emptyText));
            this.totalProcessors = Int32.Parse(allocationsDataProcessors.Replace(allocationsProcessors, emptyText));

            //Console.WriteLine("TotalAllocations: " + this.totalAllocations + "\n");
            //Console.WriteLine("TotalTasks: " + this.totalTasks + "\n");
            //Console.WriteLine("TotalProcessors: " + this.totalProcessors + "\n");
        }

        /// <summary>
        /// Method to retrieve the each individual allocation details from the tff file.
        /// </summary>
        /// <param name="allocationID">This contains the string which has the allocation Id.</param>
        /// <param name="allocationMAP">This contains the string which has the allocation map.</param>
        /// <returns>Allocation Object with assigned details.</returns>
        public Allocation RetrieveAllocationDetail(string allocationID, string allocationMAP)
        {
            // constant strings
            string emptyText = "";
            
            // Regex string init
            string regexStringAllocationDataID = @"ID=";
            string regexStringAllocationDataMap = @"MAP=";

            // Regex init
            Regex allocationDataID = new Regex(regexStringAllocationDataID);
            Regex allocationDataMap = new Regex(regexStringAllocationDataMap);

            // Getting the allocation details needed.
            int allocationId = Int32.Parse(allocationDataID.Replace(allocationID, emptyText));
            string allocationMap = allocationDataMap.Replace(allocationMAP, emptyText);
            
            // Creating a allocation object
            Allocation allocation = new Allocation();

            if (allocationId < totalAllocations)
            {
                // id assignment
                allocation.setAllocationID(allocationId);

                // Matrix size init
                int[,] allocationMatrix = new int[this.totalProcessors, this.totalTasks];

                // Matrix Tasks Row Combination extraction

                // Regex string init
                string regexStringFirstTaskSetRegex = @";\d,\d,\d,\d,\d";
                string regexStringFirstTaskSetRemovalRegex = @"^\d,\d,\d,\d,\d;";
                string regexStringFirstTaskRegex = @",\d";
                string regexStringFirstTaskRemovalRegex = @"^\d,";

                // Regex init
                Regex firstTaskSetRegex = new Regex(regexStringFirstTaskSetRegex);
                Regex firstTaskSetRemovalRegex = new Regex(regexStringFirstTaskSetRemovalRegex);
                Regex firstTaskRegex = new Regex(regexStringFirstTaskRegex);
                Regex firstTaskRemovalRegex = new Regex(regexStringFirstTaskRemovalRegex);

                // Array to hold the rows and columns
                string[] processorTaskSet = new string[totalProcessors];
                int[] taskSet = new int[totalTasks];

                // adding the value of 0/1 task assignment to the matching processor and task
                for (int processor = 0; processor < totalProcessors; processor++)
                {
                    processorTaskSet[processor] = firstTaskSetRegex.Replace(allocationMap, "");
                    for (int task = 0; task < totalTasks; task++)
                    {
                        //Console.WriteLine("Processor task Set String before: " + processorTaskSet[processor]);
                        taskSet[task] = Int32.Parse(firstTaskRegex.Replace(processorTaskSet[processor], ""));
                        //Console.WriteLine("Processor: " + processor + " , Task Number: " + task + " , Assignment:  " + taskSet[task]);
                        allocationMatrix[processor, task] = taskSet[task];
                        processorTaskSet[processor] = firstTaskRemovalRegex.Replace(processorTaskSet[processor], "");
                        //Console.WriteLine("Processor task Set String after: " + processorTaskSet[processor]);
                    }
                    //Console.WriteLine("Allocation Map String, before the set removal: " + allocationMap);
                    allocationMap = firstTaskSetRemovalRegex.Replace(allocationMap, emptyText);
                    //Console.WriteLine("Allocation Map String, after the set removal: " + allocationMap);
                }
              
                allocation.setAllocationMap(allocationMatrix);

            }
            return allocation;
        }

        /// <summary>
        /// Method used to increment the index in the loop to read the filelines of the taff file.
        /// </summary>
        /// <param name="index">Current index as a positive integer.</param>
        /// <param name="number">Number to increment with as a positive integer.</param>
        /// <returns>The new incremented index.</returns>
        public int IncrementIndex(int index, int number)
        {
            index = index + number;
            //Console.WriteLine("Current Index: " + index);
            return index;
        }

        /// <summary>
        /// Method to check if the error list contains a expected error
        /// </summary>
        /// <param name="expectedError">string error value.</param>
        /// <returns>A boolean value; true if the expected error exists, false otherwise.</returns>
        public bool ValidateTAFFFileErrors(string expectedError)
        {
            foreach (string error in errorList)
            {
                if (error == expectedError)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Method to print the error details for the error box.
        /// </summary>
        /// <param name="textBox">The text box to append the errors.</param>
        /// <param name="textHeading">The text heading to change the color.</param>
        public void PrintErrorDetails(TextBox textBox, TextBox textHeading)
        {
            if (errors > 0)
            {
                textHeading.BackColor = Color.Red;
                textBox.AppendText("TAFF File" + Environment.NewLine);
                Console.WriteLine("The TAFF file is invalid.");
                Console.WriteLine("Number of syntax errors found: " + errors + " .");
                Console.WriteLine("Errors are as follows: \n ");
                textBox.AppendText("The TAFF file is invalid." + Environment.NewLine);
                textBox.AppendText("Number of errors found: " + errors + " ." + Environment.NewLine);
                textBox.AppendText("Errors are as follows: " + Environment.NewLine);

                int errCount = 1;

                foreach (string error in errorList)
                {
                    Console.WriteLine("Error- " + errCount + " : " + error);
                    textBox.AppendText("Error- " + errCount + " : " + error + Environment.NewLine);
                    errCount++;
                }

                textBox.AppendText("END of TAFF File" + Environment.NewLine);
                textBox.AppendText(Environment.NewLine);
            }

            else
            {
                textHeading.BackColor = Color.Green;
                textBox.AppendText("TAFF File" + Environment.NewLine);
                textBox.AppendText("The TAFF file is Valid, No errors to display." + Environment.NewLine);
                textBox.AppendText("END of TAFF File" + Environment.NewLine);
            }
        }
    }
}
