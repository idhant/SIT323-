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

        TaskAllocations taskAllocations = new TaskAllocations();

        Configuration configuration = new Configuration();

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
     
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

                    taskAllocations.WriteTAFFFileData(filename, TaffFile);

                    taskAllocations.ValidateTAFFFile(taskAllocations.getTAFFFileLines());

                    taskAllocations.GetDirectoryPath(filename);

                    taskAllocations.ReadCFFFileName(filename);

                    taskAllocations.ReadCFFFilePath();

                    configuration.StoreCFFLines(taskAllocations.getCffFilePath());

                    configuration.WriteCFFFileData(taskAllocations.getCffFilePath(), CffFile);

                    configuration.ValidateCFFFile(configuration.GetCFFFileLines());
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
            if (taskAllocations.getIsValid() && configuration.getIsValid())
            {
                Console.WriteLine("Both taff and cff files are valid. You can now check the validations of the allocations.");
                TaffFileName.Text = "Taff File (Valid)";
                CffFileName.Text = "Cff File (Valid)";
                allocationsToolStripMenuItem.Enabled = true;
            }
            else
            {
                if(!taskAllocations.getIsValid())
                {
                    TaffFileName.Text = "Taff File (Invalid)";
                }
                if(!configuration.getIsValid())
                {
                    CffFileName.Text = "Cff File (Invalid)";
                }
            }
        }

        private void allocationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Validating the allocations Now..");
            // Code for validation of allocations.
            Console.WriteLine("Finished validating the allocations.");
            Console.WriteLine("Allocations are valid. \n");

            List<Allocation> allocationList =  taskAllocations.getAllocationList();

            Console.WriteLine("Starting Validating the runtime of tasks.. \n");

            foreach (Allocation allocation in allocationList)
            {
                int allocationID = allocation.getAllocationID();
                int[,] allocationMap = allocation.getAllocationMap();

                float[] processorRuntime = new float[taskAllocations.getTotalProcessors()];
                float maximumAllocationRuntime = 0;
                float allowedProgramDuration = configuration.getProgramDuration();

                for (int processor = 0; processor < taskAllocations.getTotalProcessors(); processor++)
                {
                    for(int task = 0; task < taskAllocations.getTotalTasks(); task++)
                    {
                        int value = allocationMap[processor, task];
                        float taskRuntime = 0;
                        float taskRefFrequency = 0;
                        float processorFrequency = 0;
                        float actualTaskRuntime = 0;

                        if (value == 1)
                        {
                            Console.WriteLine("Processor: " + processor + " is assigned the task: " + task);

                            List<Task> tasksList = configuration.getTasksList();

                            foreach (Task taskNumber in tasksList)
                            {
                                if (taskNumber.getTaskID() == task)
                                {
                                    taskRuntime = taskNumber.getTaskRuntime();
                                    taskRefFrequency = taskNumber.getTaskReferenceFrequency();
                                }
                            }

                            List<Processor> processorsList = configuration.getProcessorsList();

                            foreach (Processor processorNumber in processorsList)
                            {
                                if (processorNumber.getProcessorID() == processor)
                                {
                                    processorFrequency = processorNumber.getProcessorFrequency();
                                }
                            }

                        }

                        if (processorFrequency != 0)
                        {
                            actualTaskRuntime = taskRuntime * (taskRefFrequency / processorFrequency);
                            processorRuntime[processor] = actualTaskRuntime + processorRuntime[processor];
                            Console.WriteLine("Actual Task runtime for Task: " + task + " on processor: " + processor + " is: " + actualTaskRuntime);
                        }
                        // can insert some verification code here to check that the task is only assigned to one processor
                    }
                    Console.WriteLine("Total runtime for processor- " + processor + " = "+ processorRuntime[processor]);
                }

                foreach(float runtime in processorRuntime)
                {
                    if(runtime > maximumAllocationRuntime)
                    {
                        maximumAllocationRuntime = runtime;
                    }
                }
                
                Console.WriteLine("Maximum Runtime of any processor is: " + maximumAllocationRuntime);
                Console.WriteLine("Allowed Maximum Runtime: " + allowedProgramDuration);

                if(allowedProgramDuration > maximumAllocationRuntime)
                {
                    Console.WriteLine("Runtime of the allocation- " + allocationID + "is valid and is below the allowed duration of the program. \n");
                }

                else
                {
                    // Add this allocation id in the error list
                    Console.WriteLine("Runtime of allocation- " + allocationID + "is invalid and exceeds the allowed duration of the program. \n");
                }

            }

            Console.WriteLine("Ending the Validation of the runtime of tasks.. \n");

            Console.WriteLine("Starting the Validation of ram of tasks \n");

            foreach (Allocation allocation in allocationList)
            {
                Console.WriteLine("\n");
                int allocationID = allocation.getAllocationID();
                int[,] allocationMap = allocation.getAllocationMap();
                Console.WriteLine("Checking the Ram for allocation- " + allocationID );

                int[] maxRamRequired = new int[taskAllocations.getTotalProcessors()];
                int[] processorRamAvailable = new int[taskAllocations.getTotalProcessors()];

                for (int processor = 0; processor < taskAllocations.getTotalProcessors(); processor++)
                {
                    for (int task = 0; task < taskAllocations.getTotalTasks(); task++)
                    {
                        int value = allocationMap[processor, task];
                        int taskRam = 0;
                        int processorRam = 0;

                        if (value == 1)
                        {
                            Console.WriteLine("Processor: " + processor + " is assigned the task: " + task);
                            List<Task> tasksList = configuration.getTasksList();

                            foreach (Task taskNumber in tasksList)
                            {
                                if (taskNumber.getTaskID() == task)
                                {
                                    taskRam = taskNumber.getTaskRam();
                                }
                            }

                            List<Processor> processorsList = configuration.getProcessorsList();

                            foreach (Processor processorNumber in processorsList)
                            {
                                if (processorNumber.getProcessorID() == processor)
                                {
                                    processorRam = processorNumber.getProcessorRam();
                                }
                            }
                        }

                        if (taskRam != 0)
                        {
                            Console.WriteLine("Ram required for Task- " + task + " is: " + taskRam + "\n");
                            if (taskRam > maxRamRequired[processor])
                            {
                                maxRamRequired[processor] = taskRam;
                            }
                            processorRamAvailable[processor] = processorRam;
                        }
                        
                    }
                }

                for(int processor = 0; processor < maxRamRequired.Length; processor++)
                {
                    if(processorRamAvailable[processor] > 0)
                    {
                        Console.WriteLine("Processor- " + processor);
                        Console.WriteLine("Ram Available: " + processorRamAvailable[processor]);
                        Console.WriteLine("Maximum Ram Required by any task: " + maxRamRequired[processor]);

                        if (maxRamRequired[processor] > processorRamAvailable[processor])
                        {
                            Console.WriteLine("Not enough ram available on the processor- " + processor + " to complete the task, Allocation- " + allocationID + " is invalid. \n");
                            //add to erros
                        }
                        else
                        {
                            Console.WriteLine("Ram is available on the processor- " + processor + " to complete all the assigned tasks, Allocation= " + allocationID + " is valid." + "\n");
                        }
                    }                    

                }
                

            }

            Console.WriteLine("Ending the Validation of ram of tasks \n");

            Console.WriteLine("Starting the Validation of Download speeds of tasks \n");

            foreach (Allocation allocation in allocationList)
            {
                Console.WriteLine("\n");
                int allocationID = allocation.getAllocationID();
                int[,] allocationMap = allocation.getAllocationMap();
                Console.WriteLine("Checking the Download Speeds for allocation- " + allocationID);

                int[] maxDownloadRequired = new int[taskAllocations.getTotalProcessors()];
                int[] processorDownloadAvailable = new int[taskAllocations.getTotalProcessors()];

                for (int processor = 0; processor < taskAllocations.getTotalProcessors(); processor++)
                {
                    for (int task = 0; task < taskAllocations.getTotalTasks(); task++)
                    {
                        int value = allocationMap[processor, task];
                        int taskDownload = 0;
                        int processorDownload = 0;

                        if (value == 1)
                        {
                            Console.WriteLine("Processor: " + processor + " is assigned the task: " + task);
                            List<Task> tasksList = configuration.getTasksList();

                            foreach (Task taskNumber in tasksList)
                            {
                                if (taskNumber.getTaskID() == task)
                                {
                                    taskDownload = taskNumber.getTaskDownload();
                                }
                            }

                            List<Processor> processorsList = configuration.getProcessorsList();

                            foreach (Processor processorNumber in processorsList)
                            {
                                if (processorNumber.getProcessorID() == processor)
                                {
                                    processorDownload = processorNumber.getProcessorDownload();
                                }
                            }
                        }

                        if (taskDownload != 0)
                        {
                            Console.WriteLine("Download Speed required for Task- " + task + " is: " + taskDownload + "\n");
                            if (taskDownload > maxDownloadRequired[processor])
                            {
                                maxDownloadRequired[processor] = taskDownload;
                            }
                            processorDownloadAvailable[processor] = processorDownload;
                        }
                    }
                }

                for (int processor = 0; processor < maxDownloadRequired.Length; processor++)
                {
                    if (processorDownloadAvailable[processor] > 0)
                    {
                        Console.WriteLine("Processor- " + processor);
                        Console.WriteLine("Download speed Available: " + processorDownloadAvailable[processor]);
                        Console.WriteLine("Maximum download speed Required by any task: " + maxDownloadRequired[processor]);

                        if (maxDownloadRequired[processor] > processorDownloadAvailable[processor])
                        {
                            Console.WriteLine("Not enough download speed available on the processor- " + processor + " to complete the task, Allocation- " + allocationID + " is invalid. \n");
                            //add to erros
                        }
                        else
                        {
                            Console.WriteLine("Download speed is available on the processor- " + processor + " to complete all the assigned tasks, Allocation= " + allocationID + " is valid." + "\n");
                        }
                    }

                }
            }

            Console.WriteLine("Ending the Validation of Download speeds of tasks \n");

            Console.WriteLine("Starting the Validation of Upload speeds of tasks \n");

            foreach (Allocation allocation in allocationList)
            {
                Console.WriteLine("\n");
                int allocationID = allocation.getAllocationID();
                int[,] allocationMap = allocation.getAllocationMap();
                Console.WriteLine("Checking the Upload speeds for allocation- " + allocationID);

                int[] maxUploadRequired = new int[taskAllocations.getTotalProcessors()];
                int[] processorUploadAvailable = new int[taskAllocations.getTotalProcessors()];

                for (int processor = 0; processor < taskAllocations.getTotalProcessors(); processor++)
                {
                    for (int task = 0; task < taskAllocations.getTotalTasks(); task++)
                    {
                        int value = allocationMap[processor, task];
                        int taskUpload = 0;
                        int processorUpload = 0;

                        if (value == 1)
                        {
                            Console.WriteLine("Processor: " + processor + " is assigned the task: " + task);
                            List<Task> tasksList = configuration.getTasksList();

                            foreach (Task taskNumber in tasksList)
                            {
                                if (taskNumber.getTaskID() == task)
                                {
                                    taskUpload = taskNumber.getTaskUpload();
                                }
                            }

                            List<Processor> processorsList = configuration.getProcessorsList();

                            foreach (Processor processorNumber in processorsList)
                            {
                                if (processorNumber.getProcessorID() == processor)
                                {
                                    processorUpload = processorNumber.getProcessorUpload();
                                }
                            }
                        }

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
                }

                for (int processor = 0; processor < maxUploadRequired.Length; processor++)
                {
                    if (processorUploadAvailable[processor] > 0)
                    {
                        Console.WriteLine("Processor- " + processor);
                        Console.WriteLine("Upload speed Available: " + processorUploadAvailable[processor]);
                        Console.WriteLine("Maximum upload speed Required by any task: " + maxUploadRequired[processor]);

                        if (maxUploadRequired[processor] > processorUploadAvailable[processor])
                        {
                            Console.WriteLine("Not enough upload speed available on the processor- " + processor + " to complete the task, Allocation- " + allocationID + " is invalid. \n");
                            //add to erros
                        }
                        else
                        {
                            Console.WriteLine("Upload speed is available on the processor- " + processor + " to complete all the assigned tasks, Allocation= " + allocationID + " is valid." + "\n");
                        }
                    }

                }
            }

            Console.WriteLine("Ending the Validation of Upload speeds of tasks \n");
        }

    }
}
