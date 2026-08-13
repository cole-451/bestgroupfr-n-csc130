using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [Header("Panels (each needs a CanvasGroup)")]
    public CanvasGroup mainMenuPanel;
    public CanvasGroup songSelectPanel;
    public CanvasGroup settingsPanel;
    //public CanvasGroup creditsPanel;

    [Header("Audio")]
    public AudioSource sfxSource;      // one-shot SFX
    public AudioSource musicSource;    // looping menu track
    public AudioClip hoverSfx;
    public AudioClip selectSfx;

    [Header("Transition")]
    public float fadeDuration = 0.25f;


    private CanvasGroup currentPanel;



    void Start()
    {
        ShowPanelImmediate(mainMenuPanel);

        if (musicSource != null && !musicSource.isPlaying) musicSource.Play();
    }



    // ---- Hook these to Button > OnClick() in the Inspector ----

    public void OnSongSelectPressed()
    {
        PlaySelectSfx();
        SwitchPanel(songSelectPanel);
    }

    public void OnSettingsPressed()
    {
        PlaySelectSfx();
        SwitchPanel(settingsPanel);
    }

    // public void OnCreditsPressed()
    // {
    //     PlaySelectSfx();
    //     SwitchPanel(creditsPanel);
    // }

    public void OnBackToMainPressed()
    {
        PlaySelectSfx();
        SwitchPanel(mainMenuPanel);
    }

//     public void OnQuitPressed()
//     {
//         PlaySelectSfx();
// // #if UNITY_EDITOR
// //         UnityEditor.EditorApplication.isPlaying = false;
// // #else
// //         Application.Quit();
// // #endif
//     }





    // Wire this to each song entry's button, passing the gameplay scene name
    public void LoadSongScene(string sceneName)
    {
        PlaySelectSfx();
        StartCoroutine(LoadSceneRoutine(sceneName));
    }

    // Hook to Button > "Pointer Enter" via an EventTrigger, or call from a
    // small helper script on each button for hover feedback
    public void PlayHoverSfx()
    {
        if (sfxSource != null && hoverSfx != null)
            sfxSource.PlayOneShot(hoverSfx);
    }

    public void PlaySelectSfx()
    {
        if (sfxSource != null && selectSfx != null)
            sfxSource.PlayOneShot(selectSfx);
    }

    // ---- Internals ----

    private IEnumerator LoadSceneRoutine(string sceneName)
    {
        yield return StartCoroutine(Fade(currentPanel, 1f, 0f));
        SceneManager.LoadScene(sceneName);
    }

    private void SwitchPanel(CanvasGroup target)
    {
        StartCoroutine(SwitchPanelRoutine(target));
    }

    private IEnumerator SwitchPanelRoutine(CanvasGroup target)
    {
        if (currentPanel != null)
            yield return StartCoroutine(Fade(currentPanel, 1f, 0f));

        currentPanel.gameObject.SetActive(false);
        target.gameObject.SetActive(true);
        target.alpha = 0f;
        currentPanel = target;

        yield return StartCoroutine(Fade(target, 0f, 1f));
    }

    private IEnumerator Fade(CanvasGroup group, float from, float to)
    {
        if (group == null) yield break;

        float t = 0f;
        group.blocksRaycasts = to > from;

        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            group.alpha = Mathf.Lerp(from, to, t / fadeDuration);
            yield return null;
        }

        group.alpha = to;
        group.blocksRaycasts = to >= 1f;
    }

    private void ShowPanelImmediate(CanvasGroup panel)
    {
        mainMenuPanel.gameObject.SetActive(false);
        songSelectPanel.gameObject.SetActive(false);
        settingsPanel.gameObject.SetActive(false);
        //creditsPanel.gameObject.SetActive(false);

        panel.gameObject.SetActive(true);
        panel.alpha = 1f;
        panel.blocksRaycasts = true;
        currentPanel = panel;
    }

}
