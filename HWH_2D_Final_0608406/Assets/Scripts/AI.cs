using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region 宣告
public class AI : MonoBehaviour
{
    [Header("追蹤範圍"),Range(0,500)]
    public float rangetrack = 2;
    [Header("攻擊範圍"), Range(0, 50)]
    public float rangeAttack = 0.5f;
    [Header("移動速度"), Range(0, 50)]
    public float speed = 2;
    [Header("攻擊特效")]
    public ParticleSystem psAttack;
    [Header("攻擊冷卻")]
    public float cdAttack = 3;
    [Header("攻擊力")]
    public float attack = 20;

    [Header("血量")]
    public float Blood = 200;
    private float hpmax;
    [Header("血條系統")]
    public Hp hpmanager;
  
    private bool isDead = false;
    [Header("經驗值")]
    public float exp = 30;
    private Transform player;
    private player _player;
    private float timer;
    #endregion
#region 方法


    /// <summary>
    /// 追蹤玩家
    /// </summary>
    


    private void Track()
    {
        if (isDead) return;
        
        //距離 等於 三維向量 的 距離(A點,B點)
        float dis = Vector3.Distance(transform.position, player.position);
        //如果 距離 小於等於 追蹤範圍 才開始追蹤
        if (dis <= rangeAttack)
        {

            Attack();

        }
        else if (dis<=rangetrack)
        {
            
            transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        }

        //print("距離" + dis);
    }

    private void Attack()
    {
        timer += Time.deltaTime;


        if (timer >= cdAttack)
        {
            timer = 0;
            psAttack.Play();
            Collider2D hit = Physics2D.OverlapCircle(transform.position, rangeAttack,1 << 9);
            hit.GetComponent<player>().hit(attack);
        }


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
        if (isDead) return;
        Blood = 0;
        isDead = true;
        Destroy(gameObject, 1f);
        //_player.Exp(exp);


    }
    #endregion
    #region 事件
    private void Start()
    {
        hpmax = Blood;
        player = GameObject.Find("主角").transform;
        _player = player.GetComponent<player>();
    }

    private void OnDrawGizmos()
    {
        //先指定顏色再畫圈
        Gizmos.color = new Color(0, 1, 0, 0.3F);
        //繪製球體(中心點.半徑)
        Gizmos.DrawSphere(transform.position, rangetrack);


        Gizmos.color = new Color(1, 0, 0, 0.5F);
        Gizmos.DrawSphere(transform.position, rangeAttack);

    }

    private void Update()
    {
        Track();
    }
    #endregion

}
