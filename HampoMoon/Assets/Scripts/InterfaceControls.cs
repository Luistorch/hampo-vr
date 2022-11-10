using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using TMPro;
using UnityEngine.SceneManagement;

public class InterfaceControls : MonoBehaviour
{

    private TextMeshProUGUI videoButtonText;
    private VideoPlayer videoPlayer;


    public GameObject menuContainer;
    public GameObject videoUiContainer;
    public GameObject mainUiContainer;
    public Button introButton;
    public Button videoButton;
    public Button exitVideoButton;
    public Button gameButton;
    public string gameScene;

    // Start is called before the first frame update
    void Start()
    {
        // Valores por defecto de los contenedores de menus
        menuContainer.SetActive(true);
        videoUiContainer.SetActive(false);
        mainUiContainer.SetActive(true);
        // Video
        videoPlayer = GameObject.FindObjectOfType<VideoPlayer>();
        // Inactivo por defecto
        videoPlayer.gameObject.SetActive(false);

        // botones
        videoButtonText = videoButton.GetComponentInChildren<TextMeshProUGUI>();
        videoButton.onClick.AddListener(PlayVideo);
        exitVideoButton.onClick.AddListener(ReturnToMenu);
        introButton.onClick.AddListener(OpenVideoMenu);
        gameButton.onClick.AddListener(OpenGame);
    }

    void OpenGame()
    {
        SceneManager.LoadScene(gameScene, LoadSceneMode.Additive);
        menuContainer.SetActive(false);
    }

    void PlayVideo()
    {
        videoPlayer.gameObject.SetActive(true);
        if (videoPlayer) videoPlayer.Play();
        videoButtonText.text = "Stop";
        videoButton.image.color = Color.red;
        videoButton.onClick.AddListener(StopVideo);
    }

    void StopVideo()
    {
        if (videoPlayer) videoPlayer.Stop();
        videoPlayer.gameObject.SetActive(false);

        videoButtonText.text = "Play";
        videoButton.image.color = Color.green;
        videoButton.onClick.AddListener(PlayVideo);
    }
    void ReturnToMenu()
    {
        videoUiContainer.SetActive(false);
        mainUiContainer.SetActive(true);
    }

    void OpenVideoMenu()
    {
        videoUiContainer.SetActive(true);
        mainUiContainer.SetActive(false);
    }
}
