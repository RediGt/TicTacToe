using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace TicTacToe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MarkType[] results;
        private bool player1Turn;
        private bool gameEnded;
        
        public MainWindow()
        {
            InitializeComponent();
            NewGame();
        }

        private void NewGame()
        {
            results = new MarkType[9];
            for (int i = 0; i < results.Length; i++)
                results[i] = MarkType.Free;

            player1Turn = true;
            
            //Iterate every button on the grid
            Container.Children.Cast<Button>().ToList().ForEach(button => 
            {
                button.Content = string.Empty;
                button.Background = Brushes.White;
                button.Foreground = Brushes.Blue;
            });

            gameEnded = false;
        }

        /// <summary>
        /// Handles a button click event
        /// </summary>
        /// <param name="sender">The button that was clicked</param>
        /// <param name="e">The events of the click</param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (gameEnded)
            {
                NewGame();
                return;
            }

            Button button = (Button)sender;
            int colomn = Grid.GetColumn(button);
            int row = Grid.GetRow(button);
            int index = colomn + row * 3;

            if (results[index] != MarkType.Free) //if a cell is "X" or "0" - return
                return;

            //Set the cell value
            MakeMove(button, index);

            CheckForWin();
        }

        private void CheckForWin()
        {
            if (results[0] != MarkType.Free && (results[0] & results[1] & results[2]) == results[0])
            {
                gameEnded = true;
                Button0_0.Background = Button1_0.Background = Button2_0.Background = Brushes.Green;
            }

            if (results[3] != MarkType.Free && (results[3] & results[4] & results[5]) == results[3])
            {
                gameEnded = true;
                Button0_1.Background = Button1_1.Background = Button2_1.Background = Brushes.Green;
            }

            if (results[6] != MarkType.Free && (results[6] & results[7] & results[8]) == results[6])
            {
                gameEnded = true;
                Button0_2.Background = Button1_2.Background = Button2_2.Background = Brushes.Green;
            }

            if (results[0] != MarkType.Free && (results[0] & results[3] & results[6]) == results[0])
            {
                gameEnded = true;
                Button0_0.Background = Button0_1.Background = Button0_2.Background = Brushes.Green;
            }

            if (results[1] != MarkType.Free && (results[1] & results[4] & results[7]) == results[1])
            {
                gameEnded = true;
                Button1_0.Background = Button1_1.Background = Button1_2.Background = Brushes.Green;
            }

            if (results[2] != MarkType.Free && (results[2] & results[5] & results[8]) == results[2])
            {
                gameEnded = true;
                Button2_0.Background = Button2_1.Background = Button2_2.Background = Brushes.Green;
            }

            if (results[0] != MarkType.Free && (results[0] & results[4] & results[8]) == results[0])
            {
                gameEnded = true;
                Button0_0.Background = Button1_1.Background = Button2_2.Background = Brushes.Green;
            }

            if (results[2] != MarkType.Free && (results[2] & results[4] & results[6]) == results[2])
            {
                gameEnded = true;
                Button2_0.Background = Button1_1.Background = Button0_2.Background = Brushes.Green;
            }

            if (!results.Any(res => res == MarkType.Free))
            {
                gameEnded = true;
                Container.Children.Cast<Button>().ToList().ForEach(button =>
                {                    
                    button.Background = Brushes.DarkOrchid;
                });
            }
        }

        private void MakeMove(Button button, int index)
        {
            if (player1Turn)
            {
                results[index] = MarkType.Cross;
                button.Content = "X";
                player1Turn = false;
            }
            else
            { 
                results[index] = MarkType.Nought;
                button.Content = "0";
                player1Turn = true;
                button.Foreground = Brushes.Red;
            }
        }
    }
}
