using UnityEngine;
using System.Collections;

namespace Valve.VR.InteractionSystem.Sample
{
    public class Grenade : MonoBehaviour
    {
        public GameObject MoveScene;
        public int count = 0;
        public GameObject explodePartPrefab;
        public GameObject beanTree;
        public GameObject bean;
        public int explodeCount = 10;
        public bool noroutine = true;
        public float minMagnitudeToExplode = 1f;

        private Interactable interactable;
        private IEnumerator routine;
        private void Start()
        {

            interactable = this.GetComponent<Interactable>();
            routine = Giant();
        }

        private void Update()
        {
            if (bean.transform.position.y < 0 && noroutine)
            {
                noroutine = false;
                StartCoroutine("Giant");
            }
        }
        public void OnCollisionEnter(Collision collision)
        {
            if (interactable != null && interactable.attachedToHand != null) //don't explode in hand
            {
                this.gameObject.AddComponent<Rigidbody>().useGravity = true;
                return;
            }

            if (collision.impulse.magnitude > minMagnitudeToExplode)
            {
                Debug.Log("boom");
                if (noroutine)
                {
                    noroutine = false;
                    StartCoroutine("Giant");
                }
                for (int explodeIndex = 0; explodeIndex < explodeCount; explodeIndex++)
                {
                    GameObject explodePart = (GameObject)GameObject.Instantiate(explodePartPrefab, this.transform.position, this.transform.rotation);
                    explodePart.GetComponentInChildren<MeshRenderer>().material.SetColor("_TintColor", Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f));
                }

                //Destroy(this.gameObject);
            }
        }
        void OnTriggerEnter(Collider c)
        {
            if (c.gameObject.name == "Terrain" && noroutine)
            {
                Debug.Log("grenade");
                noroutine = false;
                StartCoroutine("Giant");
            }
        }

        public IEnumerator Giant()
        {

            while (count < 200)
            {
                count++;
                Debug.Log("corutine  count =" + count);
                beanTree.transform.localScale = new Vector3(200, 0 + count * 10, 200);
                yield return new WaitForSeconds(0.1f);
            }
            StopCoroutine(routine);
            MoveScene.SetActive(true);
        }
    }
}