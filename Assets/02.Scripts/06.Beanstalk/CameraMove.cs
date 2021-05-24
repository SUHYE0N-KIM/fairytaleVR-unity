using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraMove : MonoBehaviour
{
    public GameObject A;
    Vector3 pos = new Vector3(80f, -12f, 9f);
    // 도착 위치

    void Start()
    {
        StartCoroutine(NextScene());
    }
    IEnumerator NextScene()
    {
        yield return new WaitForSeconds(15);
        SceneManager.LoadScene("Load.Giant");
        // 콩나무 씬이 시작되고 15초 후 거인성씬이 열림
    }

    void Update()
    {
        A.transform.position = Vector3.Lerp(A.transform.position, pos, Time.deltaTime/10);
        // 위치 = 선형보간(현재 카메라 위치, 이동할 카메라 위치, 시간/10)
        // VR멀미 약화를 위한 카메라 선형보간법
    }
}