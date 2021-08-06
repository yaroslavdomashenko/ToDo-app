using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.Data;
using WebAPI.Data.Entities;
using WebAPI.Services.Interfaces;

namespace WebAPI.Services.Services
{
    public class TodoService : ITodoService
    {
        private readonly ApplicationDbContext _context;
        public TodoService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<TodoTask> Get(string username, int id)
        {
            var user = await _context.Accounts.FirstOrDefaultAsync(obj => obj.Username.Equals(username.ToLower()));
            var task = await _context.TodoTasks.FindAsync(id);

            if (task != null && task.AccountId != user.Id )
                return null;

            return task;
        }

        public async Task<List<TodoTask>> GetAll(string username)
        {
            var user = await _context.Accounts.FirstOrDefaultAsync(obj => obj.Username.Equals(username.ToLower()));

            var list = await _context.TodoTasks.Where(obj => obj.Account.Id == user.Id).OrderBy(o=> o.Status).ToListAsync();
            //var list = await _context.TodoTasks.Include(c => c.Account).Where(obj => obj.Account.Id == user.Id).ToListAsync();

            return list;
        }

        public async Task<TodoTask> Complete(string username, int id)
        {
            var user = await _context.Accounts.FirstOrDefaultAsync(obj => obj.Username.Equals(username.ToLower()));
            var task = await _context.TodoTasks.FindAsync(id);

            if (task != null && task.AccountId != user.Id)
                return null;

            try
            {
                task.Status = true;
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return null;
            }

            return task;
        }

        public async Task<TodoTask> Create(string username, string text)
        {
            var user = await _context.Accounts.FirstOrDefaultAsync(obj => obj.Username.ToLower().Equals(username.ToLower()));
            if (user == null)
                return null;

            TodoTask task = new TodoTask
            {
                Text = text,
                Time = DateTime.Now.ToString("dd/MM/yyyy HH:mm"),
                Account = user,
            };

            try
            {
                _context.TodoTasks.Add(task);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return null;
            }

            return task;
        }

        public async Task<TodoTask> Delete(string username, int id)
        {
            var user = await _context.Accounts.FirstOrDefaultAsync(obj => obj.Username.ToLower().Equals(username.ToLower()));
            var task = await _context.TodoTasks.FindAsync(id);

            if (user == null || task == null)
                return null;
            if (task.AccountId != user.Id)
                return null;

            try
            {
                _context.TodoTasks.Remove(task);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return null;
            }

            return task;
        }

        public async Task<TodoTask> Update(string username, int id, string text)
        {
            var user = await _context.Accounts.FirstOrDefaultAsync(obj => obj.Username.ToLower().Equals(username.ToLower()));
            var task = await _context.TodoTasks.FindAsync(id);

            if (user == null || task == null)
                return null;
            if (task.AccountId != user.Id)
                return null;

            try
            {
                task.Text = text;
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return null;
            }

            return task;
        }
    }
}
