using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers
{
    public class Position
    {
        public int Row { get; set; }
        public int Col { get; set; }

        public Position(int row = -1, int col = -1)
        {
            Row = row;
            Col = col;
        }

        public static Position operator +(Position pos1, Position pos2)
        {
            return new Position(pos1.Row + pos2.Row, pos1.Col + pos2.Col);
        }

        public static Position operator -(Position pos1, Position pos2)
        {
            return new Position(pos1.Row - pos2.Row, pos1.Col - pos2.Col);
        }

        public static Position operator *(Position pos1, int value)
        {
            return new Position(pos1.Row * value, pos1.Col * value);
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            Position pos = (Position)obj;

            return pos.Row == Row 
                && pos.Col == Col;
        }
    }
}
