using System.Collections;
using NUnit.Framework;

namespace consolidation_csharp_lesson_12;

public class TestChallenges
{
    [TestCaseSource(nameof(LatinSquareTestCases))]
    public bool SolveLatinSquare_SolvesValidLatinSquare(int[,] latinSquare)
    {
        // Act
        bool isSolved = LatinSquareSolver.SolveLatinSquare(latinSquare);

        // Assert
        return isSolved && IsLatinSquareValid(latinSquare);
    }

    public static IEnumerable LatinSquareTestCases()
    {
        yield return new TestCaseData(new[] {
            new int[,]
            {
                { 0, 0, 0},
                { 0, 0, 0},
                { 0, 0, 0},
            }
        }).Returns(true);

        yield return new TestCaseData(new[] {
            new int[,]
            {
                { 0 }
            }
        }).Returns(true);

        yield return new TestCaseData(new[] {
            new int[,]
            {
                { 0, 4, 0, 3},
                { 0, 0, 4, 0 },
                { 2, 0, 0, 0 },
                { 0, 0, 1, 0 }
            }
        }).Returns(true);

        yield return new TestCaseData(new[] {
            new int[,]
            {
                { 0, 4, 0, 3 },
                { 0, 0, 4, 0 },
                { 2, 0, 0, 0 },
                { 0, 1, 1, 0 }
            }
        }).Returns(false);
    }

    [TestCaseSource(nameof(SudokuTestCases))]
    public bool TestSolveSudoku(int?[][] sudoku)
    {
        // Act
        SudokuSolver.SolveSudoku(sudoku);

        // Assert
        return IsSudokuSolved(sudoku);
    }

    public static IEnumerable SudokuTestCases()
    {
        yield return new TestCaseData(new[] {
            new int?[][]
            {
                new int?[] { null, null, null, 8, null, 1, null, null, null },
                new int?[] { null, null, null, null, null, null, null, 4, 3 },
                new int?[] { 5, null, null, null, null, null, null, null, null },
                new int?[] { null, null, null, null, 7, null, 8, null, null },
                new int?[] { null, null, null, null, null, null, 1, null, null },
                new int?[] { null, 2, null, null, 3, null, null, null, null },
                new int?[] { 6, null, null, null, null, null, null, 7, 5 },
                new int?[] { null, null, 3, 4, null, null, null, null, null },
                new int?[] { null, null, null, 2, null, null, 6, null, null },
            }
        })
            .Returns(true);

        yield return new TestCaseData(new[] {
            new int?[][]
            {
                new int?[] { 8, null, null, 8, null, 1, null, null, null },
                new int?[] { null, null, null, null, null, null, null, 4, 3 },
                new int?[] { 5, null, null, null, null, null, null, null, null },
                new int?[] { null, null, null, null, 7, null, 8, null, null },
                new int?[] { null, null, null, null, null, null, 1, null, null },
                new int?[] { null, 2, null, null, 3, null, null, null, null },
                new int?[] { 6, null, null, null, null, null, null, 7, 5 },
                new int?[] { null, null, 3, 4, null, null, null, null, null },
                new int?[] { null, null, null, 2, null, null, 6, null, null },
            }
        })
            .Returns(false);

        yield return new TestCaseData(new[] {
            new int?[][]
            {
                new int?[] { null, null, 8, 9, null, 2, null, null, null },
                new int?[] { null, 4, 1, null, 7, 6, null, null, null },
                new int?[] { 2, null, null, null, null, 4, 7, 3, null },
                new int?[] { null, null, 2, null, 9, null, 6, null, 3 },
                new int?[] { null, null, null, null, null, 7, 4, null, null },
                new int?[] { null, null, null, 5, 8, null, null, null, null },
                new int?[] { null, null, null, 7, 2, null, null, 1, null },
                new int?[] { null, 1, null, null, null, 8, 2, null, null },
                new int?[] { null, null, null, null, null, null, 5, 6, 7 },
            }
        })
            .Returns(true);

        yield return new TestCaseData(new[] {
            new int?[][]
            {
                new int?[] { null, null, 8, 9, null, 2, null, null, null },
                new int?[] { null, 4, 1, null, 7, 6, null, null, null },
                new int?[] { 2, null, null, null, null, 4, 7, 3, null },
                new int?[] { null, null, 2, null, 9, null, 6, null, 3 },
                new int?[] { null, null, null, null, 7, 4, null, null, null },
                new int?[] { null, null, null, 5, 8, null, null, null, null },
                new int?[] { null, null, null, 7, 2, null, null, 1, null },
                new int?[] { null, 1, null, null, null, 8, 2, null, null },
                new int?[] { null, null, null, null, null, null, 5, 6, 7 },
            }
        })
            .Returns(false);
    }

    const int SudokuDimension = 9;
    const int BoxDimension = 3;

    private IEnumerable<int?> GetColumn(int?[][] sudoku, int columnIndex)
    {
        return sudoku.Select(row => row[columnIndex]);
    }

    private IEnumerable<int?> GetBox(int?[][] sudoku, int boxIndex)
    {
        int startRow = (boxIndex / BoxDimension) * BoxDimension;
        int startCol = (boxIndex % BoxDimension) * BoxDimension;

        for (int i = 0; i < BoxDimension; i++)
        {
            for (int j = 0; j < BoxDimension; j++)
            {
                yield return sudoku[startRow + i][startCol + j];
            }
        }
    }

    private bool IsSudokuSolved(int?[][] sudoku)
    {
        // Validate rows, columns, and boxes
        for (int i = 0; i < SudokuDimension; i++)
        {
            if (!IsValidSet(sudoku[i]) || !IsValidSet(GetColumn(sudoku, i)) || !IsValidSet(GetBox(sudoku, i)))
            {
                return false;
            }
        }

        return true;
    }

    private bool IsValidSet(IEnumerable<int?> set)
    {
        HashSet<int?> uniqueNumbers = new HashSet<int?>();
        foreach (int? number in set)
        {
            if (number != null && !uniqueNumbers.Add(number))
            {
                return false;
            }
        }
        return true;
    }

    private bool IsLatinSquareValid(int[,] latinSquare)
    {
        int n = latinSquare.GetLength(0);

        // Check each row and column
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                for (int k = j + 1; k < n; k++)
                {
                    if (latinSquare[i, j] == latinSquare[i, k] || latinSquare[j, i] == latinSquare[k, i])
                        return false;
                }
            }
        }

        return true;
    }
}