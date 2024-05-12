
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
    [SerializeField] private float FlatformHeightStep = 0.2f;
    [SerializeField] private float distanceBetween = 0.5f;
    [SerializeField] private float distanceToDelete;
    private bool newSpawn;
    // Update is called once per frame
    
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

                Transform Part = levelPart[RandomIndex];
                // float randomY = UnityEngine.Random.Range(-4, 2) * FlatformHeightStep;

                GameObject endPoint = Part.Find("EndPoint").gameObject;
                float width = Math.Abs(Part.Find("EndPoint").position.x - Part.Find("StartPoint").position.x);

                Vector2 newPosition = new Vector2(spawnPosition.x + width / 2 + distanceBetween, endPoint.transform.position.y);
                Transform newPart = Instantiate(Part, newPosition, transform.rotation, transform);
                spawnPosition = newPart.Find("EndPoint").position;
            }
        }

    }
    private void deletePlatform()
    {

        if (transform.childCount > 7)
        {
            for (int i = 0; i < 2; i++)
            {
                Transform element = transform.GetChild(i);
                Destroy(element.gameObject);
            }
        }
    }
}
