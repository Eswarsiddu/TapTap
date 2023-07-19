using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class StoreUITile : MonoBehaviour
{
    [SerializeField] private GameObject selectedBackground;
    [SerializeField] public Button entireButtonClick;
    [Header("Images")]
    [SerializeField] private Image spriteImage;
    [Header("Cost")]
    [SerializeField] private TextMeshProUGUI costText;
    [SerializeField] private GameObject coinsObject;

    private AssetsStore assetStore;
    private int indexNumber;

    public void setData(AssetsStore assetStore, int ind, Action<int> selectTile)
    {
        this.assetStore = assetStore;
        indexNumber = ind;
        entireButtonClick.onClick.AddListener(() => selectTile(ind));
    }

    public void updateUI()
    {
        TargetTexure targetTexure = assetStore.getTexure(indexNumber);
        spriteImage.sprite = targetTexure.sprite;

        if (targetTexure.isLocked)
        {
            costText.text = targetTexure.cost.ToString();
            coinsObject.SetActive(true);
        }
        else
        {
            coinsObject.SetActive(false);
        }

        if (indexNumber == assetStore.index)
        {
            selectedBackground.SetActive(true);
        }
        else
        {
            selectedBackground.SetActive(false);
        }

    }
}
