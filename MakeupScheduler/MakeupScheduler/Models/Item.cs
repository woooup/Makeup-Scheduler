using System;

namespace MakeupScheduler.Models
{
    public class Item
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public bool Accessories { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public double Price { get; set; }
    }
}