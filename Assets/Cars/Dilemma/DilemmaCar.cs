using Kutie;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class DilemmaCar : Car
{
    [SerializeField] Button goButton;
    [SerializeField] LayerMask switchLayerMask;

    [SerializeField] ItemUI leftItemUI;
    [SerializeField] ItemUI rightItemUI;

    [SerializeField] Animator panelAnimator;
    [SerializeField] Animator switchAnimator;

    [SerializeField] Transform leftSquirmersContainer;
    [SerializeField] Transform rightSquirmersContainer;
    [SerializeField] GameObject squirmerPrefab;

    List<Animator> leftSquirmerAnimators = new();
    List<Animator> rightSquirmerAnimators = new();

    Button leftItemButton;
    Button rightItemButton;

    new Camera camera;

    bool canFlipSwitch = false;

    Vector2 mousePosition = Vector2.zero;

    Collider2D switchCollider = null;

    bool selectingLeftItem = true;

    void Awake()
    {
        camera = Camera.main;

        panelAnimator.SetBool("Left", selectingLeftItem);
        panelAnimator.SetBool("Active", false);
        switchAnimator.SetBool("Left", selectingLeftItem);

        leftItemButton = leftItemUI.GetComponent<Button>();
        rightItemButton = rightItemUI.GetComponent<Button>();

        leftItemButton.enabled = !selectingLeftItem;
        rightItemButton.enabled = selectingLeftItem;
    }

    void OnPoint(Vector2 pos)
    {
        mousePosition = pos;
    }

    void OnClick()
    {
        if(switchCollider && canFlipSwitch)
        {
            FlipSwitch();
        }
    }

    public void FlipSwitch()
    {
        selectingLeftItem = !selectingLeftItem;

        panelAnimator.SetBool("Left", selectingLeftItem);
        switchAnimator.SetBool("Left", selectingLeftItem);

        leftItemButton.enabled = !selectingLeftItem;
        rightItemButton.enabled = selectingLeftItem;

        UpdateSquirmerAnimators();
    }

    public override void OnEnable()
    {
        base.OnEnable();

        PlayerController.Instance.enabled = false;
        PlayerMovement.Instance.Moving = true;

        goButton.onClick.AddListener(OnGoButtonPressed);
        
        PlayerPoint.Instance.PointEvent.AddListener(OnPoint);
        PlayerPoint.Instance.ClickEvent.AddListener(OnClick);

        var (leftDebuff, rightDebuff) = ItemManager.Instance.GetRandomDebuffs();
        leftItemUI.Item = leftDebuff;
        rightItemUI.Item = rightDebuff;

        SpawnSquirmers();
    }

    void SpawnSquirmers()
    {
        int left = Random.Range(1, leftSquirmersContainer.childCount + 1);
        int right = Random.Range(1, rightSquirmersContainer.childCount + 1);

        for(int i = 0; i < left; ++i)
        {
            var spawn = leftSquirmersContainer.GetChild(i);
            var squirmerGO = Instantiate(squirmerPrefab, spawn);
            (squirmerGO.transform as RectTransform).anchoredPosition = Vector2.zero;
            leftSquirmerAnimators.Add(squirmerGO.GetComponent<Animator>());
        }
        for (int i = 0; i < right; ++i)
        {
            var spawn = rightSquirmersContainer.GetChild(i);
            var squirmerGO = Instantiate(squirmerPrefab, spawn);
            (squirmerGO.transform as RectTransform).anchoredPosition = Vector2.zero;
            rightSquirmerAnimators.Add(squirmerGO.GetComponent<Animator>());
        }

        UpdateSquirmerAnimators();
    }

    void UpdateSquirmerAnimators()
    {
        foreach(var animator in leftSquirmerAnimators)
        {
            animator.SetBool("Squirming", selectingLeftItem);
        }
        foreach (var animator in rightSquirmerAnimators)
        {
            animator.SetBool("Squirming", !selectingLeftItem);
        }
    }

    public override void OnDisable()
    {
        base.OnDisable();
        if(PlayerPoint.Instance)
        {
            PlayerPoint.Instance.PointEvent.RemoveListener(OnPoint);
            PlayerPoint.Instance.ClickEvent.RemoveListener(OnClick);
        }
    }

    void OnGoButtonPressed()
    {
        canFlipSwitch = false;

        leftItemButton.interactable = false;
        rightItemButton.interactable = false;
        leftItemButton.targetGraphic.enabled = selectingLeftItem;
        rightItemButton.targetGraphic.enabled = !selectingLeftItem;

        var selectedItem = selectingLeftItem ? leftItemUI.Item : rightItemUI.Item;
        ItemManager.Instance.AddItem(selectedItem);

        GameManager.Instance.Score++;
        
        var selectedSquirmers = selectingLeftItem ? leftSquirmerAnimators : rightSquirmerAnimators;
        var nonSelectedSquirmers = !selectingLeftItem ? leftSquirmerAnimators : rightSquirmerAnimators;
        GameManager.Instance.AxolotlsKilled += selectedSquirmers.Count;
        GameManager.Instance.AxolotlsSaved += nonSelectedSquirmers.Count;
        var delta = selectedSquirmers.Count - nonSelectedSquirmers.Count;
        if(delta > 0)
        {
            GameManager.Instance.AxolotlsDeltaKilled += delta;
            GameManager.Instance.BadDecisions += 1;
        }
        else if(delta < 0)
        {
            GameManager.Instance.AxolotlsDeltaSaved += delta;
            GameManager.Instance.GoodDecisions += 1;
        }
    }

    public void OnPlayerEnterStandPoint()
    {
        goButton.interactable = true;
        PlayerMovement.Instance.Moving = false;
        canFlipSwitch = true;
        panelAnimator.SetBool("Active", true);
    }

    void Update()
    {
        if (canFlipSwitch)
        {
            Vector3 world = camera.ScreenToWorldPoint(mousePosition.WithZ(camera.nearClipPlane));
            Physics2D.queriesHitTriggers = true;
            switchCollider = Physics2D.OverlapPoint(world, switchLayerMask);
        }
    }
}
