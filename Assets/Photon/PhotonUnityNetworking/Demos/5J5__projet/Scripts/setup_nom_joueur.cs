using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class setup_nom_joueur : MonoBehaviour
{

    [Header("Nom du joueur")]
    const string playerNomBase = "dani stole my money";

    void Start()
    {
        string defaultName = string.Empty;
        InputField _inputField = GetComponent<InputField>();

        // si le input field existe, attribuer le nom du joueur dans le input au nickname photon
        if (_inputField != null)
        {
            if (PlayerPrefs.HasKey(playerNomBase))
            {
                defaultName = PlayerPrefs.GetString(playerNomBase);
                _inputField.text = defaultName;
            }
        }
        PhotonNetwork.NickName = defaultName;
    }

    // Fonction qui definit le nom du joueur dans les preferences du systeme
    public void SetPlayerName(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            Debug.Log("Nom du joueur non valide");
            return;
        }
        PhotonNetwork.NickName = value;
        PlayerPrefs.SetString(playerNomBase, value);
    }

    public void CheckName()
    {
        return string.IsNullOrEmpty(PhotonNetwork.NickName) ? false : true;
    }
}
