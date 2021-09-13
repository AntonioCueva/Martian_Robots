using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Martian_Robots.Data.Entities
{
    public class Movement
    {
        //public enum Orientation{N = 'N',S='S',E='E',W='W'}
        //public enum Instruction {L = 'L',R = 'R',F = 'F'}
        //public const char L = 'L';
        //public const char R = 'R';
        public long Id { get; set; }
        public int PositionBeforeX { get; set; }
        public int PositionBeforeY { get; set; }
        public int PositionAfterX { get; set; }
        public int PositionAfterY { get; set; }
        public DateTime Time { get; set; }
        public bool isOut { get; set; }
        public char OrientationBefore { get; set; }
        public char OrientationAfter { get; set; }
        public char InstructionMove { get; set; }
        public string User { get; set; }

        public void moveRigth()
        {
            InstructionMove = Instruction.R;
            switch (OrientationBefore)
            {
                case OrientationBoard.N:
                    OrientationAfter = OrientationBoard.E;
                    PositionAfterX = PositionBeforeX + 1;
                    PositionAfterY = PositionBeforeY;
                    break;

                case OrientationBoard.S:
                    OrientationAfter = OrientationBoard.W;
                    PositionAfterX = PositionBeforeX - 1;
                    PositionAfterY = PositionBeforeY;
                    break;

                case OrientationBoard.E:
                    OrientationAfter = OrientationBoard.S;
                    PositionAfterX = PositionBeforeX;
                    PositionAfterY = PositionBeforeY - 1;
                    break;
                case OrientationBoard.W:
                    OrientationAfter = OrientationBoard.N;
                    PositionAfterX = PositionBeforeX;
                    PositionAfterY = PositionBeforeY + 1;
                    break;

                default:
                    break;
            }
        }
        public void moveLeft()
        {
            InstructionMove = Instruction.L;
            switch (OrientationBefore)
            {
                case OrientationBoard.N:
                    OrientationAfter = OrientationBoard.W;
                    PositionAfterX = PositionBeforeX - 1;
                    PositionAfterY = PositionBeforeY;
                    break;

                case OrientationBoard.S:
                    OrientationAfter = OrientationBoard.E;
                    PositionAfterX = PositionBeforeX + 1;
                    PositionAfterY = PositionBeforeY;
                    break;

                case OrientationBoard.E:
                    OrientationAfter = OrientationBoard.N;
                    PositionAfterX = PositionBeforeX;
                    PositionAfterY = PositionBeforeY + 1;
                    break;
                case OrientationBoard.W:
                    OrientationAfter = OrientationBoard.S;
                    PositionAfterX = PositionBeforeX;
                    PositionAfterY = PositionBeforeY - 1;
                    break;

                default:
                    break;
            }
        }
        public void moveForward()
        {
            InstructionMove = Instruction.F;
            switch (OrientationBefore)
            {
                case OrientationBoard.N:
                    OrientationAfter = OrientationBoard.N;
                    PositionAfterX = PositionBeforeX ;
                    PositionAfterY = PositionBeforeY +1;
                    break;

                case OrientationBoard.S:
                    OrientationAfter = OrientationBoard.S;
                    PositionAfterX = PositionBeforeX;
                    PositionAfterY = PositionBeforeY -1;
                    break;

                case OrientationBoard.E:
                    OrientationAfter = OrientationBoard.E;
                    PositionAfterX = PositionBeforeX+1;
                    PositionAfterY = PositionBeforeY;
                    break;
                case OrientationBoard.W:
                    OrientationAfter = OrientationBoard.W;
                    PositionAfterX = PositionBeforeX-1;
                    PositionAfterY = PositionBeforeY;
                    break;

                default:
                    break;
            }
        }
    }
    public class OrientationBoard
    {
        public const char N = 'N';
        public const char S = 'S';
        public const char E = 'E';
        public const char W = 'W';
    }
    public class Instruction
    {
        public const char L = 'L';
        public const char R = 'R';
        public const char F = 'F';
    }
}
