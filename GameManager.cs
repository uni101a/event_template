using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class GameManager : SingletonMonoBehaviour<GameManager>
{
    private static PlayerSprite _playerSprite;
    private static SceneManagerScript _sceneManager;

    void Start()
    {
        DontDestroyOnLoad(gameObject);

        _playerSprite = GetComponent<PlayerSprite>();
        _sceneManager = GetComponent<SceneManagerScript>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ClickEscape();
        }
    }

    public static GameManager GetInstance()
    {
        if (!GameObject.FindGameObjectWithTag("GameManager"))
        {
            Debug.Log("GameManagerを生成しました");
            Instantiate((GameObject)Resources.Load("GameManager"));
        }

        return Instance;
    }

    private void ClickEscape()
    {
        if (_sceneManager.GetGameScene() == SceneManagerScript.GameScenes.Title)
        {
            QuitGame();
        }
        else
        {
            LoadScene(SceneManagerScript.GameScenes.Title);
            _playerSprite.Reset();
        }
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #elif UNITY_STANDALONE
            UnityEngine.Application.Quit();
        #endif
    }

    public void LoadScene(SceneManagerScript.GameScenes gameScene)
    {
        _sceneManager.ChangeScene(gameScene);

        if ((int)_sceneManager.GetGameScene() >= (int)SceneManagerScript.GameScenes.NoneScene + 1)
        {
            _playerSprite.AsyncUpdate(_playerSprite.GetPlayerSprite());
        }
    }

    public void LoadNextStage()
    {
        SceneManagerScript.GameScenes nextScene = (SceneManagerScript.GameScenes)((int)_sceneManager.GetGameScene() + 1);
        LoadScene(nextScene);
    }

    public void ReloadScene()
    {
        LoadScene(_sceneManager.GetGameScene());
    }

    public void Goal()
    {
        //最後のステージでゴールした場合Clear画面に移動
        //それ以外の場合次ステージへ進む
        if (_sceneManager.GetScenesNum() == (int)_sceneManager.GetGameScene() + 1)
        {
            LoadScene(SceneManagerScript.GameScenes.Clear);
        }
        else
        {
            LoadNextStage();
        }
    }

    /// <summary>
    /// temp実装
    /// </summary>
    /// <param name="sprite"></param>
    public async void ChangePlayerSprite(Sprite sprite, int waitSecounds)
    {
        _playerSprite.AsyncUpdate(sprite);

        await Task.Delay(waitSecounds * 1000); //ここ、キャンセル処理が必要
        _playerSprite.Reset();
    }
}
