using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class parallaxBackground : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Camera _Camera;

    [SerializeField] private float parallaxSpeed;
    private float _size;
    private float xPosition;

    void Start()
    {
        // _Camera = GameObject.Find("Main Camera");
        _Camera = GameObject.Find("Main Camera").GetComponent<Camera>();
        _size = _Camera.orthographicSize * 2f * _Camera.aspect;
        xPosition = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        float distanceMoved = _Camera.transform.position.x * (1 - parallaxSpeed);
        float distance = _Camera.transform.position.x * parallaxSpeed;
        transform.position = new Vector2(xPosition + distance, transform.position.y);
        Debug.Log(_size);
        if (distanceMoved > xPosition + _size)
        {
            xPosition += _size;
        }
    }
}
