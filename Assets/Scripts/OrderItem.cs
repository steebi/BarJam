using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderItem : MonoBehaviour {

	[SerializeField]
	SpriteRenderer m_drinkSprite;
	[SerializeField]
	SpriteRenderer m_count;

	public void SetSprites(Sprite item, Sprite count){
		m_drinkSprite.sprite = item;
		m_count.sprite = count;
	}

}
