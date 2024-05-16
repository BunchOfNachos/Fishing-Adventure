using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    public Manager game_manager;
    public Animator animator;
    public AudioSource audio_source;
    public AudioClip caught_sound;

    private Transform boat_transform;

    public float speed = 1;
    public float directionChangeInterval = 1;
    public float maxHeadingChange = 30;

    CharacterController controller;
    float heading;
    Vector3 targetRotation;

    void Awake()
    {
        controller = GetComponent<CharacterController>();

        // Set random initial rotation
        heading = Random.Range(0, 360);
        transform.eulerAngles = new Vector3(0, heading, 0);

        StartCoroutine(NewHeading());
    }

    // Start is called before the first frame update
    void Start()
    {
        game_manager = GameObject.Find("Game manager").GetComponent<Manager>();
        boat_transform = GameObject.Find("Boat").GetComponent<Transform>();
        teleport();
        StartCoroutine(jump());
    }

    void Update()
    {
        transform.eulerAngles = Vector3.Slerp(transform.eulerAngles, targetRotation, Time.deltaTime * directionChangeInterval);
        var forward = transform.TransformDirection(Vector3.forward);
        transform.position = Vector3.Slerp(transform.position, transform.position + forward, Time.deltaTime * speed);
        //controller.SimpleMove(forward * speed);
    }

    /// <summary>
    /// Repeatedly calculates a new direction to move towards.
    /// Use this instead of MonoBehaviour.InvokeRepeating so that the interval can be changed at runtime.
    /// </summary>
    IEnumerator NewHeading()
    {
        while (true)
        {
            NewHeadingRoutine();
            yield return new WaitForSeconds(directionChangeInterval);
        }
    }

    /// <summary>
    /// Calculates a new direction to move towards.
    /// </summary>
    void NewHeadingRoutine()
    {
        var floor = transform.eulerAngles.y - maxHeadingChange;
        var ceil = transform.eulerAngles.y + maxHeadingChange;
        heading = Random.Range(floor, ceil);
        targetRotation = new Vector3(0, heading, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Projectile"))
        {
            game_manager.increase_score();
            game_manager.SetCountText();
            audio_source.PlayOneShot(caught_sound);

            teleport();
        }
    }

    private void teleport()
    {
        float x_offset = Random.Range(-4f, 4f);
        float z_offset = Random.Range(-4f, 4f);

        transform.position = new Vector3(boat_transform.position.x + x_offset, transform.position.y, boat_transform.position.z + z_offset);
    }

    private IEnumerator jump()
    {
        while (true)
        {
            animator.SetBool("jump", true);
            yield return 0;
            animator.SetBool("jump", false);

            yield return new WaitForSeconds(5f);
        }
    }
}
