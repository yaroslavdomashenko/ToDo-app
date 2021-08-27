using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WebAPI.Data.DTO
{
    public class ChangeModel
    {
        [Required]
        public int Id { get; set; }
    }
}
