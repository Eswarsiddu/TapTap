using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StoreScreen : A_Screen
{
    [SerializeField] private TextMeshProUGUI coinsText;
    [SerializeField] private Transform storeTilesParent;
    [SerializeField] private GameObject notEnoughCoinsPopUp;

    private GameObject storeUITilePrefab;
    private AssetsStore assetStore;
    private List<StoreUITile> storeTiles = new List<StoreUITile>();

    private void Awake()
    {
        storeUITilePrefab = Resources.Load<GameObject>(Constants.STOREUITILE);
        assetStore = Resources.Load<AssetsStore>(Constants.ASSETSSTORE);
        for (int i = 0; i < assetStore.targetsLength; i++)
        {
            GameObject tileObject = Instantiate(storeUITilePrefab, storeTilesParent);
            StoreUITile tile = tileObject.GetComponent<StoreUITile>();
            tile.setData(assetStore, i, selectTile);
            storeTiles.Add(tile);
        }
    }

    public void selectTile(int ind)
    {
        TargetTexure texure = assetStore.getTexure(ind);
        if (assetStore.isLocked(ind))
        {
            if (canvasManager.playerData.coins >= texure.cost)
            {
                canvasManager.playerData.decreaseCoins(texure.cost);
                assetStore.purchaseTarget(ind);
                assetStore.updateSelectedIndex(ind);
            }
            else
            {
                notEnoughCoinsPopUp.SetActive(true);
            }
        }
        else
        {
            assetStore.updateSelectedIndex(ind);
        }
        updateUI();
    }

    public void updateUI()
    {
        foreach (StoreUITile tile in storeTiles)
        {
            tile.updateUI();
        }
        updateUIText();
    }

    private void OnEnable()
    {
        notEnoughCoinsPopUp.SetActive(false);
        updateUI();
    }

    public void updateUIText()
    {
        coinsText.text = canvasManager.playerData.coins.ToString();
    }

    public void goBackToHome()
    {
        canvasManager.openHomeScreen();
    }
}
