using Azure;
using Business.DTOs.Common;
using Business.DTOs.Doctor.Request;
using Business.DTOs.Doctor.Response;
using Business.Exceptions;
using Business.Services.Abstract;
using Business.Validators.Doctor;
using Common.Entities;
using DataAccess.Repositories.Abstract;
using DataAccess.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Concrete
{
    public class Doctorservice : IDoctorService
    {
        private readonly IDoctorRepository _doctorRepository;
        private readonly IUnitOfWork _unitOfWork;

        public Doctorservice(IDoctorRepository doctorRepository, IUnitOfWork unitOfWork)
        {
            _doctorRepository = doctorRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<DTOs.Common.Response<List<DoctorDTO>>> GetAllAsync()
        {
            var products = await _doctorRepository.GetAllAsync();
            var doctorDTOs = new List<DoctorDTO>();

            foreach (var product in products)
            {
                doctorDTOs.Add(new DoctorDTO
                {
                    Name = product.Name,
                    Surname = product.Surname,
                    Duty = product.Duty,
                    Email = product.Email,
                    PhoneNumber = product.PhoneNumber,
                    CreatedAt = product.CreatedAt
                });
            }

            return new DTOs.Common.Response<List<DoctorDTO>>
            {
                Data = doctorDTOs
            };
        }
        public async Task<DTOs.Common.Response<DoctorDTO>> GetAsync(int id)
        {
            var doctor = await _doctorRepository.GetByIdAsync(id);

            if (doctor == null)
                throw new NotFoundException("Doctor Not Found");

            var doctorDTO = new DoctorDTO
            {
                Name = doctor.Name,
                Surname = doctor.Surname,
                Duty = doctor.Duty,
                Email = doctor.Email,
                PhoneNumber = doctor.PhoneNumber,
                CreatedAt = doctor.CreatedAt
            };

            return new DTOs.Common.Response<DoctorDTO>
            {
                Data = doctorDTO
            };
        }
        public async Task<DTOs.Common.Response> CreateAsync(DoctorCreateDTO model)
        {
            var result = await new DoctorCreateDTOValidator().ValidateAsync(model);

            if (!result.IsValid)
            {
                throw new ValidationException(result.Errors);
            }

            var doctor = new Doctor()
            {
                Name = model.Name,
                Surname = model.Surname,
                Duty = model.Duty,
                PhoneNumber = model.PhoneNumber,
                Email = model.Email,
                CreatedAt = DateTime.Now,
            };

            await _doctorRepository.CreateAsync(doctor);
            await _unitOfWork.CommitAsync();

            return new DTOs.Common.Response
            {
                Message = "Doctor succesfully added"
            };
        }
        public async Task<DTOs.Common.Response> UpdateAsync(int id, DoctorUpdateDTO model)
        {
            var result = await new DoctorUpdateDTOValidator().ValidateAsync(model);
            if (!result.IsValid)
                throw new ValidationException(result.Errors);

            var doctor = await _doctorRepository.GetByIdAsync(id);
            if (doctor is null)
                throw new NotFoundException("Doctor Not Found");

            doctor.Name = model.Name;
            doctor.Surname = model.Surname;
            doctor.Duty = model.Duty;
            doctor.Email = model.Email;
            doctor.PhoneNumber = model.PhoneNumber;
            doctor.ModifiedAt = DateTime.Now;

            _doctorRepository.Update(doctor);
            await _unitOfWork.CommitAsync();
            return new DTOs.Common.Response
            {
                Message = "Doctor successfully Modified"
            };
        }
        public async Task<DTOs.Common.Response> DeleteAsync(int id)
        {
            var doctor = await _doctorRepository.GetByIdAsync(id);
            if (doctor is null)
                throw new NotFoundException("Doctor Not Found");
            _doctorRepository.Delete(doctor);
            await _unitOfWork.CommitAsync();

            return new DTOs.Common.Response
            {
                Message = "Doctor successfully deleted" 
            };

        }
    }
}
