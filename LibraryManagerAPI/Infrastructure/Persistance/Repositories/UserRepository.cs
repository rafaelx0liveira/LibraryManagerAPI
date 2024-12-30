using AutoMapper;
using LibraryManagerAPI.Domain.Entities;
using LibraryManagerAPI.Domain.Exceptions.UserExceptions;
using LibraryManagerAPI.Domain.ValueObjects.Input;
using LibraryManagerAPI.Domain.ValueObjects.Output;
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

        public async Task<UserResultVO> RegisterUserAsync(UserVO userVO)
        {
            User user = _mapper.Map<User>(userVO);
            _context.Users.Add(user);

            await _context.SaveChangesAsync();

            return _mapper.Map<UserResultVO>(user);
        }

        public async Task<UserResultVO> GetUserByEmailAsync(string email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == email);

            if (user == null) {
                return null;
            }

            return _mapper.Map<UserResultVO>(user);
        }

        public async Task<bool> DeleteUserAsync(string email)
        {
            User user = await _context.Users.Where(x => x.Email == email).FirstOrDefaultAsync() ?? new User();

            _context.Users.Remove(user);

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> ExistsByEmailAsync(string email)
        {
            return _context.Users.Where(x => x.Email == email).Any();
        }
    }
}
