using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Third_Person_Camera_Script : MonoBehaviour
{
    // a placer sur la Camera du joueur
    // ne pas mettre en enfant la camera du joueur

    [SerializeField] Vector3 cameraOffset; // distance caméra
    [SerializeField] float damping; // vitesse de repositionnement de la caméra
    Transform cameraLookTarget; // target set auto sur le joueur
    Player_Tps_Script localPlayer; // position joueur

    private void Awake()
    {
        GameManager.Instance.OnLocalPlayerJoined += HandleOnLocalPlayerJoined;
    }

    void HandleOnLocalPlayerJoined(Player_Tps_Script player)
    {
        localPlayer = player;
        cameraLookTarget = localPlayer.transform.Find("cameraLookTarget");

        if(cameraLookTarget == null)
        {
            cameraLookTarget = localPlayer.transform;
        }
    }

#region Update Movement
    private void Update()
    {
        Vector3 targetPosition = cameraLookTarget.position + localPlayer.transform.forward * cameraOffset.z + 
            localPlayer.transform.up * cameraOffset.y + 
            localPlayer.transform.right * cameraOffset.x;
        Quaternion targetRotation = Quaternion.LookRotation(cameraLookTarget.position - targetPosition, Vector3.up);

        transform.position = Vector3.Lerp(transform.position, targetPosition, damping * Time.deltaTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, damping * Time.deltaTime);
    }
#endregion
}
