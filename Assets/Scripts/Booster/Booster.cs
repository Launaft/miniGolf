using UnityEngine;

public class Booster : MonoBehaviour
{
    [Range(10f, 100f)] public float Force;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            other.transform.GetComponent<Rigidbody>().AddForce(-transform.forward * Force, ForceMode.Impulse);
    }
}
