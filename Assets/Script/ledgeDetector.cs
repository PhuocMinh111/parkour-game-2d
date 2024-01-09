using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ledgeDetector : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float radius;
    [SerializeField] private Player player;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private bool canDetected;

    // Update is called once per frame

    void Update()
    {
        // Debug.Log("can detected " + canDetected);
        if (canDetected)
            player.ledgeDetected = Physics2D.OverlapCircle(transform.position, radius, whatIsGround);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            canDetected = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            canDetected = true;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
