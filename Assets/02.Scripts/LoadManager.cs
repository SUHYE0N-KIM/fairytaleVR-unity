using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadManager : MonoBehaviour
{
    public Slider progressbar;
    public Text loadtext;
    public string switchToScene;

    private void Start()
    {
        diaNext.index = 0;
        StartCoroutine(LoadScene());
    }

    IEnumerator LoadScene()
    {
        yield return null;
        AsyncOperation operation = SceneManager.LoadSceneAsync(switchToScene);
        // 비동기 로드 시작
        operation.allowSceneActivation = false;
        // 로드 전까지 씬을 실행하지 않음
        loadtext.text = (progressbar.value * 100.0f).ToString() + "%";

        while (!operation.isDone)
        {
            yield return null;

            if (progressbar.value < 0.9f)
            {
                progressbar.value = Mathf.MoveTowards(progressbar.value, 0.9f, Time.deltaTime);
            }
            else if (progressbar.value >= 0.9f)
            {
                progressbar.value = Mathf.MoveTowards(progressbar.value, 1f, Time.deltaTime);
            }
            if (progressbar.value >= 1f)
            {
                loadtext.text = "100%";
            }
            if (progressbar.value >= 1f && operation.progress >= 0.9f)
            {
                operation.allowSceneActivation = true;
                // 90%가 넘어가면 씬 로드
            }
        }
    }
}
