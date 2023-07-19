using UnityEngine.UI;
using UnityEngine;
using TMPro;
using System;

public class Target : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI countText;
    [SerializeField] private RectTransform rectTransform;
    [Header("Images")]
    [SerializeField] private Image spriteImage;

    public Action targetDestroyed;

    private int _count;

    private Vector2 point1;
    private Vector2 point2;
    [SerializeField] private float speed;
    private bool isMoving;
    private float mindistance;

    public int count
    {
        set
        {
            _count = value;
            countText.text = value.ToString();
        }
    }


    public void clicked()
    {
        if (Time.timeScale == 0)
        {
            return;
        }
        _count--;
        countText.text = _count.ToString();
        if (_count <= 0)
        {
            targetDestroyed();
            gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (Time.timeScale == 0)
        {
            return;
        }
        if (isMoving == false)
        {
            return;
        }
        if (Vector2.Distance(rectTransform.anchoredPosition, point2) <= mindistance)
        {
            swapPoints();
        }

        rectTransform.anchoredPosition = Vector2.MoveTowards(rectTransform.anchoredPosition, point2, speed * Time.deltaTime);
    }

    internal void convertStandardTarget(Vector2 pos)
    {
        rectTransform.anchoredPosition = pos;
    }

    private void swapPoints()
    {
        Vector2 newVect = point1;
        point1 = point2;
        point2 = newVect;
    }

    public void convertMovingTarget(Vector2 pos1, Vector2 pos2, float speed)
    {
        point1 = pos1;
        point2 = pos2;
        rectTransform.anchoredPosition = Constants.RandomBool() ? point1 : point2;
        float dis = Vector2.Distance(pos1, pos2);
        mindistance = dis / 100;
        this.speed = speed;
        isMoving = true;
    }

    public void resetObject()
    {
        isMoving = false;
        gameObject.SetActive(false);
    }

    internal void enableTarget()
    {
        gameObject.SetActive(true);
    }

    public void setTexure(TargetTexure texure)
    {
        spriteImage.sprite = texure.sprite;
        countText.color = texure.textColor;
    }
}
