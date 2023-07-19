using System.Collections.Generic;
using UnityEngine;
using System;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private PoolManager poolManager;
    [SerializeField] private RectTransform corner1;
    [SerializeField] private RectTransform corner2;

    private GameManager _gameManager;
    private LevelsData levelsData;
    private LevelInfo levelInfo;
    private Vector2 refPosition;
    private float targetSize;
    private int noOfTargets;
    private List<Target> targetsInGame = new List<Target>();

    public GameManager gameManager => _gameManager;

    public int getTime() => levelInfo.time;

    public void loadGame(GameManager _gameManager, LevelsData levelsData, AssetsStore assetStore)
    {
        this._gameManager = _gameManager;
        poolManager.loadGame(targetDestroyed, assetStore);
        this.levelsData = levelsData;

        targetSize = poolManager.targetSize;

        refPosition = corner1.anchoredPosition;

        int noOfXPoints = (int)((corner2.anchoredPosition.x - refPosition.x) / targetSize);

        int noOfYPoints = (int)((refPosition.y - corner2.anchoredPosition.y) / targetSize);

        IJPoint.setIJPointsData(noOfYPoints, noOfXPoints);

    }

    public void generateLevel(int level)
    {
        levelInfo = levelsData.getLevelInfo(level);
        TARGETTYPES targetTypes = levelInfo.targetType;
        noOfTargets = levelInfo.noOfTargets;

        List<TargetCountPair> targets = new List<TargetCountPair>();
        foreach (TARGETTYPES targettype in Enum.GetValues(typeof(TARGETTYPES)))
        {
            if (targetTypes.HasFlag(targettype))
            {
                targets.Add(new TargetCountPair(targettype));
            }
        }

        for (int i = 0; i < noOfTargets - targets.Count; i++)
        {
            targets[Constants.RandomInt(targets.Count - 1)].count++;
        }

        foreach (TargetCountPair targetPair in targets)
        {
            while (targetPair.count > 0)
            {
                generateTarget(targetPair.targetType, levelInfo.targetCount, levelInfo.speed);
                targetPair.count--;
            }
        }

    }

    private void generateTarget(TARGETTYPES targetType, int targetCount, float targetSpeed)
    {
        switch (targetType)
        {
            case TARGETTYPES.DIAGONAL:
                generateMovingTarget(targetCount, targetSpeed, IJPoint.DiagonalPoints());
                break;
            case TARGETTYPES.HORIZONTAL:
                generateMovingTarget(targetCount, targetSpeed, IJPoint.HorizontalPoints());
                break;
            case TARGETTYPES.VERTICAL:
                generateMovingTarget(targetCount, targetSpeed, IJPoint.VerticalPoints());
                break;
            case TARGETTYPES.STANDARD:
                generateStandardTarget(targetCount);
                break;
        }
    }

    private Vector2 getPositionInPoint(IJPoint point)
    {
        Vector2 pos = Vector2.zero;
        pos.x = refPosition.x + (targetSize * point.j);
        pos.y = refPosition.y - (targetSize * point.i);
        return pos;
    }

    private void generateStandardTarget(int targetCount)
    {
        IJPoint point = IJPoint.RandomPoint();
        Vector2 postion = getPositionInPoint(point);

        Target target = poolManager.getStandardTarget(targetCount, postion);
        targetsInGame.Add(target);
    }

    private void generateMovingTarget(int targetCount, float targetSpeed, IJPoint[] points)
    {
        Vector2 point1 = getPositionInPoint(points[0]);
        Vector2 point2 = getPositionInPoint(points[1]);
        Target target = poolManager.getMovingTarget(targetCount, point1, point2, targetSpeed);
        targetsInGame.Add(target);
    }

    public void targetDestroyed()
    {
        noOfTargets--;
        gameManager.vibrateDevice();
        if (noOfTargets <= 0)
        {
            GameReward gameReward = new GameReward(true, levelInfo.coins);
            _gameManager.endGame(gameReward);
        }
    }

    public void resetLevel()
    {
        foreach (Target target in targetsInGame)
        {
            target.resetObject();
        }
        targetsInGame.Clear();
    }

}
