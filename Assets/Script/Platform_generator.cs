
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
    public Transform Player;
    // Update is called once per frame
    void Start()
    {

        Transform beginLevel = Instantiate(levelPart[0], new Vector3(Player.position.x, Player.position.y - 5), transform.rotation, transform);
        spawnPosition = beginLevel.GetChild(1).position;
    }
    void Update()
    {

        float deltaPosition = Math.Abs(Player.position.x - spawnPosition.x);
        if (deltaPosition < 2)
        {
            InstantiatePlatform();
        }
    }

    private void InstantiatePlatform()
    {
        Transform Part = levelPart[UnityEngine.Random.Range(0, levelPart.Length - 1)];
        spawnPosition = Part.GetChild(1).position;
        Instantiate(Part, spawnPosition, transform.rotation, transform);
    }
}
