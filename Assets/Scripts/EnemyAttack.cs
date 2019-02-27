using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private GameController gameController;
    public GameObject explosion;
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
        
        if (other.tag == "Player")
        {
            // PlayerController player = (PlayerController)other.gameObject;
            Instantiate(explosion, other.transform.position, transform.rotation);
        }
        
    }
}
