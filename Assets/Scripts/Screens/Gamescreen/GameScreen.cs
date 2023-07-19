using UnityEngine;
using TMPro;
public class GameScreen : A_Screen
{
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI resumeTimerText;
    [SerializeField] private GameObject resumeTimerPanel;

    [SerializeField] private Color normalColorText = Color.white;
    [SerializeField] private Color dangerColorText = Color.red;

    public void setResumeText(float time)
    {
        int _time = (int)time;
        if (_time == 0)
        {
            resumeTimerText.text = "GO";
            return;
        }
        resumeTimerText.text = _time.ToString();
    }

    public void enableResumePanel()
    {
        resumeTimerPanel.SetActive(true);
    }

    public void disbleResumePanel()
    {
        resumeTimerPanel.SetActive(false);
    }

    public void setTimerText(float timer)
    {
        if (timer < 6)
        {
            timerText.color = dangerColorText;
        }
        else
        {
            timerText.color = normalColorText;
        }
        timerText.text = Constants.getTimeText(timer);
    }

}