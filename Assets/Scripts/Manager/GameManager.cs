using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public enum GameState {
        Opening,
        playing,
        Finish
    };

    public enum StageState {
        map1,
        map2,
        map3,
        map4,
        map5,
        map6,
        map7
    };

    private GameState currentState = GameState.Opening;
    public StageState stageState;
    public static GameState state = GameState.Opening;
    private int stage = 1;

    void Start () {
        stageState = StageState.map1;
        AudioManager.Instance.PlayBGM("Main", AudioManager.BGM_FADE_SPEED_HIGH);
    }

    public void GameDispatch(GameState state) {
        GameState oldState = currentState;

        currentState = state;
        switch (state) {
            case GameState.Opening:
                GameCurrent();
                break;
            case GameState.playing:
                ChangeScene();
                break;
            case GameState.Finish:
                GameFinish();
                break;
        }
    }

    void GameCurrent() {
    }

    public void GameOpening() {
		AudioManager.Instance.PlaySE ("EyeTrigger");
        state = GameState.playing;
    }

    public void ChangeScene() {

        stage++;
    }

    void GameFinish() {
        stage = 1;
    }

}