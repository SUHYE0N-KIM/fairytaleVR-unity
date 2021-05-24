using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnDisconnectPanel : MonoBehaviour
{
    public GameObject LobbyPanel;
    public GameObject DisconnectPanel;
    public void OnButtonClick()
    {
        DisconnectPanel.SetActive(true);
        LobbyPanel.SetActive(false);
    }
}