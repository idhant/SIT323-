using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PT1
{
	/// <summary>
	/// Object type to store and manipulate the details of a processor retrieved from the cff file.
	/// </summary>
	public class Processor
    {
		// CLASS MEMBER VARIABLES

		/// <summary>
		/// Member Variable which stores the processorID as a integer.
		/// </summary>
		private int processorID;

		/// <summary>
		/// Member Variable which stores the processor type as a string.
		/// </summary>
		private string processorType;

		/// <summary>
		/// Member Variable which stores the processor frequency as a float.
		/// </summary>
		private float processorFrequency;

		/// <summary>
		/// Member Variable which stores the processor ram as a integer.
		/// </summary>
		private int processorRam;

		/// <summary>
		/// Member Variable which stores the processor download speed as a integer.
		/// </summary>
		private int processorDownload;

		/// <summary>
		/// Member Variable which stores the processor upload speed as a integer.
		/// </summary>
		private int processorUpload;

		// CLASS PROPERTY METHODS

		/// <summary>
		/// Getter property to retrieve the processor ID.
		/// </summary>
		/// <returns>A integer representing the processor ID.</returns>
		public int getProcessorID()
		{
			return this.processorID;
		}

		/// <summary>
		/// Setter property to change the processor ID.
		/// </summary>
		/// <param name="processorID">A integer representing the processor ID.</param>
		public void setProcessorID(int processorID)
		{
			this.processorID = processorID;
		}

		/// <summary>
		/// Getter property to retrieve the processor type.
		/// </summary>
		/// <returns>A string value representing the processor type.</returns>
		public string getProcessorType()
		{
			return this.processorType;
		}

		/// <summary>
		/// Setter property to change the processor type.
		/// </summary>
		/// <param name="processorType">A string value representing the processor type.</param>
		public void setProcessorType(string processorType)
		{
			this.processorType = processorType;
		}

		/// <summary>
		/// Getter property to retrieve the processor frequency.
		/// </summary>
		/// <returns>A float value representing the processor frequency.</returns>
		public float getProcessorFrequency()
		{
			return this.processorFrequency;
		}

		/// <summary>
		/// Setter property to change the processor
		/// </summary>
		/// <param name="processorFrequency">A float value representing the processor frequency.</param>
		public void setProcessorFrequency(float processorFrequency)
		{
			this.processorFrequency = processorFrequency;
		}

		/// <summary>
		/// Getter property to retrieve the processor ram.
		/// </summary>
		/// <returns>A int value representing the processor ram.</returns>
		public int getProcessorRam()
		{
			return this.processorRam;
		}

		/// <summary>
		/// Setter property to change the processor
		/// </summary>
		/// <param name="processorRam">A int value representing the processor ram.</param>
		public void setProcessorRam(int processorRam)
		{
			this.processorRam = processorRam;
		}

		/// <summary>
		/// Getter property to retrieve the processor download speed.
		/// </summary>
		/// <returns>A int value representing the processor download speed.</returns>
		public int getProcessorDownload()
		{
			return this.processorDownload;
		}

		/// <summary>
		/// Setter property to change the processor
		/// </summary>
		/// <param name="processorDownload">A int value representing the processor download speed.</param>
		public void setProcessorDownload(int processorDownload)
		{
			this.processorDownload = processorDownload;
		}

		/// <summary>
		/// Getter property to retrieve the processor upload speed.
		/// </summary>
		/// <returns>A int value representing the processor upload speed.</returns>
		public int getProcessorUpload()
		{
			return this.processorUpload;
		}

		/// <summary>
		/// Setter property to change the processor
		/// </summary>
		/// <param name="processorUpload">A int value representing the processor upload speed.</param>
		public void setProcessorUpload(int processorUpload)
		{
			this.processorUpload = processorUpload;
		}

		// CLASS CONSTRUCTORS

		public Processor()
        {

        }

		// CLASS METHODS

		/// <summary>
		/// Method to check whether the ram required by a task is available on the processor.
		/// </summary>
		/// <param name="task">A Task Object Type.</param>
		/// <returns>A boolean value; true when the ram is sufficient, false when it is not.</returns>
		public bool IsRamSufficient(Task task)
        {
			if(processorRam >= task.getTaskRam())
            {
				return true;
            }
			else
            {
				return false;
            }
        }

		/// <summary>
		/// Method to check whether the download speed required by a task is available on the processor.
		/// </summary>
		/// <param name="task">A Task Object Type.</param>
		/// <returns>A boolean value; true when the download speed is sufficient, false when it is not.</returns>
		public bool IsDownloadSufficient(Task task)
		{
			if (processorDownload >= task.getTaskDownload())
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		/// <summary>
		/// Method to check whether the upload speed required by a task is available on the processor.
		/// </summary>
		/// <param name="task">A Task Object Type.</param>
		/// <returns>A boolean value; true when the upload speed is sufficient, false when it is not.</returns>
		public bool IsUploadSufficient(Task task)
		{
			if (processorUpload >= task.getTaskUpload())
			{
				return true;
			}
			else
			{
				return false;
			}
		}

	}
}
