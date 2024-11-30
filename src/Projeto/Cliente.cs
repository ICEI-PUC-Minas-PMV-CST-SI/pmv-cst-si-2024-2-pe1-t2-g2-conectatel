using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Projeto
{
    internal class Cliente
    {
        public Cliente()
        {
            Guid guid = Guid.NewGuid();
            Id = guid.ToString();
        }
        
        public Dictionary<string,string> Login { get; set; } = new Dictionary<string,string>(); 
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Id { get; set; }
    }
}
