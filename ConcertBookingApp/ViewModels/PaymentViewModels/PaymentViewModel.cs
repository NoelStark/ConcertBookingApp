﻿using System;
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

    [QueryProperty(nameof(TotalCartCost), "totalPrice")]
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

        /// <summary>
        /// The method that takes input and passes is to further methods
        /// to validate whether the input is valid or not
        /// </summary>
        /// <param name="key">What field is changes</param>
        /// <param name="value">The new value thats in the field</param>
        /// <param name="validationType">What Regex should be used</param>
        /// <param name="setterAction">An action that changes value of the field</param>
        private void ValidateAndSetValue(string key, string value, string validationType, Action<string> setterAction)
        {
            bool isValid =
                ValidationHelper.ValidateInput(value, validationType, _lastValidValues[key], out var validatedInput);
            //If the Validation returns to be valid information
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
            //A constant check if all fields are valid or not
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
            //Checks if the input is valid or not
            if (value.Length <= 3)
            {
                CVC = value;
                _lastValidValues["CVC"] = value;
                UpdateCvc(value.Length == 3);
            }
            else
                CVC = _lastValidValues["CVC"];
        }

        /// <summary>
        /// Updates the UI where border color updates
        /// </summary>
        /// <param name="valid">If the input is valid</param>
        /// <param name="fieldType">The field that should be updated</param>
        /// <param name="border">A specific border that should be updated</param>
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

        /// <summary>
        /// Based on what field it is, adjust the variables needing to be changed
        /// for UI and future methods to know if the input is valid or not
        /// </summary>
        /// <param name="fieldType">What type of field should be updated</param>
        /// <param name="isValid">Updates an observable property based on if field is valid</param>
        /// <param name="showError">If input is invalid, an error should shown</param>
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

        partial void OnAgreeToTermsChanged(bool value)
        {
            AgreeToTerms = value;
            ValidateForm();
        }


        private void UpdateCardType(string value)
        {
            string cardType = ValidationHelper.GetCardType(value);
            typeofCard = cardType;
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
                //IsValidForm = true;
            }
            else
            {
                shouldSwitchSection = false;
                IsValidForm = IsValidCard && IsValidDate && IsValidCVC && !string.IsNullOrEmpty(Name) && AgreeToTerms;
                //IsValidForm = true;

            }
            SavePersonCommand.NotifyCanExecuteChanged();
        }

     
    }
}
