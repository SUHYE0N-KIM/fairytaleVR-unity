using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBook : MonoBehaviour
{
    public GameObject Target;
    Vector3 TargetPos;
    float num = 0.0f;

    void Update()
    {
        if (num < 3.0f)
        {
            TargetPos = new Vector3(Target.transform.position.x, Target.transform.position.y + 0.5f, Target.transform.position.z + 0.4f);
            transform.position = Vector3.Lerp(transform.position, TargetPos, Time.deltaTime);
            num += Time.deltaTime * 3;
        }
        else if (num > 6.0f)
        {
            num = 0.0f;
        }
        else if(num > 3.0f)
        {
            TargetPos = new Vector3(Target.transform.position.x, Target.transform.position.y - 0.5f, Target.transform.position.z - 0.4f);
            transform.position = Vector3.Lerp(transform.position, TargetPos, Time.deltaTime);
            num += Time.deltaTime * 3;
        }
    }
}