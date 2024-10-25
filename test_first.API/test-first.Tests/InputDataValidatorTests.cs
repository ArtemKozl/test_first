using Microsoft.Extensions.Logging;
using Moq;
using test_first.Infrastructure;
using Xunit;

namespace test_first.Tests
{
    public class InputDataValidatorTests
    {
        private readonly InputDataValidator _validator;
        private readonly Mock<ILogger<InputDataValidator>> _loggerMock;

        public InputDataValidatorTests()
        {
            _loggerMock = new Mock<ILogger<InputDataValidator>>();
            _validator = new InputDataValidator(_loggerMock.Object);
        }

        [Fact]
        public void ValidateInputData_DateString_ReturnsUTCDateTime()
        {
            string dateString = "2024-10-22T17:30:45";
            DateTime expectedDate = DateTime.Parse(dateString).ToUniversalTime();

            var result = _validator.ValidateInputData(dateString);

            Assert.Equal(expectedDate, result);
        }

        [Fact]
        public void ValidateInputData_DateString_ThrowsArgumentException()
        {
            string invalidDateString = "invalid-date";

            Assert.Throws<ArgumentException>(() => _validator.ValidateInputData(invalidDateString));
        }

        [Fact]
        public void ValidateInputData_CharCondition_ReturnsValidChar()
        {
            char condition = '<';

            var result = _validator.ValidateInputData(condition);

            Assert.Equal(condition, result);
        }

        [Fact]
        public void ValidateInputData_CharCondition_ThrowsArgumentException()
        {
            char invalidCondition = '!';

            Assert.Throws<ArgumentException>(() => _validator.ValidateInputData(invalidCondition));
        }

        [Fact]
        public void ValidateString_ValidString_ReturnsSameString()
        {
            string validString = "Test";

            var result = _validator.ValidateString(validString);

            Assert.Equal(validString, result);
        }

        [Fact]
        public void ValidateString_EmptyString_ThrowsArgumentException()
        {
            string emptyString = "";

            Assert.Throws<ArgumentException>(() => _validator.ValidateString(emptyString));
        }
    }
}
