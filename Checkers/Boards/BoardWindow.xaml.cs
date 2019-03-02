using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Checkers
{
    public partial class BoardWindow : Window
    {
        private BoardVM vm;

        public BoardWindow()
        {
            InitializeComponent();
            vm = new BoardVM();
            DataContext = vm;
            vm.OnPreparation += GenerateGrid;
            vm.AddButtonToView += AddButton;
            vm.Initialize();
        }

        private void GenerateGrid()
        {
            for (int i = 0; i < vm.BoardSize; i++)
            {
                var row = new RowDefinition();
                var col = new ColumnDefinition();
                GridBoard.RowDefinitions.Add(row);
                GridBoard.ColumnDefinitions.Add(col);
            }
            for (int i = 0; i < 3; i++)
            {
                var column = new ColumnDefinition();
                GridBoard.ColumnDefinitions.Add(column);
            }
            var stackPanel = new StackPanel();
            Grid.SetColumn(stackPanel, vm.BoardSize + 1);
            Grid.SetColumnSpan(stackPanel, vm.BoardSize);
            Grid.SetRowSpan(stackPanel, 3);
            GridBoard.Children.Add(stackPanel);
        }

        private void AddButton(Button bt, int row, int col)
        {
            Grid.SetRow(bt, row);
            Grid.SetColumn(bt, col);
            GridBoard.Children.Add(bt);
        }
    }
}
