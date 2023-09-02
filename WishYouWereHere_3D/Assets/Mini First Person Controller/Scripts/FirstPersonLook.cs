﻿using UnityEngine;

public class FirstPersonLook : MonoBehaviour
{
    [SerializeField]
    Transform character;
    public float sensitivity = 2;
    public float smoothing = 1.5f;

    Vector2 velocity;
    Vector2 frameVelocity;
    public bool isChoiceOn;

    void Reset()
    {
        // Get the character from the FirstPersonMovement in parents.
        character = GetComponentInParent<FirstPersonMovement>().transform;
    }

    void Start()
    {
    }

    void Update()
    {
        // Get smooth velocity.
        Vector2 mouseDelta = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        Vector2 rawFrameVelocity = Vector2.Scale(mouseDelta, Vector2.one * sensitivity);
        frameVelocity = Vector2.Lerp(frameVelocity, rawFrameVelocity, 1 / smoothing);
        velocity += frameVelocity;
        velocity.y = Mathf.Clamp(velocity.y, -90, 90);

        // Rotate camera up-down and controller left-right from velocity.
        transform.localRotation = Quaternion.AngleAxis(-velocity.y, Vector3.right);
        character.localRotation = Quaternion.AngleAxis(velocity.x, Vector3.up);
    }

    private void OnEnable()
    {
        float y = character.localRotation.eulerAngles.y;
        float x = transform.localRotation.eulerAngles.x;

        if(y > 180f)
        {
            y -= 360f;
        }
        else if (y < -180f)
        {
            y += 360f;
        }

        if (x > 180f)
        {
            x -= 360f;
        }
        else if(x < -180f)
        {
            x += 360f;
        }


        Debug.Log($"OnEnable {y}, {-x}");
        velocity = new Vector2(y, -x);
    }

    private void OnDisable()
    {
        velocity = Vector2.zero;
        frameVelocity = Vector2.zero;
    }
}
