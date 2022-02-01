using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class StartManager : MonoBehaviour
{
    private GameManager _gameManager;
    private PlayerSprite _playerSprite;

    void Awake()
    {
        _gameManager = GameManager.GetInstance();
    }

    void Start()
    {
        _playerSprite = GameObject.FindGameObjectWithTag("GameManager").GetComponent<PlayerSprite>();
    }

    public void OnClickStart()
    {
        Debug.Log("Click Start");
        _gameManager.LoadScene(SceneManagerScript.GameScenes.Stage1);
    }

    public void OnClickQuit()
    {
        Debug.Log("Click Quit");
        _gameManager.QuitGame();
    }

    public void OnClickRightArrow()
    {
        _playerSprite.Shift(1);
    }

    public void OnClickLeftArrow()
    {
        _playerSprite.Shift(-1);
    }
}