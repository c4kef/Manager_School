using System;

namespace Manager
{
    public struct TaskObject
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Comment { get; set; }
        public string Status { get; set; }
        public DateTime TimeFrom { get; set; }
        public DateTime TimeEnd { get; set; }
        public Device Device { get; set; }
    }
    
    public struct Device
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Type { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public DateTime AttachDate { get; set; }
        public string Status { get; set; }
        public string Cabinet { get; set; }
    }
}