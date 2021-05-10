using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PT1
{
    class ProcessorType
    {
        // Vars
        private string processorName;
        private float processorC2;
        private float processorC1;
        private float processorC0;

		// Getters and Setters
		public string getProcessorName()
		{
			return this.processorName;
		}

		public void setProcessorName(string processorName)
		{
			this.processorName = processorName;
		}

		public float getProcessorC2()
		{
			return this.processorC2;
		}

		public void setProcessorC2(float processorC2)
		{
			this.processorC2 = processorC2;
		}

		public float getProcessorC1()
		{
			return this.processorC1;
		}

		public void setProcessorC1(float processorC1)
		{
			this.processorC1 = processorC1;
		}

		public float getProcessorC0()
		{
			return this.processorC0;
		}

		public void setProcessorC0(float processorC0)
		{
			this.processorC0 = processorC0;
		}

		// Constructors
		public ProcessorType()
        {

        }

        // Methods

    }
}
