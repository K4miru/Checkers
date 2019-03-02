using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers
{
    public class DraughtsSequencer : ISequencer
    {
        private Board board;

        public DraughtsSequencer(Board board)
        {
            this.board = board;
        }

        public IEnumerable<Sequence> GetRegularSequences(IPiece piece, Position position)
        {
            var moves = GenerateAllPossibleMoves(piece, position)
                                            .Where(m => m.Capture == null);

            if (moves == null)
                return new List<Sequence>();

            return ToSeqences(moves);

        }

        public IEnumerable<Sequence> GetCaptureSequences(IPiece piece, Position position)
        {
            var captures = new List<Sequence>();

            captures = GenerateAllCaptureSequences(position, piece);

            if (captures == null)
                return new List<Sequence>();

            return captures;
        }

        private IEnumerable<Sequence> ToSeqences(IEnumerable<IMove> moves)
        {
            var sequences = new List<Sequence>();

            if (moves == null)
                return sequences;

            foreach (var m in moves)
            {
                var seq = new Sequence();
                seq.AddMove(m);
                sequences.Add(seq);
            }

            return sequences;
        }

        private IEnumerable<IMove> GenerateAllPossibleMoves(IPiece piece, Position position)
        {
            var list = new List<IMove>();

            for (int i = 0; i < piece.Directions.Count; i++)
            {
                var dir = piece.Directions.ElementAt(i);
                var posDir = DirectionPositionConverter.ToPosition(dir) * (int)piece.Player;
                var moveNumber = 1;
                var toPosition = position + posDir * moveNumber;
                do
                {
                    var moveToAdd = new Move(position, toPosition, dir);
                    if (!IsValid(toPosition) || !IsEmpty(toPosition))
                    {
                        if (IsValid(toPosition + posDir) && IsEmpty(toPosition + posDir)
                            && piece.Player != PlayerAtPosition(toPosition))
                        {
                            moveToAdd = new Move(position, toPosition + posDir, dir, toPosition);
                            list.Add(moveToAdd);
                        }
                        break;
                    }

                    list.Add(moveToAdd);
                    moveNumber++;
                    toPosition = position + posDir * moveNumber;
                } while (piece.Flying);
            }

            return list;
        }

        private List<Sequence> GenerateAllCaptureSequences(Position position, IPiece piece = null, List<Sequence> sequences = null, Sequence previousSequence = null)
        {
            var sequencesTemp = new List<Sequence>();

            var captureMoves = GenerateAllPossibleMoves(piece, position)
                                            .Where(c => c.Capture != null);

            if (captureMoves == null)
                return sequencesTemp;

            if (sequences == null)
                sequences = new List<Sequence>();

            if (previousSequence == null)
                previousSequence = new Sequence();

            foreach (var cm in captureMoves)
            {
                var actualSequence = (Sequence)previousSequence.Clone();

                if (!actualSequence.Captures.Any(c => c.Equals(cm.Capture)))
                {
                    actualSequence.AddMove(cm);
                    GenerateAllCaptureSequences(cm.To, piece, sequences, actualSequence);
                }
            }

            if (previousSequence.Captures.Any())
                sequences.Add(previousSequence);

            return sequences;
        }

        private Player PlayerAtPosition(Position position) => board.PlayerAtPosition(position);
        private PieceType PieceTypeAtPosition(Position position) => board.PieceTypeAtPosition(position);
        private bool IsValid(Position position) => board.PositionIsValid(position);
        private bool IsEmpty(Position position) => board.IsEmpty(position);
    }
}
