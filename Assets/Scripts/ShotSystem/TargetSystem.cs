using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;
public class TargetSystem : MonoSingleton<TargetSystem>
{
    private PlayerInputs PlayerInputs;

    [Header("Target_3D_Field")]
    [Space(10)]

    public GameObject mainTarget;
    public GameObject target;
    public int gunCount = 0;

    [Header("Target_Field")]
    [Space(10)]

    [SerializeField] float _horizontalSpeed;
    [SerializeField] float _leftLimit, _upLimit;
    [SerializeField] float _rightLimit, _downLimit;

    [Header("Target_UI_Panel")]
    [Space(10)]

    public Image targetUI;
    [SerializeField] GameObject targetPanel;

    [SerializeField] float _horizontalUISpeed;


    //------------------------------------------------------------------
    Vector3 movement;
    Vector3 targetVector3;
    Quaternion targetQuaternion;
    //------------------------------------------------------------------

    public void SelectGun()
    {
        gunCount = 0;
        for (int i1 = 0; i1 < 9; i1++)
            if (MultiplierSystem.Instance.multiplierStat.multiplierClass.multiplierBool[i1])
                if (MultiplierSystem.Instance.multiplierStat.multiplierClass.multiplierCount[i1] > gunCount) gunCount = MultiplierSystem.Instance.multiplierStat.multiplierClass.multiplierCount[i1];

        if (ItemData.Instance.factor.shotCountdown != 1 + gunCount)
            ItemData.Instance.SetShotCountdown();

        if (target != null)
            target.transform.parent.gameObject.SetActive(false);
        else
            targetPanel.SetActive(true);

        target = mainTarget.transform.GetChild(gunCount).GetChild(0).gameObject;

        target.transform.parent.gameObject.SetActive(true);
    }

    private void Awake()
    {
        PlayerInputs = new PlayerInputs();
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        //if (GameManager.Instance.gameStat == GameManager.GameStat.start && RotateSystem.Instance.isTouch)

        if (target != null)
        {
            Vector2 inputVector = PlayerInputs.Player.Movement.ReadValue<Vector2>();

            movement = new Vector3(inputVector.x, inputVector.y, 0);

            Look();

            UIMove();
        }
    }

    private void Look()
    {
        target.transform.LookAt(targetUI.gameObject.transform);
    }

    private void Move()
    {
        targetQuaternion = target.transform.localRotation * Quaternion.Euler(movement * _horizontalSpeed);
        BoundaryCheck();

        target.transform.localRotation = Quaternion.Lerp(target.transform.localRotation, targetQuaternion, Time.deltaTime);
    }

    private void BoundaryCheck()
    {
        Vector3 targetV3 = targetQuaternion.eulerAngles;

        targetV3.z = 0;
        if (targetV3.x > 360 + _downLimit) targetV3.x -= 360;
        if (targetV3.y > 360 + _leftLimit) targetV3.y -= 360;
        print(targetV3.x);
        print(targetV3.y);
        targetV3.y = Mathf.Clamp(targetV3.y, _leftLimit, _rightLimit);
        targetV3.x = Mathf.Clamp(targetV3.x, _downLimit, _upLimit);
        targetV3.y = -targetV3.y;
        targetV3.x = -targetV3.x;

        targetQuaternion = Quaternion.Euler(targetV3);
    }

    private void UIMove()
    {
        targetVector3 = targetUI.rectTransform.localPosition + (movement * _horizontalUISpeed);

        BoundaryUICheck();

        targetUI.rectTransform.localPosition = Vector3.Lerp(targetUI.rectTransform.localPosition, targetVector3, Time.deltaTime);

        /* float direction = Mathf.Sign(touch.position.x - lastTouchPosition.x);

         float rotationAngle = direction * rotationSpeed * Time.deltaTime;

         objectToRotate.transform.GetChild(objectCount).transform.GetChild(0).Rotate(Vector3.up, rotationAngle, Space.World);*/
    }

    private void BoundaryUICheck()
    {
        Vector3 targetV3 = targetVector3;

        targetV3.z = 0;
        targetV3.y = Mathf.Clamp(targetV3.y, -Camera.main.pixelHeight / 10, Camera.main.pixelHeight / 3);
        targetV3.x = Mathf.Clamp(targetV3.x, -Camera.main.pixelWidth / 3, Camera.main.pixelWidth / 3);

        targetVector3 = targetV3;
    }

    private void OnEnable()
    {
        PlayerInputs.Enable();
    }

    private void OnDisable()
    {
        PlayerInputs.Disable();
    }
}
