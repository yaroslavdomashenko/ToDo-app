using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WebAPI.Data.DTO
{
    public class TaskModel
    {
        [Required]
        public string Text { get; set; }
    }
}
