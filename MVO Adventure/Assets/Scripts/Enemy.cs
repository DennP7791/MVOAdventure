using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    public int hp;
    public int minDmg;
    public int maxDmg;
    public int armor;
    public int xp;
    public Sprite sprite;

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            setupEnemy();
            GameManager.instance.OnEnterCombat();
            Destroy(this.gameObject);
        }
    }

    void setupEnemy()
    {
        GameManager.instance.enemyArmor = armor;
        GameManager.instance.enemyHP = hp;
        GameManager.instance.enemyCurrentHP = hp;
        GameManager.instance.enemyMaxDmg = maxDmg;
        GameManager.instance.enemyMinDmg = minDmg;
        GameManager.instance.enemyXp = xp;
        GameManager.instance.enemySprite = sprite;
    }


}
