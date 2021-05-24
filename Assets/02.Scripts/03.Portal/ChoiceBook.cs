using UnityEngine;
using System.Collections;
using Valve.VR;

namespace Valve.VR.InteractionSystem
{
    public class ChoiceBook : MonoBehaviour
    {
        public GameObject Target1;
        public GameObject Target2;

        public void ChoiceOn()
        {
            Target1.SetActive(true);
            Target2.SetActive(false);
        }
    }
}