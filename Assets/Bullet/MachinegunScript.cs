using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class MachinegunScript : MonoBehaviour
{
    public Rigidbody rb;
    public ParticleSystem particle;

    // Start is called before the first frame update
    void Start()
    {
        float moveSpeed = 57.0f;
        rb.velocity = new Vector3(0, 0, moveSpeed);
        Destroy(gameObject, 1.5f);
    }

    // Update is called once per frame
    void Update()
    {

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
    void OnCollisionEnter(Collision other)
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
