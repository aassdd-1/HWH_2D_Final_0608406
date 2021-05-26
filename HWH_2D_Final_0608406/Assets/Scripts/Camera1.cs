using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera1 : MonoBehaviour
{
    [Header("追蹤速度"), Range(0, 50)]
    public float speed = 1.5f;
    [Header("上下邊界")]
    public Vector2 limitY = new Vector2(-5, 5);
    [Header("左右邊界")]
    public Vector2 limitx = new Vector2(-5, 5);
    [Header("玩家")]
    public Transform player;

    private void Update()
    {
        Track();
    }
    //public float a = 0;
    //public float b = 100;

    //public Vector3 v3a = new Vector3 (0, 0, 0);
    //public Vector3 v3b = new Vector3 (100, 100, 100);
    //private void updata()
    //{
    //    a = Mathf.Lerp(a, b, 0.5f);
    //    v3a = Vector3.Lerp(v3a, v3b, 0.5f);
    //}
    

    private void Track()
    {
        Vector3 posCam = transform.position;
        Vector3 posPla = player.position;

        posCam = Vector3.Lerp(posCam, posPla, 0.5f);
        posCam.z = -10;
        posCam.x =Mathf.Clamp(posCam.x, limitx.x, limitx.y);
        posCam.y = Mathf.Clamp(posCam.y, limitY.x, limitY.y);
        transform.position = posCam;
        
        //print(posCam);

    }
}
