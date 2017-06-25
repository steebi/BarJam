using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

    public enum GameState
    {
        InProgress = 1,
        Over = 2,
    }

    public GameObject Player;
    public float GameDuration = 10; //seconds

    private DateTime _gameStartTime;

    [SerializeField]
    private float _timeSinceStart;

    [SerializeField]
    private float _score;

    private GameState state = GameState.InProgress;
    private BartenderController _bartenderController;
    private GameObject _scoreCanvasObject;
    private Text _dynamicScoreText;

    //Services
    private ScoreService _scoreService;

    private string _endGameText = "NIGHT OVER. YOUR TIPS: €{0}";

    public float Progress
    {
        get
        {
            return (this._timeSinceStart / this.GameDuration);
        }
    }

    private void Start()
    {
        this._gameStartTime = DateTime.Now;
        this._scoreService = new ScoreService();
    }

    // Update is called once per frame
    void Update() {
        // Aidan: ugh gross sorry lol
        if (this._bartenderController == null)
        {
            this._bartenderController = Player.GetComponent<BartenderBehaviour>().BartenderController;
        }

        if (this.state == GameState.InProgress)
        {
            this._score = this._bartenderController.TotalTips;
            this._timeSinceStart = (float)DateTime.Now.Subtract(this._gameStartTime).TotalSeconds;
            if (this._timeSinceStart > this.GameDuration)
            {
                this.End();
            }
        }
        else if (this.state == GameState.Over)
        {
            if (this._scoreCanvasObject == null)
            {
                this._scoreCanvasObject = GameObject.FindGameObjectWithTag("ScoreCanvas");
                this._dynamicScoreText = this._scoreCanvasObject.GetComponent<Text>();
                this._dynamicScoreText.text = String.Format(this._endGameText, this._score.ToString());
                this._scoreService.SubmitScore(this._score, OnScoresFetched() );
            }
        }
    }

    Action<bool> OnScoresFetched()
    {
        return (bool i) => { print(i); };
    }

    void End()
    {
        this.state = GameState.Over;   
        SceneManager.LoadScene("End");
    }
}
