using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public GameObject PlayButton;
    public GameObject Playership;
    public GameObject EnemySpawner;
    public GameObject GameOver;
    public GameObject scoreUItext;
    public enum GameManagerState
    {
        Opening,
        Gameplay,
        GameOver,
    }

    GameManagerState GMState;
    // Update is called once per frame
    void Start()
    {
       GMState = GameManagerState.Opening;
    }

   void UpdateGameManagerState()
    {
        switch(GMState)
        {
            case GameManagerState.Opening:
                PlayButton.SetActive(true);
                GameOver.SetActive(false);
                break;
                case GameManagerState.Gameplay:
                scoreUItext.GetComponent<GameScore>().Score = 0;
                PlayButton.SetActive(false);
               Playership.GetComponent<PlayerController>().Init();
                EnemySpawner.GetComponent<EnemySpawner>().SchedulerEnemySpawner();
                break;
                    case GameManagerState.GameOver:
                EnemySpawner.GetComponent<EnemySpawner>().UnscheduleEnemySpawner();
                GameOver.SetActive(true) ;
                Invoke("ChangeToOpeningState", 8f);
                break;
        }
    }
    public void SetGameManagerState(GameManagerState state)
    {
        {
            GMState = state;
            UpdateGameManagerState();
        } }

    public void StartGamePlay()
    {
        GMState = GameManagerState.Gameplay;
        UpdateGameManagerState();
    }

    public void ChangeToOpeningState()
    {
        SetGameManagerState(GameManagerState.Opening);
    }
}
