using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MenuSceneChangeTrigger : MonoBehaviour
{
    private void OnBuzz(InputValue value)
    {
        SceneChange.LoadNextScene();
    }
}
