using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Checkers
{
    public class Highlighter
    {
        private Control[,] uiElements;

        private List<Position> higlightedToPositions;
        private List<Position> higlightedCapturesPositions;
        private int boardSize;

        public Highlighter(Control[,] uiElements, int boardSize)
        {
            this.uiElements = uiElements;
            this.boardSize = boardSize;
            DrawBoard();
        }

        private void DrawBoard()
        {
            for (int i = 0; i < boardSize; i++)
            {
                for (int j = 0; j < boardSize; j++)
                {
                    SetBasicColor(i, j);
                }
            }
        }
        private void SetBasicColor(int row, int col)
        {
            uiElements[row, col].Background = (row + col) % 2 == 0 ? Brushes.Gray : Brushes.Honeydew;
        }

        private void RemoveHighlight()
        {
            if (higlightedToPositions == null)
                return;
            foreach (var hp in higlightedToPositions)
            {
                SetBasicColor(hp.Row, hp.Col);
            }

            if (higlightedCapturesPositions == null)
                return;

            foreach (var hc in higlightedCapturesPositions)
            {
                SetBasicColor(hc.Row, hc.Col);
            }
        }

        public void HighlightFields(List<Sequence> positions)
        {
            RemoveHighlight();
            higlightedToPositions = positions?.Select(p => p.To).ToList();

            higlightedCapturesPositions = positions?.SelectMany(s => s.Captures)
                                                    .Distinct()
                                                    .Where(c => c != null)
                                                    .ToList();

            if (higlightedToPositions == null)
                return;

            foreach (var hp in higlightedToPositions)
            {
                uiElements[hp.Row, hp.Col].Background = Brushes.ForestGreen;
            }

            if (higlightedCapturesPositions == null)
                return;

            foreach (var hc in higlightedCapturesPositions)
            {
                uiElements[hc.Row, hc.Col].Background = Brushes.IndianRed;
            }
        }
    }
}
