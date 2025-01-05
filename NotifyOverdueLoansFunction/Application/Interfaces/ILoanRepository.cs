using HandleOverdueLoansFunction.Domain.ValueObjects.Output;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandleOverdueLoansFunction.Application.Interfaces
{
    public interface ILoanRepository
    {
        Task<int> UpdateOverdueLoansAsync();
        Task<IEnumerable<OverdueLoansResultVO>> GetOverduesLoansAsync();
    }
}
