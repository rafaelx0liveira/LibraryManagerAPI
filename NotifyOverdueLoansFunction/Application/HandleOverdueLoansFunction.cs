using HandleOverdueLoansFunction.Application.Interfaces;
using HandleOverdueLoansFunction.Domain.ValueObjects.Output;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace HandleOverdueLoansFunction.Application
{
    public class HandleOverdueLoansFunction
    {
        private readonly ILogger _logger;
        private readonly ILoanRepository _loanRepository;
        private readonly IEmailService _emailService;

        public HandleOverdueLoansFunction(
                ILoggerFactory loggerFactory,
                ILoanRepository loanRepository,
                IEmailService emailService
            )
        {
            _logger = loggerFactory.CreateLogger<HandleOverdueLoansFunction>();
            _loanRepository = loanRepository;
            _emailService = emailService;
        }

        [Function("NotifyOverdueLoansFunction")]
        public async Task RunAsync([TimerTrigger("*/1 * * * * *")] TimerInfo myTimer)
        {
            _logger.LogInformation($"Azure Function executed at: {DateTime.Now}");

            // 1. Update overdues loans
            int updatedLoans = await _loanRepository.UpdateOverdueLoansAsync();
            _logger.LogInformation($"Updated {updatedLoans} loans to 'Overdue' status.");

            // 2. Recover overdue loans and user information
            IEnumerable<OverdueLoansResultVO> overdueLoans = await _loanRepository.GetOverduesLoansAsync();

            foreach (var loan in overdueLoans)
            {
                string projectPath = Path.Combine(AppContext.BaseDirectory, "Infrastructure/Templates/OverdueLoanNotification.html");
                string normalizedPath = Path.GetFullPath(projectPath);

                // Create placeholders dictionary
                Dictionary<string, string> placeholders = new Dictionary<string, string>
                {
                    { "UserName", loan.UserName },
                    { "BookTitle", loan.BookTitle },
                    { "ReturnDate", loan.ReturnDate }
                };

                // 3. Send e-mails using placeholders
                await _emailService.SendEmailAsync(
                    loan.UserEmail,
                    "Empréstimo Atrasado",
                    normalizedPath,
                    placeholders
                );

                _logger.LogInformation($"Notification email sent to: {loan.UserEmail}");
            }
        }
    }
}
