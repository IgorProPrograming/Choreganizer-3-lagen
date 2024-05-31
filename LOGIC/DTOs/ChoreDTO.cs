using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOGIC.DTOs
{
    public class ChoreDTO
    {
        public int Id { get; set; }
        public string ChoreName { get; set; }
        public DateTime Deadline { get; set; }
        public bool Finished { get; set; }
    }
}
