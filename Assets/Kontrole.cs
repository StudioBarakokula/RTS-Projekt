using UnityEngine;
using UnityEngine.AI;

public class Kontrole : MonoBehaviour
{


    [SerializeField] LayerMask naseJedinice;
    [SerializeField] LayerMask neprijateljJedinice;


    Vector3 pocetnaOdabir;
    Vector3 zadnjaOdabir;

    Collider[] odabraneJedinice;

    [SerializeField] Transform odabirnaKutija;

    [SerializeField] Material obicniMaterijal;
    [SerializeField] Material odabraniMaterijal;


    bool odabiranje = true;

    [SerializeField] Transform zgrada;





    void Start()
    {
        
        odabraneJedinice = new Collider[0];

    }
    void Update()
    {

        // livi klik, odabiranje jedinica
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {

            if (odabiranje)
            {
                // raycast je samo linija koja se ispaljiva 
                // raycasthit drzi informacije za sta je ta linija pogodila
                RaycastHit hit;
                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
                {
                    pocetnaOdabir = hit.point;
                }


                if (odabraneJedinice.Length > 0)
                {
                    for (int i = 0; i < odabraneJedinice.Length; i++)
                    {
                        if (odabraneJedinice[i] && odabraneJedinice[i].gameObject.layer == 7)
                        {
                            odabraneJedinice[i].GetComponent<Renderer>().material = obicniMaterijal;
                        }
                    }
                }


                // da ne bi ostale jedinice od prosli put
                odabraneJedinice = null;
            }
            else
            {
                RaycastHit hit;
                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
                {
                    Instantiate(zgrada, hit.point, Quaternion.Euler(Vector3.zero));
                }
                odabiranje = true;
            }



        }
        // kad drzis minja se sta se bira
        else if (Input.GetKey(KeyCode.Mouse0))
        {

            // konstantno se azurira koga biramo
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
            {
                zadnjaOdabir = hit.point;
                OdabirJedinica();
            }

        }
        // i kad se digne klik onda se odabire
        else if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            KrajOdabira();
        }
        // desni klik, stavljanje dice ic
        else if (Input.GetKey(KeyCode.Mouse1) && odabraneJedinice.Length > 0)
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
            {
                for (int i = 0; i < odabraneJedinice.Length; i++)
                {
                    // prolazimo kroz sve jedinice i stavljamo destination tj dice ic
                    if (odabraneJedinice[i].gameObject.layer == 7 && 
                        odabraneJedinice[i].gameObject.activeInHierarchy)
                    {
                        if(hit.transform.gameObject.layer == 8)
                        {
                            odabraneJedinice[i].GetComponent<OsnovnaJedinica>().NovaMeta(hit.transform);

                            // stavi metu i ide prema neprijatelju ali taman na kraj dometa
                            odabraneJedinice[i].GetComponent<NavMeshAgent>().destination = 
                                hit.point + (odabraneJedinice[i].transform.position - hit.point) *
                                odabraneJedinice[i].GetComponent<OsnovnaJedinica>().domet;
                        }
                        // radnik i rude
                        else if(hit.transform.gameObject.layer == 9 && 
                            odabraneJedinice[i].GetComponent<Radnik>())
                        {
                            odabraneJedinice[i].GetComponent<Radnik>().NovaMeta(hit.transform);
                            odabraneJedinice[i].GetComponent<NavMeshAgent>().destination = hit.point;
                        }
                        else
                        {
                            odabraneJedinice[i].GetComponent<NavMeshAgent>().destination = hit.point;
                        }

                    }

                }

            }
            
        }
        // broj 1 
        else if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            odabiranje = false;
        }

    }





    void OdabirJedinica()
    {

        odabirnaKutija.position = (pocetnaOdabir + zadnjaOdabir) / 2;
        // localScale je za skaliranje velicine
        odabirnaKutija.localScale = pocetnaOdabir - zadnjaOdabir + Vector3.up * 0.1f;


        // setactive je za gasenje/paljenje objekata
        odabirnaKutija.gameObject.SetActive(true);

    }

    void KrajOdabira()
    {

        odabraneJedinice = Physics.OverlapBox((pocetnaOdabir + zadnjaOdabir) / 2,
             Absoluter(pocetnaOdabir - zadnjaOdabir) / 2 + Vector3.up * 20f, Quaternion.Euler(Vector3.zero));



        odabirnaKutija.gameObject.SetActive(false);


        for (int i = 0; i < odabraneJedinice.Length; i++)
        {
            if (odabraneJedinice[i].gameObject.layer == 7)
            {
                odabraneJedinice[i].GetComponent<Renderer>().material = odabraniMaterijal;
            }

        }

    }






    Vector3 Absoluter(Vector3 vec)
    {
        return new Vector3(Mathf.Abs(vec.x), Mathf.Abs(vec.y), Mathf.Abs(vec.z));
    }


}
