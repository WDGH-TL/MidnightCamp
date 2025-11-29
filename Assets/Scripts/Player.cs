using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    // Variables de Movimiento y Personaje
    public Rigidbody myPlayer;
    public float velocidad = 10f;
    public float runVelocidad = 10f;
    public bool isRunning = false;
    public float currentSpeed;

    public float movementX;
    public float movementY;

    // Variables de Salto y Suelo
    Vector3 velocity;
    public Transform haySuelo;
    public float radioDeSueloListener = 0.3f;
    public LayerMask suelo;
    public bool enElSuelo;

    // Variables de Cámara y Agarre
    public Vector2 sensibilidadMouse;
    public Transform camara;
    public Transform grabbedObject;
    public Transform playerHands;

    public float rayDistance = 5f;
    private Rigidbody grabbedRgby;

    void Start()
    {
        myPlayer = GetComponent<Rigidbody>();
    }

    void Update()
    {
        enElSuelo = Physics.CheckSphere(haySuelo.position, radioDeSueloListener, suelo);

        if (haySuelo && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        movement();
        mouseLook();

        if (Physics.Raycast(camara.position, camara.forward, out RaycastHit hit, rayDistance))
        {
            // Lógica de Raycast para interacción (E)
            if (Input.GetKeyDown(KeyCode.E))
            {
                // Intentar obtener el componente ItemTemplate (Ítem de Inventario)
                ItemTemplate itemTemplate = hit.transform.GetComponent<ItemTemplate>();

                if (itemTemplate != null)
                {
                    // *** 1. RECUPERAR ITEM ***
                    RecogerItem(hit);
                }
                else
                {
                    // *** 2. LÓGICA DE AGARRE (si no es un item de inventario) ***
                    if (!grabbedObject)
                    {
                        if (hit.transform.CompareTag("Item"))
                        {
                            grabTransform(hit);
                        }
                    }
                    else
                    {
                        releaseTransform();
                    }
                }
            }
        }

        Debug.DrawRay(camara.position, camara.forward * rayDistance, Color.cyan);
    }

    // --- NUEVO MÉTODO DE INTERACCIÓN: RECOLECCIÓN DE INVENTARIO ---
    private void RecogerItem(RaycastHit hit)
    {
        ItemTemplate itemTemplate = hit.transform.GetComponent<ItemTemplate>();

        if (itemTemplate != null)
        {
            // Llama a la función del inventario (PlayerInventory.cs)
            if (PlayerInventory.Instance != null)
            {
                PlayerInventory.Instance.AddItemToInventory(hit);
                // La destrucción del GameObject físico del ítem ocurre dentro de AddItemToInventory
                Debug.Log($"Ítem {itemTemplate.itemName()} añadido al inventario.");
            }
            else
            {
                Debug.LogError("PlayerInventory.Instance no está disponible.");
            }
        }
    }


    private void grabTransform(RaycastHit grab)
    {
        if (grab.transform)
        {
            Transform transformToGrab = grab.transform;
            grabbedObject = transformToGrab;
            grabbedObject.SetParent(playerHands);

            grabbedRgby = grabbedObject.GetComponent<Rigidbody>();
            Destroy(grabbedRgby); // Destruye el Rigidbody original

            grabbedObject.localPosition = Vector3.zero;

            // Nota: Aquí se intentaba acceder a un Rigidbody que acabas de destruir.
            // Para mantener el código lo más parecido al original, lo dejaré comentado.
            // La línea original causaría un error si no se maneja la referencia 'grabbedRgby' correctamente.
            // grabbedObject.GetComponent<Rigidbody>().isKinematic = true; 

            // Destrucción repetida del Rigidbody, lo mantengo para preservar la estructura original.
            // Destroy(grabbedRgby); 
        }

    }

    private void releaseTransform()
    {
        Rigidbody newRgby = grabbedObject.AddComponent<Rigidbody>();
        // Ya que el Rigidbody anterior fue destruido, esta línea debe apuntar al nuevo:
        newRgby.isKinematic = false;

        grabbedObject.SetParent(null);
        grabbedObject = null;
        // newRgby = null; // Asignación innecesaria a variable local
    }


    void movement()
    {
        movementX = Input.GetAxisRaw("Horizontal");
        movementY = Input.GetAxisRaw("Vertical");

        Vector3 movimiento = Vector3.zero;

        currentSpeed = isRunning ? runVelocidad : velocidad;

        if (movementX != 0 || movementY != 0)
        {
            Vector3 direccion = (transform.forward * movementY + transform.right * movementX).normalized;
            movimiento = direccion * currentSpeed;
        }

        movimiento.y = myPlayer.linearVelocity.y;

        myPlayer.linearVelocity = movimiento;
    }

    void mouseLook()
    {
        float moveX = Input.GetAxis("Mouse X");
        float moveY = Input.GetAxis("Mouse Y");

        if (moveX != 0)
        {
            transform.Rotate(0, moveX * sensibilidadMouse.x, 0);
        }

        if (moveY != 0)
        {
            camara.Rotate(-moveY * sensibilidadMouse.y, 0, 0);
        }
    }
}