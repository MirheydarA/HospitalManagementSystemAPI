using Business.DTOs.Common;
using Business.DTOs.Doctor.Request;
using Business.DTOs.Doctor.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Abstract
{
    public interface IDoctorService 
    {
        Task<Response<List<DoctorDTO>>> GetAllAsync();
        Task<Response<DoctorDTO>> GetAsync(int id);
        Task<Response> CreateAsync(DoctorCreateDTO model);
        Task<Response> UpdateAsync(int id, DoctorUpdateDTO model);
        Task<Response> DeleteAsync(int id);
    }
}
