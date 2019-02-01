using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour
{
    private GameController gameController;
    public GameObject explosion;
    public GameObject playerExplosion;
    public int value;

    private void Start()
    {
        GameObject result = GameObject.FindWithTag("GameController");
        if (result != null)
        {
            gameController = result.GetComponent<GameController>();
        }
        if (gameController == null)
        {
            Debug.Log("Cannot find the GameController");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Boundary")
        {
            return;
        }
        Instantiate(explosion, transform.position, transform.rotation);
        if (other.tag == "Player")
        {
            Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
            gameController.endGame();
        }
        Destroy(other.gameObject);
        Destroy(gameObject);
        gameController.IncreaseScore(value);
    }
}
