using UnityEngine;
using System.Collections.Generic;
using System;

[System.Serializable]
public class AssetStoreData
{
    public List<bool> lockedStatus;
    public int index;

    public AssetStoreData(List<bool> lockedStatus, int index)
    {
        this.lockedStatus = lockedStatus;
        this.index = index;
    }
}


[System.Serializable]
public class TargetTexure
{
    [SerializeField] private Sprite _sprite;
    [SerializeField] private Color _textColor = Color.black;
    [SerializeField] private int _cost;
    [SerializeField] private bool _isLocked;

    public Sprite sprite => _sprite;
    public Color textColor => _textColor;
    public int cost => _cost;
    public bool isLocked => _isLocked;
    // public void puchasedTile() => _isLocked = false;
    public void LockedStatus(bool status) => _isLocked = status;
}

[CreateAssetMenu(fileName = "AssetsStore", menuName = "ScriptableObjects/AssetsStore")]
public class AssetsStore : ScriptableObject
{
    [SerializeField] private TargetTexure[] targetTexures;
    public int targetsLength => targetTexures.Length;
    private int _index = 0;
    public int index => _index;
    public TargetTexure currentTexure => targetTexures[_index];
    public TargetTexure getTexure(int ind) => targetTexures[ind];

    internal void updateSelectedIndex(int ind)
    {
        _index = ind;
    }

    public void LoadData()
    {
        AssetStoreData data = SaveSystem.Load<AssetStoreData>(DATAKEYS.ASSETSSTORE);
        if (data == default)
        {
            Debug.Log("null data on assetload");
            return;
        }

        _index = data.index;
        for (int i = 0; i < data.lockedStatus.Count; i++)
        {
            targetTexures[i].LockedStatus(data.lockedStatus[i]);
        }
    }

    public void SaveData()
    {
        List<bool> lockedStatus = new List<bool>();
        foreach (TargetTexure texure in targetTexures)
        {
            lockedStatus.Add(texure.isLocked);
        }
        SaveSystem.Save<AssetStoreData>(new AssetStoreData(lockedStatus, _index), DATAKEYS.ASSETSSTORE);
    }

    internal void purchaseTarget(int ind)
    {
        targetTexures[ind].LockedStatus(false);
    }

    internal bool isLocked(int ind) => targetTexures[ind].isLocked;

    internal bool canPurchaseAny(int coins)
    {
        for (int i = 0; i < targetTexures.Length; i++)
        {
            if (targetTexures[i].isLocked == true)
            {
                if (targetTexures[i].cost <= coins)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        return false;
    }
}
