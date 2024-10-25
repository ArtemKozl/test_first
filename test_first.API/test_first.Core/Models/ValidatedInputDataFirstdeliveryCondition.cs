
namespace test_first.Core.Models
{
    public class ValidatedInputDataFirstdeliveryCondition
    {
        public ValidatedInputDataFirstdeliveryCondition(string distrctName, DateTime dateValidated) 
        {
            DistrictName = distrctName;
            DateValidated = dateValidated;
        }

        public string DistrictName { get; set; }
        public DateTime DateValidated { get; set; }

        public static ValidatedInputDataFirstdeliveryCondition Create(string distrctName, DateTime dateValidated)
        {
            var result = new ValidatedInputDataFirstdeliveryCondition(distrctName, dateValidated);

            return result;
        }

    }
}
