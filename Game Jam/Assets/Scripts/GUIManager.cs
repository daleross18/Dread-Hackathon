using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIManager : MonoBehaviour
{
    private static string ACTIVE_GUI_CONTAINER_KEY = "ActiveGUI";

    private static GUIManager instance;

    [SerializeField] GameObject mainMenuGUIPrefab;
    [SerializeField] GameObject playGUIPrefab;
    [SerializeField] GameObject pauseMenuGUIPrefab;
    [SerializeField] GameObject gameOverGUIPrefab;
    [SerializeField] GameObject blankGUIPrefab;

    private GameObject activeGUI;
    private GameObject activeGUIContainer;

    private void Awake()
    {
        instance = this;
        instance.activeGUIContainer = GameObject.FindWithTag(ACTIVE_GUI_CONTAINER_KEY);
        SetActiveGUI(GameState.FadingIn);
    }

    public static void SetActiveGUI(GameState gameState)
    {
        if (instance.activeGUI != null)
        {
            Destroy(instance.activeGUI.gameObject);
        }
        switch (gameState)
        {
            case GameState.MainMenu:
                instance.activeGUI =
                    Instantiate(instance.mainMenuGUIPrefab as GameObject, instance.activeGUIContainer.transform);
                break;
            case GameState.Play:
                instance.activeGUI =
                    Instantiate(instance.playGUIPrefab as GameObject, instance.activeGUIContainer.transform);
                break;
            case GameState.PauseMenu:
                instance.activeGUI =
                    Instantiate(instance.pauseMenuGUIPrefab as GameObject, instance.activeGUIContainer.transform);
                break;
            case GameState.GameOver:
                instance.activeGUI =
                    Instantiate(instance.gameOverGUIPrefab as GameObject, instance.activeGUIContainer.transform);
                break;
            default:
                instance.activeGUI =
                    Instantiate(instance.blankGUIPrefab as GameObject, instance.activeGUIContainer.transform);
                break;
        }
    }

    public static bool InstanceIsNull()
    {
        return instance == null;
    }
}
