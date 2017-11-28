using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Match : MonoBehaviour
{
    public Player Player1;
    public Player Player2;
    public Follow Cam;
    public Text Win;

    private Player _currentPlayer;
    private bool _gameRunning;
    private bool _nextPlayer;

    // Use this for initialization
    void Start ()
    {
        _currentPlayer = Player1;
        _gameRunning = true;

        StartCoroutine(Play());
	}
	
	IEnumerator Play()
    {
        while(_gameRunning)
        {
            Cam.SetTarget(_currentPlayer.gameObject.transform);
            _nextPlayer = false;
            StartCoroutine(_currentPlayer.SetTurn());
            yield return new WaitUntil(() => _nextPlayer == true);
        }
    }

    public void EndTurn()
    {
        if(_gameRunning)
        {
            if (_currentPlayer == Player2)
            {
                _currentPlayer = Player1;
            }
            else
            {
                _currentPlayer = Player2;
            }

            _nextPlayer = true;
        }
    }

    public IEnumerator EndGame()
    {
        _gameRunning = false;
        Cam.SetTarget(_currentPlayer.gameObject.transform);
        Win.gameObject.SetActive(true);
        Win.gameObject.transform.position = _currentPlayer.gameObject.transform.position + Vector3.up * 8;
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
