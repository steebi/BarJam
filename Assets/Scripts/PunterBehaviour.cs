using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;

public class PunterBehaviour : MonoBehaviour
{
    private Vector3 _speedVector;
    private float _speed = 10f;
    public PunterController PunterController;

    private Sprite beerSprite;
    private Sprite waterSprite;
    private Sprite whiskeySprite;
    private Sprite cokeSprite;

    public void Awake()
    {
        
    }

    private void Start()
    {
        this.PunterController = new PunterController();
        this._speedVector = new Vector3(-this._speed, 0f, 0f);
    }

    void Update()
    {
        if (this.PunterController.State == PunterState.ApproachingBar)
        {
            gameObject.transform.position += this._speedVector * Time.deltaTime;
        }
    }

    IEnumerator DisplayPunterOrder(Inventory inventory) {
        // Go through the list of items and create a sprite for each
        List<Sprite> orderItems = new List<Sprite>();
        foreach (KeyValuePair<ItemType, Item> kvp in inventory)
        {
            ItemType type = kvp.Value.type;
            int count = kvp.Value.Count;
            if (count <= 0)
            {
                continue;
            }
        }
    }

    Sprite CreateSpriteOfOrderItem(ItemType orderItem)
    {
        switch (orderItem)
        {
            case ItemType.Beer:

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Counter")
        {
            this.PunterController.State = PunterState.AtBar;
        }
    }
}