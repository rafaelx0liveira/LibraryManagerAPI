using AutoMapper;
using Microsoft.EntityFrameworkCore;
using HandleOverdueLoansFunction.Application.Interfaces;
using HandleOverdueLoansFunction.Domain.Entities.Utils;
using HandleOverdueLoansFunction.Domain.ValueObjects.Output;
using HandleOverdueLoansFunction.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandleOverdueLoansFunction.Persistance.Repository
{
    public class LoanRepository(MySQLContext context) :
        ILoanRepository 
    {
        private readonly MySQLContext _context = context;
        public async Task<int> UpdateOverdueLoansAsync()
        {
            var overdueLoans = _context.Loans
                .Where(l => l.ReturnDate < DateTime.Now && l.Status == LoanStatus.Active)
                .ToList();

           overdueLoans.ForEach(l => l.Status = LoanStatus.Overdue);

            return await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<OverdueLoansResultVO>> GetOverduesLoansAsync()
        {
            IEnumerable<OverdueLoansResultVO> loanResults = _context.Loans
                .Include(ln => ln.User) 
                .Include(ln => ln.LoanBooks) 
                    .ThenInclude(lb => lb.Book) 
                .Where(ln => ln.Status == LoanStatus.Overdue)
                .Select(ln => new OverdueLoansResultVO(
                   ln.User.Email,
                   ln.User.Name,
                   ln.LoanBooks.FirstOrDefault().Book.Title,
                   ln.ReturnDate.ToString("dd/MM/yyyy")
                ))
                .ToList();

            return loanResults;
        }
    }
}
