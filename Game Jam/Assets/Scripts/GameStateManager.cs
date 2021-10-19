using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public enum GameState
{
    MainMenu = 0,
    Play = 1,
    PauseMenu = 2,
    GameOver = 3,
    FadingIn = 4,
    FadingOut = 5
}

public enum Season
{
    Summer = 0,
    Fall = 1,
    Winter = 2,
    Spring = 3
}

public class GameStateManager : MonoBehaviour
{
    private static float MAX_TIME_PER_SEASON = 20.0f;

    private static GameStateManager instance;

    private GameState gameState;
    private Season season;
    private float timeRemainingInSeason;
    private int numCompletedSeasons;

    private void Awake()
    {
        instance = this;
        gameState = GameState.FadingIn;
        season = Season.Summer;
        timeRemainingInSeason = MAX_TIME_PER_SEASON;
    }

    private void Update()
    {
        if (GameStateManager.GetGameState() == GameState.Play)
            HandleSeasonChanges();
        HandleGameStateChanges();
    }

    private void HandleSeasonChanges()
    {
        bool shouldChangeSeasons = timeRemainingInSeason <= 0.0;
        if (shouldChangeSeasons)
        {
            season = GetNextSeason(season);
            BackgroundManager.TransistionIntoSeason(season);
            FallingDeprisManager.TransistionIntoSeason(season);
            timeRemainingInSeason = MAX_TIME_PER_SEASON;
            numCompletedSeasons++;
            bool completedYear = numCompletedSeasons % 4 == 0;
            if (completedYear)
            {
                PlayGUITextManager.FlashYear();
            }
        }
        timeRemainingInSeason -= Time.deltaTime;
    }

    private static Season GetNextSeason(Season season)
    {
        switch (season)
        {
            case Season.Summer:
                return Season.Fall;
            case Season.Fall:
                return Season.Winter;
            case Season.Winter:
                return Season.Spring;
            default:
                return Season.Summer;
        }
    }

    private void HandleGameStateChanges()
    {
        bool shouldShowMainMenu =
            FadeableScreenManager.IsDoneFadingIn() &&
            gameState == GameState.FadingIn;
        if (shouldShowMainMenu)
        {
            SetGameState(GameState.MainMenu);
        }
        bool shouldStartGame = Input.GetKeyDown(KeyCode.Return) && gameState == GameState.MainMenu;
        if (shouldStartGame)
        {
            InitializePlayValues();
            SetGameState(GameState.Play);
            return;
        }
        bool shouldUnpauseGame = Input.GetKeyDown(KeyCode.P) &&
            gameState == GameState.PauseMenu;
        if (shouldUnpauseGame)
        {
            SetGameState(GameState.Play);
            return;
        }
        bool shouldPauseGame = Input.GetKeyDown(KeyCode.P) &&
            gameState == GameState.Play;
        if (shouldPauseGame)
        {
            SetGameState(GameState.PauseMenu);
            return;
        }
        bool shouldRestartGame = Input.GetKeyDown(KeyCode.Return) &&
            gameState == GameState.GameOver;
        if (shouldRestartGame)
        {
            SetGameState(GameState.FadingOut);
            FadeableScreenManager.StartFadeOut();
            StartCoroutine(RestartGameWhenFadeOutFinished());
        }
    }

    private void InitializePlayValues()
    {
        timeRemainingInSeason = MAX_TIME_PER_SEASON;
        numCompletedSeasons = 0;
    }

    public static void SetGameState(GameState gameState)
    {
        instance.gameState = gameState;
        switch (instance.gameState)
        {
            case GameState.MainMenu:
                Time.timeScale = 1.0f;
                break;
            case GameState.Play:
                Time.timeScale = 1.0f;
                break;
            case GameState.PauseMenu:
                Time.timeScale = 0.0f;
                break;
            default:
                Time.timeScale = 1.0f;
                break;
        }
        GUIManager.SetActiveGUI(instance.gameState);
    }

    private IEnumerator RestartGameWhenFadeOutFinished()
    {
        while (!FadeableScreenManager.IsDoneFadingOut())
        {
            yield return new WaitForSeconds(Time.deltaTime);
        }
        RestartGame();
    }

    private void RestartGame()
    {
        PowerupManager.ResetInstance();
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }

    public static GameState GetGameState()
    {
        return instance.gameState;
    }

    public static Season GetSeason()
    {
        return instance.season;
    }

    public static int GetNumCompletedSeasons()
    {
        return instance.numCompletedSeasons;
    }
}
