using System;
using System.Collections.Generic;
using System.Text;
using Models;

namespace Services.Contracts
{
    public interface IGuestService
    {
        IEnumerable<GuestModel> GetAllUserGuests(long userId);
        GuestModel GetGuest(long id, long userId);
        void AddGuest(GuestModel guest);
        void UpdateGuest(GuestModel guest);
        void DeleteGuest(long id, long userId);
    }
}
