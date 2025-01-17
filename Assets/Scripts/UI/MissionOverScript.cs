using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MissionOverScript : MonoBehaviour
{
    public static MissionOverScript Instance { get; private set; }

    [SerializeField] TextMeshProUGUI stats;
    [SerializeField] TextMeshProUGUI message;

    [SerializeField] Image Background;

    private void Awake()
    {
        gameObject.SetActive(false);
        Instance = this;
    }

    private void Start()
    {
        Show(false);
        Refresh();
    }

    private void Update()
    {
        Refresh();
    }
    private void Refresh()
    {
        stats.text = $"{LevelManager.Instance.Payout} G\n{(int)(LevelManager.Instance.Timer/60):D2}:{(int)(LevelManager.Instance.Timer % 60):D2}";
    }

    public void Show(bool success,string text = null)
    {
        if (success)
        {
            Background.color = new(0f,0.5f,0f,Background.color.a);
            message.color = Color.green;
        }
        message.text = text ?? message.text;
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        gameObject.SetActive(false);
    }

    public void Retry()
    {
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void MainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }
}
