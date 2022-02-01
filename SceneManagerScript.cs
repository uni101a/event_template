using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour
{
    /// <summary>
    /// ステージを増やしたいときはここに追加
    /// 必ずNoneSceneより下にステージ順に追加する
    /// 
    /// NoneScene,
    /// ステージ1 Stage1,
    /// ステージ2 Stage2,
    /// ...
    /// </summary>
    public enum GameScenes
    {
        Title,
        Loading,
        Clear,
        NoneScene,
        Stage1,
    }
    public int GetScenesNum()
    {
        return System.Enum.GetNames(typeof(GameScenes)).Length;
    }

    private GameScenes _gameScene;
    public GameScenes GetGameScene()
    {
        return _gameScene;
    }

    void Start()
    {
        foreach(GameScenes key in sceneList.Keys)
        {
            if(sceneList[key] == SceneManager.GetActiveScene().name)
            {
                _gameScene = key;
            }
        }
    }

    /// <summary>
    /// ステージを増やしたいときはここに要素として{GameScenes.Enum型, "シーン名"}
    /// (順番は指定なし）
    /// </summary>
    private  Dictionary<GameScenes, string> sceneList = new Dictionary<GameScenes, string>()
    {
        { GameScenes.Title, "Title" },
        { GameScenes.Loading, "Loading" },
        { GameScenes.Clear, "Clear" },
        { GameScenes.Stage1, "Stage1" },
    };

    public void ChangeScene(GameScenes stageType)
    {
        _gameScene = stageType;

        SceneManager.LoadScene(sceneList[_gameScene]);
    }
}
