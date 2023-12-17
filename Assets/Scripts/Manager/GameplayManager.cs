using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    OnGameplay,
    OnSelectionUI
}
public class GameplayManager : MonoBehaviour
{
    public static GameplayManager Instance;

    private GameState state;
    public List<CharacterClassSO> characterClasses;
    public Transform mouseIndicator;    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(Instance);
        }
    }


    private void Start()
    {
        ClassList.Initialize(characterClasses);
        Cursor.visible = false;
    }
    private void Update()
    {
        OnGameplayHandler();
        OnSelectionUIHandler();
        ShowMouseIndicator();
    }

    private void ShowMouseIndicator() => mouseIndicator.transform.position = InputManager.Instance.GetMouseWorldPosition();
    
    public void UpdateGameState(GameState newState)
    {
        state = newState;
    }

    public void OnGameplayHandler()
    {
        if (state == GameState.OnGameplay)
        {
            //isPause = false;
            Time.timeScale = 1.0f;
        }
    }

    public void OnSelectionUIHandler()
    {
        if (state == GameState.OnSelectionUI)
        {
            //isPause = true;
            Time.timeScale = 0f;
        }
    }
    
}
