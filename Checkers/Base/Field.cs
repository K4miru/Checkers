using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Checkers
{
    public class Field
    {
        public Position Position { get; }
        public IPiece Piece { get; private set; }
        public bool IsEmpty => Piece == null;

        public Field(Position position)
            : this(position, null)
        {
        }

        public Field(Position position, IPiece pawn)
        {
            Position = position;
            Piece = pawn;
        }

        public void MovePieceTo(Field newfield)
        {
            newfield.Piece = Piece;
            Piece = null;
        }

        public List<IMove> PieceRegularMoves => Piece?.RegularMoves(Position);

        public List<IMove> PieceCaptureMoves => Piece?.CaptureMoves(Position);

        internal void Remove()
        {
            Piece = null;
        }
    }
}
