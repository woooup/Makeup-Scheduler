using MakeupScheduler.Helpers;
using System;

namespace MakeupScheduler.Models
{
    public class Item : ObservableObject
    {
        private string name;
        private bool accessories;
        private DateTime date;
        private TimeSpan startTime;
        private TimeSpan endTime;
        private double price;

        public string Id { get; set; }  
        public string Name
        {
            get => name;
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        } 
        public bool Accessories
        {
            get => accessories;
            set
            {
                accessories = value;
                OnPropertyChanged("Accessories");
            }
        }
        public DateTime Date
        {
            get => date;
            set
            {
                date = value;
                OnPropertyChanged("Date");
            }
        }
        public TimeSpan StartTime
        {
            get => startTime;
            set
            {
                startTime = value;
                OnPropertyChanged("StartTime");
            }
        }
        public TimeSpan EndTime
        {
            get => endTime;
            set
            {
                endTime = value;
                OnPropertyChanged("EndTime");
            }
        }
        public double Price
        {
            get => price;
            set
            {
                price = value;
                OnPropertyChanged("Price");
            }
        }
    }
}