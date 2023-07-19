using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    private PlayerData _playerData;
    private GameManager _gameManager;
    public GameManager gameManager => _gameManager;
    public PlayerData playerData => _playerData;

    private GameEndScreen _gameEndScreen;
    private GameScreen _gameScreen;
    private Dictionary<UISCREENS, GameObject> screens = new Dictionary<UISCREENS, GameObject>();

    public GameScreen gameScreen => _gameScreen;

    public void loadGame(PlayerData _Playerdata, GameManager _gameManager)
    {
        this._gameManager = _gameManager;
        _playerData = _Playerdata;
        foreach (Transform child in transform)
        {
            A_Screen a_screen = child.GetComponent<A_Screen>();
            if (a_screen == null) continue;
            a_screen.canvasManager = this;
            UISCREENS screen = a_screen.screenType;
            screens.Add(screen, child.gameObject);
            child.gameObject.SetActive(false);
        }
        _gameEndScreen = screens[UISCREENS.GAMEEND].GetComponent<GameEndScreen>();
        _gameScreen = screens[UISCREENS.GAME].GetComponent<GameScreen>();
        screens[UISCREENS.INITIALLOADING].SetActive(true);
        screens[UISCREENS.HOME].SetActive(true);
    }

    public void initialLoadingComplete()
    {
        screens[UISCREENS.HOME].SetActive(true);
        screens[UISCREENS.INITIALLOADING].SetActive(false);
    }

    private void changeScreens(UISCREENS[] enableScreens, UISCREENS[] disableScreens)
    {
        foreach (UISCREENS screen in disableScreens)
        {
            screens[screen].SetActive(false);
        }

        foreach (UISCREENS screen in enableScreens)
        {
            screens[screen].SetActive(true);
        }
    }

    public void openStoreScreen()
    {
        UISCREENS[] enableScreens = { UISCREENS.STORE };
        UISCREENS[] disableScreens = { UISCREENS.GAME, UISCREENS.GAMEEND, UISCREENS.PAUSE, UISCREENS.HOME };

        changeScreens(enableScreens, disableScreens);
    }

    public void openHomeScreen()
    {
        UISCREENS[] enableScreens = { UISCREENS.HOME };
        UISCREENS[] disableScreens = { UISCREENS.GAME, UISCREENS.GAMEEND, UISCREENS.PAUSE, UISCREENS.STORE };

        changeScreens(enableScreens, disableScreens);
    }

    public void openGameScreen()
    {
        UISCREENS[] enableScreens = { UISCREENS.GAME };
        UISCREENS[] disableScreens = { UISCREENS.HOME, UISCREENS.GAMEEND, UISCREENS.PAUSE, UISCREENS.STORE };
        changeScreens(enableScreens, disableScreens);
    }

    public void pauseGame()
    {
        screens[UISCREENS.PAUSE].SetActive(true);
    }

    public void resumeGame()
    {
        screens[UISCREENS.PAUSE].SetActive(false);
    }

    public void openGameEnd(GameReward gameReward)
    {
        UISCREENS[] enableScreens = { UISCREENS.GAMEEND };
        UISCREENS[] disableScreens = { UISCREENS.HOME, UISCREENS.GAME, UISCREENS.PAUSE, UISCREENS.STORE };
        _gameEndScreen.endGameResults(gameReward);
        changeScreens(enableScreens, disableScreens);
    }

}
