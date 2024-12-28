using AutoMapper;
using LibraryManagerAPI.Domain.Entities;
using LibraryManagerAPI.Domain.Exceptions.UserExceptions;
using LibraryManagerAPI.Domain.ValueObjects.Input;
using LibraryManagerAPI.Infrastructure.Context;
using LibraryManagerAPI.Presentation.Interfaces.Repository.User;
using Microsoft.EntityFrameworkCore;

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

        public async Task<bool> DeleteUser(string email)
        {
            User user = await _context.Users.Where(x => x.Email == email).FirstOrDefaultAsync() ?? new User();

            if(user.Email == null)
            {
                throw new UserNotFoundException($"User with email '{email}' was not found.");
            }

            _context.Users.Remove(user);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}
