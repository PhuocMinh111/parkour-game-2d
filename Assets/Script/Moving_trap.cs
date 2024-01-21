using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Moving_trap : Trap
{
    // Start is called before the first frame update
    [SerializeField] private float speed;
    [SerializeField] private float rotationSpeed = 100f;
    [SerializeField] private Transform[] movePoints;


    private int i;
    protected override void Start()
    {

        transform.position = movePoints[0].position;
    }
    private void Update()
    {

        transform.position = Vector3.MoveTowards(transform.position, movePoints[i].position, speed * Time.deltaTime);
        if (Vector3.Distance(transform.position, movePoints[i].position) < 0.25f)
        {
            i++;
        }
        if (i >= movePoints.Length)
        {
            i = 0;
        }

        if (transform.position.x > movePoints[i].position.x)
        {
            transform.Rotate(new Vector3(0, 0, rotationSpeed * Time.deltaTime));
        }
        else
        {
            transform.Rotate(new Vector3(0, 0, -rotationSpeed * Time.deltaTime));
        }
    }
    protected virtual void OnTriggerEnter2D(Collider2D collider)

    {
        base.OnTriggerEnter2D(collider);
    }
}
