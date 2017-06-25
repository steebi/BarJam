using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Score
{
    public string user;
    public string score;
    public string time;
}

public class ScoreService
{
    private static readonly string getScores = "get_scores";
    private static readonly string insertScore = "insert_score";

    private RequestHandler _requestHandler;

    public ScoreService()
    {
        this._requestHandler = new RequestHandler();
    }

    private class FetchRequest: object
    {
        public string game_name = Endpoints.GAME_NAME;
    }

    private class SubmitRequest: object
    {
        public string user;
        public float score;
        public string game_name = Endpoints.GAME_NAME;
    }

    public void FetchScores()
    {
        FetchRequest fetchRequest = new FetchRequest();
        string endpoint = Endpoints.SCORE_BOARD;
        string value = getScores;
        
        this._requestHandler.MakeRequest(fetchRequest, endpoint, value);
    }

    public void SubmitScore(float score)
    {
        SubmitRequest submitRequest = new SubmitRequest();
        submitRequest.user = System.Environment.UserName;
        submitRequest.score = score;

        this._requestHandler.MakeRequest(submitRequest, Endpoints.SCORE_BOARD, insertScore);
    }
}
