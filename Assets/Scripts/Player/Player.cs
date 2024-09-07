using UnityEngine;
using UnityEngine.UI;
public class Player : MonoBehaviour
{
    public Text txt1;
    public Text txt2;
    public Text txt3;
    public Text txt4;
    private int deaths;
    private int wins;
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

    Vector3 startPos;//Стартовая позиция шара

    private void Start()
    {
        startPos = transform.position;
        rb = GetComponent<Rigidbody>();
        lineRenderer = GetComponent<LineRenderer>();
        txt1.text = ("Победы: " + wins.ToString());
        txt2.text = ("Поражения: " + deaths.ToString());
        txt3.text = ("Сила: " + force.ToString());
        txt4.text = ("Ускорения: " + acceleration.ToString());
    }

    public void OnTriggerEnter(Collider Trigger)//Срабатывает, когда шар касается объекта с триггером
    {
        if (Trigger.CompareTag("KZone"))
        {
            transform.position = startPos;
            force = 0;
            deaths++;
            txt2.text = ("Поражения: " + deaths.ToString());
            acceleration = 0;
            txt4.text = ("Ускорения: " + acceleration.ToString());
        }
        if (Trigger.CompareTag("Finish"))
        {
            transform.position = startPos;
            force = 0;
            wins++;
            txt1.text = ("Победы: " + wins.ToString());
            acceleration = 0;
            txt4.text = ("Ускорения: " + acceleration.ToString());
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(ray.origin, hit.point);
    }
    //строка для проверки
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
                acceleration++;
                txt4.text = ("Ускорения: " + acceleration.ToString());
                rb.AddForce(dir * force, ForceMode.Impulse);
                force = 0;
            }
        }
    }
}
