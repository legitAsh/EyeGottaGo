using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndLevelManager : MonoBehaviour
{
    public static EndLevelManager instance;

    public GUIManager gUIManager;

    public GameObject WinScreen;

    public Image[] star;
    public Sprite starImage;
    public Sprite emptyStar;


    void Start()
    {

    }

    void Update()
    {
        DisplayScore();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.tag == "Player") {
            gUIManager.PauseButton();
            WinScreen.gameObject.SetActive(true);
        }
    }

    void DisplayScore() {
        for (int i = 0; i < star.Length; i++) {

            if (i < gUIManager.starsCollected) {
                star[i].sprite = starImage;
            }
            else {
                star[i].sprite = emptyStar;
            }

        }
    }

   
    public void Level1To2() {
        SceneManager.LoadScene("Level2");
        gUIManager.lvlInt += 1;
    }

    public void Level2To3() {
        SceneManager.LoadScene("Level3");
        gUIManager.lvlInt += 1;

    }

    public void Level3To4() {
        SceneManager.LoadScene("Level4");
        gUIManager.lvlInt += 1;

    }

    public void Level4To5() {
        SceneManager.LoadScene("Level5");
        gUIManager.lvlInt += 1;

    }

    public void Level5To6() {
        SceneManager.LoadScene("Level6");
        gUIManager.lvlInt += 1;

    }
}
