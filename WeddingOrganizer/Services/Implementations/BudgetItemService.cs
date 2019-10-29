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
    public class BudgetItemService : IBudgetItemService
    {
        private readonly IRepository<BudgetItem> _budgetItemRepository;
        private readonly IMapper _mapper;

        public BudgetItemService(IRepository<BudgetItem> budgetItemRepository, IMapper mapper)
        {
            _budgetItemRepository = budgetItemRepository;
            _mapper = mapper;
        }
        public IEnumerable<BudgetItemModel> GetAllUserItems(long userId)
        {
            return _mapper.Map<IEnumerable<BudgetItemModel>>(
                _budgetItemRepository.GetAll().
                    Where(x => x.UserId == userId));
        }

        public async Task<BudgetItemModel> GetBudgetItem(long id, long userId)
        {
            var budgetItem = await _budgetItemRepository.GetById(id);

            if (budgetItem.UserId != userId)
            {
                throw new ResourceNotFoundException<Guest>(id);
            }

            return _mapper.Map<BudgetItemModel>(budgetItem);
        }

        public async Task AddBudgetItem(BudgetItemModel budgetItem)
        {
            var newItem = _mapper.Map<BudgetItem>(budgetItem);
            await _budgetItemRepository.Add(newItem);
        }

        public async Task UpdateBudgetItem(BudgetItemModel budgetItem)
        {
            await _budgetItemRepository.Update(
                _mapper.Map<BudgetItem>(budgetItem));
        }

        public async Task DeleteBudgetItem(long id, long userId)
        {
            var item = await _budgetItemRepository.GetById(id);
            if (item.UserId != userId)
            {
                throw new ResourceNotFoundException<BudgetItem>(id);
            }
            await _budgetItemRepository.Delete(item);
        }
    }
}
