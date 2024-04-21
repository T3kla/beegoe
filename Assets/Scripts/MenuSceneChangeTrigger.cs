using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Playables;

public class MenuSceneChangeTrigger : MonoBehaviour
{

    [SerializeField] AudioSource buzzSource;

    private void OnBuzz(InputValue value)
    {
        buzzSource.Play();
        startTimeline();
    }

    private void startTimeline()
    {
        GetComponent<PlayableDirector>().Play();
    }

    public void LoadScene()
    {
        SceneChange.LoadNextScene();
    }
}
