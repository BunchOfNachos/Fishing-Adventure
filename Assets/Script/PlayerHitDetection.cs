using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitDetection : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Shark"))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Fishing");
        }

        if (other.gameObject.CompareTag("MainCamera"))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Fishing");
        }
    }
}
