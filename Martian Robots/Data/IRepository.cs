using Martian_Robots.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Martian_Robots.Data
{
    public interface IRepository
    {
        IEnumerable<Movement> GetMovements();
        bool RemoveAllMovements();
        void SaveMovement(Movement movement);
        int MovementLength();

        bool isOkToStart(string user);
        Movement getLastMoveByUser(string user);
        Movement getLastMoveNotLost();
        IEnumerable<Movement> getLastAllRobotsLost();
        Settings GetSettings();
    }
}
