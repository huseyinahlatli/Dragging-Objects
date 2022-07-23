using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonEvents : MonoBehaviour
{
    public void RestartGame()
    {
        SceneManager.LoadScene(sceneBuildIndex: 0);
    }
}
