using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;

public class DrinkSourceBehaviour : MonoBehaviour {

    public DrinkSourceController DrinkSourceController;

	// Use this for initialization
	void Start () {
        // TODO: default to Beer for now, but make configurable later
        this.DrinkSourceController = new DrinkSourceController(ItemType.Beer, 100);
	}
}
