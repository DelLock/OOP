using System;

namespace To_do_List__Простой_
{
    public class TaskItem
    {
        public string Description { get; set; } = "";
        public bool IsCompleted { get; set; } = false;
        public bool IsRead { get; set; } = false;
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime? CompletedDate { get; set; } = null;

        
        public TimeSpan? TimeSpent
        {
            get
            {
                return IsCompleted && CompletedDate.HasValue
                    ? CompletedDate.Value - CreatedDate
                    : null;
            }
        }

        
        public TimeSpan TimeElapsed
        {
            get { return DateTime.Now - CreatedDate; }
        }
    }
}