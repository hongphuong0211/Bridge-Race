using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : CharacterInstance
{
    public Joystick joystick;
    public float speed;

    private void Start()
    {
        ChangeState(IdleState.Instance);
    }
    public void FixedUpdate()
    {
        if (joystick != null)
        {
            if (!IsState(FallState.Instance))
            {
                Vector3 direction = Vector3.forward * joystick.Vertical + Vector3.right * joystick.Horizontal;
                rb.velocity = direction;
                //rb.AddForce(direction * speed * Time.fixedDeltaTime);
                tf.forward = Vector3.Lerp(tf.forward, direction, 0.5f);
            }
        }
    }


}
