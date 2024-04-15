using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Manager : MonoBehaviour
{
    public TextMeshProUGUI fish1_text;
    public TextMeshProUGUI fish2_text;
    public TextMeshProUGUI fish3_text;

    private int fish1_count = 0;
    private int fish2_count = 0;
    private int fish3_count = 0;

    // Start is called before the first frame update
    void Start()
    {
        SetCountText();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetCountText()
    {
        fish1_text.text = "X " + fish1_count.ToString();
        fish2_text.text = "X " + fish2_count.ToString();
        fish3_text.text = "X " + fish3_count.ToString();
    }

    public void increase_score()
    {
        fish1_count++;
    }
}
