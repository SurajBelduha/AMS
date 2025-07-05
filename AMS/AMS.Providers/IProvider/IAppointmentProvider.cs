using AMS.Common.Entities;

namespace AMS.Providers.IProvider
{
    public interface IAppointmentProvider
    {
        List<AppointmentModel> GetAll();
        AppointmentModel GetById(int id);
        ResponseModel Save(AppointmentModel appointment);
        ResponseModel Cancel(int id);
    }
}
