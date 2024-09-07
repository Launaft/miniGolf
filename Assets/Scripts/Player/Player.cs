using System;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public Text ForceTxt;
    public Text PunchesTxt;
    private int acceleration;

    public Camera Camera;
    public LayerMask Ground;

    [Range(10, 100)] public float forceLimit;
    [Range(10, 300)] public float forceRate;

    private float force = 0;

    LineRenderer lineRenderer;
    Rigidbody rb;

    RaycastHit hit;
    Ray ray;

    Vector3 startPos;

    private void Start()
    {
        startPos = transform.position;
        rb = GetComponent<Rigidbody>();
        lineRenderer = GetComponent<LineRenderer>();
        ForceTxt.text = ("Force: " + force.ToString());
        PunchesTxt.text = ("Punches: " + acceleration.ToString());
    }

    public void OnTriggerEnter(Collider Trigger)
    {
        if (Trigger.CompareTag("KZone"))
        {
            acceleration = 0;
            PunchesTxt.text = ("Punches: " + acceleration.ToString());
        }
        if (Trigger.CompareTag("Finish"))
        {
            acceleration = 0;
            PunchesTxt.text = ("Punches: " + acceleration.ToString());
        }
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

            Vector3 dir = (p - transform.position);
            dir.Normalize();

            if (Input.GetAxisRaw("Fire1") > 0)
            {
                lineRenderer.SetPosition(0, transform.position);
                lineRenderer.SetPosition(1, transform.position + (dir * (force / 10 + 2)));

                if (force < forceLimit)
                    force += forceRate * Time.deltaTime;

                ForceTxt.text = ("Force: " + Math.Round(force).ToString());
            }
            else if (force > 0)
            {
                
                acceleration++;
                PunchesTxt.text = ("Punches: " + acceleration.ToString());
                rb.AddForce(dir * force, ForceMode.Impulse);
                force = 0;
            }
        }
    }
}
