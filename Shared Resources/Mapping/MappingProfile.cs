using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Shared_Resources.DTOs;
using SharedResources.DTOs;
using SharedResources.Models;

namespace SharedResources.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Concert, ConcertDTO>()
                .ForMember(
                    dest => dest.Dates,
                    opt => opt.MapFrom(src => src.Performances.Select(p => p.Date).ToList()))
                .ReverseMap()
                .ForMember(
                    dest => dest.Performances,
                    opt => opt.MapFrom(src => src.Dates.Select(date => new Performance{Date = date})));
            CreateMap<Performance, PerformanceDTO>().ReverseMap();
            CreateMap<Booking, BookingDTO>().ReverseMap();
            CreateMap<BookingPerformance, BookingPerformanceDTO>().ReverseMap();
        }
    }
}
