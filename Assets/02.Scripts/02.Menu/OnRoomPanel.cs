using System.Collections;
using System.Collections.Generic;
using System.Security.Permissions;
using UnityEngine;
using UnityEngine.UI;

public class OnRoomPanel : MonoBehaviour
{
    public GameObject LobbyPanel;
    public GameObject RoomPanel;
    public void OnButtonClick()
    {
        RoomPanel.SetActive(true);
        LobbyPanel.SetActive(false);
    }
}