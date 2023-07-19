using UnityEngine.UI;
using UnityEngine;
using TMPro;
public class HomeScreen : A_Screen
{
    [SerializeField] private Toggle hapticButton;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI coinsText;

    private void Start()
    {
        hapticButton.onValueChanged.AddListener(canvasManager.playerData.toggleHaptic);
        hapticButton.onValueChanged.AddListener(canvasManager.gameManager.vibrateDevice);
        hapticButton.isOn = canvasManager.playerData.haptic;
        updateUIText();
    }

    private void OnEnable()
    {
        updateUIText();
    }

    private void updateUIText()
    {
        levelText.text = "Level " + canvasManager.playerData.level.ToString();
        coinsText.text = canvasManager.playerData.coins.ToString();
    }
}
