using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour, IDamageable
{
    private Rigidbody2D _playerBody;

    //private SpriteRenderer _playerRenderer;
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

    public int Health { get; set; }

    public int Diamonds = 0;

    private bool isDead = false;

    private PlayerInput playerInput;

    [SerializeField]
    private AudioClip[] sfxAudios;

    void Start()
    {
        _playerBody = GetComponent<Rigidbody2D>();
        _playerAnimation = GetComponent<PlayerAnimation>();
        _swordRenderer = transform.GetChild(1).GetComponent<SpriteRenderer>();
        playerInput = GetComponent<PlayerInput>();
        //_playerRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();

        Health = 4;
    }

    void Update()
    {
        if (!isDead)
        {
            Movement();
            Attack();
        }
    }

    void Attack()
    {
        if (Input.GetMouseButtonDown(0) && IsGrounded())
        //if (playerInput.actions["Attack"].triggered && IsGrounded())
        {
            _playerAnimation.Attack();
        }
    }

    void Movement()
    {
        //Vector2 playerMovement = playerInput.actions["Move"].ReadValue<Vector2>();
        //float horizontalMovement = playerMovement.x;
        float horizontalMovement = Input.GetAxisRaw("Horizontal");

        Flip(horizontalMovement);

        _isGrounded = IsGrounded();

        if (Input.GetButtonDown("Jump") && IsGrounded())
        //if (playerInput.actions["Jump"].triggered && IsGrounded())
        {
            _playerBody.velocity = new Vector2(_playerBody.velocity.x, _jumpForce);
            StartCoroutine(Breather());

            _playerAnimation.Jump(true);
            AudioManager.Instance.PlaySFX(sfxAudios[2], 0.5f);
        }

        _playerBody.velocity = new Vector2(
            horizontalMovement * _playerSpeed * Time.deltaTime,
            _playerBody.velocity.y
        );

        _playerAnimation.Move(horizontalMovement);
    }

    bool IsGrounded()
    {
        float rayDistanceX = 0.25f;

        RaycastHit2D hit1 = Physics2D.Raycast(
            new Vector2(transform.position.x - rayDistanceX, transform.position.y),
            Vector2.down,
            0.8f,
            _groundLayer.value
        );
        RaycastHit2D hit2 = Physics2D.Raycast(
            new Vector2(transform.position.x + rayDistanceX, transform.position.y),
            Vector2.down,
            0.8f,
            _groundLayer.value
        );

        Debug.DrawRay(
            new Vector2(transform.position.x - rayDistanceX, transform.position.y),
            Vector2.down,
            Color.green,
            0.8f
        );
        Debug.DrawRay(
            new Vector2(transform.position.x + rayDistanceX, transform.position.y),
            Vector2.down,
            Color.green,
            0.8f
        );

        if ((hit1.collider != null || hit2.collider != null) && !_resetJumpNeeded)
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
            //_playerRenderer.flipX = false;
            transform.GetChild(0).rotation = Quaternion.Euler(
                0,
                0,
                transform.rotation.eulerAngles.z
            );

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
            //_playerRenderer.flipX = true;
            transform.GetChild(0).rotation = Quaternion.Euler(
                0,
                180,
                transform.rotation.eulerAngles.z
            );

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

    public void Damage()
    {
        if (!isDead)
        {
            Debug.Log("Player Hit!");

            Health--;
            UIManager.Instance.UpdateLives(Health);

            if (Health < 1)
            {
                isDead = true;
                _playerAnimation.Dead();
                AudioManager.Instance.PlaySFX(sfxAudios[0], 0.5f);
            }
            else
            {
                _playerAnimation.Hit();
                AudioManager.Instance.PlaySFX(sfxAudios[1], 0.5f);
            }
        }
    }

    public void AddGems(int amount)
    {
        Diamonds += amount;
        UIManager.Instance.UpdateGemCount(Diamonds);
    }
}
