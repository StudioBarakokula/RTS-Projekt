using UnityEngine;

public class Kamera : MonoBehaviour
{


    // Serialized field daje moguænost namistanja vrijednosti u unityu umisto samo kodu
    [SerializeField] float brzinaKamere = 5;





    void Start()
    {
        
    }
    void Update()
    {

        // Input daje opcije za unose priko misa ili tipkovnice
        // GetAxis je specifican i daje i strelice i wasd po osi
        // Vector3.right je desni smjer cilog svita
        // transform uzima objekt na kojem je skripta i position je pozicija tog objekta
        // Vec3.right daje smjer, brzina daje brzinu, Time.deltaTime stavlja da nije ovisno o FPS-u

        if (Input.GetAxisRaw("Horizontal") < 0) 
        { transform.position += Vector3.right * brzinaKamere * Time.deltaTime; }
        else if (Input.GetAxisRaw("Horizontal") > 0) 
        { transform.position -= Vector3.right * brzinaKamere * Time.deltaTime; }

        else if (Input.GetAxisRaw("Vertical") < 0) 
        { transform.position += Vector3.forward * brzinaKamere * Time.deltaTime; }
        else if (Input.GetAxisRaw("Vertical") > 0) 
        { transform.position -= Vector3.forward * brzinaKamere * Time.deltaTime; }


    }


}
