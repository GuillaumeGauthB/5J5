using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputFieldNom : MonoBehaviour
{
    string playerNomPrefKey = "nom_value"; // nom du joueur

    // Start is called before the first frame update
    void Start()
    {
        string defaultName = string.Empty; // creer une string vide
        InputField _inputField = GetComponent<InputField>(); // prendre le champ texte ou l'utilisateur set son nom

        // si le field input n'est pas vide...
        if (_inputField != null)
        {
            // Si le nom de l'utilisateur a deja ete set
            if (PlayerPrefs.HasKey(playerNomPrefKey))
            {
                // l'utiliser
                defaultName = PlayerPrefs.GetString(playerNomPrefKey);
                _inputField.text = defaultName;
            }
            /*else
            {
                // sinon, creer l'endroit pour save le nom de du joueur et donner sa valeur a defaultName
                PlayerPrefs.SetString(playerNomPrefKey, "Player");
                defaultName = PlayerPrefs.GetString(playerNomPrefKey);
            }*/
        }
        // Donner le nom du joueur comme nickname
        PhotonNetwork.NickName = defaultName;
    }

    // Fonction qui gere la creation du nickname
    public void OnPlayButtonPressed(string Value)
    {
        // Si le input field est vide...
        if (string.IsNullOrEmpty(Value))
        {
            // ne rien faire et faire apparaitre un message d'erreur
            Debug.Log("Nom du joueur empty");
            return;
        }
        // set le contenu du input field comme value
        PhotonNetwork.NickName = Value;
        // sauvegarder le nickname
        PlayerPrefs.SetString(playerNomPrefKey, Value);
    }
}
