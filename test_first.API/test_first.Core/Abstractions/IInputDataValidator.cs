using test_first.Core.Models;

namespace test_first.Infrastructure
{
    public interface IInputDataValidator
    {
        ValidatedInputDataMoreOrLessCondition ValidateAllInputData(char condition, string startDate, string endDate);
        ValidatedInputDataFirstdeliveryCondition ValidateAllInputData(string districtDate, string Date);
        char ValidateInputData(char condition);
        DateTime ValidateInputData(string date);
        string ValidateString(string name);
    }
}