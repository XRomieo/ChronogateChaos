using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneRedirector : MonoBehaviour
{
    private void Update() {
        if (Input.GetButtonDown("Slide") && SceneManager.GetActiveScene().buildIndex == 1) {
            OpenGame();
        }
        if (Input.GetButtonDown("Slide") && SceneManager.GetActiveScene().buildIndex == 3) {
            OpenMainMenu();
        }
        if (SceneManager.GetActiveScene().buildIndex == 3) {
            StartCoroutine(WaitForEnd());
        }
    }

    public void OpenGame() {
        SceneManager.LoadScene(2);
    }

    public void OpenStoryBoard() {
        SceneManager.LoadScene(1);
    }
    public void OpenStoryBoardEnd() {
        SceneManager.LoadScene(3);
    }

    public void CloseGame() {
        Application.Quit();
    }

    public void OpenMainMenu() {
        SceneManager.LoadScene(0);
    }

    private IEnumerator WaitForEnd() {
        yield return new WaitForSeconds(20f);
        StopAllCoroutines();
        OpenMainMenu();
    }
}
