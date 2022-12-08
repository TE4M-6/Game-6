using UnityEngine;
using Cinemachine;

/// <summary>
/// AUTHOR: @Nuutti J.
/// Last modified: 05 Dec. 2022 by @Daniel K.
/// </summary>

public class CameraController : MonoBehaviour {

    /* EXPOSED FIELDS: */
    [SerializeField] CinemachineVirtualCamera mainCam;
    [SerializeField] CinemachineVirtualCamera camToSwitch;
    [SerializeField] Transform roomEntry;
    [SerializeField] Transform roomExit;

    /* HIDDEN FIELDS: */
    static bool isInRoom;

    private void Start()
    {
        isInRoom = true;
        mainCam.Priority = 0;
        camToSwitch.Priority = 1;
    }

    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("PlayerFeet")) {
            Transform player = collision.gameObject.transform.parent.gameObject.transform;
            isInRoom = !isInRoom;

            if(isInRoom) {
                mainCam.Priority = 0;
                camToSwitch.Priority = 1;
                player.position = roomEntry.position;
            } else {
                mainCam.Priority = 1;
                camToSwitch.Priority = 0;
                player.transform.position = roomExit.position;
            }
        }
    }

    // Daniel K. - 02/12/2022
    public static bool GetIsInRoom()
    {
        return isInRoom;
    }
}
