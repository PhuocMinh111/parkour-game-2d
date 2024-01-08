using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D _rb;
    private Animator _Animator;
    public bool _begin;
    [Header("Jump info")]
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float DoubleJumpForce = 0.6f;
    public int jumpStep = 2;
    private bool _isRunning;
    private bool _isJumping;
    private bool _isGround;

    [Header("collision info")]
    public float distanceToGround;
    [SerializeField] private LayerMask WhatIsGround;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private Vector2 wallCheckSize;
    [SerializeField] private float _distanceToCeil;
    private bool _isHitCeil;

    [Header("slide info")]
    [SerializeField] private float _slideSpeed = 8f;
    [SerializeField] private float _slideTimer;
    [SerializeField] private float _slideCountDown;
    private bool _isSliding;
    private float _slideTimeCounter;

    private bool _canDoubleJump;
    private bool _isHitWall = false;

    [HideInInspector] public bool ledgeDetected;
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _Animator = GetComponent<Animator>();


    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log(_isHitWall.ToString());
        _slideTimeCounter -= Time.deltaTime;
        _slideCountDown -= Time.deltaTime;
        if (_begin)
        {

            Movement();
        }

        Debug.Log("hit ceil" + _isHitCeil);

        checkForSlide();
        checkCollision();
        AnimatorControllers();

        // Debug.Log(jumpStep.ToString());

        checkInput();
    }

    private void checkForSlide()
    {
        if (_slideTimeCounter < 0 && !_isHitCeil)
        {

            _isSliding = false;
        }
    }


    private void Movement()
    {
        if (_isHitWall)
            return;

        if (!_isSliding)
            _rb.velocity = new Vector3(_speed, _rb.velocity.y);
        else
            _rb.velocity = new Vector3(_slideSpeed, _rb.velocity.y);
    }
    private void AnimatorControllers()
    {
        if (_rb.velocity.x == 0)
        {

            _isRunning = false;

        }

        _Animator.SetBool("canDoubleJump", _canDoubleJump);
        _Animator.SetFloat("xVelocity", _rb.velocity.x);
        _Animator.SetBool("isSliding", _isSliding);
        _Animator.SetFloat("yVelocity", _rb.velocity.y);
        _Animator.SetBool("isGround", _isGround);
        _Animator.SetBool("isRunning", _isRunning);
        _Animator.SetBool("isJump", _isJumping);
    }

    private void SlideButton()
    {
        if (_slideCountDown > 0) return;
        if (_rb.velocity.x > 0)
        {
            _isSliding = true;
            _slideTimeCounter = _slideTimer;
            _slideCountDown = 1.5f;
        }
    }

    private void Jump()
    {
        if (_isSliding)
            return;
        if (_isGround)
        {
            _isGround = false;
            _canDoubleJump = true;
            _rb.velocity = new Vector3(_speed, jumpForce);
        }
        else if (_canDoubleJump)
        {
            _canDoubleJump = false;
            _rb.velocity = new Vector3(_speed, jumpForce * DoubleJumpForce);
        }
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
            Debug.Log("Jump");
            Jump();

        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            SlideButton();
        }
    }
    private void checkCollision()
    {
        _isGround = Physics2D.Raycast(transform.position, Vector2.down, distanceToGround, WhatIsGround);
        _isHitWall = Physics2D.BoxCast(wallCheck.position, wallCheckSize, 0, Vector2.zero, 0, WhatIsGround);
        _isHitCeil = Physics2D.Raycast(transform.position, Vector2.up, _distanceToCeil, WhatIsGround);
        Debug.Log("ledge Detect " + ledgeDetected);
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, new Vector2(transform.position.x, transform.position.y - distanceToGround));
        Gizmos.DrawWireCube(wallCheck.position, wallCheckSize);
        Gizmos.DrawLine(transform.position, new Vector2(transform.position.x, transform.position.y + _distanceToCeil));
    }
}
