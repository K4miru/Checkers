using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers
{
    public class Move : IMove
    {
        public Direction Direction { get; }
        public Position From { get; }
        public Position To { get; }
        public Position Capture { get; }

        public Move(Position from, Position to, Direction dir)
        {
            From = from;
            To = to;
            Direction = dir;
            Capture = null;
        }

        public Move(Position from, Position to, Direction dir, Position capture)
        {
            From = from;
            To = to;
            Direction = dir;
            Capture = capture;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            Move move = (Move)obj;

            bool capturesAreSame = false;

            if (Capture == null)
                if (move.Capture == null)
                    capturesAreSame = true;
                else
                    capturesAreSame = false;
            else
                capturesAreSame = Capture.Equals(move.Capture);

            return capturesAreSame
                && From.Equals(move.From)
                && To.Equals(move.To)
                && Direction == move.Direction;
        }

        public object Clone()
        {
            return new Move(From, To, Direction, Capture);
        }
    }
}
