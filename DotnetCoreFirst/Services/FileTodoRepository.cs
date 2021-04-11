using DotnetCoreFirst.Interfaces;
using DotnetCoreFirst.Models;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DotnetCoreFirst.Services
{
    public class FileTodoRepository : ITodoRepository
    {

        private string path = default;

        public FileTodoRepository(IWebHostEnvironment hostingEnvironment)
        {
            path = Path.Join(hostingEnvironment.ContentRootPath, "Assets", "Todos.json");
        }

        public async Task AddAsync(TodoItem item)
        {
            string fileContents = await File.ReadAllTextAsync(path);
            List<TodoItem> todos = JsonConvert.DeserializeObject<List<TodoItem>>(fileContents);
            todos.Add(item);

            string updatedFileContents = JsonConvert.SerializeObject(todos);
            await File.WriteAllTextAsync(path, updatedFileContents);
        }

        public async Task DeleteAsync(Guid id)
        {
            string fileContents = await File.ReadAllTextAsync(path);
            List<TodoItem> todos = JsonConvert.DeserializeObject<List<TodoItem>>(fileContents);
            todos.RemoveAll(todoItem => todoItem.Id == id);

            string updatedFileContents = JsonConvert.SerializeObject(todos);
            await File.WriteAllTextAsync(path, updatedFileContents);
        }

        public async Task<bool> ExistsAsync(string title)
        {
            string fileContents = await File.ReadAllTextAsync(path);
            List<TodoItem> todos = JsonConvert.DeserializeObject<List<TodoItem>>(fileContents);
            return todos.Any(todoItem => todoItem.Title.Equals(title, StringComparison.OrdinalIgnoreCase));
        }

        public async Task<IEnumerable<TodoItem>> GetAllAsync()
        {
            string fileContents = await File.ReadAllTextAsync(path);
            return JsonConvert.DeserializeObject<List<TodoItem>>(fileContents);
        }

        public async Task<TodoItem> GetAsync(Guid id)
        {
            string fileContents = await File.ReadAllTextAsync(path);
            List<TodoItem> todos = JsonConvert.DeserializeObject<List<TodoItem>>(fileContents);
            return todos.Find(todoItem => todoItem.Id == id);
        }

        public async Task UpdateAsync(Guid id, TodoItem item)
        {
            string fileContents = await File.ReadAllTextAsync(path);
            List<TodoItem> todos = JsonConvert.DeserializeObject<List<TodoItem>>(fileContents);
            TodoItem foundItem = todos.Find(todoItem => todoItem.Id == id);
            
            foundItem.Id = id;
            foundItem.Title = item.Title;
            foundItem.Completed = item.Completed;

            string updatedFileContents = JsonConvert.SerializeObject(todos);
            await File.WriteAllTextAsync(path, updatedFileContents);
        }
    }
}
