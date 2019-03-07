using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController_Script : MonoBehaviour
{
    public float jumpForce = 20;
    // A placer sur le joueur
    public void Move(Vector2 direction)
    {
        transform.position += transform.forward * direction.x * Time.deltaTime +
            transform.right * direction.y * Time.deltaTime;
    }

    public void Jump()
    {
        GetComponent<Rigidbody>().AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }
}
