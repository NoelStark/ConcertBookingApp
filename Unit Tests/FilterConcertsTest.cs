//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Reflection;
//using System.Text;
//using System.Threading.Tasks;
//using ConcertBookingApp.ViewModels.ConcertsOverviewViewModels;
//using SharedResources.Models;
//using Xunit.Abstractions;

//namespace Unit_Tests
//{
//    public class FilterConcertsTest
//    {
//        private List<Concert> MockConcerts => new List<Concert>
//        {
//            new Concert
//            {
//                Name = "Classical Festival",
//                Genre = "Classical",
//                Performances = new List<Performance>
//                {
//                    new Performance { Date = new DateTime(2023, 12, 15) },
//                    new Performance { Date = new DateTime(2024, 01, 10) }
//                }
//            },
//            new Concert
//            {
//                Name = "Jazz Night",
//                Genre = "Jazz",
//                Performances = new List<Performance>
//                {
//                    new Performance { Date = new DateTime(2024, 02, 05) }
//                }
//            },
//            new Concert
//            {
//                Name = "Pop Extravaganza",
//                Genre = "Pop",
//                Performances = new List<Performance>
//                {
//                    new Performance { Date = new DateTime(2024, 05, 20) }
//                }
//            }
//            ,
//            new Concert
//            {
//                Name = "EDM Extravaganza",
//                Genre = "EDM",
//                Performances = new List<Performance>
//                {
//                    new Performance { Date = new DateTime(2024, 05, 20) }
//                }
//            }
//        };

//        private List<Category> MockCategories => new List<Category>
//        {
//            new Category { Title = "Classical", IsSelected = false },
//            new Category { Title = "Jazz", IsSelected = false },
//            new Category { Title = "Pop", IsSelected = false },
//            new Category { Title = "EDM", IsSelected = false }
//        };
        

//        private List<Concert> AllConcerts = new List<Concert>()
//        {
//            new Concert
//            {
//                ConcertId = 1,
//                Description =
//                    "A high-energy event celebrating chart-topping hits and electrifying performances by popular pop artists.",
//                Genre = "Pop", ImageUrl = "edm.png", Name = "Pop Pulse Festival",
//                Performances = new List<Performance>()
//                {
//                    new Performance
//                    {
//                        TotalSeats = 5, AvailableSeats = 5, ConcertId = 1, Date = DateTime.Now,
//                        Location = "Aspvägen", Price = 100, PerformanceId = 1
//                    },
//                    new Performance
//                    {
//                        TotalSeats = 150, AvailableSeats = 150, ConcertId = 1, Date = DateTime.Now,
//                        Location = "Aspvägen", Price = 200, PerformanceId = 2
//                    },
//                    new Performance
//                    {
//                        TotalSeats = 200, AvailableSeats = 200, ConcertId = 1, Date = DateTime.Now,
//                        Location = "Aspvägen", Price = 300, PerformanceId = 3
//                    }
//                }
//            },
//            new Concert
//            {
//                ConcertId = 2,
//                Description =
//                    "A vibrant concert featuring a mix of iconic pop hits and fresh, emerging talent under dazzling lights.",
//                Genre = "Jazz", ImageUrl = "edm.png", Name = "Starlight Pop Jazz",
//                Performances = new List<Performance>()
//                {
//                    new Performance { AvailableSeats = 100, ConcertId = 2, Date = DateTime.Parse("2024-10-04") }
//                }
//            },
//            new Concert
//            {
//                ConcertId = 3,
//                Description =
//                    "A vibrant concert featuring a mix of iconic pop hits and fresh, emerging talent under dazzling lights.",
//                Genre = "Classical", ImageUrl = "edm.png", Name = "Classical",
//                Performances = new List<Performance>()
//                {
//                    new Performance { AvailableSeats = 100, ConcertId = 3, Date = DateTime.Parse("2025-09-22") }
//                }
//            }
//        };

//        //[Fact]
//        //public void FilterConcerts_ByText_ReturnsMatches()
//        //{
//        //    ConcertOverviewViewModel viewModel = new ConcertOverviewViewModel
//        //    {
//        //        Categories = MockCategories,
//        //        filteredConcerts = new List<Concert>(AllConcerts)
//        //    };

//        //    string searchText = "Classical";
//        //    var methodInfo = typeof(ConcertOverviewViewModel).GetMethod("FilterConcerts", BindingFlags.NonPublic | BindingFlags.Instance);

//        //    methodInfo.Invoke(viewModel, new object[] { searchText, viewModel.filteredConcerts });
//        //    Assert.Equal("Classical", viewModel.filteredConcerts.First().Name);

//        //}
//        //[Fact]
//        //public void FilterConcerts_ByDates_ReturnsMatches()
//        //{
//        //    MockCategorie
//        //    ConcertOverviewViewModel viewModel = new ConcertOverviewViewModel
//        //    {
//        //        Categories = MockCategories,
//        //        filteredConcerts = new List<Concert>(AllConcerts),

//        //    };
//        //    var startDatefieldInfo = typeof(ConcertOverviewViewModel).GetField("startDate", BindingFlags.NonPublic | BindingFlags.Instance);
//        //    var endDatefieldInfo = typeof(ConcertOverviewViewModel).GetField("endDate", BindingFlags.NonPublic | BindingFlags.Instance);
//        //    startDatefieldInfo.SetValue(viewModel, new DateTime(2024,1,1));
//        //    endDatefieldInfo.GetValue(viewModel);

//        //    var methodInfo = typeof(ConcertOverviewViewModel).GetMethod("FilterConcerts", BindingFlags.NonPublic | BindingFlags.Instance);

//        //    methodInfo.Invoke(viewModel, new object[] { null, null});

//        //    Assert.Equal("Pop", viewModel.filteredConcerts.First().Genre);

//        //}

//        //[Fact]
//        //public void FilterConcerts_ByCategory_ReturnsMatches()
//        //{
//        //    ConcertOverviewViewModel viewModel = new ConcertOverviewViewModel
//        //    {
//        //        Categories = MockCategories,
//        //        filteredConcerts = new List<Concert>(AllConcerts),
//        //    };
//        //    var methodInfo = typeof(ConcertOverviewViewModel).GetMethod("SelectedFilter", BindingFlags.NonPublic | BindingFlags.Instance);
//        //    methodInfo.Invoke(viewModel, new object?[] { MockCategories.FirstOrDefault(x => x.Title == "Pop") });
//        //    Assert.Equal("Pop", viewModel.filteredConcerts.First().Genre);

//        //}
//    }
//}
