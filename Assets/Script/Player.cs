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
    private bool _isJumping;
    private bool _isGround;
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

        AnimatorControllers();



        checkInput();
    }

    private void AnimatorControllers()
    {
        if (_rb.velocity.x == 0)
        {

            _isRunning = false;

        }
        _Animator.SetFloat("yVelocity", _rb.velocity.y);
        _Animator.SetBool("isGround", _isGround);
        _Animator.SetBool("isRunning", _isRunning);
        _Animator.SetBool("isJump", _isJumping);
    }

    private void checkInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _begin = true;
            _isGround = true;
            _isRunning = true;
        }

        if (Input.GetButtonDown("Jump"))
        {
            if (jumpStep == 0) return;

            _isGround = false;

            _rb.velocity = new Vector3(speed, jumpForce);
            --jumpStep;
        }
    }
    private void OnCollisionEnter2D(Collision2D colider)
    {
        if (colider.gameObject.tag == "Platform")
        {
            jumpStep = 2;
            _isGround = true;
        }
    }
}
