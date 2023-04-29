using System.Collections;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    int currentGrade;
    Coroutine currentPosCoroutine, currentRotCoroutine;
    [SerializeField] Vector3 Offset;

    float rotationSpeed = 30.0f;
    bool isRotating;
    bool isMoving;

    void Start() { currentGrade = 7; }
    void Update()
    {

        if ((Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)) && JengaBuilder.Instance.IsThereTower(currentGrade + 1))
        { 
            if (currentPosCoroutine != null) StopCoroutine(currentPosCoroutine); 
            currentPosCoroutine = StartCoroutine(MoveToTower(JengaBuilder.Instance.GetTowerPos(++currentGrade)));
            if (currentRotCoroutine == null) currentRotCoroutine = StartCoroutine(ResetRotation());
        }
        else if ((Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)) && JengaBuilder.Instance.IsThereTower(currentGrade - 1))
        { 
            if (currentPosCoroutine != null) StopCoroutine(currentPosCoroutine);
            currentPosCoroutine = StartCoroutine(MoveToTower(JengaBuilder.Instance.GetTowerPos(--currentGrade)));
            if (currentRotCoroutine == null) currentRotCoroutine = StartCoroutine(ResetRotation());
        }

        if (!isMoving && Input.GetMouseButtonDown(0)) isRotating = true;
        if (Input.GetMouseButtonUp(0)) isRotating = false;

        if (isRotating) RotateAroundTower();
    }

    IEnumerator MoveToTower(Vector3 targetPos)
    {
        isMoving = true;
        while (Vector3.Distance(targetPos + Offset, transform.position) > .1f)
        {
            transform.position = Vector3.Lerp(transform.position, targetPos + Offset, 2 * Time.deltaTime);
            yield return null;
        }
        isMoving = false;
    }

    IEnumerator ResetRotation()
    {
        Vector3 targetRot = transform.rotation.y >= 0 ? new Vector3(25f, 0f, 0f) : new Vector3(25f, 360f, 0f);
        while (Vector3.Distance(targetRot, transform.eulerAngles) > .1f)
        {
            transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, targetRot, 4 * Time.deltaTime);
            yield return null;
        }
        transform.eulerAngles = new Vector3(25f, 0f, 0f);
        currentRotCoroutine = null;
    }
    void RotateAroundTower()
    {
        if (isMoving) return;
        Vector3 direction = IsMouseLeftOfScreen() ? Vector3.up : Vector3.down;
        transform.RotateAround(JengaBuilder.Instance.GetTowerPos(currentGrade), direction, rotationSpeed * Time.deltaTime);
    }

    bool IsMouseLeftOfScreen()
    {
        Vector3 mousePosition = Input.mousePosition;

        if (mousePosition.x < Screen.width / 2f) return true;
        return false;
    }
}
