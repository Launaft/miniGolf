using UnityEngine;

public class MainCamera : MonoBehaviour
{
    private GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        Vector3 cameraPos = player.transform.position;
        cameraPos.y = 7;
        cameraPos.z /= 2;

        gameObject.transform.position = cameraPos;
    }
}
