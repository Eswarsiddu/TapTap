using UnityEngine;

[System.Serializable]
public class LevelInfoData
{
    public int index;
    public LevelInfoData(int index)
    {
        this.index = index;
    }
}

[System.Serializable]
public class LevelInfo
{
    [SerializeField] private int _maxLevelNumber;

    [Space]
    [SerializeField] private int _time;

    [Space]
    [Range(1, 5)]
    [SerializeField] private int _noOfTargets;

    [Space]
    [SerializeField] private int _speed;


    [Header("Target Count ")]
    [SerializeField] private int _targetCount;
    // [SerializeField] private int _extra = 5;


    [Header("Reward Coins")]
    [SerializeField] private int _minCoins;
    [SerializeField] private int _maxCoins;


    [Header("Target Type")]
    [SerializeField] private bool standard;
    [SerializeField] private bool horizontal;
    [SerializeField] private bool vertical;
    [SerializeField] private bool diagonal;


    public int maxLevelNumber => _maxLevelNumber;
    public int time => _time;
    public int noOfTargets => _noOfTargets;

    public int coins => Constants.RandomInt(_minCoins, _maxCoins);

    public int targetCount => Constants.RandomInt(_targetCount, _targetCount + (_maxLevelNumber <= 2 ? 0 : 5));
    public float speed => _speed;
    public TARGETTYPES targetType
    {
        get
        {
            TARGETTYPES targettype = 0;
            if (standard) targettype |= TARGETTYPES.STANDARD;
            if (vertical) targettype |= TARGETTYPES.VERTICAL;
            if (horizontal) targettype |= TARGETTYPES.HORIZONTAL;
            if (diagonal) targettype |= TARGETTYPES.DIAGONAL;
            return targettype;
        }
    }
}

[CreateAssetMenu(fileName = "LevelsData", menuName = "ScriptableObjects/LevelsData")]
public class LevelsData : ScriptableObject
{
    [SerializeField] private LevelInfo[] levelsInfo;
    [SerializeField] private int index = -1;

    public LevelInfo getLevelInfo(int level)
    {
        if (index == levelsInfo.Length - 1)
        {
            return levelsInfo[index];
        }

        for (int i = index; i < levelsInfo.Length; i++)
        {
            if (index == -1) continue;

            if (levelsInfo[i].maxLevelNumber >= level)
            {
                index = i;
                break;
            }
        }
        return levelsInfo[index];
    }

    public void LoadData()
    {
        LevelInfoData data = SaveSystem.Load<LevelInfoData>(DATAKEYS.LEVELSDATA);
        if (data == default)
        {
            return;
        }
        else
        {
            index = data.index;
        }
    }

    public void SaveData()
    {
        SaveSystem.Save<LevelInfoData>(new LevelInfoData(index), DATAKEYS.LEVELSDATA);
    }


}
