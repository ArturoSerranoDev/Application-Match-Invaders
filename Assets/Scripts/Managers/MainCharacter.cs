﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacter : Ship
{
    public GameObject bulletPrefab;
    [SerializeField] Transform shootEndPoint;
    public delegate void OnPlayerHit(int lives);
    public event OnPlayerHit onPlayerHit;
    
    PlayerData playerData;
    bool isInCooldown;
    bool isInputEnabled;
    
    void OnEnable()
    {
        LevelManager.onGameStart += EnableInput;
        LevelManager.onGameWon += DisableInput;
        LevelManager.onGameLost+= DisableInput;
    }
    
    void OnDisable()
    {
        LevelManager.onGameStart -= EnableInput;
        LevelManager.onGameWon -= DisableInput;
        LevelManager.onGameLost -= DisableInput;
    }
    
    public void SetData(PlayerConfig playerConfig)
    {
        playerData = new PlayerData();
        playerData.speed = playerConfig.speed;
        playerData.lives = playerConfig.lives;
        playerData.bulletSpeed = playerConfig.bulletSpeed;
        playerData.maxBullets = playerConfig.maxBullets;
        playerData.bulletCooldown = playerConfig.bulletCooldown;
    }

    void EnableInput()
    {
        isInputEnabled = true;
    }
    
    void DisableInput()
    {
        isInputEnabled = false;
    }

    void Update()
    {
        if (!isInputEnabled)
            return;
        
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            Move(Vector3.left);
        } else if (Input.GetKey(KeyCode.RightArrow))
        {
            Move(Vector3.right);
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
    }

    public override void Shoot()
    {
        // Only one bullet can be shot each time
        if(PoolManager.Instance.GetActiveMembersCount(bulletPrefab) >= playerData.maxBullets ||
           isInCooldown)
            return;

        StartCoroutine(CooldownCoroutine());
        
        GameObject newBullet = PoolManager.Instance.Spawn(bulletPrefab, shootEndPoint.position, Quaternion.identity);
        newBullet.GetComponent<Bullet>().Init(playerData.bulletSpeed,Vector3.zero);
        
        SFXPlayer.Instance.PlaySFX(SFXPlayer.Instance.playerShoot,GetComponent<AudioSource>(),0.5f,Random.Range(0.5f,1f));

    }
    
    public override void Move(Vector3 direction)
    {
        transform.position += direction * (playerData.speed * Time.deltaTime);
    }
    
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EnemyBullet"))
        {
            playerData.lives -= 1;
        
            onPlayerHit?.Invoke(playerData.lives);
            SFXPlayer.Instance.PlaySFX(SFXPlayer.Instance.enemyDestroyed,GetComponent<AudioSource>(),0.5f,Random.Range(0.5f,1f));

        }
    }
    
    public void Despawn()
    {
        PoolManager.Instance.Despawn(this.gameObject);
    }
    
    IEnumerator CooldownCoroutine()
    {
        isInCooldown = true;
        yield return new WaitForSeconds(playerData.bulletCooldown);
        isInCooldown = false;
    }
}
