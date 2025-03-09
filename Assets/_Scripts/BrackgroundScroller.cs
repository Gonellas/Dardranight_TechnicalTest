using UnityEngine;

public class BrackgroundScroller : MonoBehaviour
{
    [SerializeField] private float _scrollSpeed = 2f;
    [SerializeField] private Transform _bg1, _bg2; 

    private float _spriteHeight;

    private void Start()
    {
        _spriteHeight = _bg1.GetComponent<SpriteRenderer>().bounds.size.y;
    }

    private void Update()
    {
        _bg1.position += Vector3.down * _scrollSpeed * Time.deltaTime;
        _bg2.position += Vector3.down * _scrollSpeed * Time.deltaTime;

        if (_bg1.position.y <= -_spriteHeight)
        {
            _bg1.position = new Vector3(_bg1.position.x, _bg2.position.y + _spriteHeight, _bg1.position.z);
            SwapBackgrounds();
        }

        if (_bg2.position.y <= -_spriteHeight)
        {
            _bg2.position = new Vector3(_bg2.position.x, _bg1.position.y + _spriteHeight, _bg2.position.z);
            SwapBackgrounds();
        }
    }

    private void SwapBackgrounds()
    {
        Transform temp = _bg1;
        _bg1 = _bg2;
        _bg2 = temp;
    }
}

