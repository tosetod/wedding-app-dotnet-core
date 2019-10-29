using Models;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core
{
    public class SearchService
    {
        private readonly IGuestService _guestService;

        public SearchService(IGuestService guestService)
        {
            _guestService = guestService;
        }

        public IEnumerable<GuestModel> SearchGuestsByName(long userId, string searchPattern)
        {
            var guests = _guestService.GetAllUserGuests(userId);
            
            return guests.Where(guest => 
                guest.Name.ToLowerInvariant().Contains(
                    searchPattern.ToLowerInvariant()));
        }
    }
}
