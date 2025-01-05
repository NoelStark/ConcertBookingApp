using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;

namespace SharedResources.Models
{
    public partial class Category : ObservableObject
    {
        [Key]
        public int CategoryId { get; set; }
        public string ImageSource { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;

        [ObservableProperty] private bool isSelected = false;
    }
}
