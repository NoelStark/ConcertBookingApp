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

        private void UpdateButton()
        {
            if (FullNameIsOk == true && EmailIsOk == true)
                CanBelicked = true;
            else
                CanBelicked = false;
        }

        private void UpdateFullNameBorder()
        {
            if (FullNameIsOk)
                NameBorderColor = "#C8C8C8";
            else
                NameBorderColor = "#D22B2B";
            UpdateButton();
        }

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
