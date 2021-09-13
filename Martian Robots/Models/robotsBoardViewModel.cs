using Martian_Robots.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Martian_Robots.Models
{
    public class robotsBoardViewModel
    {
        public Movement robot { get; set; }
        public IEnumerable<Movement> robotsLost { get; set; }
    }
}
