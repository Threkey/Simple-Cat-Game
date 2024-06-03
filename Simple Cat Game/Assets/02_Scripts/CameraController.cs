using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Cat");
    }

    // Update is called once per frame
    void Update()
    {
        if (Camera.main.WorldToScreenPoint(player.transform.position).y >= (Camera.main.pixelHeight / 2) && player.GetComponent<Rigidbody2D>().velocity.y > 0.2f)
            transform.position = new Vector3(transform.position.x, player.transform.position.y, transform.position.z);
    }
}
