using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WebAPI.Data.DTO
{
    public class TaskUpdateModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Text { get; set; }
    }
}
