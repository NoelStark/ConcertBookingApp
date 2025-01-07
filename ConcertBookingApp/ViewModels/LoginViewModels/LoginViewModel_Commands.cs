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
        [RelayCommand]
        private async void ValidateUser()
        {
            if (InputFullName.EndsWith(" "))
                InputFullName = InputFullName.TrimEnd();



            User userExist = await _userService.DoesUserExist(InputFullName, InputEmail);
            if (userExist.Name != string.Empty && userExist.Email != string.Empty)
            {
                _userService.CurrentUser = userExist;
            }
            else
            {
                User newUser = new User
                {
                    Name = InputFullName,
                    Email = InputEmail,
                };
                
                newUser.UserId = await _userService.SaveUser(newUser); ;
                _userService.CurrentUser = newUser;
            }

            ((AppShell)Application.Current.MainPage).RemoveTab();
            await Shell.Current.GoToAsync("///ConcertOverviewPage");

        }
    }
}
