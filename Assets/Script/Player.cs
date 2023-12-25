using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D _rb;
    private Animator _Animator;
    public bool _begin;
    public float speed = 5f;
    public float jumpForce = 10f;
    public int jumpStep = 2;
    private bool _isRunning;
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _Animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_begin)
            _rb.velocity = new Vector3(speed, _rb.velocity.y);

        if (_rb.velocity.x != 0)
        {

            _isRunning = true;
            _Animator.SetBool("isRunning", _isRunning);
        }

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

            bool isRunning = _Animator.GetBool("isRunning");

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
