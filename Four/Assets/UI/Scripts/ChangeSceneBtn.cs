using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ChangeSceneBtn : MonoBehaviour
{
   public void NextScene()
    {
        SceneManager.LoadScene(1);
    }
}
