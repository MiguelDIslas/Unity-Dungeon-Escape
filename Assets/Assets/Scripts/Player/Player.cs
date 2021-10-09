using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{
    public int Health { get; set; }

    //get reference of rigidbody
    private Rigidbody2D _rigidbody;
    [SerializeField]
    private float _jumpForce = 6.0f;
    private bool _resetJump = false;
    [SerializeField]
    private float _speed = 2.0f;
    private PlayerAnimation _playerAnimation;
    private SpriteRenderer _spriteRenderer;
    private SpriteRenderer _swordSprite;
    private bool _grounded = false;
    private Animator _animator;


    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _playerAnimation = GetComponent<PlayerAnimation>();
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _animator = GetComponentInChildren<Animator>();
        _swordSprite = GameObject.Find("Sword_Arc").GetComponentInChildren<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        Movement();
        PlayerAttack();
    }

    private void Movement()
    {
        //Horizontal Input for left/right
        float move = Input.GetAxisRaw("Horizontal");
        _grounded = IsGrounded();

        //Flip sprite
        FlipSprite(move);

        //Check if jumps
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _jumpForce);
            StartCoroutine(ResetJumpNeeded());
            _playerAnimation.Jump(true);
        }

        //Add velocity to our object
        if (!_animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
            _rigidbody.velocity = new Vector2(move * _speed, _rigidbody.velocity.y);

        //Animation to move the sprite
        _playerAnimation.Move(move);
    }

    bool IsGrounded()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.down, 1f, 1 << 8);
        if (hitInfo.collider != null)
        {
            if (!_resetJump)
            {
                _playerAnimation.Jump(false);
                return true;
            }
        }
        return false;
    }


    void PlayerAttack()
    {
        //Attack
        if (Input.GetKeyDown(KeyCode.Mouse0) && IsGrounded())
        {
            _playerAnimation.Attack();
            _rigidbody.velocity = Vector2.zero;
        }
    }

    void FlipSprite(float move)
    {
        if (move > 0)
        {
            _spriteRenderer.flipX = false;
            _swordSprite.flipX = false;
            _swordSprite.flipY = false;

            Vector3 newPosition = _swordSprite.transform.localPosition;
            newPosition.x = 1.01f;
            _swordSprite.transform.localPosition = newPosition;
        }

        else if (move < 0)
        {
            _spriteRenderer.flipX = true;
            _swordSprite.flipX = true;
            _swordSprite.flipY = true;

            Vector3 newPosition = _swordSprite.transform.localPosition;
            newPosition.x = -1.01f;
            _swordSprite.transform.localPosition = newPosition;
        }
    }

    IEnumerator ResetJumpNeeded()
    {
        _resetJump = true;
        yield return new WaitForSeconds(0.1f);
        _resetJump = false;
    }


    public void Damage()
    {
        Debug.Log("Player Damage");
    }
}

