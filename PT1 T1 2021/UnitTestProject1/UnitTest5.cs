using Microsoft.VisualStudio.TestTools.UnitTesting;
using PT1;
using System;
using System.IO;
using System.Text.RegularExpressions;

namespace UnitTestProject1
{
    [TestClass]
    public class ValidateCffFileFormat
    {
        [TestMethod]
        public void ValidateCFFFile_FirstFile_ReturnsTrue()
        {
            // Arange.
            // Getting the Configuration object
            PT1.Configuration configuration = new Configuration();

            // Getting the filelines to be passed to the Validate method
            string pathOfFile = @"Programming Task 1 - Data Files\PT1 - Test1.cff";
            string currentPath = Directory.GetCurrentDirectory();
            Console.WriteLine("Current Path: " + currentPath);
            Console.WriteLine("Path of File: " + pathOfFile);

            Regex pathRegex = new Regex("bin.*");

            string rootPath = pathRegex.Replace(currentPath, "");
            Console.WriteLine("Retrieved root path: " + rootPath);

            pathOfFile = rootPath + pathOfFile;
            Console.WriteLine("Retrieved file path: " + pathOfFile);

            // storing the fillines in a string array
            configuration.StoreCFFLines(pathOfFile);

            string[] filelines = configuration.GetCFFFileLines();

            // the expected result
            bool expectedResult = true;

            // Act.
            bool actualResult = configuration.ValidateCFFFile(filelines);

            // Assert.
            Assert.AreEqual(expectedResult, actualResult, "The actual value doesn`t match the expected value");
        }

        [TestMethod]
        public void ValidateCFFFile_SecondFile_ReturnsTrue()
        {
            // Arange.
            // Getting the Configuration object
            PT1.Configuration configuration = new Configuration();

            // Getting the filelines to be passed to the Validate method
            string pathOfFile = @"Programming Task 1 - Data Files\PT1 - Test2.cff";
            string currentPath = Directory.GetCurrentDirectory();
            Console.WriteLine("Current Path: " + currentPath);
            Console.WriteLine("Path of File: " + pathOfFile);

            Regex pathRegex = new Regex("bin.*");

            string rootPath = pathRegex.Replace(currentPath, "");
            Console.WriteLine("Retrieved root path: " + rootPath);

            pathOfFile = rootPath + pathOfFile;
            Console.WriteLine("Retrieved file path: " + pathOfFile);

            // storing the fillines in a string array
            configuration.StoreCFFLines(pathOfFile);

            string[] filelines = configuration.GetCFFFileLines();

            // the expected result
            bool expectedResult = true;

            // Act.
            bool actualResult = configuration.ValidateCFFFile(filelines);

            // Assert.
            Assert.AreEqual(expectedResult, actualResult, "The actual value doesn`t match the expected value");
        }

        [TestMethod]
        public void ValidateCFFFile_ThirdFile_ReturnsTrue()
        {
            // Arange.
            // Getting the Configuration object
            PT1.Configuration configuration = new Configuration();

            // Getting the filelines to be passed to the Validate method
            string pathOfFile = @"Programming Task 1 - Data Files\PT1 - Test3.cff";
            string currentPath = Directory.GetCurrentDirectory();
            Console.WriteLine("Current Path: " + currentPath);
            Console.WriteLine("Path of File: " + pathOfFile);

            Regex pathRegex = new Regex("bin.*");

            string rootPath = pathRegex.Replace(currentPath, "");
            Console.WriteLine("Retrieved root path: " + rootPath);

            pathOfFile = rootPath + pathOfFile;
            Console.WriteLine("Retrieved file path: " + pathOfFile);

            // storing the fillines in a string array
            configuration.StoreCFFLines(pathOfFile);

            string[] filelines = configuration.GetCFFFileLines();

            // the expected result
            bool expectedResult = true;

            // Act.
            bool actualResult = configuration.ValidateCFFFile(filelines);

            // Assert.
            Assert.AreEqual(expectedResult, actualResult, "The actual value doesn`t match the expected value");
        }

        [TestMethod]
        public void ValidateCFFFile_FourthFile_ReturnsFalse()
        {
            // Arange.
            // Getting the Configuration object
            PT1.Configuration configuration = new Configuration();

            // Getting the filelines to be passed to the Validate method
            string pathOfFile = @"Programming Task 1 - Data Files\PT1 - Test4.cff";
            string currentPath = Directory.GetCurrentDirectory();
            Console.WriteLine("Current Path: " + currentPath);
            Console.WriteLine("Path of File: " + pathOfFile);

            Regex pathRegex = new Regex("bin.*");

            string rootPath = pathRegex.Replace(currentPath, "");
            Console.WriteLine("Retrieved root path: " + rootPath);

            pathOfFile = rootPath + pathOfFile;
            Console.WriteLine("Retrieved file path: " + pathOfFile);

            // storing the fillines in a string array
            configuration.StoreCFFLines(pathOfFile);

            string[] filelines = configuration.GetCFFFileLines();

            // the expected result
            bool expectedResult = false;

            // Act.
            bool actualResult = configuration.ValidateCFFFile(filelines);

            // Assert.
            Assert.AreEqual(expectedResult, actualResult, "The actual value doesn`t match the expected value");
        }

    }
}
