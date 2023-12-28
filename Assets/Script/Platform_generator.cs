
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using Unity.VisualScripting;
using UnityEngine;

public class Platform_generator : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform[] levelPart;
    private Vector3 spawnPosition;

    public Transform Player;
    [SerializeField] private int numberCreatedFlatform = 3;
    [SerializeField] private float FlatformHeightStep = 0.5f;
    private bool newSpawn;
    // Update is called once per frame
    void Start()
    {

        Transform beginLevel = Instantiate(levelPart[0], new Vector3(Player.position.x, Player.position.y - 3), transform.rotation, transform);
        spawnPosition = beginLevel.GetChild(1).position;
    }
    void Update()
    {


        // Debug.Log(deltaPosition);

        InstantiatePlatform();
        deletePlatform();
    }

    private void InstantiatePlatform()
    {
        float deltaPosition = spawnPosition.x - Player.position.x;
        if (deltaPosition < 5 * numberCreatedFlatform)
        {

            for (var i = 0; i < numberCreatedFlatform; i++)
            {
                int RandomIndex = UnityEngine.Random.Range(0, levelPart.Length);
                Debug.Log("length" + levelPart.Length);
                Debug.Log(RandomIndex);
                Transform Part = levelPart[RandomIndex];
                float randomY = UnityEngine.Random.Range(-4, 2) * FlatformHeightStep;
                float width = Math.Abs(Part.Find("endPoint").position.x - Part.Find("startPoint").position.x);
                Vector2 newPosition = new Vector2(spawnPosition.x + width / 2 + 0.5f, randomY);
                Transform newPart = Instantiate(Part, newPosition, transform.rotation, transform);
                spawnPosition = newPart.Find("endPoint").position;
            }
        }

    }
    private void deletePlatform()
    {

        if (transform.childCount > 5)
        {
            for (int i = 0; i < 2; i++)
            {
                Transform element = transform.GetChild(i);
                Destroy(element.gameObject);
            }
        }
    }
}
