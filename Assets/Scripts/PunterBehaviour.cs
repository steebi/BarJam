using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;
using System;

public class PunterBehaviour : MonoBehaviour
{
    public PunterController punterController;
    public float orderDisplayTime = 5.0f;
    public float displayTime;
    public CanvasGroup canvasGroup;
    public OrderItem orderItemPrefab;
    public GameObject orderList;

    private Inventory myInventory;

    private Vector3 _speedVector;
    private float _speed = 10f;
    private Coroutine runningRoutine = null;

    #region UNITY_CALLBACKS

    void Awake()
    {
        this.punterController = new PunterController();
    }

    void Start()
    {
        this._speedVector = new Vector3(-this._speed, 0f, 0f);
    }

    void Update()
    {
        // TODO: probably switch case
        if (this.punterController.State == PunterState.ApproachingBar)
        {
            gameObject.transform.position += this._speedVector * Time.deltaTime;
        }
        else if(this.punterController.State == PunterState.ReturningToCrowd)
        {
            gameObject.transform.position -= this._speedVector * Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Counter")
        {
            this.punterController.State = PunterState.AtBar;
        }
        if (other.tag == "Crowd" && this.punterController.State == PunterState.ReturningToCrowd)
        {
            Destroy(gameObject);
        }
    }

    #endregion

    public void Initialize()
    {
        ToggleOrderVisible(false);
        myInventory = this.punterController.GiveOrder();
        foreach(ItemType itemType in Enum.GetValues(typeof(ItemType))){
            Item item = myInventory.Get(itemType);
            if(item != null){
                OrderItem order = Instantiate(orderItemPrefab);
                order.SetSprites(IconManager.Instance.GetItemSprite(item.type), IconManager.Instance.GetNumberSprite(item.Count));
                order.transform.SetParent(orderList.transform, false);
            }
        }
        runningRoutine = StartCoroutine(DisplayOrder());
    }

    public void ShowOrder()
    {
        if(runningRoutine != null)
            StopCoroutine(runningRoutine);
        StartCoroutine(DisplayOrder());
    }

    public void ToggleOrderVisible(bool toggleOn)
    {
        canvasGroup.alpha = toggleOn?1.0f:0.0f;
        canvasGroup.interactable = toggleOn;
    }

    public IEnumerator DisplayOrder()
    {
        ToggleOrderVisible(true);
        yield return new WaitForSeconds(displayTime);
        // Destroy the sprites
        ToggleOrderVisible(false);
        yield return null;
    }
}