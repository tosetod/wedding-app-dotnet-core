using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public GuestModel GetGuest(long id, long userId)
        {
            var guest = _guestRepository
                .GetAll()
                .FirstOrDefault(item => item.Id == id && item.UserId == userId);

            return _mapper.Map<GuestModel>(guest ?? throw new ResourceNotFoundException<Guest>(id));
        }

        public void AddGuest(GuestModel guest)
        {
            var newGuest = _mapper.Map<Guest>(guest);
            _guestRepository.Add(newGuest);
        }

        public void UpdateGuest(GuestModel guest)
        {
            _guestRepository.Update(
                _mapper.Map<Guest>(guest));
        }

        public void DeleteGuest(long id, long userId)
        {
            var guest = GetGuest(id, userId);
            _guestRepository.Delete(
                _mapper.Map<Guest>(guest ?? throw new ResourceNotFoundException<Guest>(id)));
        }
    }
}
