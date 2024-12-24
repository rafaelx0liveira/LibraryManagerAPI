using AutoMapper;
using LibraryManagerAPI.Domain.Entities;
using LibraryManagerAPI.Domain.ValueObjects;
using LibraryManagerAPI.Infrastructure.Context;
using LibraryManagerAPI.Presentation.Interfaces.Repository.User;

namespace LibraryManagerAPI.Infrastructure.Persistance.Repositories
{
    public class UserRepository(MySQLContext context, IMapper mapper)
        : IUserRepository
    {
        private readonly MySQLContext _context = context;
        private readonly IMapper _mapper = mapper;

        public async Task<UserVO> RegisterUser(UserVO userVO)
        {
            User user = _mapper.Map<User>(userVO);
            _context.Users.Add(user);

            await _context.SaveChangesAsync();

            return _mapper.Map<UserVO>(user);
        }
    }
}
