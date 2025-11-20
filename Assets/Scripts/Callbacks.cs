using UnityEngine;

public class Callbacks : MonoBehaviour
{
    public Vector3 direction; // se puede configurar desde el Inspector

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("=== LLAMADA Para una vez === ");
        //transform.position = new Vector3(x:5, y:0, z:0);
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("=== LLAMADA POR FRAME === ");
        //transform.position += direction * Time.deltaTime;
        /*
        if (Input.GetKey(KeyCode.Space))
        {
            transform.position += direction * Time.deltaTime;
        }
        */

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        //float salto = Input.GetAxis("Jump");

        //transform.position += new Vector3(horizontal, 0, vertical) * Time.deltaTime;
        //transform.Translate(new Vector3(horizontal, 0, vertical) * Time.deltaTime * 5);

        // Usamos el campo público "direction" para almacenar la dirección del input
        direction = new Vector3(horizontal, 0f, vertical);

        // Movemos al objeto en esa dirección con velocidad 5
        transform.Translate(direction * Time.deltaTime * 5);

        //transform.Rotate(0, 0, 0);
        if (Input.GetKey(KeyCode.Space))
        {
            transform.Rotate(0, 5, 0);
        }
    }
}
