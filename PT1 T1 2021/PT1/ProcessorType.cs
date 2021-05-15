using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PT1
{
    public class ProcessorType
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

		// constructor
		public ProcessorType(int C2, int C1, int C0, string processorType)
		{
			this.processorC2 = C2;
			this.processorC1 = C1;
			this.processorC0 = C0;
			this.processorName = processorType;
		}

		public ProcessorType()
        {

        }


		// Methods 
		public float CalculateProcessorEnergy(float time, float frequency)
		{
			float totalEnergy;
			float energyC2, energyC1, energyC0;

			energyC2 = (frequency * frequency) * processorC2;
			energyC1 = frequency * processorC1;
			energyC0 = processorC0;
			totalEnergy = (energyC2 + energyC1 + energyC0) * time;

			return totalEnergy;
		}

	}
}
