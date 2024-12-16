using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WikiSound.Shared.Auth
{
    public class AuthResponse
    {
        public string Email { get; set; }
        public string Username { get; set; }
        public string Token { get; set; }
        public bool Succeeded { get; set; }
        public Dictionary<string, string[]> Errors { get; set; } = new Dictionary<string, string[]>();
    }
}
