﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplinterTrigger : MonoBehaviour
{
    private float playerPos;
    private float platPos;
    void OnTriggerEnter2D(Collider2D collider)
    {
        playerPos = GameObject.FindWithTag("Player").transform.position.y;
        platPos = this.transform.position.y;
        if (collider.transform.tag == "Player" && playerPos - platPos > 0 && !GameObject.Find("DisturbanceManager").GetComponent<DisturbanceManager>().isSheild) // 플레이어가 위에 있음.
        {
            Debug.Log("플레이어가 가시에 닿음");
            // red effect
            StartCoroutine(ChangingRed(GameObject.FindWithTag("Player")));
            
            GameObject.Find("LifeManager").GetComponent<LifeManager>().LifeDown();
            if (GameSaveData.life == 0)
            {
                // 함정 이펙트 적용 시간 기다렸다가 게임 오버
                Invoke("InvokeGameOver", 1f);
            }
        }

    }

    void InvokeGameOver()
    {
        GameObject.Find("GameManager").GetComponent<GameOverManager>().gameOver("결국 생쥐들의 크리스마스는 반짝반짝거리지 못했다…");
    }

    IEnumerator ChangingRed(GameObject player)
    {
        int num = 2;
        while (num-- > 0)
        {
            player.GetComponent<SpriteRenderer>().color = new Color(1f, 0.46f, 0.46f, 1f);
            yield return new WaitForSeconds(0.2f);
            player.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
            yield return new WaitForSeconds(0.2f);
        }
    }

}