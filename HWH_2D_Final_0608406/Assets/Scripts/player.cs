using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;//引用 介面 UI
using UnityEngine.SceneManagement;

public class player : MonoBehaviour
{
    //項目 類型
    //    等級
    [Header("等級")]
    public int lv = 1;
    [Header("移動速度")]
    public float speed = 10.5f;
    [Header("死亡")]
    public bool isDead = false;
    [Header("角色名稱")]
    public string cName = "貓咪";
    [Header("搖桿")]
    public FixedJoystick joystick;
    [Header("")]
    public Transform tra;
    [Header("動畫元件")]
    public Animator ani;
    [Header("偵測範圍")]
    public float rabgeAttack = 2.5f;
    [Header("音效來源")]
    public AudioSource aud;
    [Header("攻擊特效")]
    public AudioClip andAttack;
    [Header("血量")]
    public float Blood = 200;
    private float hpmax;
    [Header("血條系統")]
    public Hp hpmanager;
    [Header("攻擊力")]
    public float attack = 20;
    [Header("等級文字")]
    public Text expText;
    [Header("經驗值吧條")]
    public Image expime;
    [Header("經驗值資料")]
    public expdata expData;
    [Header("金幣音效")]
    public AudioClip soundEat;
    [Header("金幣文字")]
    public Text texrCoin;

    public int coin;
    public int attackWeapon;
    
    private float exp;
    private float expNeed = 100;
    //事件:繪製圖示
    private void OnDrawGizmos()
    {
        //指定圖示顏色(紅,綠,藍,透明)
        Gizmos.color = new Color(1, 0, 0, 0.4f);
        //繪製圖示 球體(中心點,半徑)
        Gizmos.DrawWireSphere(transform.position, rabgeAttack);
    }

    private void Move()
    {
        if (isDead) return;
        
        //print("移動");
        float horizontal = joystick.Horizontal;
        float h = horizontal;
        //print("水平" + h);
        float v = joystick.Vertical;
        //print("垂直" + v);

        tra.Translate(h * speed * Time.deltaTime, v * speed * Time.deltaTime, 0);
        ani.SetFloat("水平", h);
        ani.SetFloat("垂直", v);

    }
    //凡是設公開就一定要設Public
    public void Attack()
    {
        if (isDead) return;
        print("攻擊");
        aud.PlayOneShot(andAttack, 0.5f);
        RaycastHit2D hit = Physics2D.CircleCast(transform.position, rabgeAttack, -transform.up,0, 1 << 8 );
        //print("碰到的物件:" + hit.collider.name);
        if (hit && hit.collider.tag == "道具") hit.collider.GetComponent<item>().DropProp();
        if (hit && hit.collider.tag == "敵人") hit.collider.GetComponent<AI>().hit(attack);
        if (hit && hit.collider.tag == "NPC") hit.collider.GetComponent<NPC>().OpenShop();

    }
    public void hit(float damage)
    {
        Blood -= damage;
        hpmanager.Updatehpbar(Blood, hpmax);
        StartCoroutine(hpmanager.ShowDamagr(damage));

        if (Blood <= 0) Dead();
    }
    private void Dead()
    {
        Blood = 0;
        isDead = true;
        Invoke("RePlay", 2);
    }
    private void RePiay()
    {
        SceneManager.LoadScene("遊戲場景");
    }

    public void Exp(float getexp)
    {
        expNeed = expData.exp[lv - 1];
        
        exp += getexp;
        print("經驗值" + exp);
        expime.fillAmount = exp / expNeed;

        while (exp >= expNeed)
        {
            lv++;
            expText.text = "LV" + lv;
            exp -= expNeed;
            expime.fillAmount = exp / expNeed;
            expNeed = expData.exp[lv - 1];
            Levelup();
        }

    }

    private void Levelup()
    {
        attack = 20 + (lv - 1) * 10;
        hpmax = 200 + (lv - 1) * 50;
    }

    private void Start()
    {
        coin = 10;
        texrCoin.text = "金幣" + coin;

        hpmax = Blood;

        for(int i=0; i < 14; i++)
        {
            expData.exp[i] = (i + 1) * 100;
        }
    }
    private void Update()
    {
        Move();
    }
    

    //觸發事件-進入:兩個物件其中1個要勾選is Trigger
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "金條")
        {
            coin++;
            aud.PlayOneShot(soundEat);
            Destroy(collision.gameObject);
            texrCoin.text = "金幣:" + coin;
            //print(collision.gameObject);
        }
    }

}
