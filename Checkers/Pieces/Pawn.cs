using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Checkers
{
    public class Pawn : IPiece
    {
        public Player Player { get; }

        public PieceType PieceType { get; }

        public List<Direction> Directions { get; }

        public bool Flying { get; }

        public List<PieceType> CanCapture { get; }

        public List<PieceType> JumpThroughType { get; }

        public bool IsCapturingBackwards { get; }

        public bool IsMultiCapturing { get; }

        public Pawn(Player player, List<Direction> directions, bool flying)
        {
            Player = player;
            Directions = directions;
            Flying = flying;
        }

        public List<IMove> RegularMoves(Position position)
        {
            var moveList = new List<IMove>();

            return moveList;
        }

        public List<IMove> CaptureMoves(Position position)
        {
            var captureList = new List<IMove>();

            return captureList;
        }

        public override bool Equals(object obj)
        {
            if ((obj == null) || !GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                Pawn p = (Pawn)obj;
                return p.Player == Player;
            }
        }
    }
}
