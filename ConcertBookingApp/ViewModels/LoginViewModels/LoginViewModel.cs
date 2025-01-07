using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ConcertBookingApp.Data.Database;
using ConcertBookingApp.Helpers;
using ConcertBookingApp.Services;
using SharedResources.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConcertBookingApp.ViewModels.LoginViewModels
{
    public partial class LoginViewModel : ObservableObject
    {
        public LoginViewModel( UserService userservice)
        {
            _userService = userservice;
        }

        /// <summary>
        /// Validation for fullname, makes sure that the user inputs is of the correct information.
        /// Nessecary to make sure the database holds the correct information.
        /// Its a onpropertychange binded to InputFullName, which updates when the user enters the entry
        /// </summary>
        partial void OnInputFullNameChanged(string value)
        {
            if (!string.IsNullOrWhiteSpace(value) && nameRegex.IsMatch(value) &&!value.StartsWith(" ") &&value.Length <= 20 && !value.EndsWith("  "))
            {
                LastValidName = value;
                FullNameIsOk = true;
            }
            else if (string.IsNullOrEmpty(value))
            {
                LastValidName = string.Empty;
                FullNameIsOk = false;
            }
            else
                FullNameIsOk = false;

            InputFullName = LastValidName;

            UpdateFullNameBorder();
        }

        /// <summary>
        /// Validation for email, makes sure that the user inputs is of the correct information.
        /// Nessecary to make sure the database holds the correct information.
        /// Its a onpropertychange binded to InputEmail, which updates when the user enters the entry
        /// </summary>
        partial void OnInputEmailChanged(string value)
        {
            if (!string.IsNullOrWhiteSpace(value) && EmailRegex.IsMatch(value))
            {
                InputEmail = value;
                EmailIsOk = true;
            }
            else
                EmailIsOk = false;
            UpdateEmailBorder();
        }

        /// <summary>
        /// Updates the button if all the required fields are valid
        /// </summary>
        private void UpdateButton()
        {
            if (FullNameIsOk == true && EmailIsOk == true)
                CanBelicked = true;
            else
                CanBelicked = false;
        }

        /// <summary>
        /// Updates the border color for InputFullName depending if its valid or not
        /// </summary>
        private void UpdateFullNameBorder()
        {
            if (FullNameIsOk)
                NameBorderColor = "#C8C8C8";
            else
                NameBorderColor = "#D22B2B";
            UpdateButton();
        }

        /// <summary>
        /// Updates the border color for InputEmail depending if its valid or not
        /// </summary>
        private void UpdateEmailBorder()
        {
            if (EmailIsOk)
                EmailBorderColor = "#C8C8C8";
            else
                EmailBorderColor = "#D22B2B";
            UpdateButton();
        }
    }
}
