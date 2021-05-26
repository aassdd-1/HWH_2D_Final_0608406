using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class item : MonoBehaviour
{
    [Header("掉落物品")]
    public GameObject prop;
    [Header("掉落機率")]
    public float percent =0.7f;

    /// <summary>
    /// 掉落道具
    /// </summary>
    public void DropProp()
    {
       float r = Random.Range(0f, 1f);

        print("隨機值:" + r);

        if (r <= percent)
        {
            //生成(生成的物件 , 座標 , )
            Instantiate(prop, transform.position, transform.rotation);
        }

        Destroy(gameObject);
    }

}
