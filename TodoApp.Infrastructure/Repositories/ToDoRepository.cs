using TodoApp.Core.Entities;
using TodoApp.Core.Interfaces.IRepositories;
using TodoApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TodoApp.Infrastructure.Repositories
{
    public class ToDoRepository : GenericRepository<TODO>, ITODORepository
    {
        public ToDoRepository(TodoAppContext dbContext)
        {
            _dbContext = dbContext;
        }



        //ACA CUSTOM QUERIES Y COMMANDS
    }
}
