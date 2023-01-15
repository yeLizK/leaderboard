using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField]
    private float LerpTime;

    private void Start()
    {
        LerpTime = 10f;
    }
    private void Update()
    {
        if(GameManager.Instance.isGameOn)
        {
            transform.Translate(-Vector3.forward * LerpTime * Time.deltaTime);
        }
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag.Equals("Wall"))
        {
            this.gameObject.SetActive(false);
        }
        else if(collision.gameObject.tag.Equals("Player"))
        {
            GameManager.Instance.GameOver();
            this.gameObject.SetActive(false);
            ObstacleManager.Instance.DestroyAllObjects();
        }
        
    }
}
