using Microsoft.Extensions.Logging;
using System.Text.Json;
using test_first.Core.Models;

namespace test_first.Infrastructure
{
    public class InputDataValidator : IInputDataValidator
    {
        private readonly ILogger<InputDataValidator> _logger;

        public InputDataValidator(ILogger<InputDataValidator> logger)
        {
            _logger = logger;
        }

        public ValidatedInputDataMoreOrLessCondition ValidateAllInputData(char condition, string startDate, string endDate)
        {
            var validatedData = ValidatedInputDataMoreOrLessCondition.Create(
                    ValidateInputData(condition),
                    ValidateInputData(startDate),
                    ValidateInputData(endDate)
                );

            _logger.LogInformation($"Валидирование прошло успешно: {JsonSerializer.Serialize(validatedData)}");

            return validatedData;
        }

        public ValidatedInputDataFirstdeliveryCondition ValidateAllInputData(string districtDate, string Date)
        {
            var validatedData = ValidatedInputDataFirstdeliveryCondition.Create(
                    ValidateString(districtDate),
                    ValidateInputData(Date)
                );

            _logger.LogInformation($"Валидирование прошло успешно: {JsonSerializer.Serialize(validatedData)}");

            return validatedData;
        }
        public DateTime ValidateInputData(string date)
        {
            if (DateTime.TryParse(date, out DateTime result) && !string.IsNullOrEmpty(date))
            {
                return result.ToUniversalTime();
            }
            else
            {
                _logger.LogError($"Не удалость валидировать переменную \"{date}\" как тип даты");

                throw new ArgumentException($"Некорректное значение для даты: {date}");
            }

        }

        public char ValidateInputData(char condition)
        {
            if (condition == '<' || condition == '>')
            {
                return condition;
            }
            else
            {
                _logger.LogError($"Не удалость валидировать переменную \"{condition}\" как условие '> || <'");

                throw new ArgumentException($"Некорректное значение для условия: {condition}");
            }
        }

        public string ValidateString(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                return name;
            }
            else
            {
                _logger.LogError($"Не удалость валидировать переменную \"{name}\", она пуста или равна null");

                throw new ArgumentException($"Некорректное значение для даты: {name}");
            }

        }
    }
}
