using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BossLazer : MonoBehaviour
{
    public Rigidbody rb;
    //public ParticleSystem particle;
   

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void Damaged()
    {
        //// パーティクルシステムのインスタンスを生成する。
        //ParticleSystem newParticle = Instantiate(particle);
        //// パーティクルの発生場所をこのスクリプトをアタッチしているGameObjectの場所にする。
        //newParticle.transform.position = this.transform.position;
        //// パーティクルを発生させる。
        //newParticle.Play();
        //// インスタンス化したパーティクルシステムのGameObjectを5秒後に削除する。(任意)
        //// ※第一引数をnewParticleだけにするとコンポーネントしか削除されない。
        //Destroy(newParticle.gameObject, 5.0f);
    }
    void OnTriggerEnter(Collider other)
    {
        Damaged();
    }
}
