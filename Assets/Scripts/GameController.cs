using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;


public class GameController : MonoBehaviour
{

    [SerializeField] private List<GameObject> _hazards; // List of enabled hazards
    
    [SerializeField] private int _hazardCount;
    [SerializeField] private float _spawnWait;
    [SerializeField] private float _startWait;
    [SerializeField] private float _waveWait;
    [SerializeField] private Text _restartText;
    [SerializeField] private Text _scoreText;
    [SerializeField] private Text _gameOverText;
    [SerializeField] private Vector3 _spawnPoint; // The point of enemy spawn


    private const string RESTART_TEXT = "Press 'R' for Restart";
    private const string SCORE_TEXT = "Score: ";
    private const string GAMEOVER_TEXT = "Game Over!";

    public Vector3 SpawnPoint
    {
        get { return _spawnPoint; }
    }

    public string ScoreText
    {
        set { _scoreText.text = value; }
    }

    public string RestartText
    {
        set { _restartText.text = value; }
    }
        
    public string GameOverText
    {
        set { _gameOverText.text = value; }
    }

    

    private List<GameObject> _hazardWave;
    private List<Vector3> _spawnPosition;
    private bool gameOver;
    private bool restart;
    private int score;

    //public int HazardCount
    //{
    //    get { return hazardCount; }
    //}

    //public int HazardCount2 => hazardCount;

    private void Start()
    {
        gameOver = false;
        restart = false;
        RestartText = string.Empty;
        GameOverText = string.Empty;
        score = 0;
        UpdateScore();
        WaveConstructor(3);
        InitPositions();
        StartCoroutine(SpawnWaves());
    }

    void Update()
    {
        if (restart)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    void WaveConstructor(int unitsCount)
    {
        _hazardWave = new List<GameObject>();
        for (int i = 0; i < unitsCount; i++)
        {
            _hazardWave.Add(_hazards[Random.Range(0, _hazards.Count)]);
        }
    }

    void InitPositions()
    {
        int x = -17; //x e [-17...17]/17 unit
        int z = 18; //  z e [1..18]/9 units 
        _spawnPosition = new List<Vector3>();
        for (int row = 0; row < 9; row++)
        {
            for (int col = 0; col < 17; col++)
            {
                _spawnPosition.Add(new Vector3(x, 0, z));
                x += 1;
            }

            z -= 2;            
        }
    }


    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(_startWait);
        while (true)
        {
            for (int i = 0; i < _hazardWave.Count; i++)
            {
                GameObject hazard = _hazardWave[i];
                //Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(hazard, _spawnPosition[i], spawnRotation);
                yield return new WaitForSeconds(_spawnWait);
            }
            yield return new WaitForSeconds(_waveWait);

            if (gameOver)
            {
                RestartText = RESTART_TEXT;
                restart = true;
                break;
            }
        }

        //IEnumerator SpawnWaves()
        //{
        //    yield return new WaitForSeconds(startWait);
        //    while (true)
        //    {
        //        for (int i = 0; i < hazardCount; i++)
        //        {
        //            GameObject hazard = hazards[Random.Range(0, hazards.Length)];
        //            Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
        //            Quaternion spawnRotation = Quaternion.identity;
        //            Instantiate(hazard, spawnPosition, spawnRotation);
        //            yield return new WaitForSeconds(spawnWait);
        //        }
        //        yield return new WaitForSeconds(waveWait);

        //        if (gameOver)
        //        {
        //            restartText.text = "Press 'R' for Restart";
        //            restart = true;
        //            break;
        //        }
        //    }
    }

    public void AddScore(int newScoreValue)
    {
        score += newScoreValue;
        UpdateScore();
    }

    private void UpdateScore()
    {
        ScoreText = SCORE_TEXT + score;
    }

    public void GameOver()
    {
        GameOverText = GAMEOVER_TEXT;
        gameOver = true;
    }
}
