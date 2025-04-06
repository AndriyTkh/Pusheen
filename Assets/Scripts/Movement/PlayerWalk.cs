using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalk : MonoBehaviour
{
    [SerializeField, Range(0.1f, 10f)] private float _speed = 4f;
    [SerializeField, Range(0f, 100f)] private float _maxAcceleration = 35f;
    [SerializeField, Range(0f, 100f)] private float _maxAirAcceleration = 20f;

    [SerializeField] private float _rotation;
    [SerializeField] private float _limitRotation;

    private Vector2 _velocity, _direction, _desiredVelocity;

    private Rigidbody2D _rb;
    private GroundCheck _collisionRetriever;
    private PlatformParenting _plat;

    private float _acceleration, _maxSpeedChange;
    private bool _onGround = false;

    [Space(10f)]
    public bool _transitionHold = false;
    public float _transitonXDir;


    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _collisionRetriever = GetComponent<GroundCheck>();
        _plat = GetComponent<PlatformParenting>();
        if (_transitionHold)
        {
            StartCoroutine(WaitControls());
        }
    }

    IEnumerator WaitControls()
    {
        yield return new WaitForSeconds(0.5f);
        _transitionHold = false;
    }

    void Update()
    {
        _direction.x = Input.GetAxis("Horizontal");

        if (_transitionHold)
            _direction.x = _transitonXDir;

        _desiredVelocity = new Vector2(_direction.x, 0f) * _speed;
    }

    private void FixedUpdate()
    {
        //Add force part

        _onGround = _collisionRetriever.OnGround;
        _velocity = _rb.velocity;

        _acceleration = _onGround ? _maxAcceleration : _maxAirAcceleration;
        _maxSpeedChange = _acceleration * Time.deltaTime;
        _velocity.x = Mathf.MoveTowards(_velocity.x, _desiredVelocity.x, _maxSpeedChange);

        _rb.velocity = _velocity;

        //Add RB rotation

        if (_direction.x > 0 && _rb.angularVelocity > -_limitRotation && _rb.angularVelocity < _limitRotation)
        {
            _rb.AddTorque(-_rotation * Time.fixedDeltaTime * _rb.gravityScale);
        }
        if (_direction.x < 0 && _rb.angularVelocity > -_limitRotation && _rb.angularVelocity < _limitRotation)
        {
            _rb.AddTorque(_rotation * Time.fixedDeltaTime * _rb.gravityScale);
        }


    }
}
