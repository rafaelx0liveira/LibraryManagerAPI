﻿using LibraryManagerAPI.Application.Commom.Validation;
using LibraryManagerAPI.Domain.Exceptions.UserExceptions;
using LibraryManagerAPI.Domain.ValueObjects.Input;
using LibraryManagerAPI.Presentation.Interfaces.Repository.User;
using LibraryManagerAPI.Presentation.Interfaces.UseCases.User;

namespace LibraryManagerAPI.Application.UseCases.User
{
    public class DeleteUserUseCase(IUserRepository userRepository)
        : IDeleteUserUseCase
    {
        private readonly IUserRepository _userRepository = userRepository;

        public async Task<bool> DeleteUser(string email)
        {
            ValidatorService.Validate(email);

            bool userExists = await _userRepository.ExistsByEmailAsync(email);

            if(!userExists)
            {
                throw new UserNotFoundException($"User with email '{email}' was not found.");
            }

            return await _userRepository.DeleteUserAsync(email);
        }
    }
}
