using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Manager : MonoBehaviour
{
    public TextMeshProUGUI fish1_text;
    public TextMeshProUGUI fish2_text;
    public TextMeshProUGUI fish3_text;
    public TextMeshProUGUI timer_text;

    public GameObject fish_prefab;

    public float targetTime = 60f;
    public float fish_apparition_rate = 30f;
    private float last_fish_apparition = 0f;
    private Transform boat_transform;

    private int fish1_count = 0;
    private int fish2_count = 0;
    private int fish3_count = 0;

    // Start is called before the first frame update
    void Start()
    {
        timer_text.text = targetTime.ToString();
        SetCountText();
        boat_transform = GameObject.Find("Boat").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        targetTime -= Time.deltaTime;
        last_fish_apparition += Time.deltaTime;

        if (last_fish_apparition > fish_apparition_rate)
        {
            Vector3 spawn_position = new Vector3(Random.Range(-1, 1), 0, Random.Range(-1, 1)).normalized * 10;
            Instantiate(fish_prefab, boat_transform.position + spawn_position, Quaternion.identity);
            last_fish_apparition = 0f;
        }

        if (targetTime <= 0.0f)
        {
            timerEnded();
        }
        else
        {
            timer_text.text = targetTime.ToString("F2");
        }
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

    void timerEnded()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Fishing");
    }
}
