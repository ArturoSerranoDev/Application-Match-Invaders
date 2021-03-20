using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacter : MonoBehaviour
{
    PlayerData playerData;

    public void Init()
    {
        playerData = new PlayerData();
    }
    
    public void SetData(PlayerConfig playerConfig)
    {
        playerData = new PlayerData();
        playerData.speed = playerConfig.speed;
        playerData.lives = playerConfig.lives;
        playerData.bulletSpeed = playerConfig.bulletSpeed;
        playerData.maxBullets = playerConfig.maxBullets;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            Move(Vector3.left);
        } else if (Input.GetKey(KeyCode.RightArrow))
        {
            Move(Vector3.right);
        }
    }

    public void Move(Vector3 direction)
    {
        transform.position += direction * playerData.speed * Time.deltaTime;
    }
}
