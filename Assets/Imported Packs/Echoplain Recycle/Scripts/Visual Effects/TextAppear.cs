using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextAppear : MonoBehaviour
{
    private string _textContent;
    private int _lastLength = 0;

    public float _minDistance;
    public float _maxDistance;
    private float _allDis;


    public Text _textMeshPro;
    private Transform _player;

    public bool _soundy = true;

    private void Start()
    {
        _textContent = _textMeshPro.text + "         ";
        _allDis = _minDistance - _maxDistance;
        _player = GameObject.Find("Player").GetComponent<Transform>();
    }

    void FixedUpdate()
    {
        float _dis = Vector2.Distance(_player.position, transform.position);
        if (_dis < _minDistance)
        {
            if (_maxDistance > _dis)
            {
                _textMeshPro.text = _textContent.Substring(0, _textContent.Length - 1);
            }
            else
            {
                _textMeshPro.text = _textContent.Substring(0, (int)(_textContent.Length * (1 - (_dis - _maxDistance) / _allDis)));
                if (_lastLength != _textMeshPro.text.Length)
                {
                    if (_soundy)
                        SoundManager.Instance.PlaySound2D("Keyboard");
                }
                _lastLength = _textMeshPro.text.Length;
            }

        }
        else
        {
            _textMeshPro.text = "";
        }
    }
}
