using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Manager : MonoBehaviour
{
    public TextMeshProUGUI fish1_text;
    public TextMeshProUGUI fish2_text;
    public TextMeshProUGUI fish3_text;
    public TextMeshProUGUI timer_text;
    public Text timer;
    public Button start_button;

    public GameObject fish_prefab;
    public GameObject shark_prefab;

    public float initial_target_time = 120f;
    private float targetTime = 120f;
    public float fish_apparition_rate = 30f;
    private float last_fish_apparition = 0f;
    private Transform boat_transform;

    private int fish1_count = 0;
    private int fish2_count = 0;
    private int fish3_count = 0;

    private bool game_is_playing = false;

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
        if (game_is_playing)
        {
            targetTime -= Time.deltaTime;
            last_fish_apparition += Time.deltaTime;

            if (last_fish_apparition > fish_apparition_rate)
            {
                Instantiate(fish_prefab);
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

    public void StartGame()
    {
        game_is_playing = true;
        start_button.gameObject.SetActive(false);
        timer_text.gameObject.SetActive(true);
        targetTime = initial_target_time;
        fish1_count = 0;

        Instantiate(fish_prefab);
        Instantiate(shark_prefab);
    }

    public void timerEnded()
    {
        game_is_playing = false;
        timer_text.gameObject.SetActive(false);
        start_button.gameObject.SetActive(true);

        GameObject[] objects_to_destroy = GameObject.FindGameObjectsWithTag("Destroy");

        foreach (GameObject item in objects_to_destroy)
        {
            Destroy(item);
        }
    }
}
