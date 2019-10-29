using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DataAccess.Contracts;
using DataModels.Entities;
using Models;
using Services.Contracts;
using Services.Exceptions;

namespace Services.Implementations
{
    public class GuestService : IGuestService
    {
        private readonly IRepository<Guest> _guestRepository;
        private readonly IMapper _mapper;

        public GuestService(IRepository<Guest> guestRepository, IMapper mapper)
        {
            _guestRepository = guestRepository;
            _mapper = mapper;
        }

        public IEnumerable<GuestModel> GetAllUserGuests(long userId)
        {
            return _mapper.Map<IEnumerable<GuestModel>>(
                _guestRepository.GetAll().
                        Where(x => x.UserId == userId));
           
        }

        public async Task<GuestModel> GetGuest(long id, long userId)
        {
            var guest = await _guestRepository.GetById(id);

            if (guest.UserId != userId)
            {
                throw new ResourceNotFoundException<Guest>(id);
            }

            return _mapper.Map<GuestModel>(guest);
        }

        public async Task AddGuest(GuestModel guest)
        {
            var newGuest = _mapper.Map<Guest>(guest);
            await _guestRepository.Add(newGuest);
        }

        public async Task UpdateGuest(GuestModel guest)
        {
            await _guestRepository.Update(
                _mapper.Map<Guest>(guest));
        }

        public async Task DeleteGuest(long id, long userId)
        {
            var guest = await _guestRepository.GetById(id);
            if (guest.UserId != userId)
            {
                throw new ResourceNotFoundException<Guest>(id);
            }
            await _guestRepository.Delete(
                _mapper.Map<Guest>(guest));
        }
    }
}
