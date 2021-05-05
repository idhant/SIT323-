using System;
using System.Collections.Generic;
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
            String[] filelines;
            
            TextBox textBox1 = new TextBox
            {
                Size = new Size(300, 300),
                Location = new Point(15, 40),
                Multiline = true,
                ScrollBars = ScrollBars.Vertical
            };
            Controls.Add(textBox1);

            OpenFileDialog theDialog = new OpenFileDialog();
            theDialog.Title = "Open TAFF File";
            //theDialog.Filter = "TXT files|*.txt";
            theDialog.InitialDirectory = @"C:\";
            if (theDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string filename = theDialog.FileName;
                    string cfffilepath = GetDirectoryPath(filename);
                 
                    filelines = File.ReadAllLines(filename);
                    Console.WriteLine("TAFF File lines copied to ArrrayList.");

                    
                    ReadTAFFFileName(filename);

                    WriteTAFFFileData(filename, textBox1);

                    ValidateTAFFFile(filelines);

                    /*
                    StreamReader streamReader = new StreamReader(filename);
                    
                    if (streamReader != null)
                    {
                        ReadTAFFFile(streamReader);
                        textBox1.Text = streamReader.ReadToEnd();
                        WriteTAFFFileData(streamReader, textBox1);
                        /*
                        using (streamReader)
                        {
                            // Insert code to read the stream here.
                            string line;
                            textBox1.Text = streamReader.ReadToEnd();
                        }
                        
                    }*/
                
                    
                    /*
                        string[] filelines = File.ReadAllLines(filename);
                        int index = 0;
                        TextBox textBox = new TextBox();

                        for (index = 0; index < filelines.Length; index++)
                        {
                            textBox.Text = filelines[index++] + textBox.Text;
                        }
                        MessageBox.Show(textBox.Text);
                    */
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
        public void ReadTAFFFileName(String filename)
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

                        //.Trim('"')
                        //CFFname.Trim('"');
                        //CFFname.Replace("\"", "");
                        //quoted.replace("\"", "");
                        //Console.WriteLine(CFFname);
                        
                        Console.WriteLine("CFF File Name Extracted: " + trimmedCFFname + " .");
                    }
                    else
                    {
                        Console.WriteLine("CFF file name not found, continuing search.....");
                    }
                }
            }

            Console.WriteLine("Ending function to extract CFF Name from the TAFF File." + "\n");
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
                                Console.WriteLine("Errors Found: " + errors + " .");
                            }
                            if (!allocationsDataTasks.IsMatch(taskCount))
                            {
                                Console.WriteLine("Line: " + taskCount + " : ALLOCATIONS CONFIG Block -> Valid");
                                errors++;
                                Console.WriteLine("Errors Found: " + errors + " .");
                            }
                            if (!allocationsDataProcessors.IsMatch(processorCount))
                            {
                                Console.WriteLine("Line: " + processorCount + " : ALLOCATIONS CONFIG Block -> Valid");
                                errors++;
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
                                Console.WriteLine("Errors Found: " + errors + " .");
                            }
                            if (!allocationDataMap.IsMatch(allocationMAP))
                            {
                                Console.WriteLine("Line: " + allocationMAP + " : ALLOCATION CONFIG Block -> Invalid");
                                errors++;
                                Console.WriteLine("Errors Found: " + errors + " .");
                            }
                        }
                    }

                }

                else
                {
                    Console.WriteLine("Line: " + trimmedline + " : Line doesn`t match TAFF Syntax -> Invalid");
                    errors++;
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
            }

            else
            {
                Console.WriteLine("The TAFF file is valid.");
            }

        }   


            /*
            for (int index = 0; index < filelines.Length; index++)
            {
                //Console.WriteLine("Current Index: " + index);
                string trimmedline = null;
                trimmedline = filelines[index].Trim();

                Regex comment = new Regex("//.*");
                Regex configurationData = new Regex("CONFIGURATION-DATA");
                Regex CFFfilename = new Regex("FILENAME=");
                Regex configurationDataEndpoint = new Regex("END-CONFIGURATION-DATA");

                if (trimmedline.Length == 0)
                {
                    Console.WriteLine("Line: " + trimmedline + " : Empty Line, is valid");
                }

                else if (comment.IsMatch(trimmedline))
                {
                    Console.WriteLine("Line: " + trimmedline + " : Comment Line, is valid");
                }

                else if (configurationData.IsMatch(trimmedline))
                {
                    int fileindex = index + 1;
                    int endconfigindex = index + 2;
                    string filename = filelines[fileindex].Trim();

                    string endconfiguration = filelines[endconfigindex].Trim();

                    if(CFFfilename.IsMatch(filename) && configurationDataEndpoint.IsMatch(endconfiguration))
                    {
                        Console.WriteLine("Configuration Block is Valid");
                    }
                    else
                    {
                        Console.WriteLine("Configuration Block is Invalid");
                        Console.WriteLine(filename);
                        Console.WriteLine(endconfiguration);
                    }
                }
            }
            */
            /*
            using(streamReader)
            {
                while ((line = streamReader.ReadLine()) != null)
                {
                    string trimmedline = null;
                    trimmedline = line.Trim();
                    Regex comment = new Regex("//.*");
                    Regex configurationData = new Regex("CONFIGURATION-DATA");
                    Regex configurationDataEndpoint = new Regex("END-CONFIGURATION-DATA");

                    if(trimmedline.Length == 0)
                    {
                        Console.WriteLine("Line: " + trimmedline + " : Empty Line, is valid");
                    }

                    else if (comment.IsMatch(trimmedline))
                    {
                        Console.WriteLine("Line: " + trimmedline + " : Comment Line, is valid");
                    }

                    else if (configurationData.IsMatch(trimmedline))
                    {
                        Console.WriteLine("Line: " + trimmedline + " : Configuration Line, is valid ");
                    }
                }
            }
            */
        

        public void WriteTAFFFileData(String filename, TextBox textBox)
        {
            StreamReader streamReader = new StreamReader(filename);
            textBox.Text = streamReader.ReadToEnd();
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
