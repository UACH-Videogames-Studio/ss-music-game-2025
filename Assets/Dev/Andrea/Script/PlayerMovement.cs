using UnityEngine;

using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f; // Velocidad de movimiento

    void Update()
    {
        // Obtener entrada horizontal (flechas o A/D)
        float moveInput = Input.GetAxisRaw("Horizontal");

        // Calcular el movimiento
        Vector3 movement = new Vector3(moveInput * speed * Time.deltaTime, 0f, 0f);

        // Aplicar el movimiento
        transform.position += movement;
    }
}