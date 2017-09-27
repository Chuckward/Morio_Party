// LoadingScreenManager
// --------------------------------
// built by Martin Nerurkar (http://www.martin.nerurkar.de)
// for Nowhere Prophet (http://www.noprophet.com)
//
// Licensed under GNU General Public License v3.0
// http://www.gnu.org/licenses/gpl-3.0.txt

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;
using UnityEngine.SceneManagement;
using XInputDotNetPure;

public class LoadingSceneManager : MonoBehaviour
{
    [Header("Loading Visuals")]
    public Text loadingText;
    public Image fadeOverlay;

    [Header("Timing Settings")]
    public float fadeDuration = 1.00f;

    [Header("Loading Settings")]
    public LoadSceneMode loadSceneMode = LoadSceneMode.Single;
    public ThreadPriority loadThreadPriority = ThreadPriority.Normal;

    [Header("Audio")]
    public AudioSource audioSource;
    public float audioFadeDuration = 2.00f;

    AsyncOperation operation;
    Scene currentScene;
    UnityEvent m_StartEvent;
    UnityEvent m_DoneLoadingEvent;

    private bool start_activated = false; 

    public static int sceneToLoad = 1;
    // IMPORTANT! This is the build index of your loading scene. You need to change this to match your actual scene index
    static int loadingSceneIndex = 0;

    public static void LoadScene(int levelNum)
    {
        Application.backgroundLoadingPriority = ThreadPriority.High;
        sceneToLoad = levelNum;
        SceneManager.LoadScene(loadingSceneIndex);
    }

    void Start()
    {
        if (sceneToLoad < 0)
            return;
        m_StartEvent = new UnityEvent();
        m_DoneLoadingEvent = new UnityEvent();
        m_StartEvent.AddListener(UnloadCurrentScene);
        fadeOverlay.gameObject.SetActive(true);             //Making sure it's on so that we can crossfade Alpha
        currentScene = SceneManager.GetActiveScene();
        StartCoroutine(LoadAsync(sceneToLoad));
    }

    void UnloadCurrentScene()
    {
        start_activated = true;
    }

    void Update()
    {
        if(GamePad.GetState(PlayerIndex.One).Buttons.Start.Equals(ButtonState.Pressed))
        {
            m_StartEvent.Invoke();
        }

        if(start_activated)
        {
            StartCoroutine(FadeAudio());
        }
    }

    private IEnumerator LoadAsync(int levelNum)
    {
        FadeIn();
        yield return new WaitForSeconds(fadeDuration);
        audioSource.Play();

        ShowLoadingVisuals();

        yield return null;

        StartOperation(levelNum);

        float lastProgress = 0f;

        // operation does not auto-activate scene, so it's stuck at 0.9
        //TODO: Also make this an event ?
        while (DoneLoading() == false)
        {
            yield return null;

            if (Mathf.Approximately(operation.progress, lastProgress) == false)
            {
                lastProgress = operation.progress;
            }
        }

        ShowCompletionVisuals();

        //yield return new WaitForSeconds(0.25f);

        //wait for Start Button; add listener for button only when ready 

        while(start_activated == false)
        {
            yield return null;            
        }

        FadeOut();

        yield return new WaitForSeconds(fadeDuration);

        if (loadSceneMode == LoadSceneMode.Additive)
            SceneManager.UnloadSceneAsync(currentScene.name);
        else
            operation.allowSceneActivation = true;

        yield return null;

    }

    private void StartOperation(int levelNum)
    {
        Application.backgroundLoadingPriority = loadThreadPriority;
        operation = SceneManager.LoadSceneAsync(levelNum, loadSceneMode);

        if (loadSceneMode == LoadSceneMode.Single)
            operation.allowSceneActivation = false;
    }

    private bool DoneLoading()
    {
        return (loadSceneMode == LoadSceneMode.Additive && operation.isDone) || (loadSceneMode == LoadSceneMode.Single && operation.progress >= 0.9f);
    }

    void FadeIn()
    {
        fadeOverlay.CrossFadeAlpha(0, fadeDuration, true);
    }

    void FadeOut()
    {
        fadeOverlay.CrossFadeAlpha(1, fadeDuration, true);
    }

    IEnumerator FadeAudio()
    {
        float startVolume = audioSource.volume;
        if (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / audioFadeDuration;
        } else
        {
            audioSource.Stop();
            audioSource.volume = 100;
        }
        yield return new WaitForSeconds(audioFadeDuration);
    }

    void ShowLoadingVisuals()
    {
        loadingText.text = "Loading...";
    }

    void ShowCompletionVisuals()
    {
        loadingText.text = "Press Start";
    }
}