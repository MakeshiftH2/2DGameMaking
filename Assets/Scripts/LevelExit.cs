using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 2f;
    [SerializeField] float levelExitSlowMoFactor = 2f;

    void OnTriggerEnter2D(Collider2D collision)
    {
        StartCoroutine(LevelExited());
    }

    IEnumerator LevelExited()
    {
        Time.timeScale = levelExitSlowMoFactor;
        yield return new WaitForSeconds(levelLoadDelay);
        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene("Level 2");

    }
}
