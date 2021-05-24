using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnPlay : MonoBehaviour
{
    public void OnButtonClick()
    {
        SceneManager.LoadScene("Load.Portal");
    }
}