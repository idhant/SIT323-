using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PT1
{
    public class Processor
    {
        // Vars
        private int processorID;
        private string processorType;
        private float processorFrequency;
        private int processorRam;
        private int processorDownload;
        private int processorUpload;

		// Getters and Setters
		public int getProcessorID()
		{
			return this.processorID;
		}

		public void setProcessorID(int processorID)
		{
			this.processorID = processorID;
		}

		public string getProcessorType()
		{
			return this.processorType;
		}

		public void setProcessorType(string processorType)
		{
			this.processorType = processorType;
		}

		public float getProcessorFrequency()
		{
			return this.processorFrequency;
		}

		public void setProcessorFrequency(float processorFrequency)
		{
			this.processorFrequency = processorFrequency;
		}

		public int getProcessorRam()
		{
			return this.processorRam;
		}

		public void setProcessorRam(int processorRam)
		{
			this.processorRam = processorRam;
		}

		public int getProcessorDownload()
		{
			return this.processorDownload;
		}

		public void setProcessorDownload(int processorDownload)
		{
			this.processorDownload = processorDownload;
		}

		public int getProcessorUpload()
		{
			return this.processorUpload;
		}

		public void setProcessorUpload(int processorUpload)
		{
			this.processorUpload = processorUpload;
		}

		// constructor
		public Processor()
        {

        }

		//Methods


		//Test method

    }
}
