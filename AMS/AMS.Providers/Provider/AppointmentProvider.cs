using AMS.Common.Entities;
using AMS.Providers.IProvider;
using AMS.Repository.Models;
using AMS.Repository.Repository;
using AutoMapper;

public class AppointmentProvider : IAppointmentProvider
{
    private readonly UnitOfWork _context = new UnitOfWork();
    private readonly IMapper _mapper;

    public AppointmentProvider(IMapper mapper)
    {
        _mapper = mapper;
    }

    public List<AppointmentModel> GetAll()
    {
        try
        {
            var data = _context.Appointment.GetAll().Select(x => new AppointmentModel
            {
                Id = x.Id,
                DoctorName = x.DoctorName,
                PatientName = x.PatientName,
                StartTime = x.StartTime,
                EndTime = x.EndTime,
                IsCancelled=x.IsCancelled

            }).ToList();
            return data;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public AppointmentModel GetById(int id)
    {
        AppointmentModel model = new AppointmentModel() { StartTime = DateTime.Now, EndTime = DateTime.Now };
        try
        {
            if (id > 0)
            {
                var appointment = _context.Appointment.GetById(id);
                model = _mapper.Map<AppointmentModel>(appointment);
            }
            return model;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public ResponseModel Save(AppointmentModel inputModel)
    {
        ResponseModel responseModel = new ResponseModel() { IsSuccess = false, Data = "" };
        try
        {
            if (string.IsNullOrWhiteSpace(inputModel.PatientName) ||
           string.IsNullOrWhiteSpace(inputModel.DoctorName) || 
           inputModel.StartTime >= inputModel.EndTime)
            {

                responseModel.Message = "Invalid appointment data";
                return responseModel;

            }

            bool overlaps = _context.Appointment.Any(a => a.Id != inputModel.Id &&
                a.DoctorName == inputModel.DoctorName &&
                ((inputModel.StartTime >= a.StartTime && inputModel.StartTime < a.EndTime) ||
                 (inputModel.EndTime > a.StartTime && inputModel.EndTime <= a.EndTime)));

            if (overlaps)
            {
                responseModel.Message = "Appointment overlaps with another for the same doctor";
                return responseModel;
            }
            var temp = _context.Appointment.GetAll(x => x.Id == inputModel.Id).FirstOrDefault();

            Appointment details = temp != null ? temp : new Appointment();
            details = _mapper.Map(inputModel, temp);
            if (temp != null)
            {
                _context.Appointment.Update(details);
                responseModel.Message = $"Appointment updated successfully";
            }
            else
            {
                _context.Appointment.Insert(details);
                responseModel.Message = $"Appointment created successfully";
            }

            _context.Save();
            responseModel.IsSuccess = true;
            return responseModel;

        }
        catch (Exception ex)
        {
            responseModel.Message = "something went wrong please try after sometime";
            return responseModel;
        }
    }

    public ResponseModel Cancel(int id)
    {
            var response = new ResponseModel { IsSuccess = false };
        try
        {
            var appt = _context.Appointment.GetAll(a => a.Id == id).FirstOrDefault();
            if (appt == null)
            {
                response.Message = "Appointment not found";
                return response;
            }

            appt.IsCancelled = true;
            _context.Save();

            response.IsSuccess = true;
            response.Message = "Appointment cancelled successfully";
            return response;
        }
        catch (Exception ex)
        {
            return new ResponseModel
            {
                IsSuccess = false,
                Message = "something went wrong please try after sometime",
                Data = false
            };
        }
    }
}
