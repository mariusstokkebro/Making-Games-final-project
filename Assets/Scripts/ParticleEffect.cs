using UnityEngine;

public class ParticleEffect : MonoBehaviour
{
    [SerializeField] private ParticleSystem particleSystem;
    private float countdown;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        countdown = particleSystem.main.duration;
        particleSystem.Play();
    }

    // Update is called once per frame
    void Update()
    {
        countdown  -= Time.deltaTime;
        if(countdown <= 0)
            Destroy(gameObject);
    }
}
