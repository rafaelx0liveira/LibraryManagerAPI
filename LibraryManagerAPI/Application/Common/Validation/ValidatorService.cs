using LibraryManagerAPI.Domain.Exceptions.ValidationFieldsExceptions;
using System.ComponentModel.DataAnnotations;

namespace LibraryManagerAPI.Application.Commom.Validation
{
    public static class ValidatorService
    {
        public static void Validate<T>(T obj)
        {
            var context = new ValidationContext(obj, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            bool isValid = Validator.TryValidateObject(obj, context, results, true);

            if(!isValid)
            {
                var errorMessage = new List<string>();

                foreach (var validationResult in results)
                {
                    errorMessage.Add(validationResult.ErrorMessage);
                }

                throw new CustomValidationFieldsException(errorMessage);
            }
        }
    }
}
