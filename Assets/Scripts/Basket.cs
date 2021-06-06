using UnityEngine;

public class Basket : MonoBehaviour
{
    #region Variables

    [SerializeField] private SpriteRenderer spriteRenderer;

    private float horizontalLimit;

    #endregion

    #region Unity lifecycle

    private void Start()
    {
        CalcHorizontalLimit();
    }

    private void Update()
    {
        if (Game.IsPaused || Game.IsFinished)
            return;

        UpdatePosition();
    }

    #endregion

    #region Private methods

    private void CalcHorizontalLimit()
    {
        Vector2 screenSizeWorld = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        float basketWidthWorld = spriteRenderer.bounds.size.x;

        horizontalLimit = screenSizeWorld.x - basketWidthWorld / 2;
    }

    private void UpdatePosition()
    {
        Vector3 pixelsPos = Input.mousePosition;
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(pixelsPos);

        Vector3 basketPos = new Vector3(worldPos.x, transform.position.y, 0);
        basketPos.x = Mathf.Clamp(basketPos.x, -horizontalLimit, horizontalLimit);

        transform.position = basketPos;
    }

    #endregion
}
