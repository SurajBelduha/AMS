using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMS.Common.Entities
{
    public class AppointmentModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Patient Name is required")]
        public string PatientName { get; set; }

        [Required(ErrorMessage = "Doctor Name is required")]
        public string DoctorName { get; set; }
        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy THH:mm}", ApplyFormatInEditMode = true)]
        public DateTime StartTime { get; set; }
        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy THH:mm}", ApplyFormatInEditMode = true)]
        public DateTime EndTime { get; set; }
        public bool IsCancelled { get; set; }
    }
}
