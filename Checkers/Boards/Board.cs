using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Checkers
{
    public class Board
    {
        public readonly int BoardSize;
        public readonly int NumberOfPieces;

        private readonly int SpacingOffSet;
        private readonly int Spacing;
        private readonly int RowOffSet;

        private Field[,] board;
        private ISequencer sequencer;
        private List<Sequence> possibleSequences;

        public Board(int boardSize, int numberOfPieces, int spacingOffSet = 1, int spacing = 2, int rowOffSet = 0)
        {
            BoardSize = boardSize;
            NumberOfPieces = numberOfPieces;
            SpacingOffSet = spacingOffSet;
            Spacing = spacing;
            RowOffSet = rowOffSet;
            this.sequencer = new DraughtsSequencer(this);
            FillBoardWithPieces();
        }

        public bool IsEmpty(Position position) => board[position.Row, position.Col].IsEmpty;

        public PieceType PieceTypeAtPosition(Position position) => board[position.Row, position.Col].Piece.PieceType;

        public Player PlayerAtPosition(Position position)
        {
            var pawn = board[position.Row, position.Col].Piece;
            return pawn != null ? pawn.Player : Player.NONE;
        }

        private void FillBoardWithPieces()
        {
            board = new Field[BoardSize, BoardSize];
            int placedPieces = 0;

            var listOfDirections = new List<Direction>() {
                Direction.Foward, Direction.Backward,
                Direction.Left, Direction.Right,
                Direction.FowardLeft, Direction.BackwardLeft,
                Direction.FowardRight, Direction.BackwardRight, 
            };

            //Generating empty fields
            for (int row = 0; row < BoardSize; row++)
            {
                for (int col = 0; col < BoardSize; col++)
                {
                    board[row, col] = new Field(new Position(row, col), null);
                }
            }

            //Putting pieces on board
            var firstRow = RowOffSet;
            var lastRow = BoardSize - RowOffSet;
            for (var row = firstRow; row < lastRow; row++)
            {
                for (var col = 0; col < BoardSize; col++)
                {
                    var position = new Position(row, col);
                    if (Spacing == 0 || (row + col) % Spacing == 0)
                    {
                        if (placedPieces >= NumberOfPieces / 2)
                            break;
                        board[row, col] = new Field(position, new Pawn(Player.WHITE, listOfDirections, false));
                        placedPieces++;
                    }
                }
            }
            for (var row = lastRow - 1 ; row >= firstRow; row--)
            {
                for (var col = 0; col < BoardSize; col++)
                {
                    var position = new Position(row, col);
                    if (Spacing == 0 || (row + col) % Spacing == 0)
                    {
                        if (placedPieces >= NumberOfPieces)
                            break;
                        board[row, col] = new Field(position, new Pawn(Player.BLACK, listOfDirections, true));
                        placedPieces++;
                    }
                }
            }
        }

        public void Move(Position from, Position to)
        {
            CapturePieces(to);
            board[from.Row, from.Col].MovePieceTo(board[to.Row, to.Col]);
        }

        private void CapturePieces(Position to)
        {
            var seq = possibleSequences.Where(m => m.To.Equals(to))
                                        .First();
            if (seq.Captures == null || !seq.Captures.Any())
                return;

            foreach (var c in seq.Captures)
                if(c != null)
                    RemovePieceFromBoard(c);
        }

        private void RemovePieceFromBoard(Position c)
        {
            board[c.Row, c.Col].Remove();
        }

        public List<Sequence> PossibleSequences(Position position)
        {
            possibleSequences = new List<Sequence>();
            var piece = board[position.Row, position.Col].Piece;

            if (piece == null)
                return possibleSequences;

            var regularSequences = sequencer.GetRegularSequences(piece, position).ToList();
            var captureSequences = sequencer.GetCaptureSequences(piece, position).ToList();

            possibleSequences = possibleSequences.Union(regularSequences)
                                                    .Union(captureSequences)
                                                    .ToList();

            return possibleSequences;
        }

        private List<IMove> CaptureMoves(Position position, IPiece piece)
        {
            var captures = piece.CaptureMoves(position)
                                .Where(m => MoveIsValid(m))
                                .Where(c => PositionIsValid(c.Capture)
                                    && IsEmpty(c.To)
                                    && !IsEmpty(c.Capture)
                                    && PlayerAtPosition(c.Capture) != piece.Player);
            return captures.ToList();
        }

        private List<IMove> RegularMoves(Position position)
        {
            var piece = board[position.Row, position.Col].Piece;
            var moves = piece.RegularMoves(position)
                                .Where(m => MoveIsValid(m)
                                    && IsEmpty(m.To));

            return moves.ToList();
        }

        public bool MoveIsValid(IMove move)
        {
            return PositionIsValid(move.To);
        }

        public bool PositionIsValid(Position pos)
        {
            return IsValid(pos.Row) && IsValid(pos.Col);
        }

        public bool IsValid(int value)
        {
            return value < BoardSize && value >= 0;
        }

        public void Promote()
        {

        }
    }
}
