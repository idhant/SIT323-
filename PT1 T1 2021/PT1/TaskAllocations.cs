using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PT1
{
    class TaskAllocations
    {
        private string CFFname = null ;
        

        public TaskAllocations()
        {

        }

        public void OpenTAFFFile()
        {
            OpenFileDialog theDialog = new OpenFileDialog();
            theDialog.Title = "Open TAFF File";
            //theDialog.Filter = "TXT files|*.txt";
            theDialog.InitialDirectory = @"C:\";
            if (theDialog.ShowDialog() == DialogResult.OK)
            {
                string filename = theDialog.FileName;

                string[] filelines = File.ReadAllLines(filename);
                int index = 0;
                TextBox textBox = new TextBox();

                if (index < filelines.Length)
                {
                    textBox.Text = filelines[index++];
                }
                /*
                List<Employee> employeeList = new List<Employee>();
                int linesPerEmployee = 4;
                int currEmployeeLine = 0;
                //parse line by line into instance of employee class
                Employee employee = new Employee();
                for (int a = 0; a < filelines.Length; a++)
                {

                    //check if to move to next employee
                    if (a != 0 && a % linesPerEmployee == 0)
                    {
                        employeeList.Add(employee);
                        employee = new Employee();
                        currEmployeeLine = 1;
                    }

                    else
                    {
                        currEmployeeLine++;
                    }
                    switch (currEmployeeLine)
                    {
                        case 1:
                            employee.EmployeeNum = Convert.ToInt32(filelines[a].Trim());
                            break;
                        case 2:
                            employee.Name = filelines[a].Trim();
                            break;
                        case 3:
                            employee.Address = filelines[a].Trim();
                            break;
                        case 4:
                            string[] splitLines = filelines[a].Split(' ');

                            employee.Wage = Convert.ToDouble(splitLines[0].Trim());
                            employee.Hours = Convert.ToDouble(splitLines[1].Trim());
                            break;


                    }

                }
                //Test to see if it works
                foreach (Employee emp in employeeList)
                {
                    MessageBox.Show(emp.EmployeeNum + Environment.NewLine +
                        emp.Name + Environment.NewLine +
                        emp.Address + Environment.NewLine +
                        emp.Wage + Environment.NewLine +
                        emp.Hours + Environment.NewLine);
                }
                */
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

        /*
        Notice that there are only 6 kinds of lines in a TAFF file:
        i. Blank lines, i.e., lines that contain 0 or more white spaces.
        ii. Lines that contain comments.
        iii. Data lines, each start with a keyword, followed by an =, followed by data.
        iv. Start of block identifier.
        v. End of block identifier.
        vi. Any other line that doesn’t match the above are simply invalid.
        */

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
    }
}
