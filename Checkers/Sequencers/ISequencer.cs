using System.Collections.Generic;

namespace Checkers
{
    public interface ISequencer
    {
        IEnumerable<Sequence> GetRegularSequences(IPiece piece, Position position);

        IEnumerable<Sequence> GetCaptureSequences(IPiece piece, Position position);
    }
}