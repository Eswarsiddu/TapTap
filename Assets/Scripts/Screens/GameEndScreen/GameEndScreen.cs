using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class GameEndScreen : A_Screen
{
    [SerializeField] private TextMeshProUGUI gameEndStatus;
    [SerializeField] private TextMeshProUGUI prevText;
    [SerializeField] private TextMeshProUGUI nextText;
    [SerializeField] private GameObject correctImage;
    [SerializeField] private GameObject wrongImage;
    [SerializeField] private TextMeshProUGUI achivedCoins;
    [SerializeField] private GameObject achivedCoinsObject;

    [SerializeField] private Sprite nextSprite;
    [SerializeField] private Sprite restartSprite;

    [SerializeField] private Image nextButtonImage;
    [SerializeField] private GameObject canPurchaseInStoreObject;

    public void endGameResults(GameReward gameReward)
    {
        int nextLevel = canvasManager.playerData.level;
        if (gameReward.gameEnd == true)
        {
            achivedCoinsObject.SetActive(true);
            achivedCoins.text = "Hurray! +" + gameReward.coins.ToString();
            canvasManager.playerData.increaseCoins(gameReward.coins);
            correctImage.SetActive(true);
            wrongImage.SetActive(false);
            gameEndStatus.text = "VICTORY";
            nextButtonImage.sprite = nextSprite;
        }
        else
        {
            achivedCoinsObject.SetActive(false);
            nextLevel++;
            correctImage.SetActive(false);
            wrongImage.SetActive(true);
            gameEndStatus.text = "TRY AGAIN";
            nextButtonImage.sprite = restartSprite;
        }
        int prevLevel = nextLevel - 1;

        prevText.text = prevLevel.ToString();
        nextText.text = nextLevel.ToString();

        canPurchaseInStoreObject.SetActive(canvasManager.gameManager.canPurchaseAny());
    }

    //TODO: implement can purchase in gameend
}