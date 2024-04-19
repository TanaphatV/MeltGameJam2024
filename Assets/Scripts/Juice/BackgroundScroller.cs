using UnityEngine;
using UnityEngine.UI;

public class BackgroundScroller : MonoBehaviour
{
    [SerializeField] private RawImage _background;
    [SerializeField] private float _xSpeed, _ySpeed;

    void Update()
    {
        _background.uvRect = new Rect(_background.uvRect.position + new Vector2(_xSpeed, _ySpeed) * Time.deltaTime, _background.uvRect.size);
    }
}