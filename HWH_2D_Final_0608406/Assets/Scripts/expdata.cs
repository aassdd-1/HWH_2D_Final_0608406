using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="經驗值資料",menuName ="賴彥廷的經驗值資料")]
public class expdata :ScriptableObject
{
    [Header("每個等級所需經驗值")]
    public float[] exp;
}
