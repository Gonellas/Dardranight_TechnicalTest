using System.Collections;
using UnityEngine;

public class SmallEnemy : Enemy
{
    [SerializeField] float _speed = 2f;
    [SerializeField] float _moveDistance = 1f;
    private Vector2 _startPos;
    private bool _isMoving = true;
    [SerializeField] float _timeToChangeDir = .2f;

    private void Start()
    {
        _startPos = transform.position;
        StartCoroutine(Patrol());
    }

    private IEnumerator Patrol()
    {
        while (true)
        {
            Vector2 targetPos = _isMoving ? _startPos + Vector2.right * _moveDistance : _startPos + Vector2.left * _moveDistance;
            float elapsedTime = 0f;

            while (elapsedTime < 1f)
            {
                transform.position = Vector2.Lerp(transform.position, targetPos, _speed * Time.deltaTime);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            _isMoving = !_isMoving;
            yield return new WaitForSeconds(_timeToChangeDir);
        }
    }

}
