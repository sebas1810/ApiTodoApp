using TodoApp.Core.Entities;
using TodoApp.Core.Interfaces.IRepositories;
using TodoApp.Core.Interfaces.IServices;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TodoApp.Core.Services
{
    public class TODOService : ITODOService
    {
        private readonly ITODORepository _toDoRepository;
        public TODOService(ITODORepository toDoRepository)
        {
            _toDoRepository = toDoRepository;
        }
        public async Task CreateAsync(TODO itemToDo)
        {
            await _toDoRepository.InsertAsync(itemToDo);
        }

        public async Task DeleteAsync(int? input = 0)
        {
            if (input > 0)
            {
                var itemToDo = await _toDoRepository.GetAsync(input.Value);
                await _toDoRepository.DeleteAsync(itemToDo);
            }
        }

        public async Task<List<TODO>> GetAllAsync(string filterBy, string value)
        {
            switch (filterBy)
            {
                case "Id":
                    int id = int.Parse(value);
                    return await _toDoRepository.Query(x => x.Id.Equals(id), (x => x.Status));
                case "Description":
                    return await _toDoRepository.Query(x => x.Description.Contains(value), (x => x.Status));
                case "Status":
                    int statuId = int.Parse(value);
                    return await _toDoRepository.Query(x => x.StatusId.Equals(statuId), (x => x.Status));

                default:
                    return await _toDoRepository.Query().Include(x => x.Status).ToListAsync();
            }
        }

        public async Task<TODO> GetByIdAsync(int id)
        {
            return await _toDoRepository.GetAsync(x => x.Id.Equals(id), x => x.Status);
        }

        public async Task UpdateAsync(TODO itemToDo)
        {
            await _toDoRepository.UpdateAsync(itemToDo);
        }

        public async Task UpdateStatusAsync(int id, int statusId)
        {
            var itemToDo = await _toDoRepository.GetAsync(x => x.Id.Equals(id));
            itemToDo.StatusId = statusId;

            await _toDoRepository.UpdateAsync(itemToDo);
        }

        public async Task<bool> ExistAsync(int? input = 0)
        {
            return await _toDoRepository.ExistAsync(input);
        }
    }
}
