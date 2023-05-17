using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D _playerBody;
    private SpriteRenderer _playerRenderer;
    private SpriteRenderer _swordRenderer;
    private PlayerAnimation _playerAnimation;

    [SerializeField]
    private float _playerSpeed;

    [SerializeField]
    private float _jumpForce;

    [SerializeField]
    private bool _resetJumpNeeded = false;

    [SerializeField]
    private bool _isGrounded = false;

    [SerializeField]
    private LayerMask _groundLayer;

    void Start()
    {
        _playerBody = GetComponent<Rigidbody2D>();
        _playerAnimation = GetComponent<PlayerAnimation>();
        _playerRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
        _swordRenderer = transform.GetChild(1).GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        Movement();
        Attack();
    }

    void Attack()
    {
        if (Input.GetMouseButtonDown(0) && IsGrounded())
        {
            _playerAnimation.Attack();
        }
    }

    void Movement()
    {
        float horizontalMovement = Input.GetAxisRaw("Horizontal");

        Flip(horizontalMovement);

        _isGrounded = IsGrounded();

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            _playerBody.velocity = new Vector2(_playerBody.velocity.x, _jumpForce);
            StartCoroutine(Breather());

            _playerAnimation.Jump(true);
        }

        _playerBody.velocity = new Vector2(
            horizontalMovement * _playerSpeed * Time.deltaTime,
            _playerBody.velocity.y
        );

        _playerAnimation.Move(horizontalMovement);
    }

    bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(
            transform.position,
            Vector2.down,
            0.8f,
            _groundLayer.value
        );

        Debug.DrawRay(transform.position, Vector2.down, Color.green, 0.8f);

        if (hit.collider != null && !_resetJumpNeeded)
        {
            _playerAnimation.Jump(false);
            return true;
        }

        return false;
    }

    IEnumerator Breather()
    {
        _resetJumpNeeded = true;
        yield return new WaitForSeconds(0.1f);
        _resetJumpNeeded = false;
    }

    void Flip(float horizontalMovement)
    {
        if (horizontalMovement > 0)
        {
            _playerRenderer.flipX = false;

            _swordRenderer.flipX = false;
            _swordRenderer.flipY = false;

            if (_swordRenderer.transform.localPosition.x < 0)
            {
                Vector3 newPos = _swordRenderer.transform.localPosition;
                newPos.x = Mathf.Abs(newPos.x);
                _swordRenderer.transform.localPosition = newPos;
            }
        }
        else if (horizontalMovement < 0)
        {
            _playerRenderer.flipX = true;

            _swordRenderer.flipX = true;
            _swordRenderer.flipY = true;

            if (_swordRenderer.transform.localPosition.x > 0)
            {
                Vector3 newPos = _swordRenderer.transform.localPosition;
                newPos.x = -newPos.x;
                _swordRenderer.transform.localPosition = newPos;
            }
        }
    }
}
