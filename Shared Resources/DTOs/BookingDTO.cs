using SharedResources.Models;

namespace Shared_Resources.DTOs
{
    public class BookingDTO
    {
        public int BookingId { get; set; } = 0;
        public int UserId { get; set; } = new User().UserId;
    }
}
