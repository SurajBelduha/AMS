using System;
using System.Collections.Generic;

namespace AMS.Repository.Models
{
    public partial class Appointment
    {
        public int Id { get; set; }
        public string PatientName { get; set; } = null!;
        public string DoctorName { get; set; } = null!;
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public bool IsCancelled { get; set; }
    }
}
