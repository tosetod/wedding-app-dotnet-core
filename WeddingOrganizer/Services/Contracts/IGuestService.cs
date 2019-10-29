using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace Services.Contracts
{
    public interface IGuestService
    {
        IEnumerable<GuestModel> GetAllUserGuests(long userId);
        Task<GuestModel> GetGuest(long id, long userId);
        Task AddGuest(GuestModel guest);
        Task UpdateGuest(GuestModel guest);
        Task DeleteGuest(long id, long userId);
    }
}
