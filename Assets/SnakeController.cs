using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeController : MonoBehaviour
{
    [Header("移动速度")] public float speed;
    public KeyCode up;
    public KeyCode down;
    public KeyCode left;
    public KeyCode right;
    [Header("蛇身")] public GameObject bodyPrefab;

    [Header("路径间隔")] public int pathInterval = 10;
    public int successFoodCount = 50;
    [Header("移动时间间隔")] public float moveDuration = 0.1f;

    private Vector2 _movement;
    private static SnakeController Instance;
    private List<Vector2> pathList = new List<Vector2>();
    private bool eat;
    private float _moveTimer;
    private bool _releaseMove = true;

    private void Awake()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(up) && _movement != Vector2.down && _releaseMove)
        {
            _movement = Vector2.up;
            _releaseMove = false;
        }

        if (Input.GetKeyDown(down) && _movement != Vector2.up && _releaseMove)
        {
            _movement = Vector2.down;
            _releaseMove = false;
        }

        if (Input.GetKeyDown(left) && _movement != Vector2.right && _releaseMove)
        {
            _movement = Vector2.left;
            _releaseMove = false;
        }

        if (Input.GetKeyDown(right) && _movement != Vector2.left && _releaseMove)
        {
            _movement = Vector2.right;
            _releaseMove = false;
        }
    }
    
    private void FixedUpdate()
    {
        /*
        * 移动蛇头
        * 并记录蛇头[pathInterval]个节点位置
        * 每有1个蛇身 蛇头记录节点就多[pathInterval]个
        * 每个蛇身按照 [pathInterval*蛇身数] 的蛇头节点进行移动
        */
        if (_movement == Vector2.zero)
            return;
        _moveTimer -= Time.fixedDeltaTime;
        if (_moveTimer > 0)
            return;
        _moveTimer = moveDuration;
        _releaseMove = true;

        var head = transform.GetChild(0);

        pathList.Add(head.position);
        if (pathList.Count > transform.childCount * pathInterval)
            pathList.RemoveAt(0);

        var targetPosition = (Vector2) head.position + _movement * head.localScale.x;
        head.position = targetPosition;

        for (int i = 1; i < transform.childCount; i++)
        {
            if (pathList.Count < i * pathInterval + 1)
                return;

            var currentBody = transform.GetChild(i);
            currentBody.position = pathList[pathInterval * i];
        }

        if (eat)
        {
            eat = false;
            GenerateBody();
        }
    }

    public static void EatSelf()
    {
        SoundManager.PlayEatSelf();
        Destroy(Instance.transform.GetChild(Instance.transform.childCount - 1).gameObject);
        if (Instance.transform.childCount <= 1)
            GameManager.GameOver();
    }

    public static void GenerateBody()
    {
        var head = Instance.transform.GetChild(0);
        var newBody = Instantiate(Instance.bodyPrefab, head.position, Quaternion.identity);
        newBody.transform.SetParent(Instance.transform);
        if (Instance.transform.childCount > Instance.successFoodCount)
            GameManager.Success();
    }

    public static int BodyLength()
    {
        return Instance.transform.childCount - 1;
    }
}