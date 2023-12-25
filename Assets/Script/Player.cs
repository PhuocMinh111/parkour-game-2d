using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody2D _rb;
    public bool _begin;
    public float speed = 5f;
    public float jumpForce = 10f;
    public int jumpStep = 2;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (_begin)
            _rb.velocity = new Vector3(speed, _rb.velocity.y);

        checkInput();
    }
    private void checkInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _begin = true;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (jumpStep == 0) return;
            _rb.velocity = new Vector3(speed, jumpForce);
            --jumpStep;
        }
    }
    private void OnCollisionEnter2D(Collision2D colider)
    {
        if (colider.gameObject.tag == "Platform")
        {
            jumpStep = 2;
        }
    }
}
