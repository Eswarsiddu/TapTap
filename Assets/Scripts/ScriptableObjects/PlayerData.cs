using UnityEngine;

[System.Serializable]
public class PlayerDataSave
{
    public int level;
    public int coins;
    public bool haptic;
    public PlayerDataSave(int level, int coins, bool haptic)
    {
        this.level = level;
        this.coins = coins;
        this.haptic = haptic;
    }
}

[CreateAssetMenu(fileName = "PlayerData", menuName = "Scriptable Objects/PlayerData")]
public class PlayerData : ScriptableObject
{
    [SerializeField] private int _level;
    [SerializeField] private int _coins;
    [SerializeField] private bool _haptic;

    public int level => _level;
    public int coins => _coins;
    public bool haptic => _haptic;

    public void LoadData()
    {
        PlayerDataSave data = SaveSystem.Load<PlayerDataSave>(DATAKEYS.PLAYERDATA);
        if (data == default)
        {
            _level = 1;
            _coins = 10;
            _haptic = true;
            return;
        }
        _level = data.level;
        _coins = data.coins;
        _haptic = data.haptic;
    }

    public void SaveData()
    {
        SaveSystem.Save<PlayerDataSave>(new PlayerDataSave(_level, _coins, _haptic), DATAKEYS.PLAYERDATA);
    }

    public void increseLevel() => _level += 1;
    public void increaseCoins(int _Coins) => _coins += _Coins;
    public void toggleHaptic(bool _Haptic) => _haptic = _Haptic;

    public bool decreaseCoins(int _Coins)
    {
        if (_Coins > _coins)
        {
            return false;
        }
        _coins -= _Coins;
        return true;
    }
}
