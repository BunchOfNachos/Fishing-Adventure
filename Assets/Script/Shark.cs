using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shark : MonoBehaviour
{
    public Transform boat_transform;
    public AudioSource attack_signal;
    public Animator animator;

    [Header("Before an attack")]
    public float attack_interval = 15;
    public float attack_interval_interrupted = 45;
    public float next_attack = 15;
    [Header("During the attack")]
    public float signal_time = 4;
    private float signal_remaining_time;

    private bool time_to_attack = false;
    private float x_offset;
    private float z_offset;
    private Vector3 direction;

    private Manager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game manager").GetComponent<Manager>();
        boat_transform = GameObject.Find("Boat").GetComponent<Transform>();
        transform.position = new Vector3(transform.position.x, -10000, transform.position.z);
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
            transform.position = new Vector3(boat_transform.position.x + x_offset, 0, boat_transform.position.z + z_offset);
            transform.LookAt(boat_transform);
            signal_remaining_time -= Time.deltaTime;

            if (signal_remaining_time <= 0)
            {
                attack_signal.Stop();
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
        animator.Rebind();
        time_to_attack = false;
        transform.position = new Vector3(transform.position.x, -10000, transform.position.z);
        time_to_attack = false;
        next_attack = attack_interval;
    }

    public void StopAttack(float time_until_next_attack)
    {
        attack_signal.Stop();
        animator.Rebind();
        time_to_attack = false;
        transform.position = new Vector3(transform.position.x, -10000, transform.position.z);
        time_to_attack = false;
        next_attack = time_until_next_attack;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Projectile"))
        {
            StopAttack(attack_interval_interrupted);
        }
    }
}
