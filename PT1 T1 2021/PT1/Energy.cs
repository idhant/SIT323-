using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PT1
{
    class Energy
    {
        // Vars
        private int C2;
        private int C1;
        private int C0;
        private string processorType;

        // constructor
        public Energy(int C2, int C1, int C0, string processorType)
        {
            this.C2 = C2;
            this.C1 = C1;
            this.C0 = C0;
            this.processorType = processorType;
        }


        // Methods 
        public float CalculateProcessorEnergy(float time, float frequency)
        {
            float totalEnergy;
            float energyC2, energyC1, energyC0;

            energyC2 = (frequency * frequency) * C2;
            energyC1 = frequency * C1;
            energyC0 = C0;
            totalEnergy = (energyC2 + energyC1 + energyC0) * time;

            return totalEnergy;
        }
    }
}
