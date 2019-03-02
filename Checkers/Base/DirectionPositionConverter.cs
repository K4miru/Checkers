using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers
{
    public static class DirectionPositionConverter
    {
        public static Position ToPosition(Direction dir)
        {
            switch (dir)
            {
                case Direction.Foward:
                    return new Position(1, 0);

                case Direction.FowardRight:
                    return new Position(1, 1);

                case Direction.FowardLeft:
                    return new Position(1, -1);

                case Direction.Right:
                    return new Position(0, 1);

                case Direction.BackwardRight:
                    return new Position(-1, 1);

                case Direction.Backward:
                    return new Position(-1, 0);

                case Direction.BackwardLeft:
                    return new Position(-1, -1);

                case Direction.Left:
                    return new Position(0, -1);

                case Direction.None:
                    return new Position(0, 0);
            }
            return new Position(0, 0);
        }
    }
}
