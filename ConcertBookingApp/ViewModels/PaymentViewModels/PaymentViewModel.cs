using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ConcertBookingApp.Helpers;
using Microsoft.Extensions.Primitives;

namespace ConcertBookingApp.ViewModels.PaymentViewModels
{
    public partial class ValidationState : ObservableObject
    {
        [ObservableProperty]
        public Color borderColor  = Color.FromArgb("C8C8C8");
        [ObservableProperty]
        public bool showError = false;
    }

    public partial class PaymentViewModel : ObservableObject
    {

        partial void OnNameChanged(string value)
        {
            ValidateAndSetValue("Name", value, "Name", x => Name = x);
        }

        partial void OnFirstNameChanged(string value)
        {
            ValidateAndSetValue("FirstName", value, "Name", x => FirstName = x);
        }

        partial void OnLastNameChanged(string value)
        {
            ValidateAndSetValue("LastName", value, "Name", x => LastName = x);
        }

        private void ValidateAndSetValue(string key, string value, string validationType, Action<string> setterAction)
        {
            bool isValid =
                ValidationHelper.ValidateInput(value, validationType, _lastValidValues[key], out var validatedInput);
            if (isValid)
            {
                _lastValidValues[key] = validatedInput;
                setterAction(validatedInput);
            }
            else
            {
                if(!string.IsNullOrEmpty(value))
                    setterAction(_lastValidValues[key]);
            }
            ValidateForm();

        }

        partial void OnEmailChanged(string value)
        {
            Email = value;
            bool isValid = ValidationHelper.ValidateInput(value, "Email", _lastValidValues["Email"], out string validatedInput);
            UpdateEmail(isValid);
        }

        partial void OnCVCChanged(string value)
        {
            if (value.Length <= 3)
            {
                CVC = value;
                _lastValidValues["CVC"] = value;
                UpdateCvc(value.Length == 3);
            }
            else
                CVC = _lastValidValues["CVC"];
        }

        private void UpdateFieldState(bool valid, string fieldType, ValidationState border)
        {
            if (valid)
            {
                border.BorderColor = _grey;
                SetFieldState(fieldType, true, false);
            }
            else
            {
                border.BorderColor = _red;
                SetFieldState(fieldType, false, true);
            }
            ValidateForm();
        }

        private void SetFieldState(string fieldType, bool isValid, bool showError)
        {
            switch (fieldType)
            {
                case "CVC":
                {
                    IsValidCVC = isValid;
                    ShowCVCError = showError;
                    if (!isValid) CVC = _lastValidValues["CVC"];
                    break;
                }
                case "Date":
                {
                    IsValidDate = isValid;
                    ShowDateError = showError;
                    if (!isValid) ExpireDate = _lastValidValues["Date"];
                    break;
                }
                case "CreditCard":
                {
                    IsValidCard = isValid;
                    ShowCreditError = showError;
                    if (!isValid) CreditCardNumber = _lastValidValues["CreditCard"];
                    break;
                }
                case "Email":
                {
                    IsValidEmail = isValid;
                    ShowErrorEmail = showError;
                    break;
                }
            }
        }

        private void UpdateEmail(bool valid)
        {
            UpdateFieldState(valid, "Email", EmailValidation);
        }
        private void UpdateCvc(bool valid)
        {
            UpdateFieldState(valid, "CVC", SecurityValidation);
        }
        private void UpdateDate(bool valid)
        {
            UpdateFieldState(valid, "Date", DateValidation);
        }
        private void UpdateCreditCard(bool valid)
        {
            UpdateFieldState(valid, "CreditCard", CreditValidation);
        }



        partial void OnCreditCardNumberChanged(string value)
        {
            if (_isCreditUpdating) return;
            _isCreditUpdating = true;
            string formatValue = value.Replace(" ", "");
            if (!(value.Length < _lastValidValues["CreditCard"].Length))
            {
                if (formatValue.Length >= 20 || IsValidCard && CardImage != "visa.png")
                {
                    CreditCardNumber = _lastValidValues["CreditCard"];
                    _isCreditUpdating = false;
                    return;
                }
            }

            string result = PaymentUtils.FormatCreditCardNumber(formatValue);
            CreditCardNumber = result;
            _lastValidValues["CreditCard"] = result;
            UpdateCardType(formatValue);
            _isCreditUpdating = false;
        }

        partial void OnExpireDateChanged(string value)
        {

            if (string.IsNullOrEmpty(value)) return;

            string parsedValue = value.Replace("/", "");
            DateTime currentDate = DateTime.Now;

            value = PaymentUtils.FormattedDate(value, _lastValidValues["Date"]);

            if (ValidationHelper.ValidateInput(parsedValue, "Expire", _lastValidValues["Date"], out string validatedValue) &&
                parsedValue.Length <= 4)
            {
                _lastValidValues["Date"] = value;

                bool isValid = ValidationHelper.IsValidExpire(value, currentDate, out DateTime parsedDate);
                UpdateDate(isValid);
            }
            else
            {
                ExpireDate = _lastValidValues["Date"];
            }
        }
       

        private void UpdateCardType(string value)
        {
            string cardType = ValidationHelper.GetCardType(value);

            if (!string.IsNullOrEmpty(cardType))
            {
                CardImage = cardType switch
                {
                    "Maestro" => "maestro.png",
                    "Mastercard" => "mastercard.png",
                    _ => "visa.png",
                };
                UpdateCreditCard(true);
            }
            else
            {
                UpdateCreditCard(false);
            }
        }

       

        private void ValidateForm()
        {
            if (shouldSwitchSection)
            {
                IsValidForm = IsValidEmail && !string.IsNullOrEmpty(FirstName) && !string.IsNullOrEmpty(LastName);
            }
            else
            {
                shouldSwitchSection = false;
                IsValidForm = IsValidCard && IsValidDate && IsValidCVC && !string.IsNullOrEmpty(Name) && AgreeToTerms;
            }
            SavePersonCommand.NotifyCanExecuteChanged();
        }

     
    }
}
