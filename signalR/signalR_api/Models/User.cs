using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace signalR_api.Models
{
    public class User
    {
        public string Id { get; set; }
        [Required] public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime CreatedAt { get; set; }
        public string PasswordHash { get; set; }
        public string Salt { get; set; }
        [EmailAddress] public string Email { get; set; }
        public int RoleId { get; set; }
        public Role Role { get; set; }

        public override string ToString()
        {
            return $"imie to: {Name}, nazwisko to {Surname}";
        }
    }
}