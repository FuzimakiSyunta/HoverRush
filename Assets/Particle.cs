using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle : MonoBehaviour
{
    public ParticleSystem particle;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.tag == "Bullet")
        {
            ParticleSystem newParticle = Instantiate(particle);
            //èÍèäå≈íË
            newParticle.transform.position = this.gameObject.transform.position;
            //î≠ê∂
            newParticle.Play();

            Destroy(newParticle.gameObject,0.5f);
        }
    }
}
