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
    /// Object type to store and manipulate the details of the .cff file.
    /// </summary>
    public class Configuration
    {
        // CLASS MEMBER VARIABLES

        /// <summary>
        /// Member Variable which stores the lines from the cff file in a string array.
        /// </summary>
        private String[] cffFileLines;

        // error and validation related vars
        /// <summary>
        /// Member Variable which stores the number of errors as a integer.
        /// </summary>
        private int errors;

        /// <summary>
        /// Member Variable which stores the errors in a Array List.
        /// </summary>
        private ArrayList errorList = new ArrayList();

        /// <summary>
        /// Member Variable which stores a boolean value to see if the cff file is valid or not.
        /// </summary>
        private bool isValid;

        /// <summary>
        /// Member variable which stores the maximum allowed program duration.
        /// </summary>
        private float programDuration;

        /// <summary>
        /// Member variable which stores the total program tasks.
        /// </summary>
        private int programTasks;

        /// <summary>
        /// Member variable which stores the total program processors
        /// </summary>
        private int programProcessors;

        /// <summary>
        /// Member Variable which stores the the list of tasks.
        /// </summary>
        private List<Task> tasksList = new List<Task>();

        /// <summary>
        /// Member Variable which stores the the list of processors.
        /// </summary>
        private List<Processor> processorsList = new List<Processor>();

        /// <summary>
        /// Member Variable which stores the the list of processors energy.
        /// </summary>
        private List<ProcessorType> processorTypesList = new List<ProcessorType>();

        //private float[,] localCommunicationEnergy;
        //private float[,] remoteCommunicationEnergy;

        // CLASS PROPERTY METHODS

        /// <summary>
        /// Getter property to retrieve all the lines read from the cff file.
        /// </summary>
        /// <returns>A string array which contains the lines.</returns>
        public String[] GetCFFFileLines()
        {
            return cffFileLines;
        }

        /// <summary>
        /// Getter property to retrieve the bool value representing the validity of the cff file.
        /// </summary>
        /// <returns>A boolean value representing the validity of the cff file.</returns>
        public bool getIsValid()
        {
            return this.isValid;
        }

        /// <summary>
        /// Getter property to retrieve the program duration.
        /// </summary>
        /// <returns>A float representing the program duration.</returns>
        public float getProgramDuration()
        {
            return this.programDuration;
        }

        /// <summary>
        /// Getter property to retrieve the tasks list.
        /// </summary>
        /// <returns>A list of type List{Task} </returns>
        public List<Task> getTasksList()
        {
            return this.tasksList;
        }

        /// <summary>
        /// Getter property to retrieve the tasks list.
        /// </summary>
        /// <returns>A list of type List{Task} </returns>
        public List<Processor> getProcessorsList()
        {
            return this.processorsList;
        }

        // CLASS CONSTRUCTORS

        public Configuration()
        {

        }

        // CLASS METHODS

        /// <summary>
        /// Method to store the lines read from the cff file to the cffFilelines member variable.
        /// </summary>
        /// <param name="filename">The path of the cff file that needs to be read.</param>
        public void StoreCFFLines(String filename)
        {
            this.cffFileLines = File.ReadAllLines(filename);
            Console.WriteLine("CFF File lines copied to ArrrayList.");
        }

        /// <summary>
        /// Method to append the text of the cff file to the textBox in the application.
        /// </summary>
        /// <param name="filename">Path of the file that needs to be read to end.</param>
        /// <param name="textBox">Textbox to which lines are to be appended.</param>
        public void WriteCFFFileData(String filename, TextBox textBox)
        {
            StreamReader streamReader = new StreamReader(filename);
            textBox.Text = streamReader.ReadToEnd();
            Console.WriteLine("CFF File Data printed on screen.");
        }

        public bool ValidateCFFFile(String[] filelines)
        {
            // Local initialisation of variables used to hold errors info and index of the line array.
            int index = 0;
            errors = 0;

            Console.WriteLine("Validating CFF File. " + "\n");

            // Looping till the file ends.
            while (index < filelines.Length)
            { 
                // initialising the trimmedline and trimming it
                string trimmedline = null;
                trimmedline = filelines[index].Trim();

                // Regex init

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

                // Empty Line
                if (trimmedline.Length == 0)
                {
                    //Console.WriteLine("Line: " + trimmedline + " : Empty Line -> Valid");
                }

                // Comment Line
                else if (comment.IsMatch(trimmedline))
                {
                    //Console.WriteLine("Line: " + trimmedline + " : Comment Line -> Valid");
                }

                // Log block
                // If block invalid add error to list and increment number of errors
                else if (logfileData.IsMatch(trimmedline))
                {
                    if (logfileDataEndpoint.IsMatch(trimmedline))
                    {
                        //Console.WriteLine("Line: " + trimmedline + " : END-LOGFILE -> Valid");
                    }

                    else
                    {
                        index = IncrementIndex(index, 1);
                        string filename = filelines[index].Trim();

                        if (logfileDataname.IsMatch(filename))
                        {
                            //Console.WriteLine("Line: " + trimmedline + " : LOGFILE -> Valid");
                            //Console.WriteLine("Line: " + filename + " : LOGFILE BLOCK  -> Valid");
                        }
                        else
                        {
                            //Console.WriteLine("Line: " + trimmedline + " : LOGFILE BLOCK -> Invalid");
                            errors++;
                            errorList.Add(trimmedline);
                            //Console.WriteLine("Errors Found: " + errors + " .");
                        }
                    }
                }

                // Limits block
                // If block invalid add error to list and increment number of errors
                else if (limitsData.IsMatch(trimmedline))
                {
                    if (limitsDataEndpoint.IsMatch(trimmedline))
                    {
                        //Console.WriteLine("Line: " + trimmedline + " : END-LOGFILE -> Valid");
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
                            //Console.WriteLine("Line: " + trimmedline + " : LIMITS -> Valid");
                            //Console.WriteLine("Line: " + minimumTasks + " : LIMITS BLOCK  -> Valid");
                            //Console.WriteLine("Line: " + minimumProcessors + " : LIMITS BLOCK  -> Valid");
                            //Console.WriteLine("Line: " + minimumProcessorFrequency + " : LIMITS BLOCK  -> Valid");
                            //Console.WriteLine("Line: " + minimumRam + " : LIMITS BLOCK  -> Valid");
                            //Console.WriteLine("Line: " + minimumDownload + " : LIMITS BLOCK  -> Valid");
                            //Console.WriteLine("Line: " + minimumUpload + " : LIMITS BLOCK  -> Valid");
                            //Console.WriteLine("Line: " + maximumTasks + " : LIMITS BLOCK  -> Valid");
                            //Console.WriteLine("Line: " + maximumProcessors + " : LIMITS BLOCK  -> Valid");
                            //Console.WriteLine("Line: " + maximumProcessorFrequency + " : LIMITS BLOCK  -> Valid");
                            //Console.WriteLine("Line: " + maximumRam + " : LIMITS BLOCK  -> Valid");
                            //Console.WriteLine("Line: " + maximumDownload + " : LIMITS BLOCK  -> Valid");
                            //Console.WriteLine("Line: " + maximumUpload + " : LIMITS BLOCK  -> Valid");
                        }

                        else
                        {
                            if (!limitsDataMinimumTasks.IsMatch(minimumTasks))
                            {
                                //Console.WriteLine("Line: " + minimumTasks + " : LIMITS BLOCK  -> Invalid");
                                errors++;
                                errorList.Add(minimumTasks);
                                //Console.WriteLine("Errors Found: " + errors + " .");
                            }
                            if (!limitsDataMinimumProcessors.IsMatch(minimumProcessors))
                            {
                                //Console.WriteLine("Line: " + minimumProcessors + " : LIMITS BLOCK  -> Invalid");
                                errors++;
                                errorList.Add(minimumProcessors);
                                //Console.WriteLine("Errors Found: " + errors + " .");
                            }
                            if (!limitsDataMinimumProcessorFrequency.IsMatch(minimumProcessorFrequency))
                            {
                                //Console.WriteLine("Line: " + minimumProcessorFrequency + " : LIMITS BLOCK  -> Invalid");
                                errors++;
                                errorList.Add(minimumProcessorFrequency);
                                //Console.WriteLine("Errors Found: " + errors + " .");
                            }
                            if (!limitsDataMinimumRam.IsMatch(minimumRam))
                            {
                                //Console.WriteLine("Line: " + minimumRam + " : LIMITS BLOCK  -> Invalid");
                                errors++;
                                errorList.Add(minimumRam);
                                //Console.WriteLine("Errors Found: " + errors + " .");
                            }
                            if (!limitsDataMinimumDownload.IsMatch(minimumDownload))
                            {
                                //Console.WriteLine("Line: " + minimumDownload + " : LIMITS BLOCK  -> Invalid");
                                errors++;
                                errorList.Add(minimumDownload);
                                //Console.WriteLine("Errors Found: " + errors + " .");
                            }
                            if (!limitsDataMinimumUpload.IsMatch(minimumUpload))
                            {
                                //Console.WriteLine("Line: " + minimumUpload + " : LIMITS BLOCK  -> Invalid");
                                errors++;
                                errorList.Add(minimumUpload);
                                //Console.WriteLine("Errors Found: " + errors + " .");
                            }
                            if (!limitsDataMaximumTasks.IsMatch(maximumTasks))
                            {
                                //Console.WriteLine("Line: " + maximumTasks + " : LIMITS BLOCK  -> Invalid");
                                errors++;
                                errorList.Add(maximumTasks);
                                //Console.WriteLine("Errors Found: " + errors + " .");
                            }
                            if (!limitsDataMaximumProcessors.IsMatch(maximumProcessors))
                            {
                                //Console.WriteLine("Line: " + maximumProcessors + " : LIMITS BLOCK  -> Invalid");
                                errors++;
                                errorList.Add(maximumProcessors);
                                //Console.WriteLine("Errors Found: " + errors + " .");
                            }
                            if (!limitsDataMaximumProcessorFrequency.IsMatch(maximumProcessorFrequency))
                            {
                                //Console.WriteLine("Line: " + maximumProcessorFrequency + " : LIMITS BLOCK  -> Invalid");
                                errors++;
                                errorList.Add(maximumProcessorFrequency);
                                //Console.WriteLine("Errors Found: " + errors + " .");
                            }
                            if (!limitsDataMaximumRam.IsMatch(maximumRam))
                            {
                                //Console.WriteLine("Line: " + maximumRam + " : LIMITS BLOCK  -> Invalid");
                                errors++;
                                errorList.Add(maximumRam);
                                //Console.WriteLine("Errors Found: " + errors + " .");
                            }
                            if (!limitsDataMaximumDownload.IsMatch(maximumDownload))
                            {
                                //Console.WriteLine("Line: " + maximumDownload + " : LIMITS BLOCK  -> Invalid");
                                errors++;
                                errorList.Add(maximumDownload);
                                //Console.WriteLine("Errors Found: " + errors + " .");
                            }
                            if (!limitsDataMaximumUpload.IsMatch(maximumUpload))
                            {
                                //Console.WriteLine("Line: " + maximumUpload + " : LIMITS BLOCK  -> Invalid");
                                errors++;
                                errorList.Add(maximumUpload);
                                //Console.WriteLine("Errors Found: " + errors + " .");
                            }
                        }
                    }
                }

                // Program Block
                // If block valid, retrieve general program details
                // If block invalid add error to list and increment number of errors
                else if (programData.IsMatch(trimmedline))
                {
                    if (programDataEndpoint.IsMatch(trimmedline))
                    {
                        //Console.WriteLine("Line: " + trimmedline + " : END-PROGRAM -> Valid");
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
                            //Console.WriteLine("Line: " + trimmedline + " : PROGRAM -> Valid");
                            //Console.WriteLine("Line: " + programDuration + " : PROGRAM BLOCK  -> Valid");
                            //Console.WriteLine("Line: " + programTasks + " : PROGRAM BLOCK  -> Valid");
                            //Console.WriteLine("Line: " + programProcessors + " : PROGRAM BLOCK  -> Valid");
                            //RetrieveProgramDetails(programDuration, programTasks, programProcessors);
                            RetrieveProgramDetails(programDuration, programTasks, programProcessors);
                        }

                        else
                        {
                            if (!programDataDuration.IsMatch(programDuration))
                            {
                                //Console.WriteLine("Line: " + programDataDuration + " : PROGRAM BLOCK  -> Valid");
                                errors++;
                                errorList.Add(programDataDuration);
                                //Console.WriteLine("Errors Found: " + errors + " .");
                            }

                            if (!programDataTasks.IsMatch(programTasks))
                            {
                                //Console.WriteLine("Line: " + programDataTasks + " : PROGRAM BLOCK  -> Valid");
                                errors++;
                                errorList.Add(programDataTasks);
                                //Console.WriteLine("Errors Found: " + errors + " .");
                            }

                            if (!programDataProcessors.IsMatch(programProcessors))
                            {
                                //Console.WriteLine("Line: " + programDataProcessors + " : PROGRAM BLOCK  -> Valid");
                                errors++;
                                errorList.Add(programDataProcessors);
                                //Console.WriteLine("Errors Found: " + errors + " .");
                            }
                        }
                    }
                }

                // Used for initial block testing block.
                else if (tasksData.IsMatch(trimmedline))
                {
                    if (tasksDataEndpoint.IsMatch(trimmedline))
                    {
                        //Console.WriteLine("Line: " + trimmedline + " : END-TASKS -> Valid");
                    }

                    else
                    {
                        //Console.WriteLine("Line: " + trimmedline + " : TASKS -> Valid");
                    }
                }

                // Task Block
                // If block valid, retrieve individual task details
                // If block invalid add errors to list and increment number of errors
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
                        //Console.WriteLine("Line: " + trimmedline + " : TASK -> Valid");
                        //Console.WriteLine("Line: " + taskID + " : TASK BLOCK  -> Valid");
                        //Console.WriteLine("Line: " + taskRuntime + " : TASK BLOCK  -> Valid");
                        //Console.WriteLine("Line: " + taskReferenceFrequency + " : TASK BLOCK  -> Valid");
                        //Console.WriteLine("Line: " + taskRam + " : TASK BLOCK  -> Valid");
                        //Console.WriteLine("Line: " + taskDownload + " : TASK BLOCK  -> Valid");
                        //Console.WriteLine("Line: " + taskUpload + " : TASK BLOCK  -> Valid");
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

                // Used for initial block testing block.
                else if (taskDataEndpoint.IsMatch(trimmedline))
                {
                    //Console.WriteLine("Line: " + trimmedline + " : END-TASK -> Valid");
                }

                // Used for initial block testing block.
                else if (processorsData.IsMatch(trimmedline))
                {
                    if (processorsDataEndpoint.IsMatch(trimmedline))
                    {
                        //Console.WriteLine("Line: " + trimmedline + " : END-PROCESSORS -> Valid");
                    }

                    else
                    {
                        //Console.WriteLine("Line: " + trimmedline + " : PROCESSORS -> Valid");
                    }
                }

                // Processor Block
                // If block valid, retrieve individual processor details
                // If block invalid add errors to list and increment number of errors
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
                        //Console.WriteLine("Line: " + trimmedline + " : PROCESSOR -> Valid");
                        //Console.WriteLine("Line: " + processorID + " : PROCESSOR BLOCK  -> Valid");
                        //Console.WriteLine("Line: " + processorType + " : PROCESSOR BLOCK  -> Valid");
                        //Console.WriteLine("Line: " + processorFrequency + " : PROCESSOR BLOCK  -> Valid");
                        //Console.WriteLine("Line: " + processorRam + " : PROCESSOR BLOCK  -> Valid");
                        //Console.WriteLine("Line: " + processorDownload + " : PROCESSOR BLOCK  -> Valid");
                        //Console.WriteLine("Line: " + processorUpload + " : PROCESSOR BLOCK  -> Valid");
                        this.processorsList.Add(RetrieveProcessorDetails(processorID, processorType, processorFrequency, processorRam,  processorDownload, processorUpload));
                    }

                    else
                    {
                        if (!processorDataID.IsMatch(processorID))
                        {
                            //Console.WriteLine("Line: " + processorID + " : PROCESSOR BLOCK  -> Invalid");
                            errors++;
                            errorList.Add(processorID);
                            //Console.WriteLine("Errors Found: " + errors + " .");
                        }

                        if (!processorDataType.IsMatch(processorType))
                        {
                            //Console.WriteLine("Line: " + processorType + " : PROCESSOR BLOCK  -> Invalid");
                            errors++;
                            errorList.Add(processorType);
                            //Console.WriteLine("Errors Found: " + errors + " .");
                        }

                        if (!processorDataFrequency.IsMatch(processorFrequency))
                        {
                            //Console.WriteLine("Line: " + processorFrequency + " : PROCESSOR BLOCK  -> Invalid");
                            errors++;
                            errorList.Add(processorFrequency);
                            //Console.WriteLine("Errors Found: " + errors + " .");
                        }

                        if (!processorDataRAM.IsMatch(processorRam))
                        {
                            //Console.WriteLine("Line: " + processorRam + " : PROCESSOR BLOCK  -> Invalid");
                            errors++;
                            errorList.Add(processorRam);
                            //Console.WriteLine("Errors Found: " + errors + " .");
                        }

                        if (!processorDataDOWNLOAD.IsMatch(processorDownload))
                        {
                            //Console.WriteLine("Line: " + processorDownload + " : PROCESSOR BLOCK  -> Invalid");
                            errors++;
                            errorList.Add(processorDownload);
                            //Console.WriteLine("Errors Found: " + errors + " .");
                        }

                        if (!processorDataUPLOAD.IsMatch(processorUpload))
                        {
                            //Console.WriteLine("Line: " + processorUpload + " : PROCESSOR BLOCK  -> Invalid");
                            errors++;
                            errorList.Add(processorUpload);
                            //Console.WriteLine("Errors Found: " + errors + " .");
                        }

                    }

                }

                // Used for initial block testing block.
                else if (processorDataEndpoint.IsMatch(trimmedline))
                {
                    //Console.WriteLine("Line: " + trimmedline + " : END-PROCESSOR -> Valid");
                }

                // Used for initial block testing block.
                else if (processorsTypesData.IsMatch(trimmedline))
                {
                    if (processorsTypesDataEndpoint.IsMatch(trimmedline))
                    {
                        //Console.WriteLine("Line: " + trimmedline + " : END-PROCESSOR-TYPES -> Valid");
                    }

                    else
                    {
                        //Console.WriteLine("Line: " + trimmedline + " : PROCESSORS-TYPES -> Valid");
                    }
                }

                // Processor energy Block
                // If block valid, retrieve individual processor energy details
                // If block invalid add errors to list and increment number of errors
                else if (processorTypeData.IsMatch(trimmedline))
                {
                    if (processorTypeDataEndpoint.IsMatch(trimmedline))
                    {
                        //Console.WriteLine("Line: " + trimmedline + " : END-PROCESSOR-TYPE -> Valid");
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
                            //Console.WriteLine("Line: " + trimmedline + " : PROCESSOR-TYPE -> Valid");
                            //Console.WriteLine("Line: " + processorName + " : PROCESSOR-TYPE BLOCK  -> Valid");
                            //Console.WriteLine("Line: " + processorC2 + " : PROCESSOR-TYPE BLOCK  -> Valid");
                            //Console.WriteLine("Line: " + processorC1 + " : PROCESSOR-TYPE BLOCK  -> Valid");
                            //Console.WriteLine("Line: " + processorC0 + " : PROCESSOR-TYPE BLOCK  -> Valid");
                            this.processorTypesList.Add(RetrieveProcessorTypeDetails(processorName, processorC2, processorC1, processorC0));
                        }

                        else
                        {
                            if (!processorTypeDataName.IsMatch(processorName))
                            {
                                //Console.WriteLine("Line: " + processorName + " : PROCESSOR-TYPE BLOCK  -> Valid");
                                errors++;
                                errorList.Add(processorName);
                                //Console.WriteLine("Errors Found: " + errors + " .");
                            }

                            if (!processorTypeDataC2.IsMatch(processorC2))
                            {
                                //Console.WriteLine("Line: " + processorC2 + " : PROCESSOR-TYPE BLOCK  -> Valid");
                                errors++;
                                errorList.Add(processorC2);
                                //Console.WriteLine("Errors Found: " + errors + " .");
                            }

                            if (!processorTypeDataC1.IsMatch(processorC1))
                            {
                                //Console.WriteLine("Line: " + processorC1 + " : PROCESSOR-TYPE BLOCK  -> Valid");
                                errors++;
                                errorList.Add(processorC1);
                                //Console.WriteLine("Errors Found: " + errors + " .");
                            }

                            if (!processorTypeDataC0.IsMatch(processorC0))
                            {
                                //Console.WriteLine("Line: " + processorC0 + " : PROCESSOR-TYPE BLOCK  -> Valid");
                                errors++;
                                errorList.Add(processorC0);
                                //Console.WriteLine("Errors Found: " + errors + " .");
                            }
                        }
                    }
                }

                // Local Comm block
                // If block valid, retrieve individual local energy details
                // If block invalid add errors to list and increment number of errors
                else if (localCommunicationData.IsMatch(trimmedline))
                {
                    if (localCommunicationDataEndpoint.IsMatch(trimmedline))
                    {
                        //Console.WriteLine("Line: " + trimmedline + " : END-LOCAL-COMMUNICATION -> Valid");
                    }

                    else
                    {
                        index = IncrementIndex(index, 1);
                        string localCommunicationMAP = filelines[index].Trim();

                        if (localCommunicationDataMAP.IsMatch(localCommunicationMAP))
                        {
                            //Console.WriteLine("Line: " + trimmedline + " : LOCAL-COMMUNICATION -> Valid");
                            //Console.WriteLine("Line: " + localCommunicationMAP + " : LOCAL-COMMUNICATION BLOCK  -> Valid");
                            RetrieveLocalCommunicationEnergyDetails();
                        }
                        else
                        {
                            //Console.WriteLine("Line: " + localCommunicationMAP + " : LOCAL-COMMUNICATION BLOCK  -> Invalid");
                            errors++;
                            errorList.Add(localCommunicationMAP);
                            //Console.WriteLine("Errors Found: " + errors + " .");
                        }
                    }
                }

                // Remote Comm block
                // If block valid, retrieve individual remote energy details
                // If block invalid add errors to list and increment number of errors
                else if (remoteCommunicationData.IsMatch(trimmedline))
                {
                    if (remoteCommunicationDataEndpoint.IsMatch(trimmedline))
                    {
                        //Console.WriteLine("Line: " + trimmedline + " : END-REMOTE-COMMUNICATION -> Valid");
                    }

                    else
                    {
                        index = IncrementIndex(index, 1);
                        string remoteCommunicationMAP = filelines[index].Trim();

                        if (remoteCommunicationDataMAP.IsMatch(remoteCommunicationMAP))
                        {
                            //Console.WriteLine("Line: " + trimmedline + " : REMOTE-COMMUNICATION -> Valid");
                            //Console.WriteLine("Line: " + remoteCommunicationMAP + " : REMOTE-COMMUNICATION BLOCK  -> Valid");
                        }
                        else
                        {
                            //Console.WriteLine("Line: " + remoteCommunicationMAP + " : REMOTE-COMMUNICATION BLOCK  -> Invalid");
                            errors++;
                            errorList.Add(remoteCommunicationMAP);
                            //Console.WriteLine("Errors Found: " + errors + " .");
                        }
                    }
                }

                // Invalid syntax line
                // Add error to list and increment number of errors
                else
                {
                    //Console.WriteLine("Line: " + trimmedline + " : Line doesn`t match CFF Syntax -> Invalid");
                    errors++;
                    errorList.Add(trimmedline);
                    //Console.WriteLine("Errors Found: " + errors + " .");
                }

                // increement 1 index after each line check
                index = IncrementIndex(index, 1);
            }

            Console.WriteLine("Ending Validation of CFF file. " + "\n");


            // If errors are there change the isValid to false
            // Also return false
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
                return false;
            }

            // Set isValid to true and return true.
            // Also print out the details of the file.
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
                return true;
            }
        }

        /// <summary>
        /// Method to retrieve the general program details.
        /// </summary>
        /// <param name="programDuration"></param>
        /// <param name="programTasks"></param>
        /// <param name="programProcessors"></param>
        public void RetrieveProgramDetails(string programDuration, string programTasks, string programProcessors)
        {
            string emptyText = "";
            Regex programDataDuration = new Regex("DURATION=");
            Regex programDataTasks = new Regex("TASKS=");
            Regex programDataProcessors = new Regex("PROCESSORS=");

            this.programDuration = float.Parse(programDataDuration.Replace(programDuration, emptyText));
            this.programTasks = Int32.Parse(programDataTasks.Replace(programTasks, emptyText));
            this.programProcessors = Int32.Parse(programDataProcessors.Replace(programProcessors, emptyText));

            //Console.WriteLine("Program Duration:  " + this.programDuration + "\n");
            //Console.WriteLine("Program Tasks: " + this.programTasks + "\n");
            //Console.WriteLine("Program Processors: " + this.programProcessors + "\n");
        }

        /// <summary>
        /// Method to retrieve specific task details.
        /// </summary>
        /// <param name="taskID"></param>
        /// <param name="taskRuntime"></param>
        /// <param name="taskReferenceFrequency"></param>
        /// <param name="taskRam"></param>
        /// <param name="taskDownload"></param>
        /// <param name="taskUpload"></param>
        /// <returns>Task Object with assigned details.</returns>
        public Task RetrieveTaskDetails(string taskID, string taskRuntime, string taskReferenceFrequency, string taskRam, string taskDownload, string taskUpload)
        {
            string emptyText = "";
            
            Regex taskDataID = new Regex("ID=");
            Regex taskDataRuntime = new Regex("RUNTIME=");
            Regex taskDataReffrequency = new Regex("REFERENCE-FREQUENCY=");
            Regex taskDataRAM = new Regex("RAM=");
            Regex taskDataDOWNLOAD = new Regex("DOWNLOAD=");
            Regex taskDataUPLOAD = new Regex("UPLOAD=");

            Task task = new Task();
            int taskId = Int32.Parse(taskDataID.Replace(taskID, emptyText));

            if (taskId < programTasks)
            {
                task.setTaskID(Int32.Parse(taskDataID.Replace(taskID, emptyText)));
                task.setTaskRuntime(float.Parse(taskDataRuntime.Replace(taskRuntime, emptyText)));
                task.setTaskReferenceFrequency(float.Parse(taskDataReffrequency.Replace(taskReferenceFrequency, emptyText)));
                task.setTaskRam(Int32.Parse(taskDataRAM.Replace(taskRam, emptyText)));
                task.setTaskDownload(Int32.Parse(taskDataDOWNLOAD.Replace(taskDownload, emptyText)));
                task.setTaskUpload(Int32.Parse(taskDataUPLOAD.Replace(taskUpload, emptyText)));

                //Console.WriteLine("Task ID:  " + task.getTaskID() + "\n");
                //Console.WriteLine("Task Runtime:  " + task.getTaskRuntime() + "\n");
                //Console.WriteLine("Task Ref Freq:  " + task.getTaskReferenceFrequency() + "\n");
                //Console.WriteLine("Task Ram:  " + task.getTaskRam() + "\n");
                //Console.WriteLine("Task Download:  " + task.getTaskDownload() + "\n");
                //Console.WriteLine("Task Upload:  " + task.getTaskUpload() + "\n");
            }

            else
            {
                //Console.WriteLine("Task Numbers exceed the maximum tasks in the program.");
                errors++;
                errorList.Add(taskDataID);
                //Console.WriteLine("Errors Found: " + errors + " .");
            }

            return task;
        }

        /// <summary>
        /// Method to retrieve the specific processor details.
        /// </summary>
        /// <param name="processorID"></param>
        /// <param name="processorType"></param>
        /// <param name="processorFrequency"></param>
        /// <param name="processorRam"></param>
        /// <param name="processorDownload"></param>
        /// <param name="processorUpload"></param>
        /// <returns>Processor Object with assigned details.</returns>
        public Processor RetrieveProcessorDetails(string processorID, string processorType, string processorFrequency, string processorRam, string processorDownload, string processorUpload)
        {
            string emptyText = "";

            Regex processorDataID = new Regex("ID=");
            Regex processorDataType = new Regex("TYPE=");
            Regex processorDataFrequency = new Regex("FREQUENCY=");
            Regex processorDataRAM = new Regex("RAM=");
            Regex processorDataDOWNLOAD = new Regex("DOWNLOAD=");
            Regex processorDataUPLOAD = new Regex("UPLOAD=");

            Processor processor = new Processor();
            int processorId = Int32.Parse(processorDataID.Replace(processorID, emptyText));

            if (processorId < programProcessors)
            {
                processor.setProcessorID(Int32.Parse(processorDataID.Replace(processorID, emptyText)));
                processor.setProcessorType(processorDataType.Replace(processorType, emptyText));
                processor.setProcessorFrequency(float.Parse(processorDataFrequency.Replace(processorFrequency, emptyText)));
                processor.setProcessorRam(Int32.Parse(processorDataRAM.Replace(processorRam, emptyText)));
                processor.setProcessorDownload(Int32.Parse(processorDataDOWNLOAD.Replace(processorDownload, emptyText)));
                processor.setProcessorUpload(Int32.Parse(processorDataUPLOAD.Replace(processorUpload, emptyText)));

                //Console.WriteLine("Processor ID:  " + processor.getProcessorID() + "\n");
                //Console.WriteLine("Processor Type:  " + processor.getProcessorType() + "\n");
                //Console.WriteLine("Processor Freq:  " + processor.getProcessorFrequency() + "\n");
                //Console.WriteLine("Processor Ram:  " + processor.getProcessorRam() + "\n");
                //Console.WriteLine("Processor Download:  " + processor.getProcessorDownload() + "\n");
                //Console.WriteLine("Processor Upload:  " + processor.getProcessorUpload() + "\n");
            }

            else
            {
                //Console.WriteLine("Processor Numbers exceed the maximum processors in the program.");
                errors++;
                errorList.Add(processorID);
                //Console.WriteLine("Errors Found: " + errors + " .");
            }

            return processor;
        }

        // Not Used
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

            //Console.WriteLine("Processor Name:  " + processorType.getProcessorName() + "\n");
            //Console.WriteLine("Processor C2:  " + processorType.getProcessorC2() + "\n");
            //Console.WriteLine("Processor C1:  " + processorType.getProcessorC1() + "\n");
            //Console.WriteLine("Processor C0:  " + processorType.getProcessorC0() + "\n");

            return processorType;
        }

        // Not Used
        public void RetrieveLocalCommunicationEnergyDetails()
        {
            //[-+]?[0-9]*\.?[0-9]+,[-+]?[0-9]*\.?[0-9]+,[-+]?[0-9]*\.?[0-9]+,[-+]?[0-9]*\.?[0-9]+,[-+]?[0-9]*\.?[0-9]+
        }

        /// <summary>
        /// Method used to increment the index in the loop to read the filelines of the cff file.
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
        public bool ValidateCFFFileErrors(string expectedError)
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
                textBox.AppendText("CFF File" + Environment.NewLine);
                Console.WriteLine("The CFF file is invalid.");
                Console.WriteLine("Number of syntax errors found: " + errors + " .");
                Console.WriteLine("Errors are as follows: \n ");
                textBox.AppendText("The CFF file is invalid." + Environment.NewLine);
                textBox.AppendText("Number of errors found: " + errors + " ." + Environment.NewLine);
                textBox.AppendText("Errors are as follows: " + Environment.NewLine);

                int errCount = 1;

                foreach (string error in errorList)
                {
                    Console.WriteLine("Error- " + errCount + " : " + error);
                    textBox.AppendText("Error- " + errCount + " : " + error + Environment.NewLine);
                    errCount++;
                }

                textBox.AppendText("END of CFF File" + Environment.NewLine);
                textBox.AppendText(Environment.NewLine);
            }
            else
            {
                textHeading.BackColor = Color.Green;
                textBox.AppendText("CFF File" + Environment.NewLine);
                textBox.AppendText("The CFF file is Valid, No errors to display." + Environment.NewLine);
                textBox.AppendText("END of CFF File" + Environment.NewLine);
            }
        }
    }
}
