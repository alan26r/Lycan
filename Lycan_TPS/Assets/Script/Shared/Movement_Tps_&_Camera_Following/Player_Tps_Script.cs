using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(MoveController_Script))]
public class Player_Tps_Script : MonoBehaviour
{
    // A placer sur le Player

    [System.Serializable]
    public class MouseInput
    {
        public Vector2 Damping; // fluidité du movement Camera

        public Vector2 Sensivity; // Sensivity deu movement Camera
    }

    [SerializeField] MouseInput Mousecontrol;
    [SerializeField] float speed;

    [System.Serializable]
    public class CheckGround
    {
        public float distToGround = 0.2f;
    }

    [SerializeField] CheckGround checkGround;

    public bool IsGrounded
    {
        get
        {
            return Physics.Raycast(transform.position, Vector3.down, checkGround.distToGround);
        }
            
    }

    #region Move Controller
    MoveController_Script m_MoveController;
    public MoveController_Script MoveController
    {
        get
        {
            if(m_MoveController == null)
            {
                m_MoveController = GetComponent<MoveController_Script>();
            }
            return m_MoveController;
        }
    }
#endregion

    InputController_Script playerInput;
    Vector2 mouseInput;

    private void Awake()
    {
        playerInput = GameManager.Instance.InputController;
        GameManager.Instance.LocalPlayer = this;
    }

    private void Start()
    {
        print("distToGround : " + checkGround.distToGround);     
    }

    private void Update()
    {
#region Debug Zone
        Debug.DrawRay(transform.position, -Vector3.up, Color.red,checkGround.distToGround);

        Debug.Log(Physics.Raycast(transform.position, Vector3.down, checkGround.distToGround));

        Debug.Log("distToGround : " + checkGround.distToGround);
#endregion

        Vector2 direction = new Vector2(playerInput.Vertical * speed, playerInput.Horizontal * speed);
        MoveController.Move(direction);

        mouseInput.x = Mathf.Lerp(mouseInput.x, playerInput.MouseInput.x, 1f / Mousecontrol.Damping.x);

        transform.Rotate(Vector3.up * mouseInput.x * Mousecontrol.Sensivity.x);

        Jump();
    }

    void Jump()
    {
        if (playerInput.Jump && IsGrounded)
        {
            print("Jump");
            MoveController.Jump();
        }
    }
}
