using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D _rb;
    private Animator _Animator;
    public bool _begin;
    [Header("Speed info")]
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _maxsPeed = 30f;
    [SerializeField] private float _milestone;
    [SerializeField] private float _mileStoneIncreaser;
    [SerializeField] private float _speedIncreaser;
    private const float _speedPrefix = 0.05f;

    [Header("Jump info")]
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float DoubleJumpForce = 0.6f;
    public int jumpStep = 2;
    private bool _isRunning;
    private bool _isJumping;
    [SerializeField] private bool _isGround;

    [Header("collision info")]
    public float distanceToGround;
    [SerializeField] private LayerMask WhatIsGround;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private Vector2 wallCheckSize;
    [SerializeField] private float _distanceToCeil;
    private bool _isHitCeil;

    [Header("slide info")]
    [SerializeField] private float _slideSpeedRatio = 8f;
    [SerializeField] private float _slideTimer;
    [SerializeField] private float _slideCountDown;
    private bool _isSliding;
    private float _slideTimeCounter;
    [Header("knock info")]
    [SerializeField] private Vector2 knockBackDir;
    private bool isKnocked;

    [Header("ledge climb info")]
    [SerializeField] private Vector2 offset1;
    [SerializeField] private Vector2 offset2;
    [HideInInspector] public bool ledgeDetected;
    private Vector2 climbBeginPosition;
    private Vector2 climbOverPosition;
    private bool isClimbing;
    private bool canGrabLedge = true;
    private bool canClimb;
    private bool canRoll = false;

    private bool _canDoubleJump;
    private bool _isHitWall = false;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _Animator = GetComponent<Animator>();
        _milestone = _mileStoneIncreaser;

    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log(_isHitWall.ToString());
        AnimatorControllers();
        _slideTimeCounter -= Time.deltaTime;
        _slideCountDown -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.K))
        {
            KnockBack();
        }
        Debug.Log(isKnocked);
        if (isKnocked)
            return;


        if (_begin)
        {

            Movement();
        }



        // Debug.Log("is climbing" + isClimbing);

        CheckForSlide();
        CheckCollision();

        CheckForClimb();
        SpeedController();
        // Debug.Log(jumpStep.ToString());

        CheckInput();
    }
    private void KnockBack()
    {
        isKnocked = true;
        _rb.velocity = new Vector2(-12, 7);
    }
    // private void KnockbackVelocity () => 
    private void KnockBackEnd() => isKnocked = false;
    private void SpeedController()
    {
        if (_speed == _maxsPeed) return;
        if (transform.position.x > _milestone)
        {
            _speedIncreaser += _speedIncreaser * _speedPrefix;
            _speed += _speedIncreaser;
            _mileStoneIncreaser *= _speedIncreaser;
            _milestone += _mileStoneIncreaser;
        }
        if (_speed > _maxsPeed)
        {
            _speed = _maxsPeed;
        }
    }
    #region climb
    private void CheckForClimb()
    {
        if (ledgeDetected && canGrabLedge)
        {
            _rb.gravityScale = 0;
            canGrabLedge = false;
            Vector2 detectorPosition = GetComponentInChildren<ledgeDetector>().transform.position;
            climbBeginPosition = detectorPosition + offset1;
            // transform.position = climbBeginPosition;
            climbOverPosition = detectorPosition + offset2;
            canClimb = true;
        }
        if (canClimb)
        {
            transform.position = climbBeginPosition;
        }

    }
    private void OnClimbOver()
    {
        _rb.gravityScale = 4;
        transform.position = climbOverPosition;
        canClimb = false;
        isClimbing = false;

        Invoke("setGrablegde", 1f);
    }
    #endregion

    #region canRoll
    private void onRollFinnish() => canRoll = false;

    #endregion
    void setGrablegde() => canGrabLedge = true;
    private void setCanClimb() => canClimb = false;

    private void CheckForSlide()
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
            _rb.velocity = new Vector3(_speed * _slideSpeedRatio, _rb.velocity.y);
    }

    private void Jump()
    {
        if (!_begin)
            return;
        if (_isSliding)
            return;
        if (canClimb)
        {
            // canGrabLedge = false;
            // Debug.Log("is climbing");
            isClimbing = true;
            // Invoke("setAfterClimbPosition", 0.3f);
            return;
        }
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
    void setAfterClimbPosition() => transform.position = climbOverPosition;

    private void AnimatorControllers()
    {
        if (_rb.velocity.x == 0)
        {

            _isRunning = false;

        }

        _Animator.SetBool("canDoubleJump", _canDoubleJump);
        if (!isKnocked)
            _Animator.SetFloat("xVelocity", _rb.velocity.x);
        _Animator.SetBool("isSliding", _isSliding);
        _Animator.SetFloat("yVelocity", _rb.velocity.y);
        _Animator.SetBool("isGround", _isGround);
        _Animator.SetBool("isRunning", _isRunning);
        _Animator.SetBool("isJump", _isJumping);
        _Animator.SetBool("canClimb", canClimb);
        _Animator.SetBool("canGrab", canGrabLedge);
        _Animator.SetBool("ledgeDetected", ledgeDetected);
        _Animator.SetBool("isKnocked", isKnocked);
        _Animator.SetBool("isClimbing", isClimbing);
        _Animator.SetBool("canRoll", canRoll);
        if (_rb.velocity.y < -14)
            canRoll = true;
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


    private void CheckInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _begin = true;
            _isGround = true;
            _isRunning = true;
        }

        if (Input.GetButtonDown("Jump"))
        {

            Jump();

        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            SlideButton();
        }
    }
    private void CheckCollision()
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
