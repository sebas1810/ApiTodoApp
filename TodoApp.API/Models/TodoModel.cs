using TodoApp.Core.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoApp.API.Models
{
    public class TodoModel
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string FileUrl { get; set; } // ver esto code-maze.com/upload-files-dot-net-core-angular/#uploadfile
        public int StatusId { get; set; }
        public Status Status { get; set; }
        public IFormFile File { get; set; }
    }
}
