using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boat : MonoBehaviour
{
    private int moving = 0;
    private float wheel_rotation = 0f;
    private Rigidbody rb;

    public float speed = 20f;
    public float force = 0.5f;
    public float lerp_time = 0.5f;
    public float steering_speed = 0.5f;
    public GameObject wheel;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (moving == 1){
            rb.AddForce(-this.transform.right * force);
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, speed);

            Vector3 m_EulerAngleVelocity = new Vector3(0, -90*wheel_rotation, 0);
            Quaternion deltaRotation = Quaternion.Euler(m_EulerAngleVelocity * Time.fixedDeltaTime);
            rb.MoveRotation(rb.rotation * deltaRotation);

            //rb.AddRelativeTorque(Vector3.up * wheel_rotation);
        }
        rb.position.Set(this.rb.position.x, 0, this.rb.position.z);
        rb.rotation.Set(0, rb.rotation.y, 0, rb.rotation.w);
    }

    public void UpdateRotation()
    {
        wheel_rotation = wheel.transform.rotation.y/0.7071068f;
    }

    public void StartMovement()
    {
        moving = 1;
    }

    public void StopMovement()
    {
        moving = 0;
    }
}
