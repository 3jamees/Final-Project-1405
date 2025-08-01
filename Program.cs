// See https://aka.ms/new-console-template for more information

// Name: James King II
// Date: August 1, 2025
// Project: Final Project 1405
// A console-based game offering two options: Tic-Tac-Toe or a Number Guessing game.

using System.Diagnostics;
class GameProject
{
    public void Run()
    {
        bool running = true;
        while (running)
        {
            Console.Clear();
            Console.WriteLine("Welcome to the Dual Game Console!");
            Console.WriteLine("Choose a game to play:");
            Console.WriteLine("1. Number Guessing Game");
            Console.WriteLine("2. Tic Tac Toe");
            Console.WriteLine("3. Exit");

            Console.Write("Enter your choice: ");
            string choice = Console.ReadLine()!;

            switch (choice)
            {
                case "1":
                    ShowNumberGuessInstructions();
                    NumberGuessGame();
                    break;
                case "2":
                    ShowTicTacToeInstructions();
                    PlayTicTacToe();
                    break;
                case "3":
                    Console.WriteLine("Thanks for playing! Goodbye!");
                    running = false;
                    break;
                default:
                    Console.WriteLine("Invalid input. Try again!");
                    break;
            }

            if (running)
            {
                Console.WriteLine("Press any key to return to the menu...");
                Console.ReadKey();
            }
        }
    }

    public void ShowNumberGuessInstructions()
    {
        Console.Clear();
        Console.WriteLine("NUMBER GUESSING GAME INSTRUCTIONS:");
        Console.WriteLine("I will think of a number between 1 and 100.");
        Console.WriteLine("You have 5 tries to guess it.");
        Console.WriteLine("After each guess, I'll tell you if your guess is too high or too low.");
        Console.WriteLine("Good luck!");
        Console.WriteLine("\nPress any key to start...");
        Console.ReadKey();
    }

    public void NumberGuessGame()
    {
        Console.Clear();
        Random rand = new Random();
        int secretNumber = rand.Next(1, 101);
        int guess;
        int tries = 0;

        while (tries < 5)
        {
            Console.Write($"Try #{tries + 1}: Enter your guess: ");
            if (int.TryParse(Console.ReadLine(), out guess))
            {
                if (guess < 1 || guess > 100)
                {
                    Console.WriteLine("Please guess a number between 1 and 100.");
                    continue;
                }

                tries++;
                if (guess == secretNumber)
                {
                    Console.WriteLine("Correct! You guessed the number!");
                    break;
                }
                else if (guess < secretNumber)
                {
                    Console.WriteLine("Too low.");
                }
                else
                {
                    Console.WriteLine("Too high.");
                }

                if (tries == 5)
                {
                    Console.WriteLine($"You're out of tries. The number was: {secretNumber}");
                }
            }
            else
            {

                Console.WriteLine("Please enter a valid number.");
            }
        }
    }

    public void ShowTicTacToeInstructions()
    {
        Console.Clear();
        Console.WriteLine("TIC TAC TOE INSTRUCTIONS:");
        Console.WriteLine("Two players take turns to place their marks (X and O) on the board.");
        Console.WriteLine("The first player to get 3 marks in a row (horizontally, vertically, or diagonally) wins.");
        Console.WriteLine("If all spots fill up without a winner, the game is a draw.");
        Console.WriteLine("Enter the number of the position where you want to place your mark.");
        Console.WriteLine("\nPress any key to start...");
        Console.ReadKey();
    }

    public void PlayTicTacToe()
    {
        char[] board = { '1', '2', '3', '4', '5', '6', '7', '8', '9' };
        int currentPlayer = 1;
        char mark;

        for (int turn = 0; turn < 9; turn++)
        {
            Console.Clear();
            DrawBoard(board);
            mark = (currentPlayer == 1) ? 'X' : 'O';

            Console.Write($"Player {currentPlayer} ({mark}), enter your move (1-9): ");
            int choice;
            while (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > 9 || board[choice - 1] == 'X' || board[choice - 1] == 'O')
            {
                Console.Write("Invalid move. Try again: ");
            }

            board[choice - 1] = mark;

            if (CheckWin(board))
            {
                Console.Clear();
                DrawBoard(board);
                Console.WriteLine($"Player {currentPlayer} wins!");
                return;
            }


            currentPlayer = (currentPlayer == 1) ? 2 : 1;
        }

        Console.Clear();
        DrawBoard(board);
        Console.WriteLine("It's a draw.");
    }

    public void DrawBoard(char[] board)
    {
        Console.WriteLine();
        Console.WriteLine($" {board[0]} | {board[1]} | {board[2]} ");
        Console.WriteLine("---+---+---");
        Console.WriteLine($" {board[3]} | {board[4]} | {board[5]} ");
        Console.WriteLine("---+---+---");
        Console.WriteLine($" {board[6]} | {board[7]} | {board[8]} ");
        Console.WriteLine();
    }

    public bool CheckWin(char[] b)
    {
        int[,] winCombos = {
            {0,1,2}, {3,4,5}, {6,7,8},
            {0,3,6}, {1,4,7}, {2,5,8},
            {0,4,8}, {2,4,6}
        };

        for (int i = 0; i < winCombos.GetLength(0); i++)
        {
            if (b[winCombos[i, 0]] == b[winCombos[i, 1]] && b[winCombos[i, 1]] == b[winCombos[i, 2]])
                return true;
        }
        return false;
    }
}

class Program
{
    static void Main()
    {
        GameProject game = new GameProject();
        game.Run();

        // Test: should return true for a win
        char[] winBoard = { 'X', 'X', 'X', '4', '5', '6', '7', '8', '9' };
        Debug.Assert(game.CheckWin(winBoard) == true, "Test failed: CheckWin should return true.");

        // Test: should return false for no win
        char[] noWinBoard = { 'X', 'O', 'X', 'O', 'X', 'O', 'O', 'X', 'O' };
        Debug.Assert(game.CheckWin(noWinBoard) == false, "Test failed: CheckWin should return false.");
    }
}

