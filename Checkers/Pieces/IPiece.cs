using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers
{
    public interface IPiece
    {
        Player Player { get; }

        PieceType PieceType { get; }

        List<Direction> Directions { get; }

        bool Flying { get; }

        bool IsCapturingBackwards { get; }

        bool IsMultiCapturing { get; }

        List<PieceType> CanCapture { get; }

        List<PieceType> JumpThroughType { get; }

        List<IMove> RegularMoves(Position position);

        List<IMove> CaptureMoves(Position position);
    }
}
