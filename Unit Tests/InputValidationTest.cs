using ConcertBookingApp.ViewModels;

namespace Unit_Tests
{
    public class InputValidationTest
    {
        [Theory]
        [InlineData("A", "A")]
        [InlineData("2", "")]
        [InlineData("John Doe", "John Doe")]
        public void FirstName_InputControl(string input, string expected)
        {
            PaymentViewModel _paymentViewModel = new PaymentViewModel();
            _paymentViewModel.FirstName = input;
            Assert.Equal(expected, _paymentViewModel.FirstName);
        }

        [Theory]
        [InlineData("A", "A")]
        [InlineData("2", "")]
        [InlineData("John Doe", "John Doe")]
        public void LastName_InputControl(string input, string expected)
        {
            PaymentViewModel _paymentViewModel = new PaymentViewModel();
            _paymentViewModel.LastName = input;
            Assert.Equal(expected, _paymentViewModel.LastName);
        }

        [Theory]
        [InlineData("A", "")]
        [InlineData("2", "")]
        [InlineData("Johndoe@gmail", "")]
        [InlineData("johndoe@gmail.com", "johndoe@gmail.com")]
        public void Email_InputControl(string input, string expected)
        {
            PaymentViewModel _paymentViewModel = new PaymentViewModel();
            
            _paymentViewModel.Email = input;
            Assert.Equal(expected, _paymentViewModel.Email);
        }

        [Theory]
        [InlineData("A", "A")]
        [InlineData("2", "")]
        [InlineData("John Doe", "John Doe")]
        public void PaymentName_InputControl(string input, string expected)
        {
            PaymentViewModel _paymentViewModel = new PaymentViewModel();

            _paymentViewModel.Name = input;
            Assert.Equal(expected, _paymentViewModel.Name);
        }
    }
}