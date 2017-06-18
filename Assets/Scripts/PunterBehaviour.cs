

    private void Start()
    {
        this._speedVector = new Vector3(-this._speed, 0f, 0f);
    }

    void Update()
    {
        if (this.punterController.State == PunterState.ApproachingBar)
        {
            gameObject.transform.position += this._speedVector * Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Counter")
        {
            this.punterController.State = PunterState.AtBar;
        }
    }