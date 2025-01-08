using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using Shared_Resources.DTOs;
using SharedResources.Models;

namespace ConcertBookingApp.Services
{
    public class UserService
    {

        private readonly HttpClient _httpClient;
        private readonly IMapper _mapper;
        public UserService(HttpClient httpClient, IMapper mapper) 
        { 
            _httpClient = httpClient;
            _mapper = mapper;
        }
        public User? CurrentUser { get; set; } = null;

        /// <summary>
        /// Checks if a user exists by querying the API with their full name and email that they have given 
        /// Sends an HTTP GET request to retrieve user information and maps it to a User object
        /// If user is not found it will create a new user, otherwise it will return that found user
        /// </summary>
        
        public async Task<User> DoesUserExist(string inputFullName, string inputEmail)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"User/FindUser/{inputFullName}/{inputEmail}");
                Console.WriteLine(response);
                UserDTO userDTO = await response.Content.ReadFromJsonAsync<UserDTO>();
                User user = _mapper.Map<User>(userDTO);
                return user;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return new User();
        }

        /// <summary>
        /// Saves a user by sending their data to the api and then maps a user object to a UserDTO and
        /// then serializes it to a json format wich will later be posts to the API.
        /// If the save is successful it returns the new user's id as an integer, if not
        /// it returns 0.
        /// </summary>
        public async Task<int> SaveUser(User user)
        {
            try
            {
                var userDto = _mapper.Map<UserDTO>(user);
                var content = JsonSerializer.Serialize(userDto);
                var httpContent = new StringContent(content, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync("User/SaveUser", httpContent);
                var userId = await response.Content.ReadAsStringAsync();
                return int.Parse(userId);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return 0;
        }
    }
}
