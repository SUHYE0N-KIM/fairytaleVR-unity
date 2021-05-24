using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dialogueInter : MonoBehaviour
{
    [SerializeField]
    public GameObject showCanvas;
    [SerializeField]
    public GameObject momCanvas;

    void Start()
    {

    }

    public void showHide(string name)
    {
        if (name == "momNPC")
        {
            showCanvas.SetActive(true);
            momCanvas.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}