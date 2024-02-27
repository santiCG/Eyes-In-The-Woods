using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FpsCamara : MonoBehaviour
{
    public Vector2 sensiblilty;

    private Transform camara;

    private bool walking = false;

    public float speed = 10.0F; // Velocidad de movimiento
    public float rotationSpeed = 100.0F; // Velocidad de rotación

    // Variables para el movimiento de balanceo
    public float bobbingFrequency = 500.0f; // Frecuencia del balanceo
    public float bobbingAmplitude = 1.0f; // Amplitud del balanceo
    private float bobbingTimer = 0f; // Timer para la función sinusoidal

    void Start()
    {
        camara = transform.Find("Camera");

        Cursor.lockState = CursorLockMode.Locked; // Evita que el cursos se salga de la pantalla de juego
    }

    void Update()
    {
        if (Input.GetAxis("Vertical") > 0 || Input.GetAxis("Horizontal") > 0)
        {
            walking = true;
        }
        else
        {
            walking = false;
        }

        transform.Translate(0, 0, Input.GetAxis("Vertical") * speed * Time.deltaTime);
        transform.Translate(Input.GetAxis("Horizontal") * speed * Time.deltaTime, 0, 0);

        float hor = Input.GetAxis("Mouse X");

        if (hor != 0)
        {
            transform.Rotate(Vector3.up * hor * sensiblilty.x);
        }

        float ver = Input.GetAxis("Mouse Y");

        if (ver != 0)
        {
            float angle = (camara.localEulerAngles.x - ver * sensiblilty.y + 360) % 360;

            if (angle > 180)
            {
                angle -= 360;
            }

            angle = Mathf.Clamp(angle, -80, 80);

            camara.localEulerAngles = Vector3.right * angle;
        }

        // Movimiento de balanceo de la cámara
        if(walking)
        {
            bobbingTimer += Time.deltaTime * 4;
            float bobbingOffset = Mathf.Sin(bobbingTimer * bobbingFrequency) * bobbingAmplitude;
            transform.position = new Vector3(transform.position.x, transform.position.y + (bobbingOffset / 50), transform.position.z);
        }
    }
}
