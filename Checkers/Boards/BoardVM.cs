using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Checkers
{
    internal class BoardVM : BaseVM
    {
        private Dictionary<Player, string> symbol = new Dictionary<Player, string>()
        {
            { Player.NONE, " " },
            { Player.WHITE, "O" },
            { Player.BLACK, "X" },
        };

        private Highlighter highlighter;
        private Board board;
        private Button[,] buttons;

        private bool wasSelected = false;
        private List<Sequence> possibleMoves;
        private Position previousPosition;

        public event Action<Button, int, int> AddButtonToView;
        public event Action OnPreparation;

        public readonly int BoardSize = 10;
        public readonly int NumberOfPieces = 40;

        public BoardVM()
        {
            board = new Board(BoardSize, NumberOfPieces,1,0,1);
            buttons = new Button[BoardSize, BoardSize];
        }

        private void GenerateButtons()
        {
            for (int i = 0; i < BoardSize; i++)
            {
                for (int j = 0; j < BoardSize; j++)
                {
                    var bt = new Button();
                    bt.Click += FieldSelected;
                    buttons[i, j] = bt;
                    Update(i, j);
                    AddButtonToView.Invoke(bt, i, j);
                }
            }
            highlighter = new Highlighter(buttons, BoardSize);
        }

        private void FieldSelected(object sender, RoutedEventArgs e)
        {
            Button bt = (Button)sender;

            var position = GetPosition(bt);
            int row = position.Row;
            int col = position.Col;

            var isEmpty = board.IsEmpty(position);

            if (!wasSelected && !isEmpty)
            {
                wasSelected = true;
                previousPosition = position;
                possibleMoves = board.PossibleSequences(position);
            }
            else if (wasSelected && !isEmpty)
            {
                previousPosition = position;
                possibleMoves = board.PossibleSequences(position);
            }
            else if (wasSelected 
                && isEmpty 
                && possibleMoves.Where(m => m.To.Equals(position))
                                .ToList()
                                .Any())
            {
                wasSelected = false;
                possibleMoves = null;
                board.Move(previousPosition, position);
                Update(previousPosition);
                Update(position);
            }
            highlighter.HighlightFields(possibleMoves);
        }

        private void Update(List<Field> fields)
        {
            if (fields == null)
                return;
            foreach (var f in fields)
                Update(f.Position.Row, f.Position.Col);
        }

        private void Update(Position position)
        {
            if (position == null)
                return;
            Update(position.Row, position.Col);
        }

        private void Update(int row, int col)
        {
            var player = board.PlayerAtPosition(new Position(row, col));
            buttons[row, col].Content = symbol.Where(s => s.Key == player).First().Value;
        }

        public void Initialize()
        {
            OnPreparation.Invoke();
            GenerateButtons();
        }

        private Position GetPosition(Button bt)
        {
            for (int i = 0; i < BoardSize; i++)
            {
                for (int j = 0; j < BoardSize; j++)
                {
                    if (buttons[i, j] == bt)
                        return new Position(i, j);
                }
            }
            return new Position();
        }
    }
}
