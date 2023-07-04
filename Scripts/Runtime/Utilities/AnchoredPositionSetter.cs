using UnityEngine;

public class AnchoredPositionSetter : MonoBehaviour
{
    [SerializeField] private RectTransform _rectTransform;

    public void SetYAnchoredPosition(int _yPosition)
    {
        _rectTransform.anchoredPosition = new Vector2(_rectTransform.anchoredPosition.x, _yPosition);
    }
    
    public void SetXAnchoredPosition(int _xPosition)
    {
        _rectTransform.anchoredPosition = new Vector2(_xPosition, _rectTransform.anchoredPosition.y);
    }
}
