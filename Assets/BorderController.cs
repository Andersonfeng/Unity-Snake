using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorderController : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("out of border dead");
        if (other.gameObject.CompareTag("Head"))
            GameManager.GameOver();
    }
}