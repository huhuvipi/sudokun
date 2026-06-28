using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NumberButton : MonoBehaviour
{
    [SerializeField] private int value;
    
    private SudokuBoardView boardView;

    public void OnClick()
    {
        this.boardView = SudokuBoardView.Instance;
        // Handle number button click event here
        if (boardView != null)
        {
            boardView.InputNumber(value);
        }
    }
}
