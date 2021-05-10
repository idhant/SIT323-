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
    class Configuration
    {
        // array to store the filelines 
        private String[] cffFileLines;

        // error and validation related vars
        private int errors;
        private ArrayList errorList = new ArrayList();
        private bool isValid;

        // Vars to store information retrieve from the file
        private string fileName;

        //private int minimumTasks;
        //private int maximumTasks;

        private float programDuration;
        private int programTasks;
        private int programProcessors;

        private List<Task> tasksList = new List<Task>();
        private List<Processor> processorsList = new List<Processor>();
        private List<ProcessorType> processorTypesList = new List<ProcessorType>();

        private float[,] localCommunicationEnergy;
        private float[,] remoteCommunicationEnergy;

        public String[] GetCFFFileLines()
        {
            return cffFileLines;
        }

        public bool getIsValid()
        {
            return this.isValid;
        }

        public float getProgramDuration()
        {
            return this.programDuration;
        }

        public void setProgramDuration(float programDuration)
        {
            this.programDuration = programDuration;
        }

        public int getProgramTasks()
        {
            return this.programTasks;
        }

        public void setProgramTasks(int programTasks)
        {
            this.programTasks = programTasks;
        }

        public int getProgramProcessors()
        {
            return this.programProcessors;
        }

        public void setProgramProcessors(int programProcessors)
        {
            this.programProcessors = programProcessors;
        }

        public List<Task> getTasksList()
        {
            return this.tasksList;
        }

        public void setTasksList(List<Task> tasksList)
        {
            this.tasksList = tasksList;
        }

        public List<Processor> getProcessorsList()
        {
            return this.processorsList;
        }

        public void setProcessorsList(List<Processor> processorsList)
        {
            this.processorsList = processorsList;
        }

        public List<ProcessorType> getProcessorTypesList()
        {
            return this.processorTypesList;
        }

        public void setProcessorTypesList(List<ProcessorType> processorTypesList)
        {
            this.processorTypesList = processorTypesList;
        }

        public float[,] getLocalCommunicationEnergy()
        {
            return this.localCommunicationEnergy;
        }

        public void setLocalCommunicationEnergy(float[,] localCommunicationEnergy)
        {
            this.localCommunicationEnergy = localCommunicationEnergy;
        }

        public float[,] getRemoteCommunicationEnergy()
        {
            return this.remoteCommunicationEnergy;
        }

        public void setRemoteCommunicationEnergy(float[,] remoteCommunicationEnergy)
        {
            this.remoteCommunicationEnergy = remoteCommunicationEnergy;
        }


        public Configuration()
        {

        }

        public void StoreCFFLines(String filename)
        {
            this.cffFileLines = File.ReadAllLines(filename);
            Console.WriteLine("CFF File lines copied to ArrrayList.");
        }

        public void WriteCFFFileData(String filename, TextBox textBox)
        {
            StreamReader streamReader = new StreamReader(filename);
            textBox.Text = streamReader.ReadToEnd();
            Console.WriteLine("CFF File Data printed on screen.");
        }

        public void ValidateCFFFile(String[] filelines)
        {
            int index = 0;
            errors = 0;

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
                            && limitsDataMaximumUpload.IsMatch(maximumUpload))
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
                            //RetrieveProgramDetails(programDuration, programTasks, programProcessors);
                            RetrieveProgramDetails(programDuration, programTasks, programProcessors);
                        }

                        else
                        {
                            if (!programDataDuration.IsMatch(programDuration))
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
                        this.tasksList.Add(RetrieveTaskDetails(taskID, taskRuntime, taskReferenceFrequency, taskRam, taskDownload, taskUpload));
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
                        this.processorsList.Add(RetrieveProcessorDetails(processorID, processorType, processorFrequency, processorRam,  processorDownload, processorUpload));
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
                            && processorTypeDataC0.IsMatch(processorC0))
                        {
                            Console.WriteLine("Line: " + trimmedline + " : PROCESSOR-TYPE -> Valid");
                            Console.WriteLine("Line: " + processorName + " : PROCESSOR-TYPE BLOCK  -> Valid");
                            Console.WriteLine("Line: " + processorC2 + " : PROCESSOR-TYPE BLOCK  -> Valid");
                            Console.WriteLine("Line: " + processorC1 + " : PROCESSOR-TYPE BLOCK  -> Valid");
                            Console.WriteLine("Line: " + processorC0 + " : PROCESSOR-TYPE BLOCK  -> Valid");
                            this.processorTypesList.Add(RetrieveProcessorTypeDetails(processorName, processorC2, processorC1, processorC0));
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
                            RetrieveLocalCommunicationEnergyDetails();
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

                foreach (string error in errorList)
                {
                    Console.WriteLine("Error- " + errCount + " : " + error);
                    errCount++;
                }
            }

            else
            {
                this.isValid = true;
                Console.WriteLine("The CFF file is valid.");

                Console.WriteLine("The details retrieved from the cff file are as follows: ");

                Console.WriteLine("Program Details: ");
                Console.WriteLine("Program Duration: " + programDuration);
                Console.WriteLine("Number of Processors: " + programProcessors);
                Console.WriteLine("Number of Tasks: " + programTasks);

                Console.WriteLine("Individual Task Details: ");
                foreach(Task task in tasksList)
                {
                    Console.WriteLine("Task ID:  " + task.getTaskID());
                    Console.WriteLine("Task Runtime:  " + task.getTaskRuntime());
                    Console.WriteLine("Task Ref Freq:  " + task.getTaskReferenceFrequency());
                    Console.WriteLine("Task Ram:  " + task.getTaskRam());
                    Console.WriteLine("Task Download:  " + task.getTaskDownload());
                    Console.WriteLine("Task Upload:  " + task.getTaskUpload() + "\n");
                }

                Console.WriteLine("Individual Processor Details: ");
                foreach(Processor processor in processorsList)
                {
                    Console.WriteLine("Processor ID:  " + processor.getProcessorID());
                    Console.WriteLine("Processor Type:  " + processor.getProcessorType());
                    Console.WriteLine("Processor Freq:  " + processor.getProcessorFrequency());
                    Console.WriteLine("Processor Ram:  " + processor.getProcessorRam());
                    Console.WriteLine("Processor Download:  " + processor.getProcessorDownload());
                    Console.WriteLine("Processor Upload:  " + processor.getProcessorUpload() + "\n");
                }

                Console.WriteLine("Individual Processor Energy Details: ");
                foreach(ProcessorType processorType in processorTypesList)
                {
                    Console.WriteLine("Processor Name:  " + processorType.getProcessorName());
                    Console.WriteLine("Processor C2:  " + processorType.getProcessorC2());
                    Console.WriteLine("Processor C1:  " + processorType.getProcessorC1());
                    Console.WriteLine("Processor C0:  " + processorType.getProcessorC0() + "\n");
                }

            }
        }

        public void RetrieveProgramDetails(string programDuration, string programTasks, string programProcessors)
        {
            Regex programDataDuration = new Regex("DURATION=");
            Regex programDataTasks = new Regex("TASKS=");
            Regex programDataProcessors = new Regex("PROCESSORS=");

            this.programDuration = float.Parse(programDataDuration.Replace(programDuration, ""));
            this.programTasks = Int32.Parse(programDataTasks.Replace(programTasks, ""));
            this.programProcessors = Int32.Parse(programDataProcessors.Replace(programProcessors, ""));

            Console.WriteLine("Program Duration:  " + this.programDuration + "\n");
            Console.WriteLine("Program Tasks: " + this.programTasks + "\n");
            Console.WriteLine("Program Processors: " + this.programProcessors + "\n");
        }

        public Task RetrieveTaskDetails(string taskID, string taskRuntime, string taskReferenceFrequency, string taskRam, string taskDownload, string taskUpload)
        {
            Regex taskDataID = new Regex("ID=");
            Regex taskDataRuntime = new Regex("RUNTIME=");
            Regex taskDataReffrequency = new Regex("REFERENCE-FREQUENCY=");
            Regex taskDataRAM = new Regex("RAM=");
            Regex taskDataDOWNLOAD = new Regex("DOWNLOAD=");
            Regex taskDataUPLOAD = new Regex("UPLOAD=");

            Task task = new Task();
            int taskId = Int32.Parse(taskDataID.Replace(taskID, ""));

            if (taskId < programTasks)
            {
                task.setTaskID(Int32.Parse(taskDataID.Replace(taskID, "")));
                task.setTaskRuntime(float.Parse(taskDataRuntime.Replace(taskRuntime, "")));
                task.setTaskReferenceFrequency(float.Parse(taskDataReffrequency.Replace(taskReferenceFrequency, "")));
                task.setTaskRam(Int32.Parse(taskDataRAM.Replace(taskRam, "")));
                task.setTaskDownload(Int32.Parse(taskDataDOWNLOAD.Replace(taskDownload, "")));
                task.setTaskUpload(Int32.Parse(taskDataUPLOAD.Replace(taskUpload, "")));

                Console.WriteLine("Task ID:  " + task.getTaskID() + "\n");
                Console.WriteLine("Task Runtime:  " + task.getTaskRuntime() + "\n");
                Console.WriteLine("Task Ref Freq:  " + task.getTaskReferenceFrequency() + "\n");
                Console.WriteLine("Task Ram:  " + task.getTaskRam() + "\n");
                Console.WriteLine("Task Download:  " + task.getTaskDownload() + "\n");
                Console.WriteLine("Task Upload:  " + task.getTaskUpload() + "\n");
            }

            else
            {
                Console.WriteLine("Task Numbers exceed the maximum tasks in the program.");
                errors++;
                errorList.Add(taskDataID);
                Console.WriteLine("Errors Found: " + errors + " .");
            }

            return task;
        }

        public Processor RetrieveProcessorDetails(string processorID, string processorType, string processorFrequency, string processorRam, string processorDownload, string processorUpload)
        {
            Regex processorDataID = new Regex("ID=");
            Regex processorDataType = new Regex("TYPE=");
            Regex processorDataFrequency = new Regex("FREQUENCY=");
            Regex processorDataRAM = new Regex("RAM=");
            Regex processorDataDOWNLOAD = new Regex("DOWNLOAD=");
            Regex processorDataUPLOAD = new Regex("UPLOAD=");

            Processor processor = new Processor();
            int processorId = Int32.Parse(processorDataID.Replace(processorID, ""));

            if (processorId < programProcessors)
            {
                processor.setProcessorID(Int32.Parse(processorDataID.Replace(processorID, "")));
                processor.setProcessorType(processorDataType.Replace(processorType, ""));
                processor.setProcessorFrequency(float.Parse(processorDataFrequency.Replace(processorFrequency, "")));
                processor.setProcessorRam(Int32.Parse(processorDataRAM.Replace(processorRam, "")));
                processor.setProcessorDownload(Int32.Parse(processorDataDOWNLOAD.Replace(processorDownload, "")));
                processor.setProcessorUpload(Int32.Parse(processorDataUPLOAD.Replace(processorUpload, "")));

                Console.WriteLine("Processor ID:  " + processor.getProcessorID() + "\n");
                Console.WriteLine("Processor Type:  " + processor.getProcessorType() + "\n");
                Console.WriteLine("Processor Freq:  " + processor.getProcessorFrequency() + "\n");
                Console.WriteLine("Processor Ram:  " + processor.getProcessorRam() + "\n");
                Console.WriteLine("Processor Download:  " + processor.getProcessorDownload() + "\n");
                Console.WriteLine("Processor Upload:  " + processor.getProcessorUpload() + "\n");
            }

            else
            {
                Console.WriteLine("Processor Numbers exceed the maximum processors in the program.");
                errors++;
                errorList.Add(processorID);
                Console.WriteLine("Errors Found: " + errors + " .");
            }

            return processor;
        }

        public ProcessorType RetrieveProcessorTypeDetails(string processorName, string processorC2, string processorC1, string processorC0)
        {
            Regex processorTypeDataName = new Regex("NAME=");
            Regex processorTypeDataC2 = new Regex("C2=");
            Regex processorTypeDataC1 = new Regex("C1=");
            Regex processorTypeDataC0 = new Regex("C0=");

            ProcessorType processorType = new ProcessorType();

            processorType.setProcessorName(processorTypeDataName.Replace(processorName, ""));
            processorType.setProcessorC2(float.Parse(processorTypeDataC2.Replace(processorC2, "")));
            processorType.setProcessorC1(float.Parse(processorTypeDataC1.Replace(processorC1, "")));
            processorType.setProcessorC0(float.Parse(processorTypeDataC0.Replace(processorC0, "")));

            Console.WriteLine("Processor Name:  " + processorType.getProcessorName() + "\n");
            Console.WriteLine("Processor C2:  " + processorType.getProcessorC2() + "\n");
            Console.WriteLine("Processor C1:  " + processorType.getProcessorC1() + "\n");
            Console.WriteLine("Processor C0:  " + processorType.getProcessorC0() + "\n");


            return processorType;
        }

        public void RetrieveLocalCommunicationEnergyDetails()
        {
            //[-+]?[0-9]*\.?[0-9]+,[-+]?[0-9]*\.?[0-9]+,[-+]?[0-9]*\.?[0-9]+,[-+]?[0-9]*\.?[0-9]+,[-+]?[0-9]*\.?[0-9]+
        }

        public int IncrementIndex(int index, int number)
        {
            index = index + number;
            Console.WriteLine("Current Index: " + index);
            return index;
        }
    }
}
