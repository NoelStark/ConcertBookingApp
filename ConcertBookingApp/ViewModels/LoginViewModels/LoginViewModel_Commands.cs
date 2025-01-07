using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedResources.Models;
using CommunityToolkit.Mvvm.Input;
using ConcertBookingApp.Views;

namespace ConcertBookingApp.ViewModels.LoginViewModels
{
    public partial class LoginViewModel
    {
        /// <summary>
        /// This method validates the user by checking if there already is a user in the database with the exact same name and email.
        /// Otherwise it will create the user with the given information from the input fields
        /// </summary>

        [RelayCommand]
        private async void ValidateUser()
        {
            if (InputFullName.EndsWith(" "))
                InputFullName = InputFullName.TrimEnd();

            User userExist = await _userService.DoesUserExist(InputFullName, InputEmail);
            if (userExist.Name != string.Empty && userExist.Email != string.Empty) //Checks the response if user exist or not
                _userService.CurrentUser = userExist; //Makes sure it is loged in as that user
            else
            {
                User newUser = new User //Creates the new user
                {
                    Name = InputFullName,
                    Email = InputEmail,
                };
                
                //Saves the new user to the database and ensures this user is loged in
                newUser.UserId = await _userService.SaveUser(newUser);
                _userService.CurrentUser = newUser;
            }

            //redirects to the application
            ((AppShell)Application.Current.MainPage).RemoveTab();
            await Shell.Current.GoToAsync("///ConcertOverviewPage");

        }
    }
}
