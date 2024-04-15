using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    public Manager game_manager;

    // Start is called before the first frame update
    void Start()
    {
        game_manager = GameObject.Find("Game manager").GetComponent<Manager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Projectile"))
        {
            game_manager.increase_score();
            game_manager.SetCountText();
        }
    }
}
