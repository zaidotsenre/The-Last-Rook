using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    private float speed = 2.0f;

    private void Start()
    {

        // Subscribe to game over event
        GameManager.GameOver += StopMoving;
    }

    void Update()
    {
        transform.Translate(Vector3.back * Time.deltaTime * speed);

        if (transform.position.z <= -3.5f)
        {
            GameManager.CallGameOver();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
            Destroy(gameObject);
    }

    private void OnDestroy()
    {
        // unsub to game over event
        GameManager.GameOver -= StopMoving;
    }

    private void StopMoving ()
    {
        speed = 0;
    } 
}
