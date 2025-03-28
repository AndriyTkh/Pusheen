using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxFlex : MonoBehaviour
{
    [SerializeField] private Vector2 _startProgress = new Vector2(1,1);
    [SerializeField] private Vector2 _minSize = new Vector2(0, 0);
    [SerializeField, Range(0.1f, 1f)] private float _ySpeed = 0.5f;
    [SerializeField, Range(0.1f, 1f)] private float _xSpeed = 0.5f;
    [SerializeField, Range(0.1f, 10f)] private float _multiplier = 1;

    public Vector2 _Dir = new Vector3(1, 1);

    [Space(30)]
    public bool _rotation = false;
    public bool _3D = false;
    [SerializeField, Range(-1f, 1f)] private float _rotationSpeed = 1;


    private Vector2 _startSize;

    void Start()
    {
        _startSize = transform.localScale;
        transform.localScale = new Vector2(_startSize.x * _startProgress.x, _startSize.y * _startProgress.y);
    }


    void FixedUpdate()
    {
        Vector3 size = transform.localScale;

        Direction(size);

        transform.localScale = new Vector2(size.x + (_xSpeed * Time.fixedDeltaTime * _Dir.x * _multiplier), 
            size.y + (_ySpeed * Time.fixedDeltaTime * _Dir.y * _multiplier));

        if (_rotation)
        {
            transform.rotation *= Quaternion.Euler(0, 0, _rotationSpeed * _multiplier);
        }
            
    }
    void Direction(Vector2 size)
    {
        if (_3D)
        {
            if (size.x < -_startSize.x && _Dir.x == -1)
            {
                _Dir.x = 1;
            }
            else if (size.x > _startSize.x && _Dir.x == 1)
            {
                _Dir.x = -1;
            }

            if (size.y < -_startSize.y && _Dir.y == -1)
            {
                _Dir.y = 1;
            }
            else if (size.y > _startSize.y && _Dir.y == 1)
            {
                _Dir.y = -1;
            }
        }
        else
        {
            if (size.x < _startSize.x * _minSize.x && _Dir.x == -1)
            {
                _Dir.x = 1;
            }
            else if (size.x > _startSize.x && _Dir.x == 1)
            {
                _Dir.x = -1;
            }

            if (size.y < _startSize.y * _minSize.y && _Dir.y == -1)
            {
                _Dir.y = 1;
            }
            else if (size.y > _startSize.y && _Dir.y == 1)
            {
                _Dir.y = -1;
            }
        }
    }
}
