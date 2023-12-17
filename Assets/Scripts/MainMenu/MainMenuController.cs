using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public string mainScene;
    public CharacterClassSO class1;
    public CharacterClassSO class2;
    public CharacterClassSO class3;

    public void LoadGameScene()
    {
        SceneManager.LoadScene(mainScene);
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    public void SelectFirstCharacterClass()
    {
        // Save the selected character class to PlayerPrefs
        PlayerPrefs.SetString("SelectedClass", class1.className);
        LoadGameScene();
    }

    public void SelectSecondCharacterClass()
    {
        // Save the selected character class to PlayerPrefs
        PlayerPrefs.SetString("SelectedClass", class2.className);
        LoadGameScene();
    }

    public void SelectThirdCharacterClass()
    {
        PlayerPrefs.SetString("SelectedClass", class3.className);
        LoadGameScene();
    }
}
