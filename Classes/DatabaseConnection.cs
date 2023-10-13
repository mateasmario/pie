using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pie.Classes
{
    public class DatabaseConnection
    {
        public string ConnectionName { get; set; }
        public string Hostname { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int Port { get; set; }
    }
}
