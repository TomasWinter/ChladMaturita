using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuScript : MonoBehaviour
{
    [SerializeField] GameObject UI;
    bool isVisible = false;
    private void Start()
    {
        UI.SetActive(false);
    }
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape) || Input.GetKeyUp(KeyCode.P))
        {
            if (isVisible)
                TurnOff();
            else if (Time.timeScale == 1)
                TurnOn();
        }
    }
    public void TurnOn()
    {
        isVisible = true;
        Time.timeScale = 0;
        UI.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
    }
    public void TurnOff()
    {
        isVisible = false;
        Time.timeScale = 1;
        UI.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void GoToMainMenu()
    {
        isVisible = false;
        Time.timeScale = 1;
        UI.SetActive(false);
        SceneManager.LoadScene("MainMenu");
    }
    public void Quit()
    {
        Application.Quit();
    }
}
