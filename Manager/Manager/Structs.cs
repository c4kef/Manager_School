using System;

namespace Manager
{
    public struct TaskObject
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Comment { get; set; }
        public string Status { get; set; }
        public DateTime? TimeFrom { get; set; }
        public DateTime? TimeEnd { get; set; }
        public Device Device { get; set; }
    }
    
    public struct Device
    {
        public int WId { get; set; }
        public int EId { get; set; }
        public int SId { get; set; }
        public string FullModelInfo
        {
            get => $"{WId} {Type}";
        }
        public string UserName { get; set; }
        public string Type { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public DateTime AttachDate { get; set; }
        public string Status { get; set; }
        public int Cabinet { get; set; }
    }

    public struct UserData
    {
        public int Id { get; set; }
        public bool IsAdmin { get; set; }
        public string FullName { get; set; }
        public string Post { get; set; }
        public int Number { get; set; }
        public int Housing { get; set; }
        public bool Stat { get; set; }
    }
}