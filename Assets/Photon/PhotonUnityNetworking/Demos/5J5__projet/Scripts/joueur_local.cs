using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class joueur_local : MonoBehaviourPunCallbacks
{
     public PhotonView photonView;
     public CharacterController characterController;
    public float speed = 200.0f;
    public float gravity = 20.0f;
    public float rotateSpeed = 8.0f;
    private Vector3 moveDirection = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine)
        {
            ProcessInputs();
        }
    }

    private void ProcessInputs()
    {
        if (characterController.isGrounded)
        {
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;
        }
        moveDirection.y -= gravity * Time.deltaTime;
        characterController.Move(moveDirection * Time.deltaTime);
        //use the mouse to look around
        transform.Rotate(0, Input.GetAxis("Mouse X") * rotateSpeed, 0);
        //lock the cursor in center of screen
        Cursor.lockState = CursorLockMode.Locked;
    }
}
