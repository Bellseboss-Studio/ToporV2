using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour, IUiControllerService
{
    [SerializeField] private GameObject startPanel, endGamePanel, animationPanel, uiOfGame;
    [SerializeField] private Button startButton, endButton;
    [SerializeField] private TextMeshProUGUI titleEndGame, subtitleEndGame;
    [SerializeField] private float timeToWatch;
    [SerializeField] private Button skipButton;

    private IGameLoop _gameLoop;
    public bool SelectedEndGame { get; private set; }
    public bool SelectedStartGame { get; private set; }
    public bool AnimationStartGame { get; private set; }

    private void Awake()
    {
        ServiceLocator.Instance.RegisterService<IUiControllerService>(this);
    }

    private void OnDestroy()
    {
        ServiceLocator.Instance.UnregisterService<IUiControllerService>();
    }

    public void Configure(IGameLoop gameLoop)
    {
        _gameLoop = gameLoop;
        SelectedEndGame = false;
        SelectedStartGame = false;
        
        startButton.onClick.AddListener(() => { SelectedStartGame = true; });

        endButton.onClick.AddListener(() => { SelectedEndGame = true; });
        startPanel.SetActive(false);
        endGamePanel.SetActive(false);
        uiOfGame.SetActive(false);
        animationPanel.SetActive(false);

        skipButton.onClick.AddListener(FinishVideo);
    }

    public void SetTitleEndGame(string title)
    {
        titleEndGame.text = title;
    }

    public void SetSubtitleEndGame(string subtitle)
    {
        subtitleEndGame.text = subtitle;
    }

    public void ShowAnimationStart()
    {
        animationPanel.SetActive(true);
        startPanel.SetActive(false);
        endGamePanel.SetActive(false);
        uiOfGame.SetActive(false);
        StartCoroutine(StartVideo(timeToWatch));
    }
    
    public void ShowUiOfGame()
    {
        animationPanel.SetActive(false);
        endGamePanel.SetActive(false);
        startPanel.SetActive(false);
        uiOfGame.SetActive(true);
    }

    public void ShowStartPanel()
    {
        animationPanel.SetActive(false);
        endGamePanel.SetActive(false);
        uiOfGame.SetActive(false);
        startPanel.SetActive(true);
    }

    private IEnumerator StartVideo(float timeToWait)
    {
        StartVideoPlayer();
        StartAudioForVideo();
        yield return new WaitForSeconds(timeToWait);
        FinishVideo();
    }

    private void StartAudioForVideo()
    {
    }

    private void StartVideoPlayer()
    {
        AnimationStartGame = false;
    }

    private void FinishVideo()
    {
        AnimationStartGame = true;
    }

    public void HideStartPanel()
    {
        startPanel.SetActive(false);
    }

    public void ShowEndGamePanel(bool winOrLose)
    {
        endGamePanel.SetActive(true);
        startPanel.SetActive(false);
        animationPanel.SetActive(false);
        uiOfGame.SetActive(false);
    }

    public void HideEndGamePanel()
    {
        endGamePanel.SetActive(false);
    }

    public void ShowEndGameAnimation(bool lose)
    {
        AnimationStartGame = false;
        animationPanel.SetActive(true);
        startPanel.SetActive(false);
        endGamePanel.SetActive(false);
        uiOfGame.SetActive(false);
        StartCoroutine(StartVideo(timeToWatch));
    }
}