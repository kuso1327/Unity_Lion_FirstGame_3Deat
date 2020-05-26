﻿using UnityEngine;

public class CameraTrack : MonoBehaviour
{
    #region 欄位與屬性
    /// <summary>
    /// 玩家變形元件
    /// </summary>
    private Transform player;
    [Header("追蹤速度"),Range(0.1f,50.5f)]
    public float speed = 1.5f;
    #endregion

    #region 方法
    private void Track()
    {
        //攝影機與小明Y軸的距離5.5-3=2.5
        //攝影機與小明Z軸的距離-2.5-0=-2.5
        Vector3 posTrack=player.position;
        posTrack.y += 2.5f;
        posTrack.z += -2.5f;
        //攝影機座標=變形.座標
        Vector3 posCam = transform.position;
        //攝影機座標=三為向量.插值(A點,B點,百分比)
        posCam = Vector3.Lerp(posCam, posTrack, 0.5F * Time.deltaTime * speed);
        //變形.座標 = 攝影機座標
        transform.position = posCam;
        

    }
    #endregion

    #region 事件
    /*實驗Lerp差值
     * public float A = 0;
    public float B = 1000;

    public Vector2 v2A = Vector2.zero;
    public Vector2 v2B = Vector2.one * 1000;
    
    //Lerp差值
    private void Update()
    {
        Mathf.Lerp(A, B, 0.5f);
        v2A = Vector2.Lerp(v2A, v2B, 0.5F);
;    }*/
    private void Start()
    {
        //小明物件=遊戲物件.尋找("物件名稱").變形
        player = GameObject.Find("小明").transform;
    }

    //延遲更新：會在Update執行後再執行
    //建議：需要追蹤座標要寫在此事件內
    private void LateUpdate()
    {
        Track();
    }

    #endregion
}
