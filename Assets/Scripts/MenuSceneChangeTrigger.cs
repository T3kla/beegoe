using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Playables;

public class MenuSceneChangeTrigger : MonoBehaviour
{


    private void OnBuzz(InputValue value)
    {
        startTimeline();
        print("z");
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
