using AutoMapper;
using LibraryManagerAPI.Domain.Entities;
using LibraryManagerAPI.Domain.ValueObjects.Input;

namespace LibraryManagerAPI.Infrastructure.Persistance.Config
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<BookVO, Book>().ReverseMap();
                config.CreateMap<BookingVO, Booking>().ReverseMap();
                config.CreateMap<LoanVO, Loan>().ReverseMap();
                config.CreateMap<UserVO, User>().ReverseMap();
            });

            return mappingConfig;
        }
    }
}
