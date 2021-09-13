using Martian_Robots.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Martian_Robots.Models
{
    public class GameViewModel
    {
        public Settings settings { get; set; }
        public Movement Movement { get; set; }
        public bool isRobotPlaying { get; set; }
        public string NameRobot { get; set; }
    }
}
