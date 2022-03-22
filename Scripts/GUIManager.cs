using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GUIManager : MonoBehaviour
{
    public static GUIManager instance;

    public TextMeshProUGUI moveDisplay;

    public int numOfMoves;

    public Image[] star;

    public Sprite emptyStar;
    public Sprite starImage;

    public int starsCollected = 0;
    public bool isPaused;
    public int starScore;

    public int lvlUnlocked = 0;
    public int lvlInt;

    public GameObject[] tape;

    public Sprite tapeCovering;
    public int tapeInt;

    void Start()
    {
        LoadValues();
    }

    void Update()
    {
        StartCoroutine(UpdateStars());
        StartCoroutine(UpdateTape());
        tapeInt = lvlInt + 1;
    }


    public IEnumerator UpdateStars() {
       
        for (int i = 0; i < star.Length; i++) {

            if(i < starsCollected) {
                star[i].sprite = starImage;
            }
            else {
                star[i].sprite = emptyStar;
            }

        }

        yield return null;

    }

    public IEnumerator UpdateTape() {

        for (int i = 0; i < tape.Length; i++) {

            if (i >= lvlInt) {
                tape[i].gameObject.SetActive(true);
            }
            else {
                tape[i].gameObject.SetActive(false);
            }

        }

        yield return null;

    }

    public void PauseButton() {
        isPaused = true;
        Time.timeScale = 0;
    }

    public void Resume() {
        isPaused = false;
        Time.timeScale = 1;
    }

    public void ResetLevel1() {
        starsCollected = 0;
        lvlInt = 0;
        SaveScore();
        SceneManager.LoadScene("Level1");
    }
    public void ResetLevel2() {
        if (lvlInt > 0) {
            starsCollected = 3;
            lvlInt = 1;
            SaveScore();
            SceneManager.LoadScene("Level2");
        }
        else {
            Debug.Log("NOT UNLOCKED");
        }
    }
    public void ResetLevel3() {
        if(lvlInt > 1) {
            starsCollected = 6;
            lvlInt = 2;
            SaveScore();
            SceneManager.LoadScene("Level3");
        }
        else {
            Debug.Log("NOT UNLOCKED");
        }
    }
    public void ResetLevel4() {
        if(lvlInt > 2) {
            starsCollected = 9;
            lvlInt = 3;
            SaveScore();
            SceneManager.LoadScene("Level4");
        }
        else {
            Debug.Log("NOT UNLOCKED");
        }
    }
    public void ResetLevel5() {
        if(lvlInt > 3) {
            starsCollected = 12;
            lvlInt = 4;
            SaveScore();
            SceneManager.LoadScene("Level5");
        }
        else {
            Debug.Log("NOT UNLOCKED");
        }
    }
    public void ResetLevel6() {
        if(lvlInt > 4) {
            starsCollected = 15;
            lvlInt = 5;
            SaveScore();
            SceneManager.LoadScene("Level6");
        }
        else {
            Debug.Log("NOT UNLOCKED");
        }
    }

    public void NewGame() {
        starsCollected = 0;
        lvlInt = 0;
        SaveScore();

        SceneManager.LoadScene("Level1");
    }

    public void LevelSelect() {
        SceneManager.LoadScene("LevelSelect");
    }

    public void Instructions() {
        SceneManager.LoadScene("Instructions");
    }

    public void Credits() {
        SceneManager.LoadScene("Credits");
    }

    public void MainMenu() {
        SceneManager.LoadScene("Menu");
    }

    public void SaveScore() {
        starScore = starsCollected;
        lvlUnlocked = lvlInt;
        PlayerPrefs.SetInt("Starz", starScore);
        PlayerPrefs.SetInt("Levil", lvlUnlocked);
        LoadValues();
    }

    void LoadValues() {
        starScore = PlayerPrefs.GetInt("Starz");
        lvlUnlocked = PlayerPrefs.GetInt("Levil");
        lvlInt = lvlUnlocked;
        starsCollected = starScore;
    }

   
}
