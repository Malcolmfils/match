using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class NodePiece : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public int value;
    public Point index;

    [HideInInspector]
    public Vector2 pos;
    [HideInInspector]
    public RectTransform rect;

    private Vector2 originalPosition;
    

    bool updating;
    Image img;

    public void Initialize(int v, Point p, Sprite piece)
    {
        img = GetComponent<Image>();
        rect = GetComponent<RectTransform>();

        value = v;
        SetIndex(p);
        img.sprite = piece;
    }

    public void SetIndex(Point p)
    {
        index = p;
        ResetPosition();
        UpdateName();
    }

    public void ResetPosition()
    {
        pos = new Vector2(32 + (100 * index.x), -32 - (100 * index.y));
    }

    public void MovePosition(Vector2 move)
    {
        rect.anchoredPosition += move * Time.deltaTime * 16f;
    }

    public void MovePositionTo(Vector2 move)
    {
        rect.anchoredPosition = Vector2.Lerp(rect.anchoredPosition, move, Time.deltaTime * 16f);
    }

    public bool UpdatePiece()
    {
        if(Vector3.Distance(rect.anchoredPosition, pos) > 10)
        {
            MovePositionTo(pos);
            updating = true;
            return true;
        }
        else
        {
            rect.anchoredPosition = pos;
            updating = false;
            return false;
        }
    }

    void UpdateName()
    {
        transform.name = "Node [" + index.x + ", " + index.y + "]";
    }

    public void OnPointerDown(PointerEventData eventData)
{
    if (updating || !CanFormMatch(this)) return;

    MovePieces.instance.MovePiece(this);
    // Store the original position before the move
    originalPosition = rect.anchoredPosition;
}

    public void OnPointerUp(PointerEventData eventData)
    {
        // Directly check if a match was formed
        if (!MatchFormed())
        {
            // If no match was formed, reset the piece to its original position
            ResetPiecePosition();
        }
    }

    bool CanFormMatch(NodePiece piece)
    {
        // Logic to check if the selected piece can form a match
        // This might involve checking adjacent pieces and the current state of the board
        return true; // Placeholder - replace with actual match checking logic
    }

    void ResetPiecePosition()
    {
        // Logic to reset the piece's position to its original location before the move
        rect.anchoredPosition = originalPosition;
    }

    bool MatchFormed()
    {
        // Logic to check if the board state includes a new match after the piece was moved
        // This should check the state of the board and determine if the move resulted in a match
        return true; // Placeholder - replace with actual logic to determine if a match was formed
    }
}