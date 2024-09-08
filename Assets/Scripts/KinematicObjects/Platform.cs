using System.Collections;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] private Transform _destinationPoint;
    [SerializeField, Range(1f, 20f)] private float _speed;
    [SerializeField, Range(1f, 20f)] private float _waitTime = 2f;
    
    private float _ignoreTime = 0;
    private float _ignoreTimeMultplier = 1f;
    private float _waitingTime;
    private bool _returning = false;
    private bool _stopped = true;

    private Rigidbody _rb;
    private Rigidbody _playerRb;
    private Vector3 _startPoint;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _startPoint = _rb.transform.position;
    }

    private void FixedUpdate()
    {
        Vector3 moveDirection;

        moveDirection = (_destinationPoint.position - _startPoint).normalized * _speed;

        Debug.Log(_ignoreTime);

        if (_ignoreTime <= 0)
        {
            if (Vector3.Distance(_rb.transform.position, _destinationPoint.position) < 0.1f)
            {
                _returning = true;
                Wait();
            }
                
            if (Vector3.Distance(_rb.transform.position, _startPoint) < 0.05f)
            {
                _returning = false;
                Wait();
            }
        }
        else
            _ignoreTime--;

        if (!_stopped)
        {
            if (!_returning)
            {
                _rb.MovePosition(transform.position + moveDirection * Time.fixedDeltaTime);

                if (_playerRb != null)
                    _playerRb.MovePosition(_playerRb.transform.position + moveDirection * Time.fixedDeltaTime / 2);
            }
            else
            {
                _rb.MovePosition(transform.position - moveDirection * Time.fixedDeltaTime);

                if (_playerRb != null)
                    _playerRb.MovePosition(_playerRb.transform.position - moveDirection * Time.fixedDeltaTime / 2);
            }
                
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Player"))
            _playerRb = collision.transform.GetComponent<Rigidbody>();
    }
    
    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform.CompareTag("Player"))
            _playerRb = null;
    }

    private void Wait()
    {
        _stopped = true;
        _waitingTime -= Time.deltaTime;

        if (_waitingTime < 0)
        {
            _ignoreTime = _speed * _ignoreTimeMultplier;
            _stopped = false;
            _waitingTime = _waitTime;
        }
    }
}
