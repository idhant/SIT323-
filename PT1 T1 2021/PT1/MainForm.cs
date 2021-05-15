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
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        // Defining the object which are used to manipulate the taff and cff file data.
        TaskAllocations taskAllocations = new TaskAllocations();
        Configuration configuration = new Configuration();

        // Event Functions which validates the taff and cff files and displays the contents on the form boxes
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Initialising the dialog method along with the propertites
            OpenFileDialog theDialog = new OpenFileDialog();
            theDialog.Title = "Open TAFF File";
            theDialog.Filter = "TAFF files|*.taff";
            theDialog.InitialDirectory = @"C:\";

            // Constant values of objects used to prevent any magic numbers
            string emptyText = "";
            string taffFileValid = "Taff File (Valid)";
            string cffFileValid = "Cff File (Valid)";
            string taffFileInvalid = "Taff File (Invalid)";
            string cffFileInvalid = "Cff File (Invalid)";
            Color colorValid = Color.Green;
            Color colorInvalid = Color.Red;
            Color colorNeutral = Color.White;



            // If the dialog result is Ok, i.e the user opens a file correctly.
            // Try and catch method to prevent the user from opening different types of files
            if (theDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // reseting the text and colors in the output boxes
                    taffFileBox.Text = emptyText;
                    cffFileBox.Text = emptyText;
                    allocationsBox.Text = emptyText;
                    errorBox.Text = emptyText;
                    taffFileHeading.BackColor = colorNeutral;
                    cffFileHeading.BackColor = colorNeutral;
                    allocationsHeading.BackColor = colorNeutral;
                    errorsHeading.BackColor = colorNeutral;

                    // Reseting the validations of allocations. They turn to true if the files are valid
                    allocationsToolStripMenuItem.Enabled = false;
                    validateAllocations.Enabled = false;

                    // Initialising the local objects 
                    TaskAllocations taskAllocations = new TaskAllocations();
                    Configuration configuration = new Configuration();

                    // getting the opened file name 
                    string filename = theDialog.FileName;

                    taskAllocations.StoreTAFFLines(filename);

                    taskAllocations.WriteTAFFFileData(filename, taffFileBox);

                    taskAllocations.ValidateTAFFFile(taskAllocations.getTAFFFileLines());

                    taskAllocations.GetDirectoryPath(filename);

                    taskAllocations.ReadCFFFileName(filename);

                    taskAllocations.ReadCFFFilePath();

                    configuration.StoreCFFLines(taskAllocations.getCffFilePath());

                    configuration.WriteCFFFileData(taskAllocations.getCffFilePath(), cffFileBox);

                    configuration.ValidateCFFFile(configuration.GetCFFFileLines());

                    this.taskAllocations = taskAllocations;
                    this.configuration = configuration;

                    // If both files are valid, display the same to the user and update the error logs
                    // also enables the ability to validate the allocations
                    if (taskAllocations.getIsValid() && configuration.getIsValid())
                    {
                        Console.WriteLine("Both taff and cff files are valid. You can now check the validations of the allocations.");
                        
                        taffFileHeading.Text = taffFileValid;
                        cffFileHeading.Text = cffFileValid;
                        errorBox.Text = emptyText;
                        taffFileHeading.BackColor = colorValid;
                        cffFileHeading.BackColor = colorValid;
                        errorsHeading.BackColor = colorValid;

                        allocationsToolStripMenuItem.Enabled = true;
                        validateAllocations.Enabled = true;

                        errorBox.AppendText("TAFF File" + Environment.NewLine);
                        errorBox.AppendText("The TAFF file is Valid, No errors to display." + Environment.NewLine);
                        errorBox.AppendText("END of TAFF File" + Environment.NewLine);
                        errorBox.AppendText(Environment.NewLine);
                        errorBox.AppendText("CFF File" + Environment.NewLine);
                        errorBox.AppendText("The CFF file is Valid, No errors to display." + Environment.NewLine);
                        errorBox.AppendText("END of CFF File" + Environment.NewLine);
                        errorBox.AppendText(Environment.NewLine);
                    }

                    // If the files are invalid, display the same to the user and update the error logs for the files
                    if (!taskAllocations.getIsValid() || !configuration.getIsValid())
                    {
                        errorBox.Text = emptyText;
                        if (!taskAllocations.getIsValid())
                        {
                            taffFileHeading.Text = taffFileInvalid;
                            taffFileHeading.BackColor = colorInvalid;
                            taskAllocations.PrintErrorDetails(errorBox, errorsHeading);
                        }
                        if (!configuration.getIsValid())
                        {
                            cffFileHeading.Text = cffFileInvalid;
                            cffFileHeading.BackColor = colorInvalid;
                            configuration.PrintErrorDetails(errorBox, errorsHeading);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }

            // If the dialog result is Cancel, do nothing.
            else
            {
                Console.WriteLine("User Cancelled the dialog operation.");
            }

        }
        private void openTaffFile_Click(object sender, EventArgs e)
        {
            // Initialising the dialog method along with the propertites
            OpenFileDialog theDialog = new OpenFileDialog();
            theDialog.Title = "Open TAFF File";
            theDialog.Filter = "TAFF files|*.taff";
            theDialog.InitialDirectory = @"C:\";

            // If the dialog result is Ok, i.e the user opens a file correctly.
            // Try and catch method to prevent the user from opening different types of files
            if (theDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string emptyText = "";
                    string taffFileValid = "Taff File (Valid)";
                    string cffFileValid = "Cff File (Valid)";
                    string taffFileInvalid = "Taff File (Invalid)";
                    string cffFileInvalid = "Cff File (Invalid)";
                    Color colorValid = Color.Green;
                    Color colorInvalid = Color.Red;

                    // reseting the text in the output boxes
                    allocationsBox.Text = emptyText;
                    errorBox.Text = emptyText;
                    taffFileBox.Text = emptyText;
                    cffFileBox.Text = emptyText;

                    // Initialising the local objects 
                    TaskAllocations taskAllocations = new TaskAllocations();
                    Configuration configuration = new Configuration();

                    // getting the opened file name 
                    string filename = theDialog.FileName;

                    taskAllocations.StoreTAFFLines(filename);

                    taskAllocations.WriteTAFFFileData(filename, taffFileBox);

                    taskAllocations.ValidateTAFFFile(taskAllocations.getTAFFFileLines());

                    taskAllocations.GetDirectoryPath(filename);

                    taskAllocations.ReadCFFFileName(filename);

                    taskAllocations.ReadCFFFilePath();

                    configuration.StoreCFFLines(taskAllocations.getCffFilePath());

                    configuration.WriteCFFFileData(taskAllocations.getCffFilePath(), cffFileBox);

                    configuration.ValidateCFFFile(configuration.GetCFFFileLines());

                    this.taskAllocations = taskAllocations;
                    this.configuration = configuration;

                    // If both files are valid, display the same to the user and update the error logs
                    // also enables the ability to validate the allocations
                    if (taskAllocations.getIsValid() && configuration.getIsValid())
                    {
                        Console.WriteLine("Both taff and cff files are valid. You can now check the validations of the allocations.");

                        taffFileHeading.Text = taffFileValid;
                        cffFileHeading.Text = cffFileValid;
                        errorBox.Text = emptyText;
                        taffFileHeading.BackColor = colorValid;
                        cffFileHeading.BackColor = colorValid;
                        errorsHeading.BackColor = colorValid;

                        allocationsToolStripMenuItem.Enabled = true;
                        validateAllocations.Enabled = true;

                        errorBox.AppendText("TAFF File" + Environment.NewLine);
                        errorBox.AppendText("The TAFF file is Valid, No errors to display." + Environment.NewLine);
                        errorBox.AppendText("END of TAFF File" + Environment.NewLine);
                        errorBox.AppendText(Environment.NewLine);
                        errorBox.AppendText("CFF File" + Environment.NewLine);
                        errorBox.AppendText("The CFF file is Valid, No errors to display." + Environment.NewLine);
                        errorBox.AppendText("END of CFF File" + Environment.NewLine);
                        errorBox.AppendText(Environment.NewLine);
                    }

                    // If the files are invalid, display the same to the user and update the error logs for the files
                    if (!taskAllocations.getIsValid() || !configuration.getIsValid())
                    {
                        errorBox.Text = emptyText;
                        if (!taskAllocations.getIsValid())
                        {
                            taffFileHeading.Text = taffFileInvalid;
                            taffFileHeading.BackColor = colorInvalid;
                            taskAllocations.PrintErrorDetails(errorBox, errorsHeading);
                        }
                        if (!configuration.getIsValid())
                        {
                            cffFileHeading.Text = cffFileInvalid;
                            cffFileHeading.BackColor = colorInvalid;
                            configuration.PrintErrorDetails(errorBox, errorsHeading);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }

            // If the dialog result is Cancel, do nothing.
            else
            {
                Console.WriteLine("User Cancelled the dialog operation.");
            }

        }


        // Event Functions which validates the allocations retrieved and stores them. For invalid allocations, display them in the error logs
        private void allocationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string allocationsValid = "Allocations (Valid)";
            string allocationsInvalid = "Allocations (Invalid)";
            Color colorValid = Color.Green;
            Color colorInvalid = Color.Red;

            Console.WriteLine("Validating the allocations Now..");

            // Getting the allocations list from the task allocation object
            List<Allocation> allocationList = taskAllocations.getAllocationList();

            // Master Function to calculate and show the relevent values
            // Validates the data and shows it in the allocations box
            // and shows any errors in the error box
            foreach (Allocation allocation in allocationList)
            {
                // Vars to store the allocation id and the 2D map array
                int allocationID = allocation.getAllocationID();
                int[,] allocationMap = allocation.getAllocationMap();
                
                // setting the inital value to true. If any error is detected, it is changed to false
                allocation.setIsValid(true);

                allocationsBox.AppendText("Allocation ID: " + allocationID + Environment.NewLine);
                allocationsBox.AppendText("Allocation Map:" + Environment.NewLine);

                // Vars to store the runtime components
                float[] processorRuntime = new float[taskAllocations.getTotalProcessors()];
                float maximumAllocationRuntime = 0;
                float allowedProgramDuration = configuration.getProgramDuration();

                // Vars to store the ram components
                int[] maxRamRequired = new int[taskAllocations.getTotalProcessors()];
                int[] processorRamAvailable = new int[taskAllocations.getTotalProcessors()];

                // Vars to store download components
                int[] maxDownloadRequired = new int[taskAllocations.getTotalProcessors()];
                int[] processorDownloadAvailable = new int[taskAllocations.getTotalProcessors()];

                // Vars to store upload components
                int[] maxUploadRequired = new int[taskAllocations.getTotalProcessors()];
                int[] processorUploadAvailable = new int[taskAllocations.getTotalProcessors()];

                // Searchin value=1 within the map to find out the assigned task to each processor
                for (int processor = 0; processor < taskAllocations.getTotalProcessors(); processor++)
                {
                    allocationsBox.AppendText(Environment.NewLine);

                    for (int task = 0; task < taskAllocations.getTotalTasks(); task++)
                    {
                        int value = allocationMap[processor, task];
                        string textSeparator = ", ";

                        // Runtime related Vars
                        float taskRuntime = 0;
                        float taskRefFrequency = 0;
                        float processorFrequency = 0;
                        float actualTaskRuntime = 0;

                        // Ram related vars
                        int taskRam = 0;
                        int processorRam = 0;

                        // Download related vars
                        int taskDownload = 0;
                        int processorDownload = 0;

                        // Upload related vars
                        int taskUpload = 0;
                        int processorUpload = 0;

                        allocationsBox.AppendText(value + textSeparator);

                        // finding and storing relevent information of the tasks and processors
                        if (value == 1)
                        {
                            Console.WriteLine("Processor- " + processor + " is assigned the task- " + task);

                            List<Task> tasksList = configuration.getTasksList();

                            foreach (Task taskNumber in tasksList)
                            {
                                if (taskNumber.getTaskID() == task)
                                {
                                    taskRuntime = taskNumber.getTaskRuntime();
                                    taskRefFrequency = taskNumber.getTaskReferenceFrequency();
                                    taskRam = taskNumber.getTaskRam();
                                    taskDownload = taskNumber.getTaskDownload();
                                    taskUpload = taskNumber.getTaskUpload();
                                }
                            }

                            List<Processor> processorsList = configuration.getProcessorsList();

                            foreach (Processor processorNumber in processorsList)
                            {
                                if (processorNumber.getProcessorID() == processor)
                                {
                                    processorFrequency = processorNumber.getProcessorFrequency();
                                    processorRam = processorNumber.getProcessorRam();
                                    processorDownload = processorNumber.getProcessorDownload();
                                    processorUpload = processorNumber.getProcessorUpload();
                                }
                            }

                        }

                        // Calculating and storing actualtaskruntime for each task which is then added in the total processor runtime for each processor
                        if (processorFrequency != 0)
                        {
                            actualTaskRuntime = taskRuntime * (taskRefFrequency / processorFrequency);
                            processorRuntime[processor] = actualTaskRuntime + processorRuntime[processor];
                            Console.WriteLine("Actual Task runtime for Task: " + task + " on processor: " + processor + " is: " + actualTaskRuntime);
                        }

                        // Calculating the maximum ram required by any task, also calculating the available ram
                        if (taskRam != 0)
                        {
                            Console.WriteLine("Ram required for Task- " + task + " is: " + taskRam + "\n");
                            if (taskRam > maxRamRequired[processor])
                            {
                                maxRamRequired[processor] = taskRam;
                            }
                            processorRamAvailable[processor] = processorRam;
                        }

                        // Calculating the maximum download speed required by any task, also calculating tha available download 
                        if (taskDownload != 0)
                        {
                            Console.WriteLine("Download Speed required for Task- " + task + " is: " + taskDownload + "\n");
                            if (taskDownload > maxDownloadRequired[processor])
                            {
                                maxDownloadRequired[processor] = taskDownload;
                            }
                            processorDownloadAvailable[processor] = processorDownload;
                        }

                        // Calculating the maximum upload speed required by any task, also clculating the available upload
                        if (taskUpload != 0)
                        {
                            Console.WriteLine("Upload Speed required for Task- " + task + " is: " + taskUpload + "\n");
                            if (taskUpload > maxUploadRequired[processor])
                            {
                                maxUploadRequired[processor] = taskUpload;
                            }
                            processorUploadAvailable[processor] = processorUpload;
                        }

                    }
                    Console.WriteLine("Total runtime for processor- " + processor + " = " + processorRuntime[processor]);
                }

                allocationsBox.AppendText(Environment.NewLine);

                // calculating the max of all the runtimes for all the processors
                foreach (float runtime in processorRuntime)
                {
                    if (runtime > maximumAllocationRuntime)
                    {
                        maximumAllocationRuntime = runtime;
                    }
                }

                Console.WriteLine("Maximum Runtime of any processor is: " + maximumAllocationRuntime);
                Console.WriteLine("Allowed Maximum Runtime: " + allowedProgramDuration);

                // Checks to check if the runtime exceeds the allowed runtime or not 
                if (allowedProgramDuration > maximumAllocationRuntime)
                {
                    allocationsBox.AppendText(Environment.NewLine);
                    allocationsBox.AppendText("Allocation Runtime: " + maximumAllocationRuntime + Environment.NewLine);
                    allocationsBox.AppendText("Maximum Acceptable Runtime: " + allowedProgramDuration + Environment.NewLine);
                    Console.WriteLine("Runtime of the allocation- " + allocationID + "is valid and is below the allowed duration of the program. \n");
                }

                // If the runtime exceeds the allowed time, error is added and allocation is declared invalid
                if (allowedProgramDuration < maximumAllocationRuntime)
                {
                    // Add this allocation id in the error list
                    allocation.setIsValid(false);
                    allocation.setErrors(allocation.getErrors() + 1);
                    allocation.AddErrorToList("Runtime of allocation- " + allocationID + "is invalid and exceeds the allowed duration of the program.");
                    allocationsBox.AppendText(Environment.NewLine);
                    allocationsBox.AppendText("Allocation Runtime: " + maximumAllocationRuntime + Environment.NewLine);
                    allocationsBox.AppendText("Maximum Acceptable Runtime: " + allowedProgramDuration + Environment.NewLine);
                    Console.WriteLine("Runtime of allocation- " + allocationID + "is invalid and exceeds the allowed duration of the program. \n");
                }

                // To check if the ram exceeds the available ram.
                for (int processor = 0; processor < maxRamRequired.Length; processor++)
                {
                    if (processorRamAvailable[processor] > 0)
                    {
                        Console.WriteLine("Processor- " + processor);
                        Console.WriteLine("Ram Available: " + processorRamAvailable[processor]);
                        Console.WriteLine("Maximum Ram Required by any task: " + maxRamRequired[processor]);
                        allocationsBox.AppendText(Environment.NewLine);

                        if (maxRamRequired[processor] > processorRamAvailable[processor])
                        {
                            Console.WriteLine("Not enough ram available on the processor- " + processor + " to complete the task, Allocation- " + allocationID + " is invalid. \n");
                            allocation.setIsValid(false);
                            allocation.setErrors(allocation.getErrors() + 1);
                            allocation.AddErrorToList("Not enough ram available on the processor- " + processor + " to complete the task, Allocation- " + allocationID + " is invalid.");
                            allocationsBox.AppendText("Processor- " + processor);
                            allocationsBox.AppendText(Environment.NewLine);
                            allocationsBox.AppendText("Maximum Required Ram: " + maxRamRequired[processor] + Environment.NewLine);
                            allocationsBox.AppendText("Available Ram: " + processorRamAvailable[processor] + Environment.NewLine);
                        }
                        else
                        {
                            Console.WriteLine("Ram is available on the processor- " + processor + " to complete all the assigned tasks, Allocation= " + allocationID + " is valid." + "\n");
                            allocationsBox.AppendText("Processor- " + processor);
                            allocationsBox.AppendText(Environment.NewLine);
                            allocationsBox.AppendText("Maximum Required Ram: " + maxRamRequired[processor] + Environment.NewLine);
                            allocationsBox.AppendText("Available Ram: " + processorRamAvailable[processor] + Environment.NewLine);
                        }
                    }

                }

                // To check if the download speed exceeds the available
                for (int processor = 0; processor < maxDownloadRequired.Length; processor++)
                {
                    if (processorDownloadAvailable[processor] > 0)
                    {
                        Console.WriteLine("Processor- " + processor);
                        Console.WriteLine("Download speed Available: " + processorDownloadAvailable[processor]);
                        Console.WriteLine("Maximum download speed Required by any task: " + maxDownloadRequired[processor]);
                        allocationsBox.AppendText(Environment.NewLine);

                        if (maxDownloadRequired[processor] > processorDownloadAvailable[processor])
                        {
                            Console.WriteLine("Not enough download speed available on the processor- " + processor + " to complete the task, Allocation- " + allocationID + " is invalid. \n");
                            allocation.setIsValid(false);
                            allocation.setErrors(allocation.getErrors() + 1);
                            allocation.AddErrorToList("Not enough download speed available on the processor- " + processor + " to complete the task, Allocation- " + allocationID + " is invalid.");
                            allocationsBox.AppendText("Processor- " + processor);
                            allocationsBox.AppendText(Environment.NewLine);
                            allocationsBox.AppendText("Maximum Required Download Speed: " + maxDownloadRequired[processor] + Environment.NewLine);
                            allocationsBox.AppendText("Available Download Speed: " + processorDownloadAvailable[processor] + Environment.NewLine);
                        }
                        else
                        {
                            Console.WriteLine("Download speed is available on the processor- " + processor + " to complete all the assigned tasks, Allocation= " + allocationID + " is valid." + "\n");
                            allocationsBox.AppendText("Processor- " + processor);
                            allocationsBox.AppendText(Environment.NewLine);
                            allocationsBox.AppendText("Maximum Required Download Speed: " + maxDownloadRequired[processor] + Environment.NewLine);
                            allocationsBox.AppendText("Available Download Speed: " + processorDownloadAvailable[processor] + Environment.NewLine);
                        }
                    }

                }

                // To check if the upload speed exceeds the avialable
                for (int processor = 0; processor < maxUploadRequired.Length; processor++)
                {
                    if (processorUploadAvailable[processor] > 0)
                    {
                        Console.WriteLine("Processor- " + processor);
                        Console.WriteLine("Upload speed Available: " + processorUploadAvailable[processor]);
                        Console.WriteLine("Maximum upload speed Required by any task: " + maxUploadRequired[processor]);
                        allocationsBox.AppendText(Environment.NewLine);

                        if (maxUploadRequired[processor] > processorUploadAvailable[processor])
                        {
                            Console.WriteLine("Not enough upload speed available on the processor- " + processor + " to complete the task, Allocation- " + allocationID + " is invalid. \n");
                            allocation.setIsValid(false);
                            allocation.setErrors(allocation.getErrors() + 1);
                            allocation.AddErrorToList("Not enough upload speed available on the processor- " + processor + " to complete the task, Allocation- " + allocationID + " is invalid.");
                            allocationsBox.AppendText("Processor- " + processor);
                            allocationsBox.AppendText(Environment.NewLine);
                            allocationsBox.AppendText("Maximum Required Upload Speed: " + maxUploadRequired[processor] + Environment.NewLine);
                            allocationsBox.AppendText("Available Upload Speed: " + processorUploadAvailable[processor] + Environment.NewLine);

                        }
                        else
                        {
                            Console.WriteLine("Upload speed is available on the processor- " + processor + " to complete all the assigned tasks, Allocation= " + allocationID + " is valid." + "\n");
                            allocationsBox.AppendText("Processor- " + processor);
                            allocationsBox.AppendText(Environment.NewLine);
                            allocationsBox.AppendText("Maximum Required Upload Speed: " + maxUploadRequired[processor] + Environment.NewLine);
                            allocationsBox.AppendText("Available Upload Speed: " + processorUploadAvailable[processor] + Environment.NewLine);
                        }
                    }

                }

                allocationsBox.AppendText(Environment.NewLine);
            }

            Console.WriteLine("Finished validating the allocations.");

            // using the static value true. If the loop detects a error this value is changed to false. This value is used to change the backcolor of the heading
            bool valid = true;

            errorBox.AppendText(Environment.NewLine);
            errorBox.AppendText("START of Allocations" + Environment.NewLine);

            foreach (Allocation allocation in allocationList)
            {
                if (!allocation.isIsValid())
                {
                    Console.WriteLine("Allocation- " + allocation.getAllocationID() + " is invalid.");
                    Console.WriteLine("Number of allocation errors found: " + allocation.getErrors() + " .");
                    Console.WriteLine("Errors are as follows: \n ");
                    errorBox.AppendText("Allocation- " + allocation.getAllocationID() + " is invalid." + Environment.NewLine);
                    errorBox.AppendText("Number of allocation errors found: " + allocation.getErrors() + " ." + Environment.NewLine);
                    errorBox.AppendText("Errors are as follows: " + Environment.NewLine);

                    int errCount = 1;

                    foreach (string error in allocation.getErrorList())
                    {
                        Console.WriteLine("Error- " + errCount + " : " + error);
                        errorBox.AppendText("Error- " + errCount + " : " + error + Environment.NewLine);
                        errCount++;
                    }

                    errorBox.AppendText("END of Allocation- " + allocation.getAllocationID() + " ."+ Environment.NewLine);
                    errorBox.AppendText(Environment.NewLine);
                    valid = false;
                }
                else
                {
                    Console.WriteLine("Allocation- " + allocation.getAllocationID() + " is valid.");
                    errorBox.AppendText("Allocation- " + allocation.getAllocationID() + " is valid." + Environment.NewLine);
                    errorBox.AppendText("END of Allocation- " + allocation.getAllocationID() + " ." + Environment.NewLine);
                    errorBox.AppendText(Environment.NewLine);
                }
            }

            errorBox.AppendText("END of Allocations" + Environment.NewLine);

            if (!valid)
            {
                Console.WriteLine("Some allocations are invalid.");
                allocationsHeading.Text = allocationsInvalid;
                allocationsHeading.BackColor = colorInvalid;
                Console.WriteLine("Some allocations are Invalid. \n");
            }

            else
            {
                Console.WriteLine("All allocations are valid.");
                allocationsHeading.Text = allocationsValid;
                allocationsHeading.BackColor = colorValid;
                Console.WriteLine("Allocations are valid. \n");
            }
        }
        private void validateAllocations_Click(object sender, EventArgs e)   
        {
            string allocationsValid = "Allocations (Valid)";
            string allocationsInvalid = "Allocations (Invalid)";
            Color colorValid = Color.Green;
            Color colorInvalid = Color.Red;

            Console.WriteLine("Validating the allocations Now..");

            // Getting the allocations list from the task allocation object
            List<Allocation> allocationList = taskAllocations.getAllocationList();

            // Master Function to calculate and show the relevent values
            // Validates the data and shows it in the allocations box
            // and shows any errors in the error box
            foreach (Allocation allocation in allocationList)
            {
                // Vars to store the allocation id and the 2D map array
                int allocationID = allocation.getAllocationID();
                int[,] allocationMap = allocation.getAllocationMap();
                
                // setting the inital value to true. If any error is detected, it is changed to false
                allocation.setIsValid(true);

                allocationsBox.AppendText("Allocation ID: " + allocationID + Environment.NewLine);
                allocationsBox.AppendText("Allocation Map:" + Environment.NewLine);

                // Vars to store the runtime components
                float[] processorRuntime = new float[taskAllocations.getTotalProcessors()];
                float maximumAllocationRuntime = 0;
                float allowedProgramDuration = configuration.getProgramDuration();

                // Vars to store the ram components
                int[] maxRamRequired = new int[taskAllocations.getTotalProcessors()];
                int[] processorRamAvailable = new int[taskAllocations.getTotalProcessors()];

                // Vars to store download components
                int[] maxDownloadRequired = new int[taskAllocations.getTotalProcessors()];
                int[] processorDownloadAvailable = new int[taskAllocations.getTotalProcessors()];

                // Vars to store upload components
                int[] maxUploadRequired = new int[taskAllocations.getTotalProcessors()];
                int[] processorUploadAvailable = new int[taskAllocations.getTotalProcessors()];

                // Searchin value=1 within the map to find out the assigned task to each processor
                for (int processor = 0; processor < taskAllocations.getTotalProcessors(); processor++)
                {
                    allocationsBox.AppendText(Environment.NewLine);

                    for (int task = 0; task < taskAllocations.getTotalTasks(); task++)
                    {
                        int value = allocationMap[processor, task];
                        string textSeparator = ", ";

                        // Runtime related Vars
                        float taskRuntime = 0;
                        float taskRefFrequency = 0;
                        float processorFrequency = 0;
                        float actualTaskRuntime = 0;

                        // Ram related vars
                        int taskRam = 0;
                        int processorRam = 0;

                        // Download related vars
                        int taskDownload = 0;
                        int processorDownload = 0;

                        // Upload related vars
                        int taskUpload = 0;
                        int processorUpload = 0;

                        allocationsBox.AppendText(value + textSeparator);

                        // finding and storing relevent information of the tasks and processors
                        if (value == 1)
                        {
                            Console.WriteLine("Processor- " + processor + " is assigned the task- " + task);

                            List<Task> tasksList = configuration.getTasksList();

                            foreach (Task taskNumber in tasksList)
                            {
                                if (taskNumber.getTaskID() == task)
                                {
                                    taskRuntime = taskNumber.getTaskRuntime();
                                    taskRefFrequency = taskNumber.getTaskReferenceFrequency();
                                    taskRam = taskNumber.getTaskRam();
                                    taskDownload = taskNumber.getTaskDownload();
                                    taskUpload = taskNumber.getTaskUpload();
                                }
                            }

                            List<Processor> processorsList = configuration.getProcessorsList();

                            foreach (Processor processorNumber in processorsList)
                            {
                                if (processorNumber.getProcessorID() == processor)
                                {
                                    processorFrequency = processorNumber.getProcessorFrequency();
                                    processorRam = processorNumber.getProcessorRam();
                                    processorDownload = processorNumber.getProcessorDownload();
                                    processorUpload = processorNumber.getProcessorUpload();
                                }
                            }

                        }

                        // Calculating and storing actualtaskruntime for each task which is then added in the total processor runtime for each processor
                        if (processorFrequency != 0)
                        {
                            actualTaskRuntime = taskRuntime * (taskRefFrequency / processorFrequency);
                            processorRuntime[processor] = actualTaskRuntime + processorRuntime[processor];
                            Console.WriteLine("Actual Task runtime for Task: " + task + " on processor: " + processor + " is: " + actualTaskRuntime);
                        }

                        // Calculating the maximum ram required by any task, also calculating the available ram
                        if (taskRam != 0)
                        {
                            Console.WriteLine("Ram required for Task- " + task + " is: " + taskRam + "\n");
                            if (taskRam > maxRamRequired[processor])
                            {
                                maxRamRequired[processor] = taskRam;
                            }
                            processorRamAvailable[processor] = processorRam;
                        }

                        // Calculating the maximum download speed required by any task, also calculating tha available download 
                        if (taskDownload != 0)
                        {
                            Console.WriteLine("Download Speed required for Task- " + task + " is: " + taskDownload + "\n");
                            if (taskDownload > maxDownloadRequired[processor])
                            {
                                maxDownloadRequired[processor] = taskDownload;
                            }
                            processorDownloadAvailable[processor] = processorDownload;
                        }

                        // Calculating the maximum upload speed required by any task, also clculating the available upload
                        if (taskUpload != 0)
                        {
                            Console.WriteLine("Upload Speed required for Task- " + task + " is: " + taskUpload + "\n");
                            if (taskUpload > maxUploadRequired[processor])
                            {
                                maxUploadRequired[processor] = taskUpload;
                            }
                            processorUploadAvailable[processor] = processorUpload;
                        }

                    }
                    Console.WriteLine("Total runtime for processor- " + processor + " = " + processorRuntime[processor]);
                }

                allocationsBox.AppendText(Environment.NewLine);

                // calculating the max of all the runtimes for all the processors
                foreach (float runtime in processorRuntime)
                {
                    if (runtime > maximumAllocationRuntime)
                    {
                        maximumAllocationRuntime = runtime;
                    }
                }

                Console.WriteLine("Maximum Runtime of any processor is: " + maximumAllocationRuntime);
                Console.WriteLine("Allowed Maximum Runtime: " + allowedProgramDuration);

                // Checks to check if the runtime exceeds the allowed runtime or not 
                if (allowedProgramDuration > maximumAllocationRuntime)
                {
                    allocationsBox.AppendText(Environment.NewLine);
                    allocationsBox.AppendText("Allocation Runtime: " + maximumAllocationRuntime + Environment.NewLine);
                    allocationsBox.AppendText("Maximum Acceptable Runtime: " + allowedProgramDuration + Environment.NewLine);
                    Console.WriteLine("Runtime of the allocation- " + allocationID + "is valid and is below the allowed duration of the program. \n");
                }

                // If the runtime exceeds the allowed time, error is added and allocation is declared invalid
                if (allowedProgramDuration < maximumAllocationRuntime)
                {
                    // Add this allocation id in the error list
                    allocation.setIsValid(false);
                    allocation.setErrors(allocation.getErrors() + 1);
                    allocation.AddErrorToList("Runtime of allocation- " + allocationID + "is invalid and exceeds the allowed duration of the program.");
                    allocationsBox.AppendText(Environment.NewLine);
                    allocationsBox.AppendText("Allocation Runtime: " + maximumAllocationRuntime + Environment.NewLine);
                    allocationsBox.AppendText("Maximum Acceptable Runtime: " + allowedProgramDuration + Environment.NewLine);
                    Console.WriteLine("Runtime of allocation- " + allocationID + "is invalid and exceeds the allowed duration of the program. \n");
                }

                // To check if the ram exceeds the available ram.
                for (int processor = 0; processor < maxRamRequired.Length; processor++)
                {
                    if (processorRamAvailable[processor] > 0)
                    {
                        Console.WriteLine("Processor- " + processor);
                        Console.WriteLine("Ram Available: " + processorRamAvailable[processor]);
                        Console.WriteLine("Maximum Ram Required by any task: " + maxRamRequired[processor]);
                        allocationsBox.AppendText(Environment.NewLine);

                        if (maxRamRequired[processor] > processorRamAvailable[processor])
                        {
                            Console.WriteLine("Not enough ram available on the processor- " + processor + " to complete the task, Allocation- " + allocationID + " is invalid. \n");
                            allocation.setIsValid(false);
                            allocation.setErrors(allocation.getErrors() + 1);
                            allocation.AddErrorToList("Not enough ram available on the processor- " + processor + " to complete the task, Allocation- " + allocationID + " is invalid.");
                            allocationsBox.AppendText("Processor- " + processor);
                            allocationsBox.AppendText(Environment.NewLine);
                            allocationsBox.AppendText("Maximum Required Ram: " + maxRamRequired[processor] + Environment.NewLine);
                            allocationsBox.AppendText("Available Ram: " + processorRamAvailable[processor] + Environment.NewLine);
                        }
                        else
                        {
                            Console.WriteLine("Ram is available on the processor- " + processor + " to complete all the assigned tasks, Allocation= " + allocationID + " is valid." + "\n");
                            allocationsBox.AppendText("Processor- " + processor);
                            allocationsBox.AppendText(Environment.NewLine);
                            allocationsBox.AppendText("Maximum Required Ram: " + maxRamRequired[processor] + Environment.NewLine);
                            allocationsBox.AppendText("Available Ram: " + processorRamAvailable[processor] + Environment.NewLine);
                        }
                    }

                }

                // To check if the download speed exceeds the available
                for (int processor = 0; processor < maxDownloadRequired.Length; processor++)
                {
                    if (processorDownloadAvailable[processor] > 0)
                    {
                        Console.WriteLine("Processor- " + processor);
                        Console.WriteLine("Download speed Available: " + processorDownloadAvailable[processor]);
                        Console.WriteLine("Maximum download speed Required by any task: " + maxDownloadRequired[processor]);
                        allocationsBox.AppendText(Environment.NewLine);

                        if (maxDownloadRequired[processor] > processorDownloadAvailable[processor])
                        {
                            Console.WriteLine("Not enough download speed available on the processor- " + processor + " to complete the task, Allocation- " + allocationID + " is invalid. \n");
                            allocation.setIsValid(false);
                            allocation.setErrors(allocation.getErrors() + 1);
                            allocation.AddErrorToList("Not enough download speed available on the processor- " + processor + " to complete the task, Allocation- " + allocationID + " is invalid.");
                            allocationsBox.AppendText("Processor- " + processor);
                            allocationsBox.AppendText(Environment.NewLine);
                            allocationsBox.AppendText("Maximum Required Download Speed: " + maxDownloadRequired[processor] + Environment.NewLine);
                            allocationsBox.AppendText("Available Download Speed: " + processorDownloadAvailable[processor] + Environment.NewLine);
                        }
                        else
                        {
                            Console.WriteLine("Download speed is available on the processor- " + processor + " to complete all the assigned tasks, Allocation= " + allocationID + " is valid." + "\n");
                            allocationsBox.AppendText("Processor- " + processor);
                            allocationsBox.AppendText(Environment.NewLine);
                            allocationsBox.AppendText("Maximum Required Download Speed: " + maxDownloadRequired[processor] + Environment.NewLine);
                            allocationsBox.AppendText("Available Download Speed: " + processorDownloadAvailable[processor] + Environment.NewLine);
                        }
                    }

                }

                // To check if the upload speed exceeds the avialable
                for (int processor = 0; processor < maxUploadRequired.Length; processor++)
                {
                    if (processorUploadAvailable[processor] > 0)
                    {
                        Console.WriteLine("Processor- " + processor);
                        Console.WriteLine("Upload speed Available: " + processorUploadAvailable[processor]);
                        Console.WriteLine("Maximum upload speed Required by any task: " + maxUploadRequired[processor]);
                        allocationsBox.AppendText(Environment.NewLine);

                        if (maxUploadRequired[processor] > processorUploadAvailable[processor])
                        {
                            Console.WriteLine("Not enough upload speed available on the processor- " + processor + " to complete the task, Allocation- " + allocationID + " is invalid. \n");
                            allocation.setIsValid(false);
                            allocation.setErrors(allocation.getErrors() + 1);
                            allocation.AddErrorToList("Not enough upload speed available on the processor- " + processor + " to complete the task, Allocation- " + allocationID + " is invalid.");
                            allocationsBox.AppendText("Processor- " + processor);
                            allocationsBox.AppendText(Environment.NewLine);
                            allocationsBox.AppendText("Maximum Required Upload Speed: " + maxUploadRequired[processor] + Environment.NewLine);
                            allocationsBox.AppendText("Available Upload Speed: " + processorUploadAvailable[processor] + Environment.NewLine);

                        }
                        else
                        {
                            Console.WriteLine("Upload speed is available on the processor- " + processor + " to complete all the assigned tasks, Allocation= " + allocationID + " is valid." + "\n");
                            allocationsBox.AppendText("Processor- " + processor);
                            allocationsBox.AppendText(Environment.NewLine);
                            allocationsBox.AppendText("Maximum Required Upload Speed: " + maxUploadRequired[processor] + Environment.NewLine);
                            allocationsBox.AppendText("Available Upload Speed: " + processorUploadAvailable[processor] + Environment.NewLine);
                        }
                    }

                }

                allocationsBox.AppendText(Environment.NewLine);
            }

            Console.WriteLine("Finished validating the allocations.");

            // using the static value true. If the loop detects a error this value is changed to false. This value is used to change the backcolor of the heading
            bool valid = true;

            errorBox.AppendText(Environment.NewLine);
            errorBox.AppendText("START of Allocations" + Environment.NewLine);

            foreach (Allocation allocation in allocationList)
            {
                if (!allocation.isIsValid())
                {
                    Console.WriteLine("Allocation- " + allocation.getAllocationID() + " is invalid.");
                    Console.WriteLine("Number of allocation errors found: " + allocation.getErrors() + " .");
                    Console.WriteLine("Errors are as follows: \n ");
                    errorBox.AppendText("Allocation- " + allocation.getAllocationID() + " is invalid." + Environment.NewLine);
                    errorBox.AppendText("Number of allocation errors found: " + allocation.getErrors() + " ." + Environment.NewLine);
                    errorBox.AppendText("Errors are as follows: " + Environment.NewLine);

                    int errCount = 1;

                    foreach (string error in allocation.getErrorList())
                    {
                        Console.WriteLine("Error- " + errCount + " : " + error);
                        errorBox.AppendText("Error- " + errCount + " : " + error + Environment.NewLine);
                        errCount++;
                    }

                    errorBox.AppendText("END of Allocation- " + allocation.getAllocationID() + " ."+ Environment.NewLine);
                    errorBox.AppendText(Environment.NewLine);
                    valid = false;
                }
                else
                {
                    Console.WriteLine("Allocation- " + allocation.getAllocationID() + " is valid.");
                    errorBox.AppendText("Allocation- " + allocation.getAllocationID() + " is valid." + Environment.NewLine);
                    errorBox.AppendText("END of Allocation- " + allocation.getAllocationID() + " ." + Environment.NewLine);
                    errorBox.AppendText(Environment.NewLine);
                }
            }

            errorBox.AppendText("END of Allocations" + Environment.NewLine);

            if (!valid)
            {
                Console.WriteLine("Some allocations are invalid.");
                allocationsHeading.Text = allocationsInvalid;
                allocationsHeading.BackColor = colorInvalid;
                Console.WriteLine("Some allocations are Invalid. \n");
            }

            else
            {
                Console.WriteLine("All allocations are valid.");
                allocationsHeading.Text = allocationsValid;
                allocationsHeading.BackColor = colorValid;
                Console.WriteLine("Allocations are valid. \n");
            }

        }
    }
}