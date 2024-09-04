using UnityEngine;

public class Player : MonoBehaviour
{
    public Camera Camera;
    public LayerMask Ground;

    [Range(10, 100)] public float forceLimit;
    [Range(10, 300)] public float forceRate;

    private float force = 0;

    LineRenderer lineRenderer;
    Rigidbody rb;

    RaycastHit hit;
    Ray ray;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        lineRenderer = GetComponent<LineRenderer>();
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(ray.origin, hit.point);
    }

    private void Update()
    {
        ray = Camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 300, Ground))
        {
            Vector3 p = hit.point;
            p.y = transform.position.y;

            Debug.Log(hit.point);

            Vector3 dir = (p - transform.position);
            dir.Normalize();

            if (Input.GetAxisRaw("Fire1") > 0)
            {
                lineRenderer.SetPosition(0, transform.position);
                lineRenderer.SetPosition(1, transform.position + (dir * (force / 10 + 2)));

                if (force < forceLimit)
                    force += forceRate * Time.deltaTime;
            }
            else if (force > 0)
            {
                rb.AddForce(dir * force, ForceMode.Impulse);
                force = 0;
            }
        }
    }
}
