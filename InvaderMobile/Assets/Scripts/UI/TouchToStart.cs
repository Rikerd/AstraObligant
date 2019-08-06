using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TouchToStart : MonoBehaviour
{
    private bool active;
    private TransitionController transition;

    // Start is called before the first frame update
    void Start()
    {
        active = false;
        transition = GetComponent<TransitionController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0 && !active)
        {
            StartCoroutine(LoadNextScene());
            active = true;
        }

        if (Input.GetMouseButtonDown(0) && !active)
        {
            StartCoroutine(LoadNextScene());
            active = true;
        }
    }

    IEnumerator LoadNextScene()
    {
        transition.fadeToBlack();

        yield return new WaitForSecondsRealtime(transition.getFadeTime());

        Time.timeScale = 1;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
