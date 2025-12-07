using UnityEngine;

public class Strategijsko : MonoBehaviour
{


    public Transform kutija;
    public Transform kamera;


    [Header("Ostali")]

    public float aiDometGledanja = 4;

    public Transform roditelj;

    public Transform neprijatelj;
    public Transform vanzemaljciCentar;
    public Transform neutral;

    public int neprijateljMaks;
    public int neutralMaks;

    public int noviVanzemaljciSansaDo100;

    public float maksJacina;
    public float minJacina;



    [Header("Generalno")]

    public int duljina;
    public int visina;

    public float kretanjeIgracUdaljenostDoKocke = 2;

    public float brzinaKamere;





    void Start()
    {

        for (int i = 0; i < duljina; i++) 
        {

            for(int j = 0; j < visina; j++)
            {
                Instantiate(kutija, new Vector3(i - duljina/2, 0, j - visina/2), transform.rotation);
            }
            
        }


        for (int i = 0; i < neprijateljMaks; i++)
        {
            Stvaranje(neprijatelj);
        }

        for (int i = 0; i < neutralMaks; i++)
        {
            Stvaranje(neutral);
        }



        float dulj = Random.Range(-duljina, duljina);
        float vis = Random.Range(-visina, visina);

        while(Vector3.Distance(Vector3.zero, new Vector3(dulj, 0, vis)) < 
            Vector3.Distance(Vector3.zero, new Vector3(duljina, 0, visina)) * 4 / 5)
        {
            dulj = Random.Range(-duljina, duljina);
            vis = Random.Range(-visina, visina);
        }

        Instantiate(vanzemaljciCentar, new Vector3(dulj, 0, vis), transform.rotation);


    }
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
            {
                if (hit.transform.gameObject.layer == 19)
                {
                    Kretanje(hit.collider.transform.position + Vector3.up * 2);
                }

            }

        }


        if (Input.GetAxisRaw("Horizontal") < 0)
        { kamera.position += Vector3.right * brzinaKamere * Time.deltaTime; }
        if (Input.GetAxisRaw("Horizontal") > 0)
        { kamera.position -= Vector3.right * brzinaKamere * Time.deltaTime; }

        if (Input.GetAxisRaw("Vertical") < 0)
        { kamera.position += Vector3.forward * brzinaKamere * Time.deltaTime; }
        if (Input.GetAxisRaw("Vertical") > 0)
        { kamera.position -= Vector3.forward * brzinaKamere * Time.deltaTime; }


    }




    void Kretanje(Vector3 novaPozicija)
    {
        Vector3 trenutacno = FindAnyObjectByType<OsnovnaJedinica>().transform.position;

        if(Vector3.Distance(trenutacno, novaPozicija) < kretanjeIgracUdaljenostDoKocke)
        {
            FindAnyObjectByType<OsnovnaJedinica>().transform.position = novaPozicija;
            KretanjeOstalih();
        }

        

    }



    void KretanjeOstalih()
    {

        for (int i = 0; i < roditelj.childCount; i++)
        {

            if (roditelj.GetChild(i).gameObject.layer == 8 || roditelj.GetChild(i).gameObject.layer == 16)
            {

                Collider[] colls = Physics.OverlapSphere(roditelj.GetChild(i).position, aiDometGledanja);

                for (int y = 0; y < colls.Length; y++)
                {

                    if(roditelj.GetChild(i).gameObject.layer == 8 && colls[y].gameObject.layer == 16)
                    {
                        // uvik napada
                        MicanjeOstalih(roditelj.GetChild(i).transform, colls[y].transform.position);
                        y = colls.Length;
                        Debug.Log("vanz");
                    }
                    else if (roditelj.GetChild(i).gameObject.layer == 16 &&
                        colls[y].gameObject.layer == 8)
                    {

                        if (roditelj.GetChild(i).transform.localScale.x >= colls[y].transform.localScale.x)
                        {
                            MicanjeOstalih(roditelj.GetChild(i).transform, colls[y].transform.position);
                            y = colls.Length;
                            Debug.Log("neu jaci");
                        }
                        else
                        {
                            MicanjeOstalih(roditelj.GetChild(i).transform, -(colls[y].transform.position -
                            roditelj.GetChild(i).transform.position) + 
                            roditelj.GetChild(i).transform.position); y = colls.Length;
                            Debug.Log("neu slab");
                        }
                    }
                    else
                    {
                        Debug.Log("rend");
                        Debug.Log(roditelj.GetChild(i).gameObject.layer);

                        // -1.5  do -0.5 je -1, -0.5 do 0.5 je 0
                        MicanjeOstalih(roditelj.GetChild(i).transform, (Vector3.right * 
                            Mathf.Round(Random.Range(-1.49f, 1.49f)) + Vector3.forward * 
                            Random.Range(-1.49f, 1.49f)) + roditelj.GetChild(i).transform.position);
                        y = colls.Length;
                    }

                }

            }
            else if(roditelj.GetChild(i).transform == vanzemaljciCentar)
            {
                if(Random.Range(0, 100) < noviVanzemaljciSansaDo100)
                {
                    Stvaranje(neprijatelj);
                }

            }

        }

    }

    









    void Stvaranje(Transform vrsta)
    {

        Transform p = Instantiate(vrsta, Random.Range(-duljina / 2, duljina / 2) * Vector3.right +
                Random.Range(-visina / 2, visina / 2) * Vector3.forward, transform.rotation, roditelj);
        p.localScale = Random.Range(minJacina, maksJacina) * Vector3.one;

    }


    void MicanjeOstalih(Transform ostali, Vector3 meta)
    {


        if (ostali.position.x < duljina / 2 - 1 && ostali.position.x > -duljina / 2 +  1)
        {
            //unutar  okvira
            if (ostali.position.x > meta.x)
            {
                ostali.position += Vector3.right;
            }
            else if (ostali.position.x < meta.x)
            {
                ostali.position -= Vector3.right;
            }
        }
        else
        {
            // izvan
            if (ostali.position.x >= duljina / 2 - 1)
            {
                ostali.position -= Vector3.right;
            }
            else if (ostali.position.x <= -duljina / 2 + 1)
            {
                ostali.position += Vector3.right;
            }
        }

        if (ostali.position.z < visina / 2 - 1 && ostali.position.z > -visina / 2 +  1)
        {
            if (ostali.position.z > meta.z)
            {
                ostali.position += Vector3.forward;
            }
            else if (ostali.position.z < meta.z)
            {
                ostali.position -= Vector3.forward;
            }
        }
        else
        {
            // izvan
            if (ostali.position.z >= visina / 2 - 1)
            {
                ostali.position -= Vector3.forward;
            }
            else if (ostali.position.z <= -visina / 2 + 1)
            {
                ostali.position += Vector3.forward;
            }
        }



    }


}
