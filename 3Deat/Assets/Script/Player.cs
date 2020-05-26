using UnityEngine;

public class Player : MonoBehaviour
{
    #region 欄位屬性
    [Header("移動速度"),Range(1,1000)]
    public float Speed = 10;
    [Header("跳躍高度"),Range(1,500)]
    public float Height;

    /// <summary>
    /// 是否在地板上
    /// </summary>
    public bool isGround
    {
        get
        {
            if (transform.position.y < 0.0877777F) return true; // 如果Y軸小於 0.087傳回true
            else return false;                              //否則 傳回false
        }    

}

   /// <summary>
   /// 旋轉角度
   /// </summary>
    private Vector3 angle;

    private Animator ani;
    private Rigidbody rig;
    /// <summary>
    /// 跳躍力道：從0慢慢增加
    /// </summary>
    private float jump;


    #endregion

    #region 方法
    /// <summary>
    /// 移動：透過鍵盤
    /// </summary>
    private void Move()
    {
        #region 移動
        //浮點數 前後值 = 輸入類別.取得軸向值("垂直") - 垂直W S上下
        //float v = Input.GetAxisRaw("Vertical");
        float v = Input.GetAxis("Vertical");
        //水平AD左右
        float h = Input.GetAxis("Horizontal");

        //剛體物件.添加推力(x,y,z) - 世界座標
        //rig.AddForce(Speed *h, 0, Speed*v);
        //剛體.添加推力(三維向量)
        //前方 transform.forward - z
        //右方 transform.right - x
        //上方 transform.up - y
        rig.AddForce(transform.forward * Speed * Mathf.Abs(v));
        rig.AddForce(transform.forward * Speed * Mathf.Abs(h));


        //動畫.設定的布林值("跑步參數",布林值) - 當 前後取絕對值 大於0時勾選
        ani.SetBool("跑步開關", Mathf.Abs(v) > 0 ||Mathf.Abs(h)>0);
        //ani.SetBool("跑步開關", v==1 || v==-1); //使用邏輯運算子
        #endregion

        #region 轉向
        
        if (v == 1) angle = new Vector3(0, 0, 0);           //前 Y 0
        else if (v == -1) angle = new Vector3(0, 180, 0);   //後 Y 180
        else if (h == 1) angle = new Vector3(0, 90, 0);     //右 Y 90
        else if (h == -1) angle = new Vector3(0, 270, 0);   //左 Y 270
        //只要類別後面有：MonoBehaviour
        //就可以直接使用關鍵字transform取得此物件Transform元件
        //eulerAngle歐拉角度0 -360
        transform.eulerAngles = angle;
      
        #endregion

    }

    /// <summary>
    /// 跳躍：判斷在地板上並按下空白鍵時跳躍
    /// </summary>
    private void Jump()
    {
        //如果在地板上 為 勾選 並且 按下空白鍵
        if(isGround && Input.GetButtonDown("Jump"))
        {
            //每次跳躍 值從0開始
            jump = 0;
            //剛體.推力(0,跳躍高度,0)
            rig.AddForce(0, Height, 0);
        }

        //如果 不是在地板上
        if(!isGround)
        {
            //跳躍 遞增 時間.一禎時間
            jump += Time.deltaTime;
        }
        //動畫.設定浮點數("跳躍參數",跳躍時間)
        ani.SetFloat("跳躍力道", jump);
    }
    /// <summary>
    ///碰到道具：碰到帶有標籤【肥宅餅】的物件 
    /// </summary>
    private void HitProp()
    {

    }



    #endregion

    #region 事件
    private void Start()
    {
        //GetComponent<泛型>()泛型方法 - 泛型 所有類型 Rigidbody,Transform,Collider
        //剛體 = 取得元件<剛體>();
        rig = GetComponent<Rigidbody>();
        ani = GetComponent<Animator>();
    }

    //固定更新頻率事件：1秒50禎，是用物理必須在事件內
    private void FixedUpdate()
    {
        Move();

    }

    private void Update()
    {
        Jump();
        
    }
    #endregion

}
