using UnityEngine;

public class ItemsController : MonoBehaviour
{
    #region Variables

    [Header("Items Generation")] [SerializeField]
    private BaseItem[] usualItems;

    [SerializeField] private BaseItem[] specialItems;

    [Range(1, 100)] [SerializeField] private int specialItemGenerationProbability = 10;
    [SerializeField] private int itemsGeneratedToIncreaseDifficulty = 5;
    [SerializeField] private int itemDropSpeedFactorMax = 30;
    [SerializeField] private float itemGenerationIntervalMin = 0.6f;
    [SerializeField] private float itemGenerationIntervalMax = 1.5f;
    [SerializeField] private float itemGenerationIntervalChangeStep = 0.05f;

    [SerializeField] private float itemMaxSize = 1f;
    private float itemPosXMax;
    private float itemPosYMax;

    private int itemsGenerated;
    private float timeToNewItemGeneration;

    private int itemDropSpeedFactor;
    private float itemGenerationInterval;

    #endregion

    #region Unity lifecycle

    private void Start()
    {
        InitItemGenerationParams();
    }

    private void Update()
    {
        if (Game.IsFinished)
            return;

        UpdateItemGeneration();
    }

    #endregion

    #region Private methods

    private void UpdateItemGeneration()
    {
        timeToNewItemGeneration -= Time.deltaTime;

        if (timeToNewItemGeneration <= 0)
        {
            GenerateNewItem();
            timeToNewItemGeneration = itemGenerationInterval;
        }
    }

    private void InitItemGenerationParams()
    {
        itemsGenerated = 0;
        itemDropSpeedFactor = 0;
        itemGenerationInterval = itemGenerationIntervalMax;
        timeToNewItemGeneration = itemGenerationInterval;

        Vector2 screenSizeWorld = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        itemPosXMax = screenSizeWorld.x - itemMaxSize;
        itemPosYMax = screenSizeWorld.y + itemMaxSize;
    }

    private void GenerateNewItem()
    {
        float posX = Random.Range(itemPosXMax, -itemPosXMax);
        int randIndex;
        int randNum = Random.Range(1, 100);

        BaseItem item;
        if (randNum <= specialItemGenerationProbability)
        {
            randIndex = Random.Range(0, specialItems.Length);
            item = Instantiate(specialItems[randIndex], new Vector3(posX, itemPosYMax, 0), Quaternion.identity);
        }
        else
        {
            randIndex = Random.Range(0, usualItems.Length);
            item = Instantiate(usualItems[randIndex], new Vector3(posX, itemPosYMax, 0), Quaternion.identity);
        }

        itemsGenerated++;
        UpdateDifficultyLevel();

        item.ChangeDropSpeed(itemDropSpeedFactor);
    }

    private void UpdateDifficultyLevel()
    {
        // увеличиваем сложность через каждые itemsGeneratedToIncreaseDifficulty предметов
        if (itemsGenerated % itemsGeneratedToIncreaseDifficulty == 0)
        {
            ChangeDifficultyLevel(1);
        }
    }

    private void ChangeDifficultyLevel(int value)
    {
        itemDropSpeedFactor += value;
        itemDropSpeedFactor = Mathf.Clamp(itemDropSpeedFactor, 0, itemDropSpeedFactorMax);

        itemGenerationInterval -= itemGenerationIntervalChangeStep * value;
        itemGenerationInterval =
            Mathf.Clamp(itemGenerationInterval, itemGenerationIntervalMin, itemGenerationIntervalMax);

        //Debug.Log($"VAL-{value}   DSF-{itemDropSpeedFactor}   IGI-{itemGenerationInterval}");
    }

    #endregion
}