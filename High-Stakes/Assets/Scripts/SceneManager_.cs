using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class SceneManager_ : MonoBehaviour
{

    public static SceneManager_ Instance;
	public static int lastSceneIndex = -1;
    public Image overlay;

    public void Awake()
    {
        Instance = this;
    }

    public void Start()
    {
        overlay.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, 1.0f);
        StartCoroutine(FadeIn());
    }
	void OnDestroy() {
		lastSceneIndex = GetActiveScene();
	}
    public void LoadScene(int index)
    {
        StartCoroutine(FadeToScene(index));
    }
    public void ReloadScene()
    {
        LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public int GetActiveScene()
    {
        return SceneManager.GetActiveScene().buildIndex;
    }
    private IEnumerator FadeToScene(int index)
    {
        for (float i = 0f; i <= 1f; i += 0.01f)
        {
            overlay.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, Mathf.SmoothStep(0.0f, 1.0f, i));
            yield return new WaitForSeconds(.01f);
        }
        SceneManager.LoadScene(index);
    }

    private IEnumerator FadeIn()
    {
        for (float i = 0f; i <= 1f; i += 0.01f)
        {
            overlay.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, Mathf.SmoothStep(1.0f, 0.0f, i));
            yield return new WaitForSeconds(.01f);
        }
    }

	public void LoadLastScene() {
		LoadScene(lastSceneIndex);
	}
}
