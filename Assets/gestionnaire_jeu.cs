using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using Photon.Realtime;

#pragma warning disable 649
public class gestionnaire_jeu : MonoBehaviourPunCallbacks
{
    static public gestionnaire_jeu Instance;
    private GameObject instance;

    [SerializeField]
    private GameObject[] playerPrefab;

    [SerializeField]
    private Transform[] positionsInstances;

    private void Start()
    {
        if (Instance != null && Instance != this)
            Destroy(this);

        else
            Instance = this;

        // Si le joueur n'[est pas connecter...
        if (!PhotonNetwork.IsConnected)
        {
            // retourner a l'ecran de connection
            SceneManager.LoadScene("Scene_connexion");
            return;
        }

        // Donner une erreur si il n'y a pas de prefab pour le perso
        if (playerPrefab == null)
        {
            Debug.Log("Pas de reference au prefab du joueur");
            return;
        }

        else
        { 
            // Sinon, l'instantier dans la scene
            if (joueur_local.LocalPlayerInstance == null)
            {
                PhotonNetwork.Instantiate
                    (
                        this.playerPrefab[(PhotonNetwork.CurrentRoom.PlayerCount == 1 ? 0 : 1)].name,
                        positionsInstances[(PhotonNetwork.CurrentRoom.PlayerCount == 1 ? 0 : 1)].transform.position,
                        Quaternion.identity,
                        0
                    );
                /*if(PhotonNetwork.CurrentRoom.PlayerCount == 1)
                {
                    PhotonNetwork.Instantiate
                    (
                        this.playerPrefab[(PhotonNetwork.CurrentRoom.PlayerCount == 1 ? 0 : 1)].name,
                        positionsInstances[(PhotonNetwork.CurrentRoom.PlayerCount == 1 ? 0 : 1)].transform.position,
                        Quaternion.identity,
                        0
                    );
                }
                else
                {
                    PhotonNetwork.Instantiate
                    (
                        this.playerPrefab[0].name,
                        positionsInstances[0].transform.position,
                        Quaternion.identity,
                        0
                    );
                    PhotonNetwork.Instantiate
                    (
                        this.playerPrefab[1].name,
                        positionsInstances[1].transform.position,
                        Quaternion.identity,
                        0
                    );
                }*/
            }
        }

    }

    private void Update()
    {
        // Lorsque le joueur appuie sur Escape, le faire quitter le jeu
        if (Input.GetKeyDown(KeyCode.Escape))
        {

            Application.Quit();
        }
    }

    // Fonction qui fait charger l'arene si le joueur qui a entre la salle est le premier joueur
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        /*if (PhotonNetwork.IsMasterClient)
        {
            LoadArena();
        }*/
    }

    // Fonction qui fait changer le master client si il quitte la salle
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        /*if (PhotonNetwork.IsMasterClient)
        {
            LoadArena();
        }*/
    }

    // Fonction qui fait retourner a la scene de connexion
    public override void OnLeftRoom()
    {
        SceneManager.LoadScene("Scene_connexion");
    }

    // Fonction qui fait quitter la salle
    public void Quitter()
    {
        PhotonNetwork.LeaveRoom();
    }

    // Fonction qui fait quitter le jeu
    public void QuitApplication()
    {
        Application.Quit();
    }

    // Fonction qui gere le loading de la scene de jeu
    /*void LoadArena()
    {
        PhotonNetwork.LoadLevel("5J5_Jeu");
    }*/
}