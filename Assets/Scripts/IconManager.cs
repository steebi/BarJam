using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts{

	public class IconManager : Singleton<IconManager> {

		public List<Sprite> m_itemSprites = new List<Sprite>();
		public List<Sprite> m_numberSprites = new List<Sprite>();
		public Sprite m_errorSprite;

		public Sprite GetItemSprite(ItemType itemtype){
			int position = (int)itemtype;
			if(position < m_itemSprites.Count)
				return m_itemSprites[position];
			else
				return m_errorSprite;
		}	

		public Sprite GetNumberSprite(int count){
			if(count < m_numberSprites.Count)
				return m_numberSprites[count];
			else
				return m_errorSprite;
		}

	}
}