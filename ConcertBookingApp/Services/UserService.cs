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
        public async Task SaveUser(User user)
        {
            try
            {
                var userDto = _mapper.Map<UserDTO>(user);
                var content = JsonSerializer.Serialize(userDto);
                var httpContent = new StringContent(content, Encoding.UTF8, "application/json");
                await _httpClient.PostAsync("User/SaveUser", httpContent);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return;
        }
    }
}
