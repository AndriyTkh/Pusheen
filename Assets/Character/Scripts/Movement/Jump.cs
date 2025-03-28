using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    [SerializeField] private float _jumpForce;
    [SerializeField] private bool _onGround;
    [SerializeField, Range(0f, 0.5f)] private float _maxCoyoteTime = 0.2f;
    [SerializeField, Range(0f, 0.5f)] private float _maxJumpBufferCounter = 0.2f;
    private float _coyoteTime, _jumpBufferCounter;

    private Rigidbody2D _rb;
    private GroundCheck _collisionRetriever;

    public bool _entryJump = false;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _collisionRetriever = GetComponent<GroundCheck>();

        if (_entryJump)
        {
            _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, 0);
            _rb.AddForce(Vector2.up * 8 * _rb.gravityScale, ForceMode2D.Impulse);

            SoundManager.Instance.PlaySound2D("Player-jump");
        }
    }

    void Update()
    {
        _onGround = _collisionRetriever.OnGround;

        // Added coyote time because of player shape

        if (_onGround)
        {
            _coyoteTime = _maxCoyoteTime;
        }
        else
        {
            _coyoteTime -= Time.deltaTime;
        }

        //Jump Buffer

        if (_jumpBufferCounter > 0)
        {
            _jumpBufferCounter -= Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Space) && !_onGround)
        {
            _jumpBufferCounter = _maxJumpBufferCounter;
        }

        //Performing jump

        if (Input.GetKeyDown(KeyCode.Space) || _jumpBufferCounter > 0)
        {
            if (_onGround || _coyoteTime > 0)
            {
                _jumpBufferCounter = -1;
                _coyoteTime = -1;
                _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, 0);
                _rb.AddForce(Vector2.up * _jumpForce * _rb.gravityScale, ForceMode2D.Impulse);

                //SoundManager.Instance.PlaySound2D("Player-jump");
            }
        }
    }
}
