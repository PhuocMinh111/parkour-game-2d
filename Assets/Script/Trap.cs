using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] protected float changeToSpawn = 60;


    protected virtual void Start()
    {
        if (changeToSpawn > Random.Range(0, 100))
            Destroy(gameObject);
    }
    protected virtual void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.GetComponent<Player>() != null)
        {
            collider.GetComponent<Player>().Damge();
        }
    }
}
