using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class PlayerSprite : MonoBehaviour
{
    [SerializeField] List<Sprite> spritesList = default;

    private int spritesIndex = 0;
    private Sprite playerSprite = null; //スタート画面で選択したキャラ絵を参照

    void Start()
    {
        playerSprite = spritesList[0];
    }

    public Sprite GetPlayerSprite()
    {
        return playerSprite;
    }

    public void Shift(int shift)
    {
        spritesIndex += shift;

        if (spritesIndex <= -1)
        {
            spritesIndex = spritesList.Count - 1;
        }
        else if (spritesIndex > spritesList.Count - 1)
        {
            spritesIndex = 0;
        }

        playerSprite = spritesList[spritesIndex];
        AsyncUpdate(playerSprite);
    }

    public async void AsyncUpdate(Sprite sprite)
    {
        GameObject player = null;
        
        //Playerオブジェクトを探索する回数に制限
        int count = 0;
        int LIMIT = 10; 

        while (player == null && count++ <= LIMIT)
        {
            await Task.Delay(10);

            player = GameObject.FindGameObjectWithTag("Player");
        }

        player.GetComponent<SpriteRenderer>().sprite = sprite;
    }

    public void Reset()
    {
        AsyncUpdate(playerSprite);
    }
}
