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

        public BudgetItemModel GetBudgetItem(long id, long userId)
        {
            var budgetItem = _budgetItemRepository
                .GetAll().
                FirstOrDefault(item => item.Id == id && item.UserId == userId);

            return _mapper.Map<BudgetItemModel>(
                budgetItem ?? throw new ResourceNotFoundException<Guest>(id));
        }

        public void AddBudgetItem(BudgetItemModel budgetItem)
        {
            var newItem = _mapper.Map<BudgetItem>(budgetItem);
            _budgetItemRepository.Add(newItem);
        }

        public void UpdateBudgetItem(BudgetItemModel budgetItem)
        {
            _budgetItemRepository.Update(
                _mapper.Map<BudgetItem>(budgetItem));
        }

        public void DeleteBudgetItem(long id, long userId)
        {
            var item = GetBudgetItem(id, userId);
            _budgetItemRepository.Delete(
                _mapper.Map<BudgetItem>(
                    item ?? throw new ResourceNotFoundException<Guest>(id)));
        }
    }
}
