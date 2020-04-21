using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Debug.Log("有东西撞上来" + other.name);
        var parent = gameObject.transform.parent.gameObject;
        if (gameObject == parent.transform.GetChild(parent.transform.childCount - 1).gameObject)
            return;
        if (other.gameObject.CompareTag("Head"))
        {
            SnakeController.EatSelf();
            GameManager.SetMessage("吃到尾巴,长度和速度减少");
            GameManager.SpeedDown();
        }
    }
}