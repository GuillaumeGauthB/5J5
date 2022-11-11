using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class gestion_connexion : MonoBehaviourPunCallbacks
{
    //#region test lobby
        /*private TypedLobby customLobby = new TypedLobby("customLobby", LobbyType.Default);

        private Dictionary<string, RoomInfo> cachedRoomList = new Dictionary<string, RoomInfo>();

        public void JoinLobby()
        {
            PhotonNetwork.JoinLobby(customLobby);
        }*/
    //#endregion

    [SerializeField]
    private byte maxJoueursParSalle = 2;

    [SerializeField]
    private Text feedbackText;

    private void Awake()
    {
        // Activer le Sync de la scene avec les autres joueurs
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    // Se connecter a une partie
    public void Connect()
    {
        Debug.Log("bruh this do be kinda shit");
    }

    // Voir le Lobby
    public void ShowLobby()
    {
        Debug.Log("lmao top G");
    }

    // Fermer le Lobby
    public void ExitLobby()
    {

    }

    // Fonction gerant le FeedBack envoyer au joueur
    private void LogFeedback(string feedback)
    {
        // Faire apparaitre le log dans l'affichage
        feedbackText.text = feedback;
        feedbackText.text += System.Environment.NewLine + feedback;
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
