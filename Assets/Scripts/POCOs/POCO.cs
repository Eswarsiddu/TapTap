public class GameReward
{
    private bool _gameEnd;

    private int _coins;

    public GameReward(bool gameEnd)
    {
        _gameEnd = gameEnd;
        _coins = 0;
    }

    public GameReward(bool gameEnd, int coins)
    {
        _coins = coins;
        _gameEnd = gameEnd;
    }

    public int coins => _coins;

    public bool gameEnd => _gameEnd;
}

public class TargetCountPair
{
    public int count;
    public TARGETTYPES targetType;
    public TargetCountPair(TARGETTYPES targetType)
    {
        count = 1;
        this.targetType = targetType;
    }
}