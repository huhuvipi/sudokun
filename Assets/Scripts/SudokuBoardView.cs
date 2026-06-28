using UnityEngine;
using UnityEngine.InputSystem;
public class SudokuBoardView : MonoBehaviour
{
    [SerializeField] private GameObject cellPrefab;
    public static SudokuBoardView Instance;
    private SudokuCell selectedCell;
    public SudokuCell CurrentSelectedCell { get { return selectedCell; } }
    private SudokuCell[,] cells = new SudokuCell[9, 9];
    private Transform gridRoot;
    private Transform lineRoot;
    private readonly int[,] puzzle = {

        {5,3,0,0,7,0,0,0,0},

        {6,0,0,1,9,5,0,0,0},

        {0,9,8,0,0,0,0,6,0},

        {8,0,0,0,6,0,0,0,3},

        {4,0,0,8,0,3,0,0,1},

        {7,0,0,0,2,0,0,0,6},

        {0,6,0,0,0,0,2,8,0},

        {0,0,0,4,1,9,0,0,5},

        {0,0,0,0,8,0,0,7,9}

    };

    private readonly int[,] solution =
    {

        {5,3,4,6,7,8,9,1,2},

        {6,7,2,1,9,5,3,4,8},

        {1,9,8,3,4,2,5,6,7},

        {8,5,9,7,6,1,4,2,3},

        {4,2,6,8,5,3,7,9,1},

        {7,1,3,9,2,4,8,5,6},

        {9,6,1,5,3,7,2,8,4},

        {2,8,7,4,1,9,6,3,5},

        {3,4,5,2,8,6,1,7,9}

    };
    private void Awake()
    {
        Instance = this;
        GameObject grid = new GameObject("Grid");

        grid.transform.SetParent(transform, false);

        gridRoot = grid.transform;

        GameObject lines = new GameObject("GridLines");

        lines.transform.SetParent(transform, false);

        lineRoot = lines.transform;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CreateBoard();
    }

    private void CreateBoard()
    {
        for (int row = 0; row < 9; row++)
        {
            for (int col = 0; col < 9; col++)
            {
                GameObject obj = Instantiate(cellPrefab, transform);
                SudokuCell cell = obj.GetComponent<SudokuCell>();
                cell.Initialize(this, row, col);
                int number = puzzle[row, col];
                cell.LoadNumber(number);
                cell.SetFixed(number != 0);
                cells[row, col] = cell;
            }
        }
    }

    public void SelectedCell(SudokuCell cell)
    {
        // Handle cell selection logic here
        Debug.Log("Selected cell: " + cell.name);
        if (selectedCell != null)
        {
            selectedCell.SetSelected(false);
        }
        selectedCell = cell;
        selectedCell.SetSelected(true);
            
    }

    public void InputNumber(int value)

    {

        if (selectedCell == null)

            return;

        if (selectedCell.IsFixed)

            return;

        selectedCell.InputNumber(value);

        CheckBoardCompleted();

    }

    private void CheckBoardCompleted()

    {

        for (int row = 0; row < 9; row++)

        {

            for (int col = 0; col < 9; col++)

            {

                if (cells[row, col].CurrentValue == 0)

                    return;

            }

        }

        CheckWin();

    }

    private void CheckWin()

    {

        for (int row = 0; row < 9; row++)

        {

            for (int col = 0; col < 9; col++)

            {

                if (cells[row, col].CurrentValue != solution[row, col])

                {

                    Debug.Log("Chưa đúng");

                    return;

                }

            }

        }

        Debug.Log("YOU WIN!");

    }
    
}