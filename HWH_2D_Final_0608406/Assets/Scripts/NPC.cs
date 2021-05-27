using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    [Header("商店介面")]
    public GameObject Object;
    /// <summary>
    /// 玩家選取的武器
    /// 0 石劍
    /// 1 銅劍
    /// </summary>
    public int indexWeapon;

    public GameObject[] objWeapon;

    private player player;
    private void Start()
    {
        player = GameObject.Find("主角").GetComponent<player>();
    }
    /// <summary>
    /// 
    /// </summary>
    private int[] priceWeapon = { 5, 3 };
    private float[] attackWeapon = { 50, 30 };
    /// <summary>
    /// 開啟商店介面
    /// </summary>
    public void OpenShop()
    {
        Object.SetActive(true);
    }
    /// <summary>
    /// 關閉商店介面
    /// </summary>
    public void CloseShop()
    {
        Object.SetActive(false);
    }

    public void ChooseWeapon(int choose)
    {
        indexWeapon = choose;
    }


    //public void Buy()
    //{
        //if (player.coin > -priceWeapon[indexWeapon])
        //{
            //player.coin -= priceWeapon[indexWeapon];
            //player.texrCoin.text = "金幣" + player.coin;
            //for (int i = 0 ; i < objWeapon.Length; i++)
            //{
                //objWeapon[i].SetActive(false);
            //}
            
            //objWeapon[indexWeapon].SetActive(true);
        //}
    //}
    
        
    }



