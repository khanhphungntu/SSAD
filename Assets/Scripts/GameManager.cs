﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject loginPage;
    public GameObject profilePage;
    public GameObject startPage;
    public GameObject playPage;
    public GameObject gameOverPage;
    public GameObject chestPopUpPage;
    public GameObject worldPage;
    public GameObject sectionPage;
    public GameObject questionPopUpPage;
    public Text scoreText;
    GameObject initialPlayState;

    public enum PageState {
        None,    //None of others
        Login,
        Profile,
        Start,
        Play,
        GameOver,
        ChestPopUp,
        WorldUI,
        SectionUI,
        QuestionPopUp
    } 

    bool gameOver = true; //Initially not start game

    public bool GameOver {
        get { return gameOver; }
    }

    void Awake() {
        Instance = this;
        SetPageState(PageState.WorldUI);
        initialPlayState = Instantiate(playPage, playPage.transform.parent);
       
    }

    void OnEnable() {
        // += to subscribe event in other C# script
        PlayerController.OnPlayerDied += OnPlayerDied;
    }

    void OnDisable() {
        // -= to unsubscribe event in other C# script
        PlayerController.OnPlayerDied -= OnPlayerDied;
    }

    void OnPlayerDied(int value) {
        gameOver = true;
        scoreText.text = "Score: " + value.ToString();
        CreateOpenedChest.OpenedChestInstance.CloseOpenedChest();
        SetPageState(PageState.GameOver);
    }

    public void SetPageState(PageState state) {
        switch (state) {
            case PageState.None:
                profilePage.SetActive(false);
                loginPage.SetActive(false);
                startPage.SetActive(false);
                gameOverPage.SetActive(false);
                playPage.SetActive(false);
                chestPopUpPage.SetActive(false);
                worldPage.SetActive(false);
                sectionPage.SetActive(false);
                questionPopUpPage.SetActive(false);
                break;
            case PageState.Login:
                FindObjectOfType<SoundManager>().Play("GameLaunch");
                profilePage.SetActive(false);
                loginPage.SetActive(true);
                startPage.SetActive(false);
                gameOverPage.SetActive(false);
                playPage.SetActive(false);
                chestPopUpPage.SetActive(false);
                worldPage.SetActive(false);
                sectionPage.SetActive(false);
                questionPopUpPage.SetActive(false);
                break;
            case PageState.Start:
                profilePage.SetActive(true);
                loginPage.SetActive(false);
                startPage.SetActive(true);
                gameOverPage.SetActive(false);
                playPage.SetActive(false);
                chestPopUpPage.SetActive(false);
                worldPage.SetActive(false);
                sectionPage.SetActive(false);
                questionPopUpPage.SetActive(false);
                break;
            case PageState.GameOver:
                FindObjectOfType<SoundManager>().Pause("EasyStage");
                profilePage.SetActive(false);
                loginPage.SetActive(false);
                startPage.SetActive(false);
                gameOverPage.SetActive(true);
                playPage.SetActive(false);
                chestPopUpPage.SetActive(false);
                worldPage.SetActive(false);
                sectionPage.SetActive(false);
                questionPopUpPage.SetActive(false);
                break;
            case PageState.Play:
                FindObjectOfType<SoundManager>().Play("EasyStage");
                FindObjectOfType<SoundManager>().Pause("Lobby");
                profilePage.SetActive(false);
                loginPage.SetActive(false);
                startPage.SetActive(false);
                gameOverPage.SetActive(false);
                playPage.SetActive(true);
                chestPopUpPage.SetActive(false);
                worldPage.SetActive(false);
                sectionPage.SetActive(false);
                questionPopUpPage.SetActive(false);
                break;
            case PageState.ChestPopUp:
                profilePage.SetActive(false);
                loginPage.SetActive(false);
                startPage.SetActive(false);
                gameOverPage.SetActive(false);
                playPage.SetActive(true);
                chestPopUpPage.SetActive(true);
                questionPopUpPage.SetActive(false);
                worldPage.SetActive(false);
                sectionPage.SetActive(false);
                break;
            case PageState.WorldUI:
                FindObjectOfType<SoundManager>().Play("Lobby");
                profilePage.SetActive(true);
                loginPage.SetActive(false);
                startPage.SetActive(false);
                gameOverPage.SetActive(false);
                playPage.SetActive(false);
                chestPopUpPage.SetActive(false);
                worldPage.SetActive(true);
                sectionPage.SetActive(false);
                questionPopUpPage.SetActive(false);
                break;
            case PageState.SectionUI:
                FindObjectOfType<SoundManager>().Play("Lobby");
                profilePage.SetActive(true);
                loginPage.SetActive(false);
                startPage.SetActive(false);
                gameOverPage.SetActive(false);
                playPage.SetActive(false);
                chestPopUpPage.SetActive(false);
                worldPage.SetActive(false);
                sectionPage.SetActive(true);
                questionPopUpPage.SetActive(false);
                break;
            case PageState.QuestionPopUp:
                profilePage.SetActive(false);
                loginPage.SetActive(false);
                startPage.SetActive(false);
                gameOverPage.SetActive(false);
                playPage.SetActive(true);
                worldPage.SetActive(false);
                sectionPage.SetActive(false);
                chestPopUpPage.SetActive(true);
                questionPopUpPage.SetActive(true);
                break;
        }
    }

    //activated when replay button is hit
    public void MainMenu() {
        SetPageState(PageState.Start);
        playPage = initialPlayState;
        
        //Save initial state again (clone another object)
        initialPlayState = Instantiate(playPage, playPage.transform.parent);
    }

    //activated when play button is hit
    public void StartGame() {
        FindObjectOfType<SoundManager>().Play("MajorButton");
        SetPageState(PageState.Play);
    }

    public void ChestPopUp()
    {
        SetPageState(PageState.ChestPopUp);
    }

    public void sectionUI()
    {
        FindObjectOfType<SoundManager>().Play("MajorButton");
        SetPageState(PageState.SectionUI);
    }

    public void worldUI()
    {
        FindObjectOfType<SoundManager>().Play("MajorButton");
        SetPageState(PageState.WorldUI);
    }

    public void QuestionPopUp(){
        SetPageState(PageState.QuestionPopUp);
    }
}
