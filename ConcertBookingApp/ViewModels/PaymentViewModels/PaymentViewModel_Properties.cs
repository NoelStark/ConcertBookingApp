using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ConcertBookingApp.Services;

namespace ConcertBookingApp.ViewModels.PaymentViewModels
{
    public partial class PaymentViewModel
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
        [ObservableProperty] private bool isValidCVC = false;
        [ObservableProperty] private bool isValidEmail = false;
        [ObservableProperty] private bool agreeToTerms = false;

        [ObservableProperty] private bool showDateError = false;
        [ObservableProperty] private bool showCVCError = false;
        [ObservableProperty] private bool showCreditError = false;

        [ObservableProperty] private string name = string.Empty;
        [ObservableProperty] private string creditCardNumber;
        [ObservableProperty] private string expireDate;
        [ObservableProperty] private string cardImage;
        [ObservableProperty] private string cVC;
        [ObservableProperty] private double totalCartCost;

        [ObservableProperty] private ValidationState creditValidation = new ValidationState();

        [ObservableProperty] private ValidationState dateValidation = new ValidationState();

        [ObservableProperty] private ValidationState securityValidation = new ValidationState();
        [ObservableProperty] private ValidationState emailValidation = new ValidationState();

        private readonly Color _red = Color.FromArgb("D22B2B");
        private readonly Color _grey = Color.FromArgb("C8C8C8");
        private bool _isCreditUpdating = false;
        private bool shouldSwitchSection = true;
        [ObservableProperty]
        private bool isValidForm = false;


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

        private readonly UserService _userService;
        public PaymentViewModel(UserService userService)
        {
            _userService = userService;
        }
    }
}
