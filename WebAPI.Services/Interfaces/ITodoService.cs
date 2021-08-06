using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebAPI.Data.Entities;

namespace WebAPI.Services.Interfaces
{
    public interface ITodoService
    {
        Task<List<TodoTask>> GetAll(string username);
        Task<TodoTask> Get(string username, int id);
        Task<TodoTask> Create(string username, string text);
        Task<TodoTask> Delete(string username, int id);
        Task<TodoTask> Complete(string username, int id);
        Task<TodoTask> Update(string username, int id, string text);
    }
}
