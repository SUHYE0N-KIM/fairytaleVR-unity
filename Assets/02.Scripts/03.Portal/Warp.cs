using UnityEngine;
using System.Collections;
using Valve.VR;

namespace Valve.VR.InteractionSystem
{
    public class Warp : MonoBehaviour
    {
        public GameObject Target;

        public void PortalOn()
        {
            Target.SetActive(true);
        }
        public void PortalOff()
        {
            Target.SetActive(false);
        }
    }
}