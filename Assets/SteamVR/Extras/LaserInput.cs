using System.Collections;
using System.Collections.Generic;
using Valve.VR;
using Valve.VR.Extras;
using Valve.VR.InteractionSystem;
using UnityEngine;

public class LaserInput : MonoBehaviour
{
    public static bool isClick = false;
    public static GameObject currentObject;
    int currentID;

    void Start()
    {
        currentObject = null;
        currentID = 0;
    }

    
    void Update()
    {
        RaycastHit[] hits;
        hits = Physics.RaycastAll(transform.position, transform.forward, 100.0F);

        for(int i=0; i<hits.Length; i++)
        {
            RaycastHit hit = hits[i];
            int id = hit.collider.gameObject.GetInstanceID();

            if(currentID != id)
            {
                currentID = id;
                currentObject = hit.collider.gameObject;
                string name = currentObject.name;

                if(name =="Next")
                {
                    Debug.Log("Hit NEXT1");
                }

                string tag = currentObject.tag;
                
                if(tag == "Button")
                {
                    Debug.Log("HIT BUTTON");
                }
            }
        }
    }
}
