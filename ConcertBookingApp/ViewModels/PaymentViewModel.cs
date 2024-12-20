using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace ConcertBookingApp.ViewModels
{
    public class ValidationState
    {
        public Color BorderColor { get; set; } = Color.FromArgb("C8C8C8");
        public bool ShowError { get; set; } = false;
    }
    public partial class PaymentViewModel : ObservableObject
    {
        [ObservableProperty] private bool isVisible = true;
        //Personal Information
        [ObservableProperty] private string firstName;
        [ObservableProperty] private string lastName;
        [ObservableProperty] private string email;
        [ObservableProperty] private bool showErrorEmail = false;

        //Payment Information
        [ObservableProperty] private bool isValidCard = false;
        [ObservableProperty] private bool agreeToTerms = false;

        [ObservableProperty] private bool showDateError = false;
        [ObservableProperty] private bool showCVCError = false;
        [ObservableProperty] private bool showCreditError = false;

        [ObservableProperty] private string name = string.Empty;
        [ObservableProperty] private string creditCardNumber;
        [ObservableProperty] private string expireDate;
        [ObservableProperty] private string cardImage;
        [ObservableProperty] private string cVC;
        [ObservableProperty] private int totalCartCost;

        [ObservableProperty]
        private ValidationState creditValidation = new ValidationState();

        [ObservableProperty]
        private ValidationState dateValidation = new ValidationState();

        [ObservableProperty]
        private ValidationState securityValidation = new ValidationState();

        private readonly Color _red = Color.FromArgb("D22B2B");
        private readonly Color _grey = Color.FromArgb("C8C8C8");
        private bool _isCreditUpdating = false;

        private readonly Dictionary<string, Regex> _regexPatterns = new Dictionary<string, Regex>
        {
            { "Email", new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$") },
            { "Name", new Regex(@"^[a-z]+$", RegexOptions.IgnoreCase) },
            { "Maestro", new Regex(@"^(50|5[6-9]|6[0-9])\d{10,17}$") },
            { "Mastercard", new Regex(@"^(5[1-5])\d{14}$") },
            { "Visa", new Regex(@"^(4)\d{11,17}$") },
        };

        private readonly Dictionary<string, string> _lastValidValues = new Dictionary<string, string>
        {
            { "FirstName", string.Empty },
            { "LastName", string.Empty },
            { "Email", string.Empty },
            { "Name", string.Empty }
        };
        partial void OnNameChanged(string value)
        {
            var validatedInput = ValidateInput(value, _regexPatterns["Name"], _lastValidValues["Name"]);
            if (validatedInput != Name)
            {
                _lastValidValues["Name"] = validatedInput;
                Name = _lastValidValues["Name"];
            }
        }

        partial void OnFirstNameChanged(string value)
        {
            var validatedInput= ValidateInput(value, _regexPatterns["Name"], _lastValidValues["FirstName"]);
            if (validatedInput != FirstName)
            {
                _lastValidValues["FirstName"] = validatedInput;
                FirstName = _lastValidValues["FirstName"];
            }
            //ValidateForm();
        }

        partial void OnLastNameChanged(string value)
        {
            _lastValidValues["LastName"] = ValidateInput(value, _regexPatterns["Name"], _lastValidValues["LastName"]);
            LastName = _lastValidValues["LastName"];
            //ValidateForm();
        }

        partial void OnEmailChanged(string value)
        {
            _lastValidValues["Email"] = ValidateInput(value, _regexPatterns["Email"], _lastValidValues["Email"]);
            Email = _lastValidValues["Email"];
            //ValidateForm();
        }


        partial void OnCreditCardNumberChanged(string value)
        {
            if (!_isCreditUpdating)
            {
                _isCreditUpdating = true;
                value = value.Replace(" ", "");
                if (value.Length % 4 == 0)
                    CreditCardNumber += " ";
                string type = GetCardType(value);
                if (!string.IsNullOrEmpty(type))
                {
                    if (type == "Maestro")
                        CardImage = "maestro.png";
                    else if (type == "Mastercard")
                        CardImage = "mastercard.png";
                    else
                        CardImage = "visa.png";
                    IsValidCard = true;
                    ShowCreditError = false;
                    CreditValidation.BorderColor = _grey;
                }
                else
                {
                    CreditValidation.BorderColor = _red;
                    IsValidCard = false;
                    ShowCreditError = true;
                }
                OnPropertyChanged(nameof(CreditCardNumber));
                OnPropertyChanged(nameof(CreditValidation));
            }

            _isCreditUpdating = false;
        }


        private string ValidateInput(string input, Regex regex, string lastValidValue)
        {
            if (regex.IsMatch(input.Replace(" ", "")))
            {
                if (!input.EndsWith(" ") && !lastValidValue.EndsWith(" "))
                {
                    return input;
                }
            }

            return lastValidValue;
        }

        [RelayCommand]
        void Edit()
        {
            IsVisible = !IsVisible;
        }

        [RelayCommand]
        void GoBack()
        {

        }
        [RelayCommand]
        void SavePerson()
        {
            IsVisible = !IsVisible;
        }
        private string GetCardType(string value)
        {
            var formatString = value.Replace(" ", "");
            return _regexPatterns.FirstOrDefault(x => x.Key != "Email" && x.Key != "Name" && x.Value.IsMatch(formatString)).Key ?? string.Empty;
           
        }
    }
}
