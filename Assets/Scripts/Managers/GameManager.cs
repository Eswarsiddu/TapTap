using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private float intialLoadingTimer = 3;
    [SerializeField] private float resumeTime;
    [SerializeField] private CanvasManager canvasManager;

    private PlayerData playerData;
    private AssetsStore assetStore;
    private LevelsData levelsData;

    private LevelManager levelManager;
    private GameScreen gameScreen;

    private bool isInitialLoading;
    private bool gamePaused;
    private bool gameResumeRequest;
    private bool gameStart;
    private float resumeTimer;
    private float timer;

    private void Awake()
    {
        isInitialLoading = true;
        levelManager = GetComponent<LevelManager>();
        assetStore = Resources.Load<AssetsStore>(Constants.ASSETSSTORE);
        levelsData = Resources.Load<LevelsData>(Constants.LEVELSDATA);
        playerData = Resources.Load<PlayerData>(Constants.PLAYERDATA);
        loadData();
        canvasManager.loadGame(playerData, this);
        levelManager.loadGame(this, levelsData, assetStore);
        gameScreen = canvasManager.gameScreen;
        gameStart = false;
    }

    public void endGame(GameReward gameReward)
    {
        if (gameReward.gameEnd == true)
        {
            playerData.increseLevel();
        }
        canvasManager.openGameEnd(gameReward);
        resetGame();
    }

    private void resetGame()
    {
        gamePaused = false;
        gameStart = false;
        levelManager.resetLevel();
    }

    public void startGame()
    {
        levelManager.generateLevel(playerData.level);
        canvasManager.openGameScreen();
        timer = levelManager.getTime();
        gameStart = true;
    }

    public void goHome()
    {
        Time.timeScale = 1f;
        resetGame();
        canvasManager.openHomeScreen();
    }

    public void vibrateDevice(bool vibrate)
    {
        if (vibrate == true)
        {
            Handheld.Vibrate();
        }
    }

    public void vibrateDevice()
    {
        if (playerData.haptic == true)
        {
            Handheld.Vibrate();
        }
    }

    private void Update()
    {
        if (isInitialLoading == true)
        {
            if (intialLoadingTimer <= 0)
            {
                canvasManager.initialLoadingComplete();
                isInitialLoading = false;
            }
            else
            {
                intialLoadingTimer -= Time.deltaTime;
            }
            return;
        }

        if (gameStart == false || gamePaused == true) { return; }
        if (gameResumeRequest)
        {
            resumeTimer -= Time.unscaledDeltaTime;
            gameScreen.setResumeText(resumeTimer);
            if (resumeTimer <= 0)
            {
                resumeGame();
            }
            return;
        }
        timer -= Time.deltaTime;
        if (timer <= 0.7)
        {
            GameReward gameReward = new GameReward(false);
            endGame(gameReward);
            return;
        }
        gameScreen.setTimerText(timer);
    }

    public void pauseGame()
    {
        Time.timeScale = 0f;
        resumeTimer = resumeTime;
        gamePaused = true;
        gameResumeRequest = false;
        canvasManager.pauseGame();
    }

    public void requestResumeGame()
    {
        gameScreen.enableResumePanel();
        canvasManager.resumeGame();
        gamePaused = false;
        gameResumeRequest = true;
    }

    private void resumeGame()
    {
        gameScreen.disbleResumePanel();
        gameResumeRequest = false;
        Time.timeScale = 1f;
    }

    private void OnApplicationQuit()
    {
        saveData();
    }

    private void OnApplicationPause(bool pauseStatus)
    {
        if (gameStart == true)
        {
            pauseGame();
        }
        else
        {
            saveData();
        }
    }

    private void saveData()
    {
        playerData.SaveData();
        assetStore.SaveData();
        levelsData.SaveData();
    }

    private void loadData()
    {
        assetStore.LoadData();
        levelsData.LoadData();
        playerData.LoadData();
    }

    internal bool canPurchaseAny()
    {
        return assetStore.canPurchaseAny(playerData.coins);
    }
}
