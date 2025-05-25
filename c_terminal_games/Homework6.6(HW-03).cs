// A homework for a Skillfactory course. A Tic-tac-toe game using basic terminal and C# language

using System;

class Program
{
    // Constants for game board dimensions and symbols
    const int BOARD_SIZE = 3;
    const char EMPTY_CELL = ' ';
    const char PLAYER_X = 'X';
    const char PLAYER_O = 'O';

    // Game board represented as a 2D array of characters
    static char[,] gameBoard = new char[BOARD_SIZE, BOARD_SIZE];

    // --- Drawing methods for the console ---
    static void DrawCross(int x, int y, int size)
    {
        for (int i = x; i <= x + size; i++)
        {
            for (int j = y; j <= y + size; j++)
            {
                // Check if the current position is part of the cross shape
                if ((i - x == j - y) || (i - x + j - y == size))
                {
                    Console.SetCursorPosition(i, j);
                    Console.Write(PLAYER_X);
                }
            }
        }
    }

    static void DrawRectangle(int x, int y, int size)
    {
        for (int i = x; i <= x + size; i++)
        {
            for (int j = y; j <= y + size; j++)
            {
                // Check if the current position is on the border of the rectangle
                if (i == x || i == x + size || j == y || j == y + size)
                {
                    Console.SetCursorPosition(i, j);
                    Console.Write(PLAYER_O);
                }
            }
        }
    }

    // Method to draw the Tic-Tac-Toe field (grid)
    static void DrawField(int width, int height, int cellSize)
    {
        Console.Clear(); // Clear the console before drawing

        // Draw horizontal lines
        for (int y = 0; y <= height; y += cellSize)
        {
            for (int x = 0; x < width; x++)
            {
                Console.SetCursorPosition(x, y);
                Console.Write("-"); // Horizontal line character
            }
        }

        // Draw vertical lines
        for (int x = 0; x <= width; x += cellSize)
        {
            for (int y = 0; y < height; y++)
            {
                Console.SetCursorPosition(x, y);
                Console.Write("|"); // Vertical line character
            }
        }

        // Draw cell numbers
        int cellNumber = 1;
        for (int row = 0; row < BOARD_SIZE; row++)
        {
            for (int col = 0; col < BOARD_SIZE; col++)
            {
                int displayX = col * cellSize + cellSize / 2;
                int displayY = row * cellSize + cellSize / 2;
                Console.SetCursorPosition(displayX, displayY);
                // Only display the cell number if the cell is empty.
                // Otherwise, the figure (X or O) will be displayed.
                if (gameBoard[row, col] == EMPTY_CELL)
                {
                    Console.Write(cellNumber);
                }
                else
                {
                    Console.Write(gameBoard[row, col]); // Display the player's symbol
                }
                cellNumber++;
            }
        }

        // Display current game board state (redundant if handled in cell number drawing, but ensures all symbols are shown)
        for (int row = 0; row < BOARD_SIZE; row++)
        {
            for (int col = 0; col < BOARD_SIZE; col++)
            {
                int displayX = col * cellSize + cellSize / 2;
                int displayY = row * cellSize + cellSize / 2;
                Console.SetCursorPosition(displayX, displayY);
                Console.Write(gameBoard[row, col]);
            }
        }
    }

    // --- Game Logic Methods ---

    // Initializes the game board with empty cells
    static void InitializeBoard()
    {
        for (int row = 0; row < BOARD_SIZE; row++)
        {
            for (int col = 0; col < BOARD_SIZE; col++)
            {
                gameBoard[row, col] = EMPTY_CELL;
            }
        }
    }

    // Checks if the given row and column are within the board boundaries
    static bool IsValidMoveCoordinates(int row, int col)
    {
        return row >= 0 && row < BOARD_SIZE && col >= 0 && col < BOARD_SIZE;
    }

    // Checks if the chosen cell is empty
    static bool IsCellEmpty(int row, int col)
    {
        return gameBoard[row, col] == EMPTY_CELL;
    }

    // Converts a cell number (1-9) to row and column indices
    static void GetRowColFromCellNumber(int cellNumber, out int row, out int col)
    {
        row = (cellNumber - 1) / BOARD_SIZE;
        col = (cellNumber - 1) % BOARD_SIZE;
    }

    // Places the player's figure on the board
    static void PlaceFigure(int row, int col, char playerSymbol)
    {
        gameBoard[row, col] = playerSymbol;
    }

    // Checks for a win condition
    static bool CheckForWin(char playerSymbol)
    {
        // Check rows
        for (int r = 0; r < BOARD_SIZE; r++)
        {
            if (gameBoard[r, 0] == playerSymbol && gameBoard[r, 1] == playerSymbol && gameBoard[r, 2] == playerSymbol)
            {
                return true;
            }
        }

        // Check columns
        for (int c = 0; c < BOARD_SIZE; c++)
        {
            if (gameBoard[0, c] == playerSymbol && gameBoard[1, c] == playerSymbol && gameBoard[2, c] == playerSymbol)
            {
                return true;
            }
        }

        // Check diagonals
        if ((gameBoard[0, 0] == playerSymbol && gameBoard[1, 1] == playerSymbol && gameBoard[2, 2] == playerSymbol) ||
            (gameBoard[0, 2] == playerSymbol && gameBoard[1, 1] == playerSymbol && gameBoard[2, 0] == playerSymbol))
        {
            return true;
        }

        return false;
    }

    // Checks for a draw condition
    static bool CheckForDraw()
    {
        for (int row = 0; row < BOARD_SIZE; row++)
        {
            for (int col = 0; col < BOARD_SIZE; col++)
            {
                if (gameBoard[row, col] == EMPTY_CELL)
                {
                    return false;
                }
            }
        }
        return true;
    }

    // --- Main Game Loop ---

    static void Main(string[] args)
    {
        // Basic settings and drawing the initial field
        InitializeBoard();
        int cellSize = 5;
        int fieldWidth = BOARD_SIZE * cellSize;
        int fieldHeight = BOARD_SIZE * cellSize;

        char currentPlayer = PLAYER_X;
        bool gameOver = false;

        Console.OutputEncoding = System.Text.Encoding.UTF8;

        // Main game loop
        while (!gameOver)
        {
            DrawField(fieldWidth, fieldHeight, cellSize);

            Console.SetCursorPosition(0, fieldHeight + 2);
            Console.WriteLine($"Ход игрока {currentPlayer}. Введите номер клетки (1-9): ");

            // User input
            string userInput = Console.ReadLine();
            int chosenCellNumber;

            // Check for correct input - Is it a number? (TryParse)
            if (!int.TryParse(userInput, out chosenCellNumber))
            {
                Console.WriteLine("Ошибка: Введите число!");
                Console.WriteLine("Нажмите любую клавишу для продолжения...");
                Console.ReadKey();
                continue;
            }

            // Convert cell number to row and column
            int chosenRow, chosenCol;
            GetRowColFromCellNumber(chosenCellNumber, out chosenRow, out chosenCol);

            // Check for correct input - within field boundaries
            if (!IsValidMoveCoordinates(chosenRow, chosenCol))
            {
                Console.WriteLine("Ошибка: Номер клетки вне границ поля (1-9).");
                Console.WriteLine("Нажмите любую клавишу для продолжения...");
                Console.ReadKey();
                continue;
            }

            // Check for correct input - Is the cell already occupied?
            if (!IsCellEmpty(chosenRow, chosenCol))
            {
                Console.WriteLine("Ошибка: Эта клетка уже занята. Выберите другую.");
                Console.WriteLine("Нажмите любую клавишу для продолжения...");
                Console.ReadKey();
                continue;
            }

            // Determine where to place the figure and which figure to draw
            PlaceFigure(chosenRow, chosenCol, currentPlayer);

            // Determine who won or if it's a draw
            if (CheckForWin(currentPlayer))
            {
                DrawField(fieldWidth, fieldHeight, cellSize);
                Console.SetCursorPosition(0, fieldHeight + 2);
                Console.WriteLine($"Игрок {currentPlayer} победил! Поздравляем!");
                gameOver = true;
            }
            else if (CheckForDraw())
            {
                DrawField(fieldWidth, fieldHeight, cellSize);
                Console.SetCursorPosition(0, fieldHeight + 2);
                Console.WriteLine("Ничья! Игра окончена.");
                gameOver = true;
            }
            else
            {
                // Switch to the next player
                currentPlayer = (currentPlayer == PLAYER_X) ? PLAYER_O : PLAYER_X;
            }
        }

        Console.WriteLine("Нажмите любую клавишу, чтобы выйти...");
        Console.ReadKey();
    }
}
