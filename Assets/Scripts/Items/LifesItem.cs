using System;
using UnityEngine;

public class LifesItem : BaseItem
{
    #region Variables

    [Header("Base settings")] [SerializeField]
    private int lifesNumber = 1;

    #endregion

    #region Properties

    public int LifesNumber => lifesNumber;

    #endregion

    #region Events

    public static event Action<LifesItem> OnCatched;

    #endregion

    protected override void HandleCatch()
    {
        OnCatched?.Invoke(this);
    }

    protected override void HandleLoss()
    {
    }
}