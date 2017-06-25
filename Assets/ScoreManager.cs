using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Networking;

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
                this._scoreService.SubmitScore(this._score, OnScoreSubmitted());
                this._scoreCanvasObject = GameObject.FindGameObjectWithTag("ScoreCanvas");
                this._dynamicScoreText = this._scoreCanvasObject.GetComponent<Text>();
                this._dynamicScoreText.text = String.Format(this._endGameText, this._score.ToString());
                this._scoreService.FetchScores( OnScoresFetched() );
            }
        }
    }

    Action<bool, UnityWebRequest> OnScoresFetched()
    {
        return delegate (bool requestSuccessful, UnityWebRequest request)
        {
            if (requestSuccessful)
            {
                // TODO: move into ScoreService, this callback should expect the object, not a unitwebrequest.
                ScoreService.FetchResponse responseObj = JsonUtility.FromJson<ScoreService.FetchResponse>(request.downloadHandler.text);
                Debug.Log("Request successful!");
                this._dynamicScoreText.text = this.GetScoreSummaryString(responseObj);
            }
            else
            {
                Debug.Log("NOPE.");
            }
        };
    }

    Action<bool, UnityWebRequest> OnScoreSubmitted()
    {
        return delegate (bool requestSuccessful, UnityWebRequest request)
        {
            if (requestSuccessful)
            {
                Debug.Log(request.downloadHandler.text);
            }
        };
    }

    private string GetScoreSummaryString(ScoreService.FetchResponse fetchResponse)
    {
        string summaryString = "~~~TOP SCORES~~~\n";
        summaryString += "NAME\t\t\t\t\tSCORE\n";

        int numScores = Math.Min(fetchResponse.scores.Count, 6);
        for(int i = 0; i < numScores; i++)
        {
            Debug.Log("adding score line");
            summaryString += String.Format("{0}\t\t\t\t\t{1}\n",fetchResponse.scores[i].user, fetchResponse.scores[i].score);
        }

        return summaryString;
    }

    void End()
    {
        this.state = GameState.Over;   
        SceneManager.LoadScene("End");
    }
}
