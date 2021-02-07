using AutoMapper;
using Cinema.Dtos;
using Cinema.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cinema.App_Start
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //To Create A Mapping Configuration
            //Genarec Method That take Two Paramters <Source, Target> 
            //When we Called this CreateMap<>(); Method => AutoMapper Uses Reflection To Scan This Types 
            //and find their Properties and Map Them Base-Under-Name

            //Mapper.CreateMap<Source, Target>()
            
            //Domain => To => Dto
            Mapper.CreateMap<Customer, CustomerDto>();

            Mapper.CreateMap<Movie, MovieDto>();

            Mapper.CreateMap<MemberShipType, MemberShipTypeDto>();
            
            Mapper.CreateMap<Genre, GenreDto>();

            Mapper.CreateMap<Rental, NewRentalDto>();


            //-------------------------------------------------------------------------------------------

            //Dto => To => Domain
            Mapper.CreateMap<CustomerDto, Customer>().ForMember(f => f.Id, opt => opt.Ignore());

            Mapper.CreateMap<MovieDto, Movie>().ForMember(f => f.Id, opt => opt.Ignore());


            //Mapper.CreateMap<NewRentalDto, Rental>().ForMember(f => f.Id, opt => opt.Ignore());

            //Mapper.CreateMap<MemberShipTypeDto, MemberShipType>();/*.ForMember(f => f.MemberShipTypeID, opt => opt.Ignore());*/


        }
    }
}





