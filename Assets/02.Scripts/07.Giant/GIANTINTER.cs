using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GIANTINTER : MonoBehaviour
{
    public static int count = 0;
    [SerializeField] GameObject bomool;
    public GameObject NextScene;
    void OnTriggerEnter(Collider c)
    {
        if (c.name == "Bow Arrow")
        {
            count++;
            if(count==3)
            {
                bomool.SetActive(true);
                NextScene.SetActive(true);
                Destroy(c.gameObject);
                Destroy(this.gameObject);
            }
        }
    }
}