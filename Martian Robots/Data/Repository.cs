using Martian_Robots.Data.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Martian_Robots.Data
{
    public class Repository:IRepository
    {

        private string path = Path.Combine(
                Directory.GetCurrentDirectory(), "wwwroot\\json", "movements.json");
        /// <summary>
        /// Get Settings of Settings.json
        /// </summary>
        /// <returns>object setting with lenghX and lengh Y</returns>
        public Settings GetSettings()
        {
            try
            {
                var pathSettings = Path.Combine(
                Directory.GetCurrentDirectory(), "wwwroot\\json", "Settings.json");
                string text = System.IO.File.ReadAllText(pathSettings);

                var settings = JsonConvert.DeserializeObject<List<Settings>>(text);


                return settings.FirstOrDefault();
            }
            catch (Exception ex)
            {
                var ex1 = ex;
                return null;
            }
        }
        /// <summary>
        /// Get all the movemnts of the board, stored in json
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Movement> GetMovements()
        {
            try
            {
                //var path = Path.Combine(
                //Directory.GetCurrentDirectory(), "wwwroot\\json", "movements.json");
                string text = System.IO.File.ReadAllText(path);

                var movements = JsonConvert.DeserializeObject<List<Movement>>(text);


                return movements.OrderByDescending(m=>m.Time).ToList();
            }
            catch (Exception ex)
            {
                var ex1 = ex;
                return null;
            }
        }
        /// <summary>
        /// Remove all movement of movements.json
        /// </summary>
        /// <returns></returns>
        public bool RemoveAllMovements()
        {
            try
            {
                //var path = Path.Combine(
                //Directory.GetCurrentDirectory(), "wwwroot\\json", "movements.json");
                System.IO.File.WriteAllText(path, "");


                return true;
            }
            catch (Exception ex)
            {
                var ex1 = ex;
                return false;
            }
        }
        /// <summary>
        /// MovementLength to ID movement
        /// </summary>
        /// <returns></returns>
        public int MovementLength()
        {
            try
            {



                return GetMovements().Count();
            }
            catch (Exception ex)
            {
                var ex1 = ex;
                return 0;
            }
        }
        /// <summary>
        /// Save movements of the board, saving a json list in a .json file
        /// </summary>
        /// <param name="movement">Entiti movement </param>
        public void SaveMovement(Movement movement)
        {
            try
            {
                //var path = Path.Combine(
                //Directory.GetCurrentDirectory(), "wwwroot\\json", "movements.json");
                string text = System.IO.File.ReadAllText(path);

                if (text != "")
                {
                    var movementsSave = JsonConvert.DeserializeObject<List<Movement>>(text);
                    movementsSave.Add(movement);
                    string json = JsonConvert.SerializeObject(movementsSave.ToArray());
                    System.IO.File.WriteAllText(path, json);
                }
                else
                {
                    var movementsSave = new List<Movement>();
                    movementsSave.Add(movement);
                    string jsonList = JsonConvert.SerializeObject(movementsSave);
                    System.IO.File.WriteAllText(path, jsonList);
                }
            }
            catch (Exception ex)
            {
                var ex1 = ex;
            }
        }
        public bool isOkToStart(string user)
        {
            try
            {

                var gruopUser = GetMovements().OrderByDescending(m => m.Time).GroupBy(g => g.User);
                //gruopUser.Where(m=>m.u)
                return true;


            }
            catch (Exception ex)
            {
                var ex1 = ex;
                return false;
            }
        }
        /// <summary>
        /// Get last movement of user 
        /// </summary>
        /// <param name="user">robot name</param>
        /// <returns></returns>
        public Movement getLastMoveByUser(string user)
        {
            try
            {

                return GetMovements().Where(m => m.User.Equals(user)).OrderByDescending(g => g.Time).FirstOrDefault();


            }
            catch (Exception ex)
            {
                var ex1 = ex;
                return null;
            }
        }
        /// <summary>
        /// last  movement in board
        /// </summary>
        /// <returns></returns>
        public Movement getLastMoveNotLost()
        {
            try
            {
                var group = GetMovements().GroupBy(x => x.User, (key, g) => g.OrderByDescending(e => e.Time).FirstOrDefault());

                return group.Where(m => !m.isOut).OrderByDescending(g => g.Time).FirstOrDefault();


            }
            catch (Exception ex)
            {
                var ex1 = ex;
                return null;
            }
        }
        /// <summary>
        /// get all the robots lost
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Movement> getLastAllRobotsLost()
        {
            try
            {
                var group = GetMovements().GroupBy(x => x.User, (key, g) => g.OrderByDescending(e => e.Time).FirstOrDefault());

                return group.Where(m => m.isOut).OrderByDescending(g => g.Time).ToList();


            }
            catch (Exception ex)
            {
                var ex1 = ex;
                return null;
            }
        }
    }
}
