using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private Slider _loadingBar;
    [SerializeField] private Text _loadingText;

    [Header("Config")]
    [SerializeField] private string _sceneToLoad = "HomeScene";
    [SerializeField] private float _minLoadingTime = 1.2f;
    [SerializeField] private float _progressSpeed = 0.6f;
    [SerializeField] private float _extraWaitTime = 0.5f;

    private float _shownProgress = 0f;


    private void Start()
    {
        StartCoroutine(LoadSceneAsync());
    }

    private IEnumerator LoadSceneAsync()
    {
        float elapsedTime = 0f;
        
        AsyncOperation op = SceneManager.LoadSceneAsync(_sceneToLoad);
        op.allowSceneActivation = false;

        while(!op.isDone)
        {
            elapsedTime += Time.deltaTime;
            
            float realProgress = Mathf.Clamp01(op.progress / 0.9f);
            _shownProgress = Mathf.MoveTowards(_shownProgress, realProgress, _progressSpeed * Time.deltaTime);

            _loadingBar.value = _shownProgress;
            int percent = Mathf.RoundToInt(_shownProgress * 100);
            _loadingText.text = $"LOADING... {percent}%";

            if (realProgress >= 1f && elapsedTime >= _minLoadingTime)
            {
                _loadingBar.value = 1f;
                _loadingText.text = $"LOADING... 100%";

                yield return new WaitForSeconds(_extraWaitTime);

                op.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}
