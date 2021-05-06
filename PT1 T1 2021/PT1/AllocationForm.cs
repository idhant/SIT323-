using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace PT1
{
    public partial class AllocationForm : Form 
    {
        public AllocationForm()
        {
            InitializeComponent();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TaskAllocations taskAllocations = new TaskAllocations();
            
            String[] taffFilelines, cffFilelines;
            
            TextBox textBox1 = new TextBox
            {
                Size = new Size(300, 300),
                Location = new Point(15, 40),
                Multiline = true,
                ScrollBars = ScrollBars.Vertical
            };
            Controls.Add(textBox1);

            TextBox textBox2 = new TextBox
            {
                Size = new Size(300, 300),
                Location = new Point(325, 40),
                Multiline = true,
                ScrollBars = ScrollBars.Vertical
            };
            Controls.Add(textBox2);

            OpenFileDialog theDialog = new OpenFileDialog();
            theDialog.Title = "Open TAFF File";
            //theDialog.Filter = "TXT files|*.txt";
            theDialog.InitialDirectory = @"C:\";
            if (theDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string filename = theDialog.FileName;

                    taskAllocations.StoreTAFFLines(filename);

                    taskAllocations.WriteTAFFFileData(filename, textBox1);

                    taskAllocations.ValidateTAFFFile(taskAllocations.getTAFFFileLines());

                    taskAllocations.GetDirectoryPath(filename);

                    taskAllocations.ReadCFFFileName(filename);

                    taskAllocations.ReadCFFFilePath();

                    cffFilelines = File.ReadAllLines(taskAllocations.getCffFilePath());
                    Console.WriteLine("CFF File lines copied to ArrrayList.");

                    WriteCFFFileData(taskAllocations.getCffFilePath(), textBox2);
                    ValidateCFFFile(cffFilelines);


                    //taskAllocations.setCffFilePath(taskAllocations.)

                    //taffFilelines = File.ReadAllLines(filename);
                    //Console.WriteLine("TAFF File lines copied to ArrrayList.");

                    //WriteTAFFFileData(filename, textBox1);

                    //ValidateTAFFFile(taffFilelines);


                    //string cfffilepath = GetDirectoryPath(filename);
                    //string cfffilename = ReadCFFFileName(filename);
                    //taskAllocations.setCffName(cfffilename);

                    //cfffilepath = cfffilepath + "\\" + cfffilename;
                    //Console.WriteLine("Location of the CFF File is: " + cfffilepath);

                    //cffFilelines = File.ReadAllLines(cfffilepath);
                    //Console.WriteLine("CFF File lines copied to ArrrayList.");

                    //WriteCFFFileData(cfffilepath, textBox2);
                    //ValidateCFFFile(cffFilelines);

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
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
            using(streamReader)
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
            return CFFname;
        }

        /*
        OPEN taff file
        WHILE NOT EOF
        line = read one line from the taff file
        remove leading and trailing white spaces

        IF line length = 0 THEN
        do nothing as it’s just a blank line
        ELSE IF line starts with // THEN
        do nothing as it’s just a comment line
        ELSEIF line starts with keyword-1 THEN
        process line(s) with respect to keyword-1
        ELSEIF line starts with keyword-2 THEN
        process line(s) with respect to keyword-2
        ELSEIF line starts with keyword-3 THEN
        process line(s) with respect to keyword-3
        ...
        ELSEIF line starts with keyword-N THEN
        process line(s) with respect to keyword-N
        ELSE
        ERROR invalid data line
        END IF
        END WHILE
        CLOSE taff file
        */

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
                    if (allocationDataEndpoint.IsMatch(trimmedline))
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
                Console.WriteLine("The TAFF file is invalid.");
                Console.WriteLine("Number of errors found: " + errors + " .");

                Console.WriteLine("Errors are as follows: \n ");

                int errCount = 1;

                foreach (string error in errorList)
                {
                    Console.WriteLine("Error- " + errCount + " : " + error);
                    errCount++;
                }
            }

            else
            {
                Console.WriteLine("The TAFF file is valid.");
            }

        }   

        public void ValidateCFFFile(String[] filelines)
        {
            string line;
            int index = 0;
            int errors = 0;
            ArrayList errorList = new ArrayList();

            Console.WriteLine("Validating CFF File. " + "\n");

            while (index < filelines.Length)
            {
                string trimmedline = null;
                trimmedline = filelines[index].Trim();

                Regex comment = new Regex(@"//.*");

                Regex logfileData = new Regex("LOGFILE");
                Regex logfileDataname = new Regex("DEFAULT=.*");
                Regex logfileDataEndpoint = new Regex("END-LOGFILE");
                
                Regex limitsData = new Regex("LIMITS");
                Regex limitsDataEndpoint = new Regex("END-LIMITS");
                
                Regex limitsDataMinimumTasks = new Regex("MINIMUM-TASKS=.*");
                Regex limitsDataMinimumProcessors = new Regex("MINIMUM-PROCESSORS=.*");
                Regex limitsDataMinimumProcessorFrequency = new Regex("MINIMUM-PROCESSOR-FREQUENCIES=.*");
                Regex limitsDataMinimumRam = new Regex("MINIMUM-RAM=.*");
                Regex limitsDataMinimumDownload = new Regex("MINIMUM-DOWNLOAD=.*");
                Regex limitsDataMinimumUpload = new Regex("MINIMUM-UPLOAD=.*");
                
                Regex limitsDataMaximumTasks = new Regex("MAXIMUM-TASKS=.*");
                Regex limitsDataMaximumProcessors = new Regex("MAXIMUM-PROCESSORS=.*");
                Regex limitsDataMaximumProcessorFrequency = new Regex("MAXIMUM-PROCESSOR-FREQUENCIES=.*");
                Regex limitsDataMaximumRam = new Regex("MAXIMUM-RAM=.*");
                Regex limitsDataMaximumDownload = new Regex("MAXIMUM-DOWNLOAD=.*");
                Regex limitsDataMaximumUpload = new Regex("MAXIMUM-UPLOAD=.*");
                
                Regex programData = new Regex("PROGRAM");
                Regex programDataEndpoint = new Regex("END-PROGRAM");
                Regex programDataDuration = new Regex("DURATION=.*");
                Regex programDataTasks = new Regex("TASKS=.*");
                Regex programDataProcessors = new Regex("PROCESSORS=.*");

                Regex tasksData = new Regex("TASKS");
                Regex tasksDataEndpoint = new Regex("END-TASKS");

                Regex taskData = new Regex("^TASK$");
                Regex taskDataEndpoint = new Regex("END-TASK");

                Regex taskDataID = new Regex("ID=.*");
                Regex taskDataRuntime = new Regex("RUNTIME=.*");
                Regex taskDataReffrequency = new Regex("REFERENCE-FREQUENCY=.*");
                Regex taskDataRAM = new Regex("RAM=.*");
                Regex taskDataDOWNLOAD = new Regex("DOWNLOAD=.*");
                Regex taskDataUPLOAD = new Regex("UPLOAD=.*");

                Regex processorsData = new Regex("PROCESSORS");
                Regex processorsDataEndpoint = new Regex("END-PROCESSORS");

                Regex processorData = new Regex("^PROCESSOR$");
                Regex processorDataEndpoint = new Regex("END-PROCESSOR");

                Regex processorDataID = new Regex("ID=.*");
                Regex processorDataType = new Regex("TYPE=.*");
                Regex processorDataFrequency = new Regex("FREQUENCY=.*");
                Regex processorDataRAM = new Regex("RAM=.*");
                Regex processorDataDOWNLOAD = new Regex("DOWNLOAD=.*");
                Regex processorDataUPLOAD = new Regex("UPLOAD=.*");

                Regex processorsTypesData = new Regex("PROCESSOR-TYPES");
                Regex processorsTypesDataEndpoint = new Regex("END-PROCESSOR-TYPES");

                Regex processorTypeData = new Regex("PROCESSOR-TYPE");
                Regex processorTypeDataEndpoint = new Regex("END-PROCESSOR-TYPE");

                Regex processorTypeDataName = new Regex("NAME=.*");
                Regex processorTypeDataC2 = new Regex("C2=.*");
                Regex processorTypeDataC1 = new Regex("C1=.*");
                Regex processorTypeDataC0 = new Regex("C0=.*");

                Regex localCommunicationData = new Regex("LOCAL-COMMUNICATION");
                Regex localCommunicationDataEndpoint = new Regex("END-LOCAL-COMMUNICATION");
                Regex localCommunicationDataMAP = new Regex("MAP=.*");

                Regex remoteCommunicationData = new Regex("REMOTE-COMMUNICATION");
                Regex remoteCommunicationDataEndpoint = new Regex("END-REMOTE-COMMUNICATION");
                Regex remoteCommunicationDataMAP = new Regex("MAP=.*");


                if (trimmedline.Length == 0)
                {
                    Console.WriteLine("Line: " + trimmedline + " : Empty Line -> Valid");
                }

                else if (comment.IsMatch(trimmedline))
                {
                    Console.WriteLine("Line: " + trimmedline + " : Comment Line -> Valid");
                }

                else if (logfileData.IsMatch(trimmedline))
                {
                    if (logfileDataEndpoint.IsMatch(trimmedline))
                    {
                        Console.WriteLine("Line: " + trimmedline + " : END-LOGFILE -> Valid");
                    }

                    else
                    {
                        index = IncrementIndex(index, 1);
                        string filename = filelines[index].Trim();

                        if (logfileDataname.IsMatch(filename))
                        {
                            Console.WriteLine("Line: " + trimmedline + " : LOGFILE -> Valid");
                            Console.WriteLine("Line: " + filename + " : LOGFILE BLOCK  -> Valid");
                        }
                        else
                        {
                            Console.WriteLine("Line: " + trimmedline + " : LOGFILE BLOCK -> Invalid");
                            errors++;
                            errorList.Add(trimmedline);
                            Console.WriteLine("Errors Found: " + errors + " .");
                        }
                    }
                }

                else if (limitsData.IsMatch(trimmedline))
                {
                    if (limitsDataEndpoint.IsMatch(trimmedline))
                    {
                        Console.WriteLine("Line: " + trimmedline + " : END-LOGFILE -> Valid");
                    }

                    else
                    {
                        index = IncrementIndex(index, 1);
                        string minimumTasks = filelines[index].Trim();
                        index = IncrementIndex(index, 1);
                        string maximumTasks = filelines[index].Trim();
                        index = IncrementIndex(index, 1);
                        string minimumProcessors = filelines[index].Trim();
                        index = IncrementIndex(index, 1);
                        string maximumProcessors = filelines[index].Trim();
                        index = IncrementIndex(index, 1);
                        string minimumProcessorFrequency = filelines[index].Trim();
                        index = IncrementIndex(index, 1);
                        string maximumProcessorFrequency = filelines[index].Trim();
                        index = IncrementIndex(index, 1);
                        string minimumRam = filelines[index].Trim();
                        index = IncrementIndex(index, 1);
                        string maximumRam = filelines[index].Trim();
                        index = IncrementIndex(index, 1);
                        string minimumDownload = filelines[index].Trim();
                        index = IncrementIndex(index, 1);
                        string maximumDownload = filelines[index].Trim();
                        index = IncrementIndex(index, 1);
                        string minimumUpload = filelines[index].Trim();
                        index = IncrementIndex(index, 1);
                        string maximumUpload = filelines[index].Trim();

                        if (limitsDataMinimumTasks.IsMatch(minimumTasks) 
                            && limitsDataMinimumProcessors.IsMatch(minimumProcessors) 
                            && limitsDataMinimumProcessorFrequency.IsMatch(minimumProcessorFrequency) 
                            && limitsDataMinimumRam.IsMatch(minimumRam) 
                            && limitsDataMinimumDownload.IsMatch(minimumDownload) 
                            && limitsDataMinimumUpload.IsMatch(minimumUpload) 
                            && limitsDataMaximumTasks.IsMatch(maximumTasks) 
                            && limitsDataMaximumProcessors.IsMatch(maximumProcessors) 
                            && limitsDataMaximumProcessorFrequency.IsMatch(maximumProcessorFrequency) 
                            && limitsDataMaximumRam.IsMatch(maximumRam) 
                            && limitsDataMaximumDownload.IsMatch(maximumDownload) 
                            && limitsDataMaximumUpload.IsMatch(maximumUpload) )
                        {
                            Console.WriteLine("Line: " + trimmedline + " : LIMITS -> Valid");
                            Console.WriteLine("Line: " + minimumTasks + " : LIMITS BLOCK  -> Valid");
                            Console.WriteLine("Line: " + minimumProcessors + " : LIMITS BLOCK  -> Valid");
                            Console.WriteLine("Line: " + minimumProcessorFrequency + " : LIMITS BLOCK  -> Valid");
                            Console.WriteLine("Line: " + minimumRam + " : LIMITS BLOCK  -> Valid");
                            Console.WriteLine("Line: " + minimumDownload + " : LIMITS BLOCK  -> Valid");
                            Console.WriteLine("Line: " + minimumUpload + " : LIMITS BLOCK  -> Valid");
                            Console.WriteLine("Line: " + maximumTasks + " : LIMITS BLOCK  -> Valid");
                            Console.WriteLine("Line: " + maximumProcessors + " : LIMITS BLOCK  -> Valid");
                            Console.WriteLine("Line: " + maximumProcessorFrequency + " : LIMITS BLOCK  -> Valid");
                            Console.WriteLine("Line: " + maximumRam + " : LIMITS BLOCK  -> Valid");
                            Console.WriteLine("Line: " + maximumDownload + " : LIMITS BLOCK  -> Valid");
                            Console.WriteLine("Line: " + maximumUpload + " : LIMITS BLOCK  -> Valid");
                        }

                        else
                        {
                            if (!limitsDataMinimumTasks.IsMatch(minimumTasks))
                            {
                                Console.WriteLine("Line: " + minimumTasks + " : LIMITS BLOCK  -> Invalid");
                                errors++;
                                errorList.Add(minimumTasks);
                                Console.WriteLine("Errors Found: " + errors + " .");
                            }
                            if (!limitsDataMinimumProcessors.IsMatch(minimumProcessors))
                            {
                                Console.WriteLine("Line: " + minimumProcessors + " : LIMITS BLOCK  -> Invalid");
                                errors++;
                                errorList.Add(minimumProcessors);
                                Console.WriteLine("Errors Found: " + errors + " .");
                            }
                            if (!limitsDataMinimumProcessorFrequency.IsMatch(minimumProcessorFrequency))
                            {
                                Console.WriteLine("Line: " + minimumProcessorFrequency + " : LIMITS BLOCK  -> Invalid");
                                errors++;
                                errorList.Add(minimumProcessorFrequency);
                                Console.WriteLine("Errors Found: " + errors + " .");
                            }
                            if (!limitsDataMinimumRam.IsMatch(minimumRam))
                            {
                                Console.WriteLine("Line: " + minimumRam + " : LIMITS BLOCK  -> Invalid");
                                errors++;
                                errorList.Add(minimumRam);
                                Console.WriteLine("Errors Found: " + errors + " .");
                            }
                            if (!limitsDataMinimumDownload.IsMatch(minimumDownload))
                            {
                                Console.WriteLine("Line: " + minimumDownload + " : LIMITS BLOCK  -> Invalid");
                                errors++;
                                errorList.Add(minimumDownload);
                                Console.WriteLine("Errors Found: " + errors + " .");
                            }
                            if (!limitsDataMinimumUpload.IsMatch(minimumUpload))
                            {
                                Console.WriteLine("Line: " + minimumUpload + " : LIMITS BLOCK  -> Invalid");
                                errors++;
                                errorList.Add(minimumUpload);
                                Console.WriteLine("Errors Found: " + errors + " .");
                            }
                            if (!limitsDataMaximumTasks.IsMatch(maximumTasks))
                            {
                                Console.WriteLine("Line: " + maximumTasks + " : LIMITS BLOCK  -> Invalid");
                                errors++;
                                errorList.Add(maximumTasks);
                                Console.WriteLine("Errors Found: " + errors + " .");
                            }
                            if (!limitsDataMaximumProcessors.IsMatch(maximumProcessors))
                            {
                                Console.WriteLine("Line: " + maximumProcessors + " : LIMITS BLOCK  -> Invalid");
                                errors++;
                                errorList.Add(maximumProcessors);
                                Console.WriteLine("Errors Found: " + errors + " .");
                            }
                            if (!limitsDataMaximumProcessorFrequency.IsMatch(maximumProcessorFrequency))
                            {
                                Console.WriteLine("Line: " + maximumProcessorFrequency + " : LIMITS BLOCK  -> Invalid");
                                errors++;
                                errorList.Add(maximumProcessorFrequency);
                                Console.WriteLine("Errors Found: " + errors + " .");
                            }
                            if (!limitsDataMaximumRam.IsMatch(maximumRam))
                            {
                                Console.WriteLine("Line: " + maximumRam + " : LIMITS BLOCK  -> Invalid");
                                errors++;
                                errorList.Add(maximumRam);
                                Console.WriteLine("Errors Found: " + errors + " .");
                            }
                            if (!limitsDataMaximumDownload.IsMatch(maximumDownload))
                            {
                                Console.WriteLine("Line: " + maximumDownload + " : LIMITS BLOCK  -> Invalid");
                                errors++;
                                errorList.Add(maximumDownload);
                                Console.WriteLine("Errors Found: " + errors + " .");
                            }
                            if (!limitsDataMaximumUpload.IsMatch(maximumUpload))
                            {
                                Console.WriteLine("Line: " + maximumUpload + " : LIMITS BLOCK  -> Invalid");
                                errors++;
                                errorList.Add(maximumUpload);
                                Console.WriteLine("Errors Found: " + errors + " .");
                            }
                        }
                    }
                }

                else if (programData.IsMatch(trimmedline))
                {
                    if (programDataEndpoint.IsMatch(trimmedline))
                    {
                        Console.WriteLine("Line: " + trimmedline + " : END-PROGRAM -> Valid");
                    }

                    else
                    {
                        index = IncrementIndex(index, 1);
                        string programDuration = filelines[index].Trim();
                        index = IncrementIndex(index, 1);
                        string programTasks = filelines[index].Trim();
                        index = IncrementIndex(index, 1);
                        string programProcessors = filelines[index].Trim();

                        if (programDataDuration.IsMatch(programDuration) && programDataTasks.IsMatch(programTasks) && programDataProcessors.IsMatch(programProcessors))
                        {
                            Console.WriteLine("Line: " + trimmedline + " : PROGRAM -> Valid");
                            Console.WriteLine("Line: " + programDuration + " : PROGRAM BLOCK  -> Valid");
                            Console.WriteLine("Line: " + programTasks + " : PROGRAM BLOCK  -> Valid");
                            Console.WriteLine("Line: " + programProcessors + " : PROGRAM BLOCK  -> Valid");
                        }

                        else
                        {
                            if(!programDataDuration.IsMatch(programDuration))
                            {
                                Console.WriteLine("Line: " + programDataDuration + " : PROGRAM BLOCK  -> Valid");
                                errors++;
                                errorList.Add(programDataDuration);
                                Console.WriteLine("Errors Found: " + errors + " .");
                            }
                            
                            if (!programDataTasks.IsMatch(programTasks))
                            {
                                Console.WriteLine("Line: " + programDataTasks + " : PROGRAM BLOCK  -> Valid");
                                errors++;
                                errorList.Add(programDataTasks);
                                Console.WriteLine("Errors Found: " + errors + " .");
                            }
                            
                            if (!programDataProcessors.IsMatch(programProcessors))
                            {
                                Console.WriteLine("Line: " + programDataProcessors + " : PROGRAM BLOCK  -> Valid");
                                errors++;
                                errorList.Add(programDataProcessors);
                                Console.WriteLine("Errors Found: " + errors + " .");
                            }
                        }
                    }
                }

                else if (tasksData.IsMatch(trimmedline))
                {
                    if (tasksDataEndpoint.IsMatch(trimmedline))
                    {
                        Console.WriteLine("Line: " + trimmedline + " : END-TASKS -> Valid");
                    }

                    else
                    {
                        Console.WriteLine("Line: " + trimmedline + " : TASKS -> Valid");
                    }
                }

                else if (taskData.IsMatch(trimmedline))
                {

                    index = IncrementIndex(index, 1);
                    string taskID = filelines[index].Trim();
                    index = IncrementIndex(index, 1);
                    string taskRuntime = filelines[index].Trim();
                    index = IncrementIndex(index, 1);
                    string taskReferenceFrequency = filelines[index].Trim();
                    index = IncrementIndex(index, 1);
                    string taskRam = filelines[index].Trim();
                    index = IncrementIndex(index, 1);
                    string taskDownload = filelines[index].Trim();
                    index = IncrementIndex(index, 1);
                    string taskUpload = filelines[index].Trim();

                    if (taskDataID.IsMatch(taskID)
                        && taskDataRuntime.IsMatch(taskRuntime)
                        && taskDataReffrequency.IsMatch(taskReferenceFrequency)
                        && taskDataRAM.IsMatch(taskRam)
                        && taskDataDOWNLOAD.IsMatch(taskDownload)
                        && taskDataUPLOAD.IsMatch(taskUpload))
                    {
                        Console.WriteLine("Line: " + trimmedline + " : TASK -> Valid");
                        Console.WriteLine("Line: " + taskID + " : TASK BLOCK  -> Valid");
                        Console.WriteLine("Line: " + taskRuntime + " : TASK BLOCK  -> Valid");
                        Console.WriteLine("Line: " + taskReferenceFrequency + " : TASK BLOCK  -> Valid");
                        Console.WriteLine("Line: " + taskRam + " : TASK BLOCK  -> Valid");
                        Console.WriteLine("Line: " + taskDownload + " : TASK BLOCK  -> Valid");
                        Console.WriteLine("Line: " + taskUpload + " : TASK BLOCK  -> Valid");
                    }

                    else
                    {
                        if (!taskDataID.IsMatch(taskID))
                        {
                            Console.WriteLine("Line: " + taskID + " : TASK BLOCK  -> Invalid");
                            errors++;
                            errorList.Add(taskID);
                            Console.WriteLine("Errors Found: " + errors + " .");
                        }

                        if (!taskDataRuntime.IsMatch(taskRuntime))
                        {
                            Console.WriteLine("Line: " + taskRuntime + " : TASK BLOCK  -> Invalid");
                            errors++;
                            errorList.Add(taskRuntime);
                            Console.WriteLine("Errors Found: " + errors + " .");
                        }

                        if (!taskDataReffrequency.IsMatch(taskReferenceFrequency))
                        {
                            Console.WriteLine("Line: " + taskReferenceFrequency + " : TASK BLOCK  -> Invalid");
                            errors++;
                            errorList.Add(taskReferenceFrequency);
                            Console.WriteLine("Errors Found: " + errors + " .");
                        }

                        if (!taskDataRAM.IsMatch(taskRam))
                        {
                            Console.WriteLine("Line: " + taskRam + " : TASK BLOCK  -> Invalid");
                            errors++;
                            errorList.Add(taskRam);
                            Console.WriteLine("Errors Found: " + errors + " .");
                        }

                        if (!taskDataDOWNLOAD.IsMatch(taskDownload))
                        {
                            Console.WriteLine("Line: " + taskDownload + " : TASK BLOCK  -> Invalid");
                            errors++;
                            errorList.Add(taskDownload);
                            Console.WriteLine("Errors Found: " + errors + " .");
                        }

                        if (!taskDataUPLOAD.IsMatch(taskUpload))
                        {
                            Console.WriteLine("Line: " + taskUpload + " : TASK BLOCK  -> Invalid");
                            errors++;
                            errorList.Add(taskUpload);
                            Console.WriteLine("Errors Found: " + errors + " .");
                        }

                    }

                }

                else if (taskDataEndpoint.IsMatch(trimmedline))
                {
                    Console.WriteLine("Line: " + trimmedline + " : END-TASK -> Valid");
                }

                else if (processorsData.IsMatch(trimmedline))
                {
                    if (processorsDataEndpoint.IsMatch(trimmedline))
                    {
                        Console.WriteLine("Line: " + trimmedline + " : END-PROCESSORS -> Valid");
                    }

                    else
                    {
                        Console.WriteLine("Line: " + trimmedline + " : PROCESSORS -> Valid");
                    }
                }

                else if (processorData.IsMatch(trimmedline))
                {
                    index = IncrementIndex(index, 1);
                    string processorID = filelines[index].Trim();
                    index = IncrementIndex(index, 1);
                    string processorType = filelines[index].Trim();
                    index = IncrementIndex(index, 1);
                    string processorFrequency = filelines[index].Trim();
                    index = IncrementIndex(index, 1);
                    string processorRam = filelines[index].Trim();
                    index = IncrementIndex(index, 1);
                    string processorDownload = filelines[index].Trim();
                    index = IncrementIndex(index, 1);
                    string processorUpload = filelines[index].Trim();

                    if (processorDataID.IsMatch(processorID)
                        && processorDataType.IsMatch(processorType)
                        && processorDataFrequency.IsMatch(processorFrequency)
                        && processorDataRAM.IsMatch(processorRam)
                        && processorDataDOWNLOAD.IsMatch(processorDownload)
                        && processorDataUPLOAD.IsMatch(processorUpload))
                    {
                        Console.WriteLine("Line: " + trimmedline + " : PROCESSOR -> Valid");
                        Console.WriteLine("Line: " + processorID + " : PROCESSOR BLOCK  -> Valid");
                        Console.WriteLine("Line: " + processorType + " : PROCESSOR BLOCK  -> Valid");
                        Console.WriteLine("Line: " + processorFrequency + " : PROCESSOR BLOCK  -> Valid");
                        Console.WriteLine("Line: " + processorRam + " : PROCESSOR BLOCK  -> Valid");
                        Console.WriteLine("Line: " + processorDownload + " : PROCESSOR BLOCK  -> Valid");
                        Console.WriteLine("Line: " + processorUpload + " : PROCESSOR BLOCK  -> Valid");
                    }

                    else
                    {
                        if (!processorDataID.IsMatch(processorID))
                        {
                            Console.WriteLine("Line: " + processorID + " : PROCESSOR BLOCK  -> Invalid");
                            errors++;
                            errorList.Add(processorID);
                            Console.WriteLine("Errors Found: " + errors + " .");
                        }

                        if (!processorDataType.IsMatch(processorType))
                        {
                            Console.WriteLine("Line: " + processorType + " : PROCESSOR BLOCK  -> Invalid");
                            errors++;
                            errorList.Add(processorType);
                            Console.WriteLine("Errors Found: " + errors + " .");
                        }

                        if (!processorDataFrequency.IsMatch(processorFrequency))
                        {
                            Console.WriteLine("Line: " + processorFrequency + " : PROCESSOR BLOCK  -> Invalid");
                            errors++;
                            errorList.Add(processorFrequency);
                            Console.WriteLine("Errors Found: " + errors + " .");
                        }

                        if (!processorDataRAM.IsMatch(processorRam))
                        {
                            Console.WriteLine("Line: " + processorRam + " : PROCESSOR BLOCK  -> Invalid");
                            errors++;
                            errorList.Add(processorRam);
                            Console.WriteLine("Errors Found: " + errors + " .");
                        }

                        if (!processorDataDOWNLOAD.IsMatch(processorDownload))
                        {
                            Console.WriteLine("Line: " + processorDownload + " : PROCESSOR BLOCK  -> Invalid");
                            errors++;
                            errorList.Add(processorDownload);
                            Console.WriteLine("Errors Found: " + errors + " .");
                        }

                        if (!processorDataUPLOAD.IsMatch(processorUpload))
                        {
                            Console.WriteLine("Line: " + processorUpload + " : PROCESSOR BLOCK  -> Invalid");
                            errors++;
                            errorList.Add(processorUpload);
                            Console.WriteLine("Errors Found: " + errors + " .");
                        }

                    }

                }

                else if (processorDataEndpoint.IsMatch(trimmedline))
                {
                    Console.WriteLine("Line: " + trimmedline + " : END-PROCESSOR -> Valid");
                }

                else if (processorsTypesData.IsMatch(trimmedline))
                {
                    if (processorsTypesDataEndpoint.IsMatch(trimmedline))
                    {
                        Console.WriteLine("Line: " + trimmedline + " : END-PROCESSOR-TYPES -> Valid");
                    }

                    else
                    {
                        Console.WriteLine("Line: " + trimmedline + " : PROCESSORS-TYPES -> Valid");
                    }
                }

                else if (processorTypeData.IsMatch(trimmedline))
                {
                    if (processorTypeDataEndpoint.IsMatch(trimmedline))
                    {
                        Console.WriteLine("Line: " + trimmedline + " : END-PROCESSOR-TYPE -> Valid");
                    }

                    else
                    {
                        index = IncrementIndex(index, 1);
                        string processorName = filelines[index].Trim();
                        index = IncrementIndex(index, 1);
                        string processorC2 = filelines[index].Trim();
                        index = IncrementIndex(index, 1);
                        string processorC1 = filelines[index].Trim();
                        index = IncrementIndex(index, 1);
                        string processorC0 = filelines[index].Trim();

                        if (processorTypeDataName.IsMatch(processorName) 
                            && processorTypeDataC2.IsMatch(processorC2)
                            && processorTypeDataC1.IsMatch(processorC1)
                            && processorTypeDataC0.IsMatch(processorC0) )
                        {
                            Console.WriteLine("Line: " + trimmedline + " : PROCESSOR-TYPE -> Valid");
                            Console.WriteLine("Line: " + processorName + " : PROCESSOR-TYPE BLOCK  -> Valid");
                            Console.WriteLine("Line: " + processorC2 + " : PROCESSOR-TYPE BLOCK  -> Valid");
                            Console.WriteLine("Line: " + processorC1 + " : PROCESSOR-TYPE BLOCK  -> Valid");
                            Console.WriteLine("Line: " + processorC0 + " : PROCESSOR-TYPE BLOCK  -> Valid");
                        }

                        else
                        {
                            if (!processorTypeDataName.IsMatch(processorName))
                            {
                                Console.WriteLine("Line: " + processorName + " : PROCESSOR-TYPE BLOCK  -> Valid");
                                errors++;
                                errorList.Add(processorName);
                                Console.WriteLine("Errors Found: " + errors + " .");
                            }

                            if (!processorTypeDataC2.IsMatch(processorC2))
                            {
                                Console.WriteLine("Line: " + processorC2 + " : PROCESSOR-TYPE BLOCK  -> Valid");
                                errors++;
                                errorList.Add(processorC2);
                                Console.WriteLine("Errors Found: " + errors + " .");
                            }

                            if (!processorTypeDataC1.IsMatch(processorC1))
                            {
                                Console.WriteLine("Line: " + processorC1 + " : PROCESSOR-TYPE BLOCK  -> Valid");
                                errors++;
                                errorList.Add(processorC1);
                                Console.WriteLine("Errors Found: " + errors + " .");
                            }

                            if (!processorTypeDataC0.IsMatch(processorC0))
                            {
                                Console.WriteLine("Line: " + processorC0 + " : PROCESSOR-TYPE BLOCK  -> Valid");
                                errors++;
                                errorList.Add(processorC0);
                                Console.WriteLine("Errors Found: " + errors + " .");
                            }
                        }
                    }
                }

                else if (localCommunicationData.IsMatch(trimmedline))
                {
                    if (localCommunicationDataEndpoint.IsMatch(trimmedline))
                    {
                        Console.WriteLine("Line: " + trimmedline + " : END-LOCAL-COMMUNICATION -> Valid");
                    }

                    else
                    {
                        index = IncrementIndex(index, 1);
                        string localCommunicationMAP = filelines[index].Trim();

                        if (localCommunicationDataMAP.IsMatch(localCommunicationMAP))
                        {
                            Console.WriteLine("Line: " + trimmedline + " : LOCAL-COMMUNICATION -> Valid");
                            Console.WriteLine("Line: " + localCommunicationMAP + " : LOCAL-COMMUNICATION BLOCK  -> Valid");
                        }
                        else
                        {
                            Console.WriteLine("Line: " + localCommunicationMAP + " : LOCAL-COMMUNICATION BLOCK  -> Invalid");
                            errors++;
                            errorList.Add(localCommunicationMAP);
                            Console.WriteLine("Errors Found: " + errors + " .");
                        }
                    }
                }

                else if (remoteCommunicationData.IsMatch(trimmedline))
                {
                    if (remoteCommunicationDataEndpoint.IsMatch(trimmedline))
                    {
                        Console.WriteLine("Line: " + trimmedline + " : END-REMOTE-COMMUNICATION -> Valid");
                    }

                    else
                    {
                        index = IncrementIndex(index, 1);
                        string remoteCommunicationMAP = filelines[index].Trim();

                        if (remoteCommunicationDataMAP.IsMatch(remoteCommunicationMAP))
                        {
                            Console.WriteLine("Line: " + trimmedline + " : REMOTE-COMMUNICATION -> Valid");
                            Console.WriteLine("Line: " + remoteCommunicationMAP + " : REMOTE-COMMUNICATION BLOCK  -> Valid");
                        }
                        else
                        {
                            Console.WriteLine("Line: " + remoteCommunicationMAP + " : REMOTE-COMMUNICATION BLOCK  -> Invalid");
                            errors++;
                            errorList.Add(remoteCommunicationMAP);
                            Console.WriteLine("Errors Found: " + errors + " .");
                        }
                    }
                }

                else
                {
                    Console.WriteLine("Line: " + trimmedline + " : Line doesn`t match CFF Syntax -> Invalid");
                    errors++;
                    errorList.Add(trimmedline);
                    Console.WriteLine("Errors Found: " + errors + " .");
                }

                // increement 1 index after each line check
                index = IncrementIndex(index, 1);
            }

            Console.WriteLine("Ending Validation of CFF file. " + "\n");

            if (errors > 0)
            {
                Console.WriteLine("The CFF file is invalid.");
                Console.WriteLine("Number of errors found: " + errors + " .");
                
                Console.WriteLine("Errors are as follows: \n ");

                int errCount = 1;

                foreach(string error in errorList)
                {
                    Console.WriteLine("Error- " + errCount + " : " + error);
                    errCount++;
                }
            }

            else
            {
                Console.WriteLine("The CFF file is valid.");
            }
        }

        public void WriteTAFFFileData(String filename, TextBox textBox)
        {
            StreamReader streamReader = new StreamReader(filename);
            textBox.Text = streamReader.ReadToEnd();
            Console.WriteLine("TAFF File Data printed on screen.");
        }

        public void WriteCFFFileData(String filename, TextBox textBox)
        {
            StreamReader streamReader = new StreamReader(filename);
            textBox.Text = streamReader.ReadToEnd();
            Console.WriteLine("CFF File Data printed on screen.");
        }

        public string GetDirectoryPath(string file)
        {
            FileInfo fileInfo = new FileInfo(file);
            string path = fileInfo.Directory.ToString();
            Console.WriteLine("CFF File Path: " + path + "");
            return path;
        }

        public int IncrementIndex(int index, int number)
        {
            index = index + number;
            Console.WriteLine("Current Index: " + index);
            return index;
        }
    }
}
