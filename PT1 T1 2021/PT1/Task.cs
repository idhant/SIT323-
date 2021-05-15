using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PT1
{
    public class Task
    {
        // Vars
        private int taskID;
        private float taskRuntime;
        private float taskReferenceFrequency;
        private int taskRam;
        private int taskDownload;
        private int taskUpload;

        // Getters
        public int getTaskID()
        {
            return taskID;
        }

        public float getTaskRuntime()
        {
            return taskRuntime;
        }

        public float getTaskReferenceFrequency()
        {
            return taskReferenceFrequency;
        }

        public int getTaskRam()
        {
            return taskRam;
        }

        public int getTaskDownload()
        {
            return taskDownload;
        }

        public int getTaskUpload()
        {
            return taskUpload;
        }

        // Setters
        public void setTaskID(int taskID)
        {
            this.taskID = taskID;
        }

        public void setTaskRuntime(float taskRuntime)
        {
            this.taskRuntime = taskRuntime;
        }

        public void setTaskReferenceFrequency(float taskReferenceFrequency)
        {
            this.taskReferenceFrequency = taskReferenceFrequency;
        }

        public void setTaskRam(int taskRam)
        {
            this.taskRam = taskRam;
        }

        public void setTaskDownload(int taskDownload)
        {
            this.taskDownload = taskDownload;
        }

        public void setTaskUpload(int taskUpload)
        {
            this.taskUpload = taskUpload;
        }

        // Constructors
        public Task()
        {

        }

        // Methods
    }
}
