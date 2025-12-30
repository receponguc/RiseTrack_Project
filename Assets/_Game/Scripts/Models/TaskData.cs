using System;

namespace FluxPlan.Models
{
    [System.Serializable]
    public class TaskData
    {
        public string id;
        public string title;
        public bool isCompleted;
        public TaskPriority priority;
        public long createdDate;
        public long scheduledDate;

        public TaskData(string title, long scheduledDate, TaskPriority priority = TaskPriority.Medium)
        {
            this.id = Guid.NewGuid().ToString();
            this.title = title;
            this.scheduledDate = scheduledDate;
            this.priority = priority;
            this.createdDate = DateTimeOffset.Now.ToUnixTimeSeconds();
            this.isCompleted = false;
        }
    }
}