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

        [ObservableProperty] private string name;
        [ObservableProperty] private string creditCardNumber;
        [ObservableProperty] private string expireDate;
        [ObservableProperty] private string cardImage;
        [ObservableProperty] private string cVC;
        [ObservableProperty] private int totalCartCost;

        private static readonly Regex EmailRegex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        private static readonly Regex firstNameRegex = new Regex(@"^[a-z]+$", RegexOptions.IgnoreCase);
        private static readonly Regex lastNameRegex = new Regex(@"^[a-z]+$", RegexOptions.IgnoreCase);
        private string _lastValidFirstName = string.Empty;
        private string _lastValidLastName = string.Empty;
        private string _lastValidEmail = string.Empty;
        partial void OnFirstNameChanged(string value)
        {
            var validatedInput= ValidateInput(value, firstNameRegex, _lastValidFirstName);
            if (validatedInput != FirstName)
            {
                _lastValidFirstName = validatedInput;
                FirstName = _lastValidFirstName;
            }
            //ValidateForm();
        }

        partial void OnLastNameChanged(string value)
        {
            _lastValidLastName = ValidateInput(value, lastNameRegex, _lastValidLastName);
            LastName = _lastValidLastName;
            //ValidateForm();
        }

        partial void OnEmailChanged(string value)
        {
            _lastValidEmail = ValidateInput(value, EmailRegex, _lastValidEmail);
            Email = _lastValidEmail;
            //ValidateForm();
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

        }
        public PaymentViewModel()
        {
            
        }
    }
}
