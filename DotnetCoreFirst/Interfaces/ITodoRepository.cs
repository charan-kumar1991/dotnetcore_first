using DotnetCoreFirst.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DotnetCoreFirst.Interfaces
{
    public interface ITodoRepository
    {
        Task<IEnumerable<TodoItem>> GetAllAsync();
        Task<TodoItem> GetAsync(Guid id);
        Task AddAsync(TodoItem item);
        Task<bool> ExistsAsync(string title);
        Task UpdateAsync(Guid id, TodoItem item);
        Task DeleteAsync(Guid id);
    }
}
