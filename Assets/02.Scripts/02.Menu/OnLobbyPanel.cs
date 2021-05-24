using System.Collections;
using System.Collections.Generic;
using System.Security.Permissions;
using UnityEngine;
using UnityEngine.UI;

public class OnLobbyPanel : MonoBehaviour
{
    public GameObject LobbyPanel;
    public GameObject DisconnectPanel;
    public void OnButtonClick()
    {
        LobbyPanel.SetActive(true);
        DisconnectPanel.SetActive(false);
    }
}