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

    public float distanceToGround;
    [SerializeField] private LayerMask WhatIsGround;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private Vector2 wallCheckSize;

    private bool _isHitWall = false;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _Animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(_isHitWall.ToString());
        if (_begin && !_isHitWall)
            _rb.velocity = new Vector3(speed, _rb.velocity.y);

        checkCollision();
        AnimatorControllers();



        checkInput();
    }

    private void checkCollision()
    {
        _isGround = Physics2D.Raycast(transform.position, Vector2.down, distanceToGround, WhatIsGround);
        _isHitWall = Physics2D.BoxCast(wallCheck.position, wallCheckSize, 0, Vector2.zero, 0, WhatIsGround);
        if (_isGround)
            jumpStep = 2;
    }

    private void AnimatorControllers()
    {
        if (_rb.velocity.x == 0)
        {

            _isRunning = false;

        }
        _Animator.SetFloat("xVelocity", _rb.velocity.x);
        _Animator.SetFloat("yVelocity", _rb.velocity.y);
        _Animator.SetBool("isGround", _isGround);
        _Animator.SetBool("isRunning", _isRunning);
        _Animator.SetBool("isJump", _isJumping);
    }


    private void CheckWallHit()
    {
        _isHitWall = Physics2D.BoxCast(wallCheck.position, wallCheckSize, 0, Vector3.forward, 0, WhatIsGround);
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

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, new Vector2(transform.position.x, transform.position.y - distanceToGround));
        Gizmos.DrawWireCube(wallCheck.position, wallCheckSize);
    }
}
