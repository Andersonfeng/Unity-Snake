using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class FoodController : MonoBehaviour
{
    private float offsetX = 700;
    private float offsetY = 350;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("被吃掉啦");
        ChangePosition();
        SnakeController.GenerateBody();
        GameManager.SpeedUp();
        GameManager.RamdomBackground();
        GameManager.ResetTimer();
        GameManager.SetMessage("吃到食物,时间重置");
        GameManager.EatFood();
        SoundManager.PlayEatFood();
    }

    private void Start()
    {
        transform.position = new Vector3(Random.Range(-offsetX, offsetX), Random.Range(-offsetY, offsetY));
    }

    void ChangePosition()
    {
        transform.position = new Vector3(Random.Range(-offsetX, offsetX), Random.Range(-offsetY, offsetY));
    }
}