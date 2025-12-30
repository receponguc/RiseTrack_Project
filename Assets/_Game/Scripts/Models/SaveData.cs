using System.Collections.Generic;
using FluxPlan.Models;

namespace FluxPlan.Models
{
    [System.Serializable]
    public class SaveData
    {
        public List<TaskData> savedTasks;

        // Yapýcý metot (Constructor)
        public SaveData(List<TaskData> tasks)
        {
            this.savedTasks = tasks;
        }
    }
}