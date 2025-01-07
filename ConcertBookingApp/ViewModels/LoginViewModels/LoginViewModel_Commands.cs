using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedResources.Models;
using CommunityToolkit.Mvvm.Input;

namespace ConcertBookingApp.ViewModels.LoginViewModels
{
    public partial class LoginViewModel
    {
        [RelayCommand]
        private async void ValidateUser()
        {
            if (InputFullName.EndsWith(" "))
                InputFullName = InputFullName.TrimEnd();

            User userExist = await _unitOfWork.User.FindUser(InputFullName, InputEmail);
            if (userExist != null)
                _userService.CurrentUser = userExist;
            else
            {
                _userService.CurrentUser = new User
                {
                    Name = InputFullName,
                    Email = InputEmail,
                };
            }

            await Shell.Current.GoToAsync("///ConcertOverviewPage");
        }
    }
}
