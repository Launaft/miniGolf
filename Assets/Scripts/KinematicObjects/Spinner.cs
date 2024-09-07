using UnityEngine;

public class Spinner : MonoBehaviour
{
    [Range(10f, 300f)] public float Speed = 60f;
    public bool Clockwise = false;

    private Rigidbody _rb;
    private Vector3 _angleVelocity;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _angleVelocity = new Vector3(0, Speed, 0);
    }

    private void FixedUpdate()
    {
        Quaternion deltaRotation;

        if (_angleVelocity.y != Speed)
            _angleVelocity = new Vector3(0, Speed, 0);

        if (Clockwise)
            deltaRotation = Quaternion.Euler(_angleVelocity * Time.fixedDeltaTime);
        else
            deltaRotation = Quaternion.Euler(-_angleVelocity * Time.fixedDeltaTime);

        _rb.MoveRotation(_rb.rotation * deltaRotation);

        Debug.Log(Speed);
    }
}
