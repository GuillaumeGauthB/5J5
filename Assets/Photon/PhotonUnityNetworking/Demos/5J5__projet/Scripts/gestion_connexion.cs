using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
//using Photon.Realtime;

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


    private GameObject ScriptName[];

    //private LoadBalancingClient loadBalancingClient;

    void Start()
    {
        //loadBalancingClient.RoomsCount;

        ScriptName = GameObject.FindObjectsOfType(typeof("setup_nom_joueur"));
    }

    private void Awake()
    {
        // Activer le Sync de la scene avec les autres joueurs
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    // Se connecter a une partie
    public void Connect()
    {
        if (!ScriptName.GetComponent<setup_nom_joueur>().CheckName())
            return "bruh";

        // on set la connection a true
        isConnecting = true;
        // On empeche la modification du nom
        controlPanel.SetActive(false);
        // Si la connexion fonctionne, connecter le joueur a une salle de jeu
        if (PhotonNetwork.IsConnected)
        {
            PhotonNetwork.JoinRandomRoom();
        }
        else
        {
            // si la connexion est en train de se faire, essayer de se connecter avec les infos pertinentes
            // LogFeedback("Connexion en cours...");
            PhotonNetwork.ConnectUsingSettings();
            PhotonNetwork.GameVersion = gameVersion;
        }
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

    // Fonction qui gere ce qui se passe lorsqu'aucune salle est trouvée
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        // Dire qu'aucune salle a été trouvé
        LogFeedback("Aucune room trouvee");
        LogFeedback("Creation d'une room...");
        // Créer une nouvelle salle
        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = maxPlayersPerRoom });
    }

    // Fonction qui gere la deconnection de l'utilisation
    public override void OnDisconnected(DisconnectCause cause)
    {
        // dire qu'on se deconnecte et annuler la connection
        LogFeedback($"Deconnecte : {cause}");
        isConnecting = false;
        controlPanel.SetActive(true);
    }

    // Fonction qui gere ce qui se passe quand le joueur se conencte a une salle
    public override void OnJoinedRoom()
    {
        // Load la scene
        if (PhotonNetwork.CurrentRoom.PlayerCount == 1)
        {
            PhotonNetwork.LoadLevel("5J5_Jeu");
        }
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
