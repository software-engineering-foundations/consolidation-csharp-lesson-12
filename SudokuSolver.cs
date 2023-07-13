namespace consolidation_csharp_lesson_12;

public static class SudokuSolver
{
    const int SudokuDimension = 9;
    const int BoxDimension = 3;

    public static bool WouldBeValid(int?[][] sudoku, int targetRowIndex, int targetColIndex, int number)
    {
        throw new NotImplementedException();
    }

    private static (int, int) GetNextEmptyCell(int?[][] sudoku)
    {
        throw new NotImplementedException();
    }

    private static void PrintSudoku(int?[][] sudoku)
    {
        foreach (var row in sudoku)
        {
            Console.WriteLine(string.Join(" ", row.Select(number => number != null ? number.ToString() : ".")));
        }
    }

    public static void SolveSudoku(int?[][] sudoku)
    {
        throw new NotImplementedException();
    }
}
