using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WebAPI.Data.Entities
{
    public class TodoTask
    {
        [Key]
        public int Id { get; set; }
        public string Text { get; set; }
        public bool Status { get; set; } = false;
        public string Time { get; set; }

        public int AccountId { get; set; }
        [System.Text.Json.Serialization.JsonIgnore]
        public Account Account { get; set; }
    }
}
