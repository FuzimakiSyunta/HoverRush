using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public Rigidbody rb;
    public ParticleSystem particle;
    private float moveSpeedZ;
    private float moveSpeedY;

    // Start is called before the first frame update
    void Start()
    {
        moveSpeedZ = 23.0f;
        moveSpeedY = -35.0f;
        rb.velocity = new Vector3(0, moveSpeedY, moveSpeedZ);
        Destroy(gameObject, 1.5f);
    }

    // Update is called once per frame
    void Update()
    {
        // 経過時間に基づいてスピードを変化させる
        float timeFactor = Time.time; // Unityのワールド開始からの時間を取得
        float newSpeedZ = moveSpeedZ + (timeFactor * 1.1f); // Zスピードが徐々に増加
        float newSpeedY = moveSpeedY + (timeFactor * 2.0f); // Yスピードが徐々に増加
        rb.velocity = new Vector3(0, newSpeedY, newSpeedZ);
    }
    void Damaged()
    {
        // パーティクルシステムのインスタンスを生成する。
        ParticleSystem newParticle = Instantiate(particle);
        // パーティクルの発生場所をこのスクリプトをアタッチしているGameObjectの場所にする。
        newParticle.transform.position = this.transform.position;
        // パーティクルを発生させる。
        newParticle.Play();
        // インスタンス化したパーティクルシステムのGameObjectを5秒後に削除する。(任意)
        // ※第一引数をnewParticleだけにするとコンポーネントしか削除されない。
        Destroy(newParticle.gameObject, 5.0f);
    }
    void OnTriggerEnter(Collider other)
    {   
        if (other.gameObject.tag == "Enemy")
        {
            Damaged();
            Destroy(this.gameObject);
        }
        if (other.gameObject.tag == "Boss")
        {
            Damaged();
            Destroy(this.gameObject);
        }
    }

}
