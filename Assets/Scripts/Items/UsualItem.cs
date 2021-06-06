using System;
using UnityEngine;

public class UsualItem : BaseItem
{
    #region Variables

    [Header("Base Settings")] [SerializeField]
    private int scoreForCatch = 10;

    #endregion

    #region Properties

    public int ScoreForCatch => scoreForCatch;

    #endregion

    #region Events

    public static event Action<UsualItem> OnCatched;
    public static event Action<UsualItem> OnCatchFailed;

    #endregion

    protected override void HandleCatch()
    {
        OnCatched?.Invoke(this);
    }


    protected override void HandleLoss()
    {
        if (scoreForCatch > 0)
        {
            OnCatchFailed?.Invoke(this);
        }
    }
}