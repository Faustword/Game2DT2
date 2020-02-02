using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CollectableType
{
    healthPotion,
    manaPotion,
    money
}

public class Collectable : MonoBehaviour
{
    public CollectableType type = CollectableType.money;

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
        switch (this.type)
        {
            case CollectableType.money:
                GameManager.sharedInstance.CollectObject(value);
                break;
            case CollectableType.healthPotion:
                PlayerController.sharedInstance.CollectHealth(value);
                break;
            case CollectableType.manaPotion:
                PlayerController.sharedInstance.CollectMana(value);
                break;

        }
    }

    void OnTriggerEnter2D(Collider2D otherCollider)
    {
        if(otherCollider.tag == "Player")
        {
            Collect();

        }
    }
}
