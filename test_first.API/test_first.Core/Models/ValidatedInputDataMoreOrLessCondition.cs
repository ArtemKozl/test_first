
namespace test_first.Core.Models
{
    public class ValidatedInputDataMoreOrLessCondition
    {
        public ValidatedInputDataMoreOrLessCondition( char conditionMoreOrLessValidated,
            DateTime startDateValidated, DateTime endDateValidated)
        {
            ConditionMoreOrLessValidated = conditionMoreOrLessValidated;
            StartDateValidated = startDateValidated;
            EndDateValidated = endDateValidated;
        }
        public char ConditionMoreOrLessValidated { get; set; }
        public DateTime StartDateValidated { get; set; }
        public DateTime EndDateValidated { get; set; }

        public static ValidatedInputDataMoreOrLessCondition Create(char conditionMoreOrLess,
            DateTime startDate, DateTime endDate)
        {
            var result = new ValidatedInputDataMoreOrLessCondition(conditionMoreOrLess,
                startDate, endDate);

            return result;
        }
    }
}
