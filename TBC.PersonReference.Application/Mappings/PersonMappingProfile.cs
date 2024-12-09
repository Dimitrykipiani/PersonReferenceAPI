using AutoMapper;
using Microsoft.AspNetCore.Http;
using TBC.PersonReference.Application.Models.Request;
using TBC.PersonReference.Application.Models.Response;
using TBC.PersonReference.Core.Entities;
using TBC.PersonReference.Core.Models;

namespace TBC.PersonReference.Application.Mappings
{
    public class PersonMappingProfile : Profile
    {
        public PersonMappingProfile()
        {
            CreateMap<Person, PersonResponse>()
                .ForMember(dest => dest.RelatedPersons, opt => opt.MapFrom(x => x.RelatedPersons.Select(rp => rp.RelatedPersonId)));

            CreateMap<SearchPersonRequest, PersonSearchSpecification>();

            CreateMap<UpdatePersonRequest, Person>();
        }
    }
}
