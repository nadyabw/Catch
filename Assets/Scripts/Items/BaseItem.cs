using UnityEngine;

public abstract class BaseItem : MonoBehaviour
{
    #region Variables

    [SerializeField] protected float dropSpeed = 3f;
    [SerializeField] protected float dropSpeedMax = 10f;
    [SerializeField] private float dropSpeedChangeStep = 0.2f;

    private float currentDropSpeed;

    #endregion

    #region Unity lifecycle

    private void Update()
    {
        if (Game.IsPaused || Game.IsFinished)
            return;

        transform.Translate(0, -currentDropSpeed * Time.deltaTime, 0);
    }

    #endregion

    #region Event handlers

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(Tags.Basket))
        {
            HandleCatch();
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag(Tags.Floor))
        {
            HandleLoss();
            Destroy(gameObject);
        }
    }

    #endregion

    public void ChangeDropSpeed(int factor)
    {
        currentDropSpeed = dropSpeed + dropSpeedChangeStep * factor;
        currentDropSpeed = Mathf.Clamp(currentDropSpeed, dropSpeed, dropSpeedMax);
    }

    protected abstract void HandleCatch();
    protected abstract void HandleLoss();
}