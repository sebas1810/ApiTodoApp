using TodoApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TodoApp.Core.Interfaces.IServices
{
    public interface ITODOService
    {
        Task<TODO> GetByIdAsync(int id);
        Task<List<TODO>> GetAllAsync(string filterBy, string value);
        Task CreateAsync(TODO TODO);
        Task UpdateAsync(TODO TODO);
        Task UpdateStatusAsync(int id, int statusId);
        Task DeleteAsync(int? input = 0);
        Task<bool> ExistAsync(int? input = 0);
    }
}
