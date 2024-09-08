using UnityEngine;

public class MainCamera : MonoBehaviour
{
    private GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        Vector3 cameraPos = player.transform.position;
        cameraPos.y = player.transform.position.y + 7;

        gameObject.transform.position = cameraPos;
    }
}
