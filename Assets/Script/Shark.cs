using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shark : MonoBehaviour
{
    public Transform boat_transform;
    public AudioSource attack_signal;
    private AudioSource emerge_audio_source;
    private Animator animator;

    [Header("Before an attack")]
    public float attack_interval = 15;
    public float attack_interval_interrupted = 60;
    public float next_attack = 0;
    [Header("During the attack")]
    public float signal_time = 4;
    private float signal_remaining_time;

    private bool time_to_attack = false;
    private float x_offset;
    private float z_offset;
    private Vector3 direction;
    private bool playing_animation;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        emerge_audio_source = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (time_to_attack != true)
        {
            next_attack -= Time.deltaTime;
        }

        if (next_attack <= 0)
        {
            PrepareAttack();
            next_attack = 999999999;
        }
    }

    private void FixedUpdate()
    {
        if (time_to_attack)
        {
            transform.position = new Vector3(boat_transform.position.x + x_offset, -1, boat_transform.position.z + z_offset);
            transform.LookAt(boat_transform);
            signal_remaining_time -= Time.deltaTime;

            if (signal_remaining_time <= 0)
            {
                attack_signal.Stop();
                if (!emerge_audio_source.isActiveAndEnabled)
                {
                    emerge_audio_source.enabled = true;
                }
                animator.enabled = true;
                animator.Play("Attack");
            }
        }
    }

    private void PrepareAttack()
    {
        time_to_attack = true;

        x_offset = Random.Range(-4f, 4f);
        z_offset = Random.Range(-4f, 4f);
        direction = boat_transform.position - transform.position;
        direction.y = 0;

        attack_signal.Play();
        signal_remaining_time = signal_time;
    }

    public void StopAttack()
    {
        attack_signal.Stop();
        time_to_attack = false;
        animator.Rebind();
        animator.enabled = false;
        emerge_audio_source.enabled = false;
        transform.position = new Vector3(transform.position.x, -3, transform.position.z);
        time_to_attack = false;
        next_attack = attack_interval;
    }
}
