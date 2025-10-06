using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingManager : MonoBehaviour
{
    public Slider progressBar;
    public float fakeLoadingSpeed = 0.5f;

    void Start()
    {
        StartCoroutine(LoadSceneAsync());
    }

    IEnumerator LoadSceneAsync()
    {
        string sceneToLoad = PlayerPrefs.GetString("NextScene", "");
        if (string.IsNullOrEmpty(sceneToLoad)) yield break;

        AsyncOperation op = SceneManager.LoadSceneAsync(sceneToLoad);
        op.allowSceneActivation = false;

        float fakeProgress = 0f;

        while (!op.isDone)
        {
            float realProgress = Mathf.Clamp01(op.progress / 0.9f);

            if (fakeProgress < realProgress)
                fakeProgress += Time.deltaTime * fakeLoadingSpeed;

            if (progressBar != null)
                progressBar.value = fakeProgress;

            if (fakeProgress >= 1f && op.progress >= 0.9f)
            {
                yield return new WaitForSeconds(0.3f);
                op.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}
