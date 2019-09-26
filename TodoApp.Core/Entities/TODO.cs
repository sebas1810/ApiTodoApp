using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Text;

namespace TodoApp.Core.Entities
{
    public class TODO
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string FileUrl { get; set; }
        public int StatusId { get; set; }
        public Status Status { get; set; }
    }
}
