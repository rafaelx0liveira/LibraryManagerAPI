﻿using LibraryManagerAPI.Domain.ValueObjects.Input;
using LibraryManagerAPI.Domain.ValueObjects.Output;

namespace LibraryManagerAPI.Presentation.Interfaces.UseCases.Loan
{
    public interface IRegisterLoanUseCase
    {
        Task<LoanResultVO> RegisterLoan(LoanVO loan);
    }
}