using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController_Script : MonoBehaviour
{
    // Ne pas placer dans la scène

    public float Vertical;
    public float Horizontal;
    public Vector2 MouseInput;
    public bool Fire1;
    public bool Jump;

    private void Update()
    {
        Vertical = Input.GetAxis("Vertical");
        Horizontal = Input.GetAxis("Horizontal");
        MouseInput = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        Fire1 = Input.GetButton("Fire1");
        Jump = Input.GetButton("Jump");
    }
}
