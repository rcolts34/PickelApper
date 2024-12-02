using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseAim : MonoBehaviour
{

    [SerializeField] private float _rotateSpeed;
    [SerializeField] GameObject _aimCursor;
    private void AimMouse()
    {
        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, _rotateSpeed * Time.deltaTime);
        Vector2 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _aimCursor.transform.position = cursorPos;
    }

    private void Start()
    {
        Cursor.visible = true;
    }

}
