//======= Copyright (c) Valve Corporation, All rights reserved. ===============
using UnityEngine;
using System.Collections;

namespace Valve.VR.Extras
{
    public class SteamVR_LaserPointer : MonoBehaviour
    {
        public SteamVR_Behaviour_Pose pose;
        [SerializeField] public GameObject showCanvas;
        [SerializeField] public GameObject momCanvas;
        [SerializeField] public GameObject nagneCanvas;
        [SerializeField] public GameObject cow;
        [SerializeField] public GameObject giantCanvas;
        [SerializeField] public GameObject tresure;
        //public SteamVR_Action_Boolean interactWithUI = SteamVR_Input.__actions_default_in_InteractUI;
        public SteamVR_Action_Boolean interactWithUI = SteamVR_Input.GetBooleanAction("InteractUI");
        public bool isClick = false;
        public bool interacting = false;
        public bool active = true;
        public Color color;
        public float thickness = 0.002f;
        public Color clickColor = Color.green;
        public GameObject holder;
        public GameObject pointer;
        bool isActive = false;
        public bool addRigidBody = false;
        public Transform reference;
        public event PointerEventHandler PointerIn;
        public event PointerEventHandler PointerOut;
        public event PointerEventHandler PointerClick;
        public static GameObject currentObject;
        int currentID;
        //dialogueInter dialouge = new dialogueInter();
        Transform previousContact = null;


        private void Start()
        {
            currentObject = null;
            currentID = 0;

            if (pose == null)
                pose = this.GetComponent<SteamVR_Behaviour_Pose>();
            if (pose == null)
                Debug.LogError("No SteamVR_Behaviour_Pose component found on this object", this);

            if (interactWithUI == null)
                Debug.LogError("No ui interaction action has been set on this component.", this);


            holder = new GameObject();
            holder.transform.parent = this.transform;
            holder.transform.localPosition = Vector3.zero;
            holder.transform.localRotation = Quaternion.identity;

            pointer = GameObject.CreatePrimitive(PrimitiveType.Cube);
            pointer.transform.parent = holder.transform;
            pointer.transform.localScale = new Vector3(thickness, thickness, 100f);
            pointer.transform.localPosition = new Vector3(0f, 0f, 50f);
            pointer.transform.localRotation = Quaternion.identity;
            BoxCollider collider = pointer.GetComponent<BoxCollider>();
            if (addRigidBody)
            {
                if (collider)
                {
                    collider.isTrigger = true;
                }
                Rigidbody rigidBody = pointer.AddComponent<Rigidbody>();
                rigidBody.isKinematic = true;
            }
            else
            {
                if (collider)
                {
                    Object.Destroy(collider);
                }
            }
            Material newMaterial = new Material(Shader.Find("Unlit/Color"));
            newMaterial.SetColor("_Color", color);
            pointer.GetComponent<MeshRenderer>().material = newMaterial;
        }

        public virtual void OnPointerIn(PointerEventArgs e)
        {
            if (PointerIn != null)
                PointerIn(this, e);
        }

        public virtual void OnPointerClick(PointerEventArgs e)
        {
            if (PointerClick != null)
                PointerClick(this, e);
        }

        public virtual void OnPointerOut(PointerEventArgs e)
        {
            if (PointerOut != null)
                PointerOut(this, e);
        }


        private void Update()
        {

            RaycastHit[] hits;
            hits = Physics.RaycastAll(transform.position, transform.forward, 100.0F);

            if (isClick)
            {
                for (int i = 0; i < hits.Length; i++)
                {
                    RaycastHit hit1 = hits[i];
                    int id = hit1.collider.gameObject.GetInstanceID();

                    if (currentID != id && interacting == false)
                    {
                        
                        currentID = id;
                        currentObject = hit1.collider.gameObject;
                        string name = currentObject.name;

                        if (name == "Next")
                        {
                            //Debug.Log("Hit NEXT1");
                            interacting = true;
                        }

                        string tag = currentObject.tag;

                        if (tag == "Button")
                        {
                           // Debug.Log("HIT BUTTON");
                        }
                    }
                    else
                    {
                        interacting = false;
                    }
                }
            }

            if (!isActive)
            {
                isActive = true;
                this.transform.GetChild(0).gameObject.SetActive(true);
            }

            float dist = 100f;

            Ray raycast = new Ray(transform.position, transform.forward);
            RaycastHit hit;
            bool bHit = Physics.Raycast(raycast, out hit);

            if (previousContact && previousContact != hit.transform)
            {
                PointerEventArgs args = new PointerEventArgs();
                args.fromInputSource = pose.inputSource;
                args.distance = 0f;
                args.flags = 0;
                args.target = previousContact;
                OnPointerOut(args);
                previousContact = null;
            }
            if (bHit && previousContact != hit.transform)
            {
                PointerEventArgs argsIn = new PointerEventArgs();
                argsIn.fromInputSource = pose.inputSource;
                argsIn.distance = hit.distance;
                argsIn.flags = 0;
                argsIn.target = hit.transform;
                OnPointerIn(argsIn);
                previousContact = hit.transform;
            }
            if (!bHit)
            {
                previousContact = null;
            }
            if (bHit && hit.distance < 100f)
            {
                dist = hit.distance;
            }

            if (bHit && interactWithUI.GetStateUp(pose.inputSource))
            {
                //인터랙션
                if (hit.collider.gameObject.name == "momNPC")
                {
                    diaNext.index++;
                    showCanvas.SetActive(true);
                    momCanvas.SetActive(false);
                }
                if (hit.collider.gameObject.name == "nagneNPC")
                {
                    diaNext.index++;
                    showCanvas.SetActive(true);
                    nagneCanvas.SetActive(false);
                }
                if (hit.collider.gameObject.name == "cow2")
                {
                    cow.SetActive(false);
                }
                if (hit.collider.gameObject.name == "gaintNPC")
                {
                    diaNext.index++;
                    showCanvas.SetActive(true);
                    giantCanvas.SetActive(false);
                }
                if (hit.collider.gameObject.name == "tresure")
                {
                    tresure.SetActive(false);
                }
                if (hit.collider.gameObject.name == "Next")
                {
                    diaNext.index++;
                }
                Debug.Log(hit.collider.gameObject.name); //여기
                PointerEventArgs argsClick = new PointerEventArgs();
                argsClick.fromInputSource = pose.inputSource;
                argsClick.distance = hit.distance;
                argsClick.flags = 0;
                argsClick.target = hit.transform;
                OnPointerClick(argsClick);
            }

            if (interactWithUI != null && interactWithUI.GetState(pose.inputSource))
            {
                if (!isClick)
                {
                    //Debug.Log("click click");
                    isClick = true;
                }
                pointer.transform.localScale = new Vector3(thickness * 5f, thickness * 5f, dist);
                pointer.GetComponent<MeshRenderer>().material.color = clickColor;
            }
            else
            {
                if (isClick)
                {
                    //Debug.Log("off off");
                    isClick = false;
                }
                pointer.transform.localScale = new Vector3(thickness, thickness, dist);
                pointer.GetComponent<MeshRenderer>().material.color = color;
            }
            pointer.transform.localPosition = new Vector3(0f, 0f, dist / 2f);
        }
    }

    public struct PointerEventArgs
    {
        public SteamVR_Input_Sources fromInputSource;
        public uint flags;
        public float distance;
        public Transform target;
    }

    public delegate void PointerEventHandler(object sender, PointerEventArgs e);
}