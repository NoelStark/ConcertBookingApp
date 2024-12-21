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
        [ObservableProperty] private bool isValidDate = false;
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

        [ObservableProperty] private ValidationState creditValidation = new ValidationState();

        [ObservableProperty] private ValidationState dateValidation = new ValidationState();

        [ObservableProperty] private ValidationState securityValidation = new ValidationState();

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
            { "Expire", new Regex(@"^[0-9]\d{0,3}$")}
        };

        private readonly Dictionary<string, string> _lastValidValues = new Dictionary<string, string>
        {
            { "FirstName", string.Empty },
            { "LastName", string.Empty },
            { "Email", string.Empty },
            { "Name", string.Empty },
            { "CreditCard", string.Empty},
            { "CVC", string.Empty},
            { "Date", string.Empty},
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
            var validatedInput = ValidateInput(value, _regexPatterns["Name"], _lastValidValues["FirstName"]);
            if (validatedInput != FirstName)
            {
                _lastValidValues["FirstName"] = validatedInput;
                FirstName = _lastValidValues["FirstName"];
            }
        }

        partial void OnLastNameChanged(string value)
        {
            _lastValidValues["LastName"] = ValidateInput(value, _regexPatterns["Name"], _lastValidValues["LastName"]);
            LastName = _lastValidValues["LastName"];
        }

        partial void OnEmailChanged(string value)
        {
            _lastValidValues["Email"] = ValidateInput(value, _regexPatterns["Email"], _lastValidValues["Email"]);
            Email = _lastValidValues["Email"];
        }

        partial void OnCVCChanged(string value)
        {
            if (value.Length <= 3)
            {
                CVC = value;
                _lastValidValues["CVC"] = value;
            }
            else
                CVC = _lastValidValues["CVC"];
        }


        partial void OnCreditCardNumberChanged(string value)
        {
            if (_isCreditUpdating) return;
            _isCreditUpdating = true;
            string formatValue = value.Replace(" ", "");
            if (formatValue.Length >= 20)
            {
                CreditCardNumber = _lastValidValues["CreditCard"];
                _isCreditUpdating = false;
                return;
            }

            string result = FormatCreditCardNumber(formatValue);
            CreditCardNumber = result;
            _lastValidValues["CreditCard"] = result;
            UpdateCardType(formatValue);
            _isCreditUpdating = false;
        }

        partial void OnExpireDateChanged(string value)
        {

            if (string.IsNullOrEmpty(value)) return;

            string parsedValue = value.Replace("/", "");
            string dateToday = DateTime.Now.ToString("MM/YY");
            DateTime currentDate = DateTime.ParseExact(dateToday, "MM/YY", null);

            value = FormattedDate(value);
            if (_regexPatterns["Expire"].IsMatch(parsedValue) && parsedValue.Length <= 4)
            {
                _lastValidValues["Date"] = value;
                if (IsValidExpire(value, currentDate, out DateTime parsedDate))
                {
                    UpdateValidDate(value);
                }
                else
                    UpdateInvalidDate();

            }
            else
                ExpireDate = _lastValidValues["Date"];
        }

        private string FormattedDate(string value)
        {
            int index = 2;

            if (value.Length < _lastValidValues["Date"].Length && value.EndsWith("/"))
            {
                value = value.Substring(0, value.Length - 1);
            }
            else if (value.Length > _lastValidValues["Date"].Length && !value.Contains("/"))
            {
                if (value.Length == 2)
                    value += "/";
                else if (value.Length == 3)
                {
                    value = value.Substring(0, index) + "/" + value.Substring(index);
                }

            }

            return value;

        }

        private bool IsValidExpire(string value, DateTime currentDate, out DateTime parsedDate)
        {
            parsedDate = DateTime.MinValue;
            return value.Length == 5 &&
                   DateTime.TryParseExact(value, "MM/yy", null, System.Globalization.DateTimeStyles.None,
                       out parsedDate) &&
                   parsedDate >= currentDate &&
                   parsedDate <= currentDate.AddYears(10); 
        }

        private void UpdateInvalidDate()
        {
            IsValidDate = false;
            DateValidation.BorderColor = _red;
            ShowDateError = true;
            ExpireDate = _lastValidValues["Date"];
        }

        private void UpdateValidDate(string value)
        {
            IsValidDate = true;
            DateValidation.BorderColor = _grey;
            ShowDateError = false;
            ExpireDate = value;

        }

        private string FormatCreditCardNumber(string value)
        {
            StringBuilder builder = new StringBuilder();

            for (int i = 0; i < value.Length; i++)
            {
                if (i > 0 && i % 4 == 0)
                    builder.Append(' ');
                builder.Append(value[i]);
            }
            return builder.ToString();
        }

        private void UpdateCardType(string value)
        {
            string cardType = GetCardType(value);

            if (!string.IsNullOrEmpty(cardType))
            {
                CardImage = cardType switch
                {
                    "Maestro" => "maestro.png",
                    "Mastercard" => "mastercard.png",
                    _ => "visa.png",
                };
                IsValidCard = true;
                ShowCreditError = false;
                CreditValidation.BorderColor = _grey;
            }
            else
            {
                IsValidCard = false;
                ShowCreditError = true;
                CreditValidation.BorderColor = _red;
            }
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
            ValidateForm();

            return lastValidValue;
        }

        private void ValidateForm()
        {
            var t = IsValidCard && IsValidDate && !string.IsNullOrEmpty(Name) && !string.IsNullOrEmpty(FirstName) && !string.IsNullOrEmpty(LastName) &&
                    !string.IsNullOrEmpty(Email);
            Console.WriteLine();
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
