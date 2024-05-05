using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DTOs.Doctor.Response
{
    public class DoctorDTO
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Duty { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
