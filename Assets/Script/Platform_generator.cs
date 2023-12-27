
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Platform_generator : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform[] levelPart;
    private Vector3 spawnPosition;
    private float deltaPosition;
    public Transform Player;
    private bool newSpawn;
    // Update is called once per frame
    void Start()
    {

        Transform beginLevel = Instantiate(levelPart[0], new Vector3(Player.position.x, Player.position.y - 5), transform.rotation, transform);
        spawnPosition = beginLevel.GetChild(1).position;
    }
    void Update()
    {

        deltaPosition = spawnPosition.x - Player.position.x;
        // Debug.Log(deltaPosition);
        if (deltaPosition < 2)
        {
            InstantiatePlatform();
        }
    }

    private void InstantiatePlatform()
    {

        for (var i = 0; i < 2; i++)
        {

            Transform Part = levelPart[UnityEngine.Random.Range(0, levelPart.Length - 1)];
            Debug.Log(spawnPosition.x);
            Transform newPart = Instantiate(Part, spawnPosition, transform.rotation, transform);
            spawnPosition = newPart.Find("endPoint").position;
        }

    }
}
