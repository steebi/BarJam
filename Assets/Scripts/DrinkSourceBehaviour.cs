using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;

public class DrinkSourceBehaviour : MonoBehaviour {

    public DrinkSourceController DrinkSourceController;
    public SpriteRenderer m_sprite;
    public ItemType drinkType;

    private void Awake() {
        m_sprite.sprite = IconManager.Instance.GetItemSprite(drinkType);
    }

	// Use this for initialization
	void Start () {
        // TODO: default to Beer for now, but make configurable later
        this.DrinkSourceController = new DrinkSourceController(this.drinkType, 100);
	}
}
