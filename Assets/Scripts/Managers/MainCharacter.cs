using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacter : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform shootEndPoint;
    public delegate void OnPlayerHit(int lives);
    public event OnPlayerHit onPlayerHit;
    
    PlayerData playerData;

    bool isInCooldown;
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
        playerData.bulletCooldown = playerConfig.bulletCooldown;
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
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
        
    }

    void Shoot()
    {
        // Only one bullet can be shot each time
        if(PoolManager.Instance.GetActiveMembersCount(bulletPrefab) >= playerData.maxBullets ||
           isInCooldown)
            return;

        StartCoroutine(CooldownCoroutine());
        
        GameObject newBullet = PoolManager.Instance.Spawn(bulletPrefab, shootEndPoint.position, Quaternion.identity);
        newBullet.GetComponent<Bullet>().Init(playerData.bulletSpeed,Vector3.up);
        
        // TODO: Play SFX
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            playerData.lives -= 1;
        
            onPlayerHit?.Invoke(playerData.lives);
        }
  
    }

    public void Move(Vector3 direction)
    {
        transform.position += direction * (playerData.speed * Time.deltaTime);
    }

    IEnumerator CooldownCoroutine()
    {
        isInCooldown = true;
        yield return new WaitForSeconds(playerData.bulletCooldown);
        isInCooldown = false;
    }
}
