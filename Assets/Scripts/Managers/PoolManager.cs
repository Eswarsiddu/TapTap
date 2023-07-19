using System.Collections.Generic;
using UnityEngine;
using System;

public class PoolManager : MonoBehaviour
{
    [SerializeField] private GameObject targetPrefab;
    [SerializeField] private Transform targetsParent;
    [SerializeField] private int noOfPoolElements;

    private Queue<Target> targets = new Queue<Target>();
    private AssetsStore assetStore;
    private float _targetSize;

    public float targetSize => _targetSize;

    public void loadGame(Action targetDestroyed, AssetsStore assetStore)
    {
        targetPrefab = Resources.Load<GameObject>(Constants.TARGETPREFAB);
        this.assetStore = assetStore;
        _targetSize = targetPrefab.GetComponent<RectTransform>().rect.width;
        for (int i = 0; i < noOfPoolElements; i++)
        {
            GameObject temp = Instantiate(targetPrefab, targetsParent);
            temp.name = i.ToString();
            Target target = temp.GetComponent<Target>();
            target.targetDestroyed = targetDestroyed;
            temp.SetActive(false);
            targets.Enqueue(target);
        }
    }

    public Target getStandardTarget(int count, Vector2 point)
    {
        Target target = getTarget(count);
        target.enableTarget();
        target.convertStandardTarget(point);
        return target;
    }

    public Target getMovingTarget(int count, Vector2 point1, Vector2 point2, float speed)
    {
        Target target = getTarget(count);
        target.enableTarget();
        target.convertMovingTarget(point1, point2, speed);
        return target;
    }

    private Target getTarget(int count)
    {
        Target target = targets.Dequeue();
        target.setTexure(assetStore.currentTexure);
        target.count = count;
        targets.Enqueue(target);
        return target;
    }
}
