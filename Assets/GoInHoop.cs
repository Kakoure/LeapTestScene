using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoInHoop : MonoBehaviour
{
    public int score;
    public TextMesh text;
    public ParticleSystem ps;
    public int materialChangeNum;
    public SpawnerController spawner;

    private void Start()
    {
        score = 0;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Ball"))
        {
            GameObject.Destroy(other);
            score++;
            text.text = "Your Score: " + score;
            ps.Play();
            if(score % materialChangeNum == 0)
            {
                spawner.NextMaterial();
            }
        }
    }
}
