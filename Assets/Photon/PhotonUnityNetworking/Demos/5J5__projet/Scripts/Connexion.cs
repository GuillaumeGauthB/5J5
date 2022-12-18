using System.Collections;   
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

#pragma warning disable 649

public class Connexion : MonoBehaviourPunCallbacks
{

    [SerializeField]
    private GameObject controlPanel;

    [SerializeField]
    private Text feedbackText;

    [SerializeField]
    private byte maxPlayersPerRoom = 2;

    bool isConnecting = false,
        inLobby = false;

    string gameVersion = "1";

    private TypedLobby customLobby = new TypedLobby("baseLobby", LobbyType.Default);

    private Dictionary<string, RoomInfo> cachedRoomList = new Dictionary<string, RoomInfo>();

    private List<string> roomNames = new List<string>();

    public void Update()
    {
        //Debug.Log("actual: " + cachedRoomList.Count);
    }

    private void Awake()
    {
        // Activer le Sync de la scene avec les autres joueurs
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    // Fonction qui gere la connection au jeu
    public void Connect()
    {
        // Vider le log d'info
        feedbackText.text = "";
        // on set la connection a true
        isConnecting = true;
        // On empeche la modification du nom
        controlPanel.SetActive(false);
        // Si la connexion fonctionne, connecter le joueur a une salle de jeu
        if (PhotonNetwork.IsConnected)
        {
            
        }
        else
        {
            // si la connexion est en train de se faire, essayer de se connecter avec les infos pertinentes
            LogFeedback("Connexion en cours...");
            PhotonNetwork.ConnectUsingSettings();
            PhotonNetwork.GameVersion = gameVersion;
        }
    }

    // Fonction gerant le feedback
    private void LogFeedback(string feedback)
    {
        // Faire apparaitre le log dans l'affichage
        feedbackText.text = feedback;
        feedbackText.text += System.Environment.NewLine + feedback;
    }

    private void UpdateCachedRoomList(List<RoomInfo> roomList)
    {
        for (int i = 0; i < roomList.Count; i++)
        {
            RoomInfo info = roomList[i];
            if (info.RemovedFromList)
            {
                cachedRoomList.Remove(info.Name);
            }
            else
            {
                cachedRoomList[info.Name] = info;
            }
        }
    }

    public void JoinLobby()
    {
        PhotonNetwork.JoinLobby(customLobby);
    }

    public override void OnJoinedLobby()
    {
        inLobby = true;
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        UpdateCachedRoomList(roomList);

        if(inLobby == true)
        {
            // Si le joueur est en train de se connecter
            if (isConnecting)
            {
                bool found = false;
                RoomInfo roomToJoin = null;
                Debug.Log("photon: " + PhotonNetwork.CountOfRooms);
                if(roomList.Count == 1)
                    Debug.Log("actual: " + roomList[0].Name);
                /*foreach (KeyValuePair<string, RoomInfo> kv in cachedRoomList)
                {
                    RoomInfo info = kv.Value;
                    Debug.Log("test:: " + kv.Value.CustomProperties["secret"].ToString());
                    if (info.CustomProperties["secret"].ToString() == PlayerPrefs.GetString("code_value"))
                    {
                        found = true;
                        roomToJoin = info;
                    }

                }*/

                for (int i = 0; i < roomList.Count; i++)
                {
                    RoomInfo info = roomList[i];
                    Debug.Log("test:: " + info.Name);
                    if (info.Name == PlayerPrefs.GetString("code_value"))
                    {
                        found = true;
                        roomToJoin = info;
                    }

                    roomNames.Add(info.Name);
                }

                // Le connecter
                //PhotonNetwork.JoinRandomRoom();
                if (found)
                {
                    PhotonNetwork.JoinRoom(roomToJoin.Name);
                }
                else
                {
                    CreateNewRoomWithPW();
                }
                LogFeedback("Connecte");
            }
        }
    }

    public override void OnLeftLobby()
    {
        cachedRoomList.Clear();
    }

    public override void OnConnectedToMaster()
    {
        JoinLobby();
    }

    // Fonction qui gere ce qui se passe lorsqu'aucune salle est trouvée
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        if (isConnecting)
        {
            // Dire qu'aucune salle a été trouvé
            LogFeedback("Aucune room trouvee");
            LogFeedback("Creation d'une room...");
            // Créer une nouvelle salle
            CreateNewRoomWithPW();
        }
    }

    // Fonction qui gere la deconnection de l'utilisation
    public override void OnDisconnected(DisconnectCause cause)
    {
        // dire qu'on se deconnecte et annuler la connection
        LogFeedback($"Deconnecte : {cause}");
        isConnecting = false;
        controlPanel.SetActive(true);

        cachedRoomList.Clear();
    }

    // Fonction qui gere ce qui se passe  quand le joueur se conencte a une salle
    public override void OnJoinedRoom()
    {
        // Load la scene
        if (PhotonNetwork.CurrentRoom.PlayerCount == 1)
        {
            PhotonNetwork.CurrentRoom.IsVisible = true;
            /*ExitGames.Client.Photon.Hashtable table = new ExitGames.Client.Photon.Hashtable();
                table.Add("secret", PlayerPrefs.GetString("code_value"));
            PhotonNetwork.CurrentRoom.SetCustomProperties(table);*/
            string[] a = { "secrets" };
            PhotonNetwork.CurrentRoom.SetPropertiesListedInLobby(a);

            PhotonNetwork.LoadLevel("5J5_Jeu");
            Destroy(this);
        }
    }

    public void CreateNewRoomWithPW()
    {
        if (roomNames.Contains(PlayerPrefs.GetString("code_value")))
            return;
        ExitGames.Client.Photon.Hashtable table = new ExitGames.Client.Photon.Hashtable();
            table.Add("secret", PlayerPrefs.GetString("code_value"));

        RoomOptions roomOptions = new RoomOptions();
        roomOptions.CustomRoomProperties = table;
        roomOptions.MaxPlayers = 2;

        PhotonNetwork.CreateRoom(PlayerPrefs.GetString("code_value"), roomOptions);
    }
}
