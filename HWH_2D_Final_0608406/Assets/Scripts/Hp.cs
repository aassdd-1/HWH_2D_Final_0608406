using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using System.Collections;
public class Hp : MonoBehaviour
{
    [Header("血條")]
    public Image Bar;
    [Header("傷害數值")]
    public RectTransform rectDamage;
    /// <summary>
    /// 血條跟最大值
    /// </summary>
    /// <param name="hp">當天血量</param>
    /// <param name="hpmax">血量最大值</param>
    public void Updatehpbar(float hp, float hpmax)
    {
        Bar.fillAmount = hp / hpmax;
    }

    public IEnumerator ShowDamagr(float damage)
    {
        RectTransform rect = Instantiate(rectDamage, transform);
        rect.anchoredPosition = new Vector2(223, 88);
        rect.GetComponent<Text>().text = damage.ToString();

        float y = rect.anchoredPosition.y;

        while ( y < 400)
        {
            y += 1;
            rect.anchoredPosition = new Vector2(0, y);
            yield return new WaitForSeconds(0.02F);


        }
        Destroy(rect.gameObject,0.5f);
    }
}
