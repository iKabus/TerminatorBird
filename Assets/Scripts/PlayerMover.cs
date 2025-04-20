using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float _tapForce;
    [SerializeField] private float _speed;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _maxRotationZ;
    [SerializeField] private float _minRotationZ;

    private Vector3 _startPosition;

    private Rigidbody2D _rigidbody2D;

    private Quaternion _maxRotation;
    private Quaternion _minRotation;

    private Coroutine _coroutine;

    private Transform _chachedTransform;

    private void Awake()
    {
        _chachedTransform = transform;

        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        _startPosition = _chachedTransform.position;

        InitializeRotations();
        Reset();
    }

    public void MoveUp()
    {
        _rigidbody2D.linearVelocity = new Vector2(_speed, _tapForce);

        _chachedTransform.rotation = _maxRotation;

        if (_coroutine == null == false)
        {
            StopCoroutine(_coroutine);
        }

        _coroutine = StartCoroutine(RotateToMinAngle());
    }

    public void Reset()
    {
        if (_coroutine == null == false)
        {
            StopCoroutine(_coroutine );

            _coroutine = null;
        }

        transform.position = _startPosition;
        transform.rotation = Quaternion.identity;
        _rigidbody2D.linearVelocity = Vector2.zero;
    }

    private void InitializeRotations()
    {
        _maxRotation = Quaternion.Euler(0, 0, _maxRotationZ);
        _minRotation = Quaternion.Euler(0, 0, _minRotationZ);
    }

    private IEnumerator RotateToMinAngle()
    {
        while(Quaternion.Angle(_chachedTransform.rotation, _minRotation) > 0.1f)
        {
            _chachedTransform.rotation = Quaternion.Lerp(_chachedTransform.rotation, _minRotation, _rotationSpeed * Time.deltaTime);

            yield return null;
        }

        _chachedTransform.rotation = _minRotation;
    }
}
