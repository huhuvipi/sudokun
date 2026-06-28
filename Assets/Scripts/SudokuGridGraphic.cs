using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasRenderer))]
public class SudokuGridGraphic : Graphic
{
    [Header("Grid")]
    [SerializeField] private int gridSize = 9;

    [Header("Line Width")]
    [SerializeField] private float thinLineWidth = 1f;
    [SerializeField] private float thickLineWidth = 3f;

    [Header("Color")]
    [SerializeField] private Color thinLineColor = new Color(0.75f, 0.75f, 0.75f);
    [SerializeField] private Color thickLineColor = Color.black;

    protected override void OnPopulateMesh(VertexHelper vh)
    {
        vh.Clear();

        Rect rect = GetPixelAdjustedRect();

        float cellWidth = rect.width / gridSize;
        float cellHeight = rect.height / gridSize;

        float left = rect.xMin;
        float right = rect.xMax;
        float bottom = rect.yMin;
        float top = rect.yMax;

        // Vertical Lines
        for (int i = 0; i <= gridSize; i++)
        {
            bool thick = (i == 0 || i == gridSize || i % 3 == 0);

            float width = thick ? thickLineWidth : thinLineWidth;
            Color color = thick ? thickLineColor : thinLineColor;

            float x = left + i * cellWidth;

            AddQuad(
                vh,
                new Vector2(x - width * 0.5f, bottom),
                new Vector2(x + width * 0.5f, top),
                color);
        }

        // Horizontal Lines
        for (int i = 0; i <= gridSize; i++)
        {
            bool thick = (i == 0 || i == gridSize || i % 3 == 0);

            float width = thick ? thickLineWidth : thinLineWidth;
            Color color = thick ? thickLineColor : thinLineColor;

            float y = bottom + i * cellHeight;

            AddQuad(
                vh,
                new Vector2(left, y - width * 0.5f),
                new Vector2(right, y + width * 0.5f),
                color);
        }
    }

    private void AddQuad(
        VertexHelper vh,
        Vector2 bottomLeft,
        Vector2 topRight,
        Color color)
    {
        int startIndex = vh.currentVertCount;

        UIVertex vert = UIVertex.simpleVert;
        vert.color = color;

        vert.position = new Vector3(bottomLeft.x, bottomLeft.y);
        vh.AddVert(vert);

        vert.position = new Vector3(bottomLeft.x, topRight.y);
        vh.AddVert(vert);

        vert.position = new Vector3(topRight.x, topRight.y);
        vh.AddVert(vert);

        vert.position = new Vector3(topRight.x, bottomLeft.y);
        vh.AddVert(vert);

        vh.AddTriangle(startIndex, startIndex + 1, startIndex + 2);
        vh.AddTriangle(startIndex, startIndex + 2, startIndex + 3);
    }

#if UNITY_EDITOR
    protected override void OnValidate()
    {
        base.OnValidate();
        SetVerticesDirty();
    }
#endif
}