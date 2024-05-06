using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishIndicator : MonoBehaviour
{
    private Transform player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Main Camera").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(player.position);
    }
}
