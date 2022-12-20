using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class joueur_local : MonoBehaviourPunCallbacks
{
    public static GameObject LocalPlayerInstance;
     public PhotonView photonView;
     public CharacterController characterController;
    public float speed = 200.0f;
    public float gravity = 20.0f;
    public float rotateSpeed = 8.0f;
    private Vector3 moveDirection = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        this.characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log(PhotonNetwork.CurrentRoom.Name);
        // Debug.Log(PhotonNetwork.CurrentLobby.Name);
        //Debug.Log("offline: " + PhotonNetwork.CurrentRoom.IsOffline);
        //Debug.Log("visible: " + PhotonNetwork.CurrentRoom.IsVisible);
        GameObject.Find("nom_salle").GetComponent<Text>().text = "Nom de salle: "+PhotonNetwork.CurrentRoom.Name+"   Nom lobby: "+PhotonNetwork.CurrentLobby.Name+"    Joueurs dans salle: "+PhotonNetwork.CurrentRoom.PlayerCount;
        if (photonView.IsMine)
        {
            ProcessInputs();
            LocalPlayerInstance = gameObject;
            Debug.Log(this.characterController.isGrounded);
        }
    }

    private void ProcessInputs()
    {
            
            this.moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            
            this.moveDirection = transform.TransformDirection(moveDirection);
            this.moveDirection *= speed;
            
            
        this.moveDirection.y -= gravity * Time.deltaTime;
        if (Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0)
        {
            this.GetComponent<Animator>().SetBool("Marche", false);
        }else
        {
            this.GetComponent<Animator>().SetBool("Marche", true);
        }
        characterController.Move(moveDirection * Time.deltaTime);
        //use the mouse to look around
        this.transform.Rotate(0, Input.GetAxis("Mouse X") * rotateSpeed, 0);
        //lock the cursor in center of screen
        Cursor.lockState = CursorLockMode.Locked;
    }
}
