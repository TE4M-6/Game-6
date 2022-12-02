using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

/// <summary>
/// AUTHOR: @Nuutti J.
/// Last modified: 1 Dec. 2022 by @Nuutti J.
/// </summary>

public class CameraController : MonoBehaviour {

    /* EXPOSED FIELDS: */
    [SerializeField] CinemachineVirtualCamera mainCam;
    [SerializeField] CinemachineVirtualCamera camToSwitch;
    [SerializeField] Transform roomEntry;
    [SerializeField] Transform roomExit;

    /* HIDDEN FIELDS: */
    bool isInRoom = false;

    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            isInRoom = !isInRoom;

            if(isInRoom) {
                mainCam.Priority = 0;
                camToSwitch.Priority = 1;
                collision.transform.position = roomEntry.position;
            } else {
                mainCam.Priority = 1;
                camToSwitch.Priority = 0;
                collision.transform.position = roomExit.position;
            }
            
        }
    }
}
