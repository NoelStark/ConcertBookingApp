using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedResources.Models;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Text.RegularExpressions;
using ConcertBookingApp.Data.Database;
using ConcertBookingApp.Services;

namespace ConcertBookingApp.ViewModels.LoginViewModels
{
    public partial class LoginViewModel
    {
        private readonly UnitOfWork _unitOfWork;

        private readonly UserService _userService;

        [ObservableProperty]
        private string inputFullName = string.Empty;

        [ObservableProperty]
        private string inputEmail = string.Empty;

        [ObservableProperty]
        private string nameBorderColor = "#C8C8C8";

        [ObservableProperty]
        private string emailBorderColor = "#C8C8C8";

        [ObservableProperty]
        private bool canBelicked = false;

        [ObservableProperty]
        private bool fullNameIsOk = false;

        [ObservableProperty]
        private bool emailIsOk = false;
        [ObservableProperty]
        private bool isVisible = false;

        [ObservableProperty]
        private string lastValidName = string.Empty;

        /// <summary>
        /// Regex to that are used for to validate the users input, makes sure thet they are matching the regex
        /// </summary>

        Regex nameRegex = new Regex("^[a-zA-Z]+( ?[a-zA-Z]*)*$", RegexOptions.IgnoreCase);
        private static readonly Regex EmailRegex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
    }
}
