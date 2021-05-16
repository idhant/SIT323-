using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PT1
{
    /// <summary>
    ///  Object type to store and manipulate the details of a task retrieved from the cff file.
    /// </summary>
    public class Task
    {
        // CLASS MEMBER VARIABLES

        /// <summary>
        /// Member Variable which stores the taskID as a integer.
        /// </summary>
        private int taskID;

        /// <summary>
        /// Member Variable which stores the task Runtime as a float.
        /// </summary>
        private float taskRuntime;

        /// <summary>
        /// Member Variable which stores the task Reference frequency as a float.
        /// </summary>
        private float taskReferenceFrequency;

        /// <summary>
        /// Member Variable which stores the task Ram as a integer.
        /// </summary>
        private int taskRam;

        /// <summary>
        /// Member Variable which stores the task download speed as a integer.
        /// </summary>
        private int taskDownload;

        /// <summary>
        /// Member Variable which stores the task upload speed as a integer.
        /// </summary>
        private int taskUpload;

        // CLASS PROPERTY METHODS

        /// <summary>
        /// Getter property to retrieve the task ID.
        /// </summary>
        /// <returns>A integer representing the task ID.</returns>
        public int getTaskID()
        {
            return this.taskID;
        }

        /// <summary>
        /// Setter property to change the task ID.
        /// </summary>
        /// <param name="taskID">A integer representing the task ID.</param>
        public void setTaskID(int taskID)
        {
            this.taskID = taskID;
        }

        /// <summary>
        /// Getter property to retrieve the task runtime.
        /// </summary>
        /// <returns>A float representing the task runtime.</returns>
        public float getTaskRuntime()
        {
            return this.taskRuntime;
        }

        /// <summary>
        /// Setter property to change the task runtime.
        /// </summary>
        /// <param name="taskRuntime">A float representing the task runtime.</param>
        public void setTaskRuntime(float taskRuntime)
        {
            this.taskRuntime = taskRuntime;
        }

        /// <summary>
        /// Getter property to retrieve the task reference frequency.
        /// </summary>
        /// <returns>A float representing the task ref frequency.</returns>
        public float getTaskReferenceFrequency()
        {
            return this.taskReferenceFrequency;
        }

        /// <summary>
        /// Setter property to change the task ref frequency.
        /// </summary>
        /// <param name="taskReferenceFrequency">A float representing the task ref frequency.</param>
        public void setTaskReferenceFrequency(float taskReferenceFrequency)
        {
            this.taskReferenceFrequency = taskReferenceFrequency;
        }

        /// <summary>
        /// Getter property to retrieve the task ram.
        /// </summary>
        /// <returns>A int value representing the task ram.</returns>
        public int getTaskRam()
        {
            return this.taskRam;
        }

        /// <summary>
        /// Setter property to change the task ram.
        /// </summary>
        /// <param name="taskRam">A int value representing the task ram.</param>
        public void setTaskRam(int taskRam)
        {
            this.taskRam = taskRam;
        }

        /// <summary>
        /// Getter property to retrieve the task download speed.
        /// </summary>
        /// <returns>A int value representing the task download speed.</returns>
        public int getTaskDownload()
        {
            return this.taskDownload;
        }

        /// <summary>
        /// Setter property to change the task download speed.
        /// </summary>
        /// <param name="taskDownload">A int value representing the task download speed.</param>
        public void setTaskDownload(int taskDownload)
        {
            this.taskDownload = taskDownload;
        }

        /// <summary>
        /// Getter property to retrieve the task upload speed.
        /// </summary>
        /// <returns>A int value representing the task upload speed.</returns>
        public int getTaskUpload()
        {
            return this.taskUpload;
        }

        /// <summary>
        /// Setter property to change the task upload speed.
        /// </summary>
        /// <param name="taskUpload">A int value representing the task upload speed.</param>
        public void setTaskUpload(int taskUpload)
        {
            this.taskUpload = taskUpload;
        }

        // CLASS CONSTRUCTORS
        public Task()
        {

        }

        // CLASS METHODS
    }
}
