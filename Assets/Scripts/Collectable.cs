using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    bool isCollected = false;
    public int value = 0;

  void Show()
    {
        this.GetComponent<SpriteRenderer>().enabled = true;
        this.GetComponent<CircleCollider2D>().enabled = true;
        isCollected = false;
    }

  void Hide()
    {
        this.GetComponent<SpriteRenderer>().enabled = false;
        this.GetComponent<CircleCollider2D>().enabled = false;

    }

    void Collect()
    {
        isCollected = true;
        Hide();
        GameManager.sharedInstance.CollectObject(value);
    }

    void OnTriggerEnter2D(Collider2D otherCollider)
    {
        if(otherCollider.tag == "Player")
        {
            Collect();

        }
    }
}
