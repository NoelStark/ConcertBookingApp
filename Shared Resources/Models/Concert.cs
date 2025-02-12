﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;

namespace SharedResources.Models
{
    public partial class Concert : ObservableObject
    {
        public int ConcertId { get; set; } = 0;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public string Genre { get; set; } = string.Empty;

        [ObservableProperty] private bool isFavorite = false;
        public List<Performance> Performances { get; set; } = new List<Performance>();
        

    }
}
