using AutoMapper;
using LibraryManagerAPI.Domain.Entities;
using LibraryManagerAPI.Domain.ValueObjects.Input;
using LibraryManagerAPI.Domain.ValueObjects.Output;

namespace LibraryManagerAPI.Infrastructure.Persistance.Config
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<BookResultVO, Book>().ReverseMap();
                config.CreateMap<LoanVO, Loan>().ReverseMap();
                config.CreateMap<UserResultVO, User>().ReverseMap();
            });

            return mappingConfig;
        }
    }
}
