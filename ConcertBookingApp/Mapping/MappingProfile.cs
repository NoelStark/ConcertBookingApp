using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ConcertBookingApp.DTOs;
using ConcertBookingApp.Models;

namespace ConcertBookingApp.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Concert, ConcertDTO>().ReverseMap();
            CreateMap<Performance, PerformanceDTO>().ReverseMap();
        }
    }
}
