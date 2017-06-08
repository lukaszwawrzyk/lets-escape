using UnityEngine;

public class GuiCrosshair : MonoBehaviour
{
    [SerializeField]
    private Texture2D _crosshairTexture;
        
    private Rect _position;
    private Vector2 _lastWindowSize;

    private void Start()
    {
        _lastWindowSize = new Vector2(Screen.width, Screen.height);
        CalculateCrosshair();
    }

    private void FixedUpdate()
    {
        if (_lastWindowSize.x != Screen.width || _lastWindowSize.y != Screen.height)
        {
            _lastWindowSize = new Vector2(Screen.width, Screen.height);
            CalculateCrosshair();
        }
    }

    private void CalculateCrosshair()
    {
        var posX = (_lastWindowSize.x - _crosshairTexture.width) / 2;
        var posY = (_lastWindowSize.y - _crosshairTexture.height) / 2;
        _position = new Rect(posX, posY,
            _crosshairTexture.width, _crosshairTexture.height);
    }

    private void OnGUI()
    {
        GUI.DrawTexture(_position, _crosshairTexture);
    }
}