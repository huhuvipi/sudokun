using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SudokuCell : MonoBehaviour
{
    [SerializeField] private TMP_Text cellText;
    [SerializeField] private Image backgroundImage;
    private SudokuBoardView boardView;
    public bool IsFixed;
    public int Row { get; private set; }
    public int Column { get; private set; }
    private int currentNumber;
    public int CurrentValue { get;  private set; }

    public void Initialize(SudokuBoardView boardView, int row, int column)
    {
        this.boardView = boardView;
        this.Row = row;
        this.Column = column;
    }

    public void SetFixed(bool fixedCell)
    {
        IsFixed = fixedCell;
        cellText.color = fixedCell ? Color.black : new Color(0.2f,0.4f,1f);
    }

    public void LoadNumber(int number)
    {
        currentNumber = number;
        cellText.text = number.ToString();
        if (number <= 0)
        {
            cellText.text = "";
        }
        else
        {
            cellText.text = number.ToString();
        }
    }

    public void InputNumber(int number)
    {
        if (IsFixed)
        {
            Debug.LogWarning("Cannot change the number of a fixed cell.");
            return;
        }
        currentNumber = number;
        cellText.text = number.ToString();
        if (number <= 0)
        {
            cellText.text = "";
        }
        else
        {
            cellText.text = number.ToString();
        }
    }

    public void OnClick()
    {
        Debug.Log(boardView);
        // Handle cell click event here
        boardView.SelectedCell(this);
    }

    public void SetSelected(bool selected)
    {
        // Handle cell click event here
        Debug.Log("Cell clicked: " + selected);
        backgroundImage.color = selected ? Color.yellow : Color.white;
    }

}