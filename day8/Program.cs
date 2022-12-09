using System.IO;
using System.Diagnostics;

if (args.Length != 1)
{
    Console.WriteLine("Usage: day8 <file>");
    return;
}

// Read in the lines
var lines = File.ReadAllText(args[0]).ReplaceLineEndings("\n").Split("\n").Select(x => x.Trim()).ToArray();

// Generate the tree array
var trees = new int[lines.Length][];
var scores = new int[lines.Length][];
for (int row = 0; row < lines.Length; row++ )
{
    trees[row] = lines[row].ToArray().Select(x => int.Parse($"{x}")).ToArray();
    scores[row] = new int[lines.Length];
}

PrintGrid(trees);
Console.WriteLine();

// Step 1
int treesVisible = PermieterLook(trees);
Console.WriteLine($"There are {treesVisible} trees visible from the permiter");
Console.WriteLine();

// Step 2
CalculateScenicScore(trees, scores);
var (bestScore, bestRow, bestCol) = FindHighestScore(scores);
Console.WriteLine($"The best scenic score is {bestScore} at {bestRow},{bestCol}");

(int, int, int) FindHighestScore(int[][] scores)
{
    int bestScore = -1;
    int bestRow = -1;
    int bestCol = -1;
    for (int row = 0; row < trees.Length; row++)
    {
        for (int col = 0; col < trees[row].Length; col++)
        {
            if (scores[row][col] > bestScore)
            {
                bestScore = scores[row][col];
                bestRow = row;
                bestCol = col;
            }
        }
    }

    return (bestScore, bestRow, bestCol);
}

void CalculateScenicScore(int[][] trees, int[][] scores)
{
    for (int row = 0; row < trees.Length; row++)
    {
        for (int col = 0; col < trees[row].Length; col++)
        {
            int left = CountVisibleTrees(row, col, Direction.LEFT, trees);
            int right = CountVisibleTrees(row, col, Direction.RIGHT, trees);
            int up = CountVisibleTrees(row, col, Direction.UP, trees);
            int down = CountVisibleTrees(row, col, Direction.DOWN, trees);
            scores[row][col] = left * right * up * down;
        }
    }
}

int PermieterLook(int[][] trees)
{
    int treesVisible = 0;
    // Pick a tree, and look at it from all directions to see if it's visible
    for (int row = 0; row < trees.Length; row++)
    {
        for (int col = 0; col < trees[row].Length; col++) 
        {
            // Look from the top
            if (IsTreeVisible(-1, col, row, col, trees))
            {
                treesVisible++;
                continue;
            }

            // Look from the bottom
            if (IsTreeVisible(trees.Length, col, row, col, trees))
            {
                treesVisible++;
                continue;
            }

            // Look from the left
            if (IsTreeVisible(row, -1, row, col, trees))
            {
                treesVisible++;
                continue;
            }

            // Look from the right
            if (IsTreeVisible(row, trees[row].Length, row, col, trees))
            {
                treesVisible++;
                continue;
            }
        }
    }

    return treesVisible;
}

// Helpers
void PrintGrid(int[][] grid)
{
    for (int row = 0; row < grid.Length; row++)
    {
        for (int col = 0; col < grid[row].Length; col++)
        {
            Console.Write($"{grid[row][col]}");
        }
        Console.WriteLine();
    }
}

int CountVisibleTrees(int treeRow, int treeCol, Direction dir, int[][] trees)
{
    int count = 0;
    if (dir == Direction.UP || dir == Direction.DOWN)
    {
        int movement = (dir == Direction.UP ? -1 : 1);
        for (int row = treeRow + movement; row != -1 && row < trees.Length; row += movement)
        {
            count++;
            if (trees[row][treeCol] >= trees[treeRow][treeCol])
                break;
        }
    }
    else
    {
        int movement = (dir == Direction.LEFT ? -1 : 1);
        for (int col = treeCol + movement; col != -1 && col < trees[treeRow].Length; col += movement)
        {
            count++;
            if (trees[treeRow][col] >= trees[treeRow][treeCol])
                break;
        }
    }

    return count;
}

bool IsTreeVisible(int viewRow, int viewCol, int treeRow, int treeCol, int[][] trees)
{
    // Since we can only look in straight lines, that means either the row
    // or column needs to be the same as the tree. If it's a row, we're looking
    // up or down. And if it's a column, we're looking left or right

    if (viewRow == treeRow) 
    {
        int movement = viewCol < treeCol ? 1 : -1;

        Debug.WriteLine($"At row {viewRow} looking {(movement == 1 ? "right" : "left")} at {treeCol}");

        // Loop within the columns to find the tree
        int height = -1;
        for (int col = viewCol + movement; col != treeCol; col += movement)
        {
            Debug.WriteLine($"Tree {treeRow},{col} is {trees[treeRow][col]}, max is {height}");
            if (trees[treeRow][col] > height)
            {
                height = trees[treeRow][col];
            }
        }

        if (trees[treeRow][treeCol] > height)
        {
            Debug.WriteLine($"Tree {treeRow},{treeCol} is visible from {viewRow},{viewCol}");
            return true;
        }
        else
        {
            Debug.WriteLine($"Tree {treeRow},{treeCol} is NOT visible from {viewRow},{viewCol}");
            return false;
        }
    } 
    else if (viewCol == treeCol) // Up or down
    {
        int movement = viewRow < treeRow ? 1 : -1;

        Debug.WriteLine($"At column {viewCol} looking {(movement == 1 ? "down" : "up")} at {treeRow}");

        // Loop within the columns to find the tree
        int height = -1;
        for (int row = viewRow + movement; row != treeRow; row += movement)
        {
            Debug.WriteLine($"Tree {row},{treeCol} is {trees[row][treeCol]}, max is {height}");
            if (trees[row][treeCol] > height)
            {
                height = trees[row][treeCol];
            }
        }

        if (trees[treeRow][treeCol] > height)
        {
            Debug.WriteLine($"Tree {treeRow},{treeCol} is visible from {viewRow},{viewCol}");
            return true;
        }
        else
        {
            Debug.WriteLine($"Tree {treeRow},{treeCol} is NOT visible from {viewRow},{viewCol}");
            return false;
        }

    }
    else
    {
        throw new InvalidDataException("You can only look in a straight line!");
    }
}

enum Direction {
    LEFT,
    RIGHT,
    UP,
    DOWN
};
