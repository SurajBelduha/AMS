using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using System.Threading.Tasks;
using AMS.Repository.Models;
using AMS.Common.Entities;

namespace AMS.Providers.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Appointment, AppointmentModel>().ReverseMap();
        }
    }
}
