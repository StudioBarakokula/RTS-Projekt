using UnityEngine;

public class Kamera : MonoBehaviour
{


    // Serialized field daje moguænost namistanja vrijednosti u unityu umisto samo kodu
    [SerializeField] float brzinaKamere = 9;
    [SerializeField] float scrollMul = 9;


    public Material fogOfWarMaterial; // The material with the shader
    public Transform roditeljJedinica; // The units whose positions will be used for the fog of war





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
        if (Input.GetAxisRaw("Horizontal") > 0) 
        { transform.position -= Vector3.right * brzinaKamere * Time.deltaTime; }

        if (Input.GetAxisRaw("Vertical") < 0) 
        { transform.position += Vector3.forward * brzinaKamere * Time.deltaTime; }
        if (Input.GetAxisRaw("Vertical") > 0) 
        { transform.position -= Vector3.forward * brzinaKamere * Time.deltaTime; }


        if (Input.GetAxisRaw("Mouse ScrollWheel") < 0)
        {
            transform.position += Vector3.up * brzinaKamere * scrollMul * Time.deltaTime;
        }
        else if (Input.GetAxisRaw("Mouse ScrollWheel") > 0)
        {
            transform.position -= Vector3.up * brzinaKamere * scrollMul * Time.deltaTime;
        }
    



        // shader
        Vector4[] points = new Vector4[512]; 
        for (int i = 0; i < roditeljJedinica.childCount; i++)
        {
            points[i] = roditeljJedinica.GetChild(i).position; // Set the world position of each unit
        }

        // Set the points and number of points on the shader
        fogOfWarMaterial.SetVectorArray("_Points", points);
        fogOfWarMaterial.SetFloat("_PointsNum", Mathf.Min(32, roditeljJedinica.childCount));

    }


}
