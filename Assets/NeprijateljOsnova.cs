using UnityEngine;
using UnityEngine.AI;

public class NeprijateljOsnova : MonoBehaviour
{


    [SerializeField] float brzina = 2;
    [SerializeField] float dometPogleda = 10;
    [SerializeField] float pregledVrijeme = 1;
    float zadnjiPregled;

    public float zivoti = 1;
    public float napad = 2; 
    // public se koristi da bi druge skripte vidile i minjale varijable na ovoj

    Transform meta;

    NavMeshAgent agent;



    void Start()
    {
        

        agent = GetComponent<NavMeshAgent>();


    }
    void Update()
    {

        if (meta)
        {
            agent.destination = meta.position;
        }
        else if(zadnjiPregled + pregledVrijeme < Time.time) { Pregled(); }
        
    }





    void Pregled()
    {

        Collider[] colls = Physics.OverlapSphere(transform.position, dometPogleda);
        for (int i = 0; i < colls.Length; i++)
        {
            if (colls[i].GetComponent<OsnovnaJedinica>()) 
            { meta = colls[i].transform; i = colls.Length; }
        }

    }



    public void Pogoden(float steta)
    {
        zivoti -= steta;
        if(zivoti <= 0)
        {
            Destroy(gameObject);
        }

    }


    private void OnTriggerEnter(Collider other)
    {

        if(other.gameObject.layer == 7 && !meta)
        {
            meta = other.transform;
            //GetComponent<SphereCollider>().enabled = false;
            if(other.transform.GetComponent<OsnovnaJedinica>() != null)
            {
                other.transform.GetComponent<OsnovnaJedinica>().NovaMeta(transform);
            }

        }

    }



}
