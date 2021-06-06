using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class ScoreItem : BaseItem
{
    #region Variables

    [Header("Base settings")] [SerializeField]
    private int[] possibleScores = new int[] {25, 50, 75, 100, 125};


    private int score;

    #endregion

    #region Properties

    public int Score => score;

    #endregion

    #region Events

    public static event Action<ScoreItem> OnCatched;

    #endregion

    protected override void HandleCatch()
    {
        int randIndex = Random.Range(0, possibleScores.Length);
        score = possibleScores[randIndex];

        OnCatched?.Invoke(this);
    }

    protected override void HandleLoss()
    {
    }
}