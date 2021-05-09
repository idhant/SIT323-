using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PT1
{
    class TaskAllocations
    {
        // array to store the filelines 
        private String[] taffFileLines;

        // error and validation related vars
        private int errors;
        private ArrayList errorList = new ArrayList();
        private bool isValid;

        // cff path and name related vars
        private string dirPath;
        private string cffName;
        private string cffFilePath;

        // vars used to store the values of relevent things from the TAFF file
        private int totalAllocations;
        private int totalTasks;
        private int totalProcessors;

        //private ArrayList allocationList = new ArrayList();
        private List<Allocation> allocationList = new List<Allocation>();
        //private Allocation[] allocationList;

        public TaskAllocations()
        {

        }

        public TaskAllocations(string cffName, int totalAllocations, int totalTasks, int totalProcessors)
        {

        }

        public void setTotalAllocations(int totalAllocations)
        {
            this.totalAllocations = totalAllocations;
        }

        public void setTotalTasks(int totalTasks)
        {
            this.totalTasks = totalTasks;
        }

        public void setTotalProcessors(int totalProcessors)
        {
            this.totalProcessors = totalProcessors;
        }

        public void setCffName(string cffName)
        {
            this.cffName = cffName;
        }

        public void setCffFilePath(string cffFilePath)
        {
            this.cffFilePath = cffFilePath;
        }

        public string getCffFilePath()
        {
            return this.cffFilePath;
        }

        public String[] getTAFFFileLines()
        {
            return taffFileLines;
        }

        public int getTotalAllocations()
        {
            return this.totalAllocations;
        }

        public int getTotalTasks()
        {
            return this.totalTasks;
        }

        public int getTotalProcessors()
        {
            return this.totalProcessors;
        }

        public void StoreTAFFLines(String filename)
        {
            this.taffFileLines = File.ReadAllLines(filename);
            Console.WriteLine("TAFF File lines copied to ArrrayList.");
        }

        /*
         CFFname = null
         OPEN taff file
         WHILE NOT EOF and CFFname == null
         line = read one line from the taff file
         remove leading and trailing white spaces
         IF line starts with “FILENAME” THEN
         CFFname = extract CFF filename from line
         END IF
         END WHILE
         CLOSE taff file
        */
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
                    Regex regex = new Regex("^FILENAME=");
                    Regex trimQuotes = new Regex("\"");

                    if (regex.IsMatch(trimmedline))
                    {
                        Console.WriteLine("CFF file name found, Extracting name... ");
                        CFFname = regex.Replace(trimmedline, "");
                        String trimmedCFFname;
                        trimmedCFFname = trimQuotes.Replace(CFFname, "");
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

        public void GetDirectoryPath(string file)
        {
            FileInfo fileInfo = new FileInfo(file);
            string path = fileInfo.Directory.ToString();
            Console.WriteLine("CFF File Path: " + path + "");
            this.dirPath = path;
        }

        public string ReadCFFFilePath()
        {
            this.cffFilePath = this.dirPath + "\\" + this.cffName;
            Console.WriteLine("Location of the CFF File is: " + this.cffFilePath);

            return this.cffFilePath;
        }

        public void WriteTAFFFileData(String filename, TextBox textBox)
        {
            StreamReader streamReader = new StreamReader(filename);
            textBox.Text = streamReader.ReadToEnd();
            Console.WriteLine("TAFF File Data printed on screen.");
        }

        public void ValidateTAFFFile(String[] filelines)
        {
            //StreamReader streamReader = new StreamReader(filename);
            string line;
            int index = 0;
            int errors = 0;
            ArrayList errorList = new ArrayList();

            Console.WriteLine("Validating TAFF File. " + "\n");

            while (index < filelines.Length)
            {
                //Console.WriteLine("Current Index: " + index);
                string trimmedline = null;
                trimmedline = filelines[index].Trim();

                Regex comment = new Regex(@"//.*");

                Regex configurationData = new Regex("CONFIGURATION-DATA");
                Regex CFFfilename = new Regex("FILENAME=");
                Regex configurationDataEndpoint = new Regex("END-CONFIGURATION-DATA");

                Regex allocationsData = new Regex("ALLOCATIONS");
                Regex allocationsDataEndpoint = new Regex("END-ALLOCATIONS");
                Regex allocationsDataCount = new Regex(@"COUNT=\d");
                Regex allocationsDataTasks = new Regex(@"TASKS=\d");
                Regex allocationsDataProcessors = new Regex(@"PROCESSORS=\d");

                Regex allocationData = new Regex("ALLOCATION");
                Regex allocationDataEndpoint = new Regex("END-ALLOCATION");
                Regex allocationDataID = new Regex(@"ID=\d");
                Regex allocationDataMap = new Regex(@"MAP=.*");

                if (trimmedline.Length == 0)
                {
                    Console.WriteLine("Line: " + trimmedline + " : Empty Line -> Valid");
                }

                else if (comment.IsMatch(trimmedline))
                {
                    Console.WriteLine("Line: " + trimmedline + " : Comment Line -> Valid");
                }

                else if (configurationData.IsMatch(trimmedline))
                {
                    if (configurationDataEndpoint.IsMatch(trimmedline))
                    {
                        Console.WriteLine("Line: " + trimmedline + " : END-CONFIGURATION -> Valid");
                    }

                    else
                    {
                        index = IncrementIndex(index, 1);
                        string filename = filelines[index].Trim();

                        if (CFFfilename.IsMatch(filename))
                        {
                            Console.WriteLine("Line: " + trimmedline + " : CONFIGURATION -> Valid");
                            Console.WriteLine("Line: " + filename + " : CONFIGURATION Block -> Valid");
                        }
                        else
                        {
                            Console.WriteLine("Line: " + trimmedline + " : CONFIGURATION Block -> Invalid");
                            errors++;
                            errorList.Add(trimmedline);
                            Console.WriteLine("Errors Found: " + errors + " .");
                        }
                    }
                }

                else if (allocationsData.IsMatch(trimmedline))
                {
                    if (allocationsDataEndpoint.IsMatch(trimmedline))
                    {
                        Console.WriteLine("Line: " + trimmedline + " : END-ALLOCATIONS -> Valid");
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
                            Console.WriteLine("Line: " + allocationCount + " : ALLOCATIONS CONFIG Block -> Valid");
                            Console.WriteLine("Line: " + taskCount + " : ALLOCATIONS CONFIG Block -> Valid");
                            Console.WriteLine("Line: " + processorCount + " : ALLOCATIONS CONFIG Block -> Valid");
                            RetrieveAllocationsDetails(allocationCount, taskCount, processorCount);
                        }

                        else
                        {
                            if (!allocationsDataCount.IsMatch(allocationCount))
                            {
                                Console.WriteLine("Line: " + allocationCount + " : ALLOCATIONS CONFIG Block -> Invalid");
                                errors++;
                                errorList.Add(allocationCount);
                                Console.WriteLine("Errors Found: " + errors + " .");
                            }
                            if (!allocationsDataTasks.IsMatch(taskCount))
                            {
                                Console.WriteLine("Line: " + taskCount + " : ALLOCATIONS CONFIG Block -> Valid");
                                errors++;
                                errorList.Add(taskCount);
                                Console.WriteLine("Errors Found: " + errors + " .");
                            }
                            if (!allocationsDataProcessors.IsMatch(processorCount))
                            {
                                Console.WriteLine("Line: " + processorCount + " : ALLOCATIONS CONFIG Block -> Valid");
                                errors++;
                                errorList.Add(processorCount);
                                Console.WriteLine("Errors Found: " + errors + " .");
                            }
                        }
                    }

                }

                else if (allocationData.IsMatch(trimmedline))
                {
                    if (allocationDataEndpoint.IsMatch(trimmedline))
                    {
                        Console.WriteLine("Line: " + trimmedline + " : END-ALLOCATION -> Valid");
                    }

                    else
                    {
                        index = IncrementIndex(index, 1);
                        string allocationID = filelines[index].Trim();
                        index = IncrementIndex(index, 1);
                        string allocationMAP = filelines[index].Trim();

                        if (allocationDataID.IsMatch(allocationID) && allocationDataMap.IsMatch(allocationMAP))
                        {
                            Console.WriteLine("Line: " + allocationID + " : ALLOCATION CONFIG Block -> Valid");
                            Console.WriteLine("Line: " + allocationMAP + " : ALLOCATION CONFIG Block -> Valid");
                            this.allocationList.Add(RetrieveAllocationDetail(allocationID, allocationMAP));
                            
                        }

                        else
                        {
                            if (!allocationDataID.IsMatch(allocationID))
                            {
                                Console.WriteLine("Line: " + allocationID + " : ALLOCATION CONFIG Block -> Invalid");
                                errors++;
                                errorList.Add(allocationID);
                                Console.WriteLine("Errors Found: " + errors + " .");
                            }
                            if (!allocationDataMap.IsMatch(allocationMAP))
                            {
                                Console.WriteLine("Line: " + allocationMAP + " : ALLOCATION CONFIG Block -> Invalid");
                                errors++;
                                errorList.Add(allocationMAP);
                                Console.WriteLine("Errors Found: " + errors + " .");
                            }
                        }
                    }

                }

                else
                {
                    Console.WriteLine("Line: " + trimmedline + " : Line doesn`t match TAFF Syntax -> Invalid");
                    errors++;
                    errorList.Add(trimmedline);
                    Console.WriteLine("Errors Found: " + errors + " .");
                }

                // increement 1 index after each line check
                index = IncrementIndex(index, 1);
            }

            Console.WriteLine("Ending Validation of TAFF file. " + "\n");

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
            }

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
                    
            }

        }

        public void RetrieveAllocationsDetails(string allocationsCount, string allocationsTasks, string allocationsProcessors)
        {
            Regex allocationsDataCount = new Regex(@"COUNT=");
            Regex allocationsDataTasks = new Regex(@"TASKS=");
            Regex allocationsDataProcessors = new Regex(@"PROCESSORS=");

            this.totalAllocations = Int32.Parse(allocationsDataCount.Replace(allocationsCount, ""));
            this.totalTasks = Int32.Parse(allocationsDataTasks.Replace(allocationsTasks, ""));
            this.totalProcessors = Int32.Parse(allocationsDataProcessors.Replace(allocationsProcessors, ""));

            //this.allocationList = new Allocation[totalAllocations];
            //this.allocationList = new Allocation[totalAllocations];

            Console.WriteLine("TotalAllocations: " + this.totalAllocations + "\n");
            Console.WriteLine("TotalTasks: " + this.totalTasks + "\n");
            Console.WriteLine("TotalProcessors: " + this.totalProcessors + "\n");
        }

        public Allocation RetrieveAllocationDetail(string allocationID, string allocationMAP)
        {
            Regex allocationDataID = new Regex(@"ID=");
            Regex allocationDataMap = new Regex(@"MAP=");

            int allocationId = Int32.Parse(allocationDataID.Replace(allocationID, ""));
            string allocationMap = allocationDataMap.Replace(allocationMAP, "");
            //Allocation[] allocations = new Allocation[totalAllocations];
            Allocation allocation = new Allocation();

            //this.allocationList = new Allocation[totalAllocations];


            if (allocationId < totalAllocations)
            {
                // id assignment
                //this.allocationList[allocationId].setAllocationID(allocationId);
                //allocations[allocationId].setAllocationID(allocationId);
                allocation.setAllocationID(allocationId);

                // Matrix size init
                int[,] allocationMatrix = new int[this.totalProcessors, this.totalTasks];

                // Matrix Tasks Row Combination extraction

                Regex firstTaskSetRegex = new Regex(@";\d,\d,\d,\d,\d");
                Regex firstTaskSetRemovalRegex = new Regex(@"^\d,\d,\d,\d,\d;");
                //Regex firstTaskColonRemovalRegex = new Regex(@"^;");
                Regex firstTaskRegex = new Regex(@",\d");
                Regex firstTaskRemovalRegex = new Regex(@"^\d,");

                string[] processorTaskSet = new string[totalProcessors];
                int[] taskSet = new int[totalTasks];

                for (int processor = 0; processor < totalProcessors; processor++)
                {
                    processorTaskSet[processor] = firstTaskSetRegex.Replace(allocationMap, "");
                    for (int task = 0; task < totalTasks; task++)
                    {
                        Console.WriteLine("Processor task Set String before: " + processorTaskSet[processor]);
                        taskSet[task] = Int32.Parse(firstTaskRegex.Replace(processorTaskSet[processor], ""));
                        Console.WriteLine("Processor: " + processor + " , Task Number: " + task + " , Assignment:  " + taskSet[task]);
                        allocationMatrix[processor, task] = taskSet[task];
                        processorTaskSet[processor] = firstTaskRemovalRegex.Replace(processorTaskSet[processor], "");
                        Console.WriteLine("Processor task Set String after: " + processorTaskSet[processor]);
                    }
                    Console.WriteLine("Allocation Map String, before the set removal: " + allocationMap);
                    allocationMap = firstTaskSetRemovalRegex.Replace(allocationMap, "");
                    Console.WriteLine("Allocation Map String, after the set removal: " + allocationMap);
                }
                //this.allocationList[allocationId].setAllocationMap(allocationMatrix);
                //allocations[allocationId].setAllocationMap(allocationMatrix);
              
                allocation.setAllocationMap(allocationMatrix);

            }
            return allocation;
        }

        public int IncrementIndex(int index, int number)
        {
            index = index + number;
            Console.WriteLine("Current Index: " + index);
            return index;
        }
    
    
    }
}
