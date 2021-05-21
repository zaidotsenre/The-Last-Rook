using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    private float speed = 5.0f;
    private float horizontalInput = 0.0f;
    private float sideBound = 3.5f;
    private AudioSource chessSound;

    // Start is called before the first frame update
    void Start()
    {
        chessSound = GetComponent<AudioSource>();
        GameManager.GameOver += StopMoving;
    }

    // Update is called once per frame
    void Update()
    {
        // Read player input
        horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.right * Time.deltaTime * speed * horizontalInput);

        // Keep player between boundaries
        if (transform.position.x < -sideBound)
        {
            Vector3 correctedPosition = transform.position;
            correctedPosition.x = -sideBound;
            transform.position = correctedPosition;
        } else if (transform.position.x > sideBound)
        {
            Vector3 correctedPosition = transform.position;
            correctedPosition.x = sideBound;
            transform.position = correctedPosition;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        chessSound.Play();
    }

    private void OnDestroy()
    {
        GameManager.GameOver -= StopMoving;
    }

    void StopMoving ()
    {
        speed = 0;
    }
}
