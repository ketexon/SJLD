using Kutie;
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

    new Camera camera;

    bool canFlipSwitch = false;

    Vector2 mousePosition = Vector2.zero;

    Collider2D switchCollider = null;

    bool selectingLeftItem = true;

    void Awake()
    {
        camera = Camera.main;
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

    void FlipSwitch()
    {
        selectingLeftItem = !selectingLeftItem;
        panelAnimator.SetBool("Left", selectingLeftItem);
        switchAnimator.SetBool("Left", selectingLeftItem);
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

        var selectedItem = selectingLeftItem ? leftItemUI.Item : rightItemUI.Item;
        ItemManager.Instance.AddItem(selectedItem);
    }

    public void OnPlayerEnterStandPoint()
    {
        goButton.interactable = true;
        PlayerMovement.Instance.Moving = false;
        canFlipSwitch = true;
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
