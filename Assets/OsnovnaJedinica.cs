using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI; // za navmesh

public class OsnovnaJedinica : MonoBehaviour
{

    // navmeshagent se koristi za micanje stvari po navmeshu
    protected NavMeshAgent agent;

    public float zivoti = 3;
    public float napad = 2;

    public float brzina = 2;
    public float domet = 4;
    public float trazenjePauza = 0.5f;
    protected float trazenjeZadnje;

    public float maksMunicija = 20;
    protected float municija;
    public float brzinaPucanjaPoSekundi = 5;
    protected float zadnjePucanje;
    public float promjenaMunicije = 2;
    public float sansaZaPogodak = 90;

    protected Transform meta;

    public float nedodirljivTrajanje = 0.3f;
    protected bool nedodirljiv;

    public ParticleSystem efektPucanja;




    void Start()
    {
        
        // getcomponent uzima komponentu ako postoji na tom objektu
        // moze i sa drugih objekata, ali vako bez icega isprid uzima sa objekta na kojem je skripta
        agent = GetComponent<NavMeshAgent>();
        
        // ako ode koristimo transform.position onda ce ic direkrno ravno, vako ide tamo di bude definirano
        // i posto je brzina stavljena na varijablu koju smo definiralni, ici ce ton brzinon
        agent.speed = brzina;

        municija = maksMunicija;

    }
    void Update()
    {

        if (meta && Time.time > zadnjePucanje + 1 / brzinaPucanjaPoSekundi
            && Vector3.Distance(transform.position, meta.position) < domet)
        {
            Pucanje();
        }
        else if (trazenjeZadnje + trazenjePauza < Time.time)
        {
            Trazenje();
        }

    }



    protected virtual void Pucanje()
    {

        // ako ima metaka moze pucat
        if (municija > 0)
        {
            efektPucanja.transform.position = transform.position;
            efektPucanja.Play();
            // stvaramo rendom broj i gledamo jeli unutar postotka za pogodit
            if (Random.Range(0, 101) < sansaZaPogodak)
            {
                meta.GetComponent<NeprijateljOsnova>().Pogoden(napad);
            }

            zadnjePucanje = Time.time;
            municija--;

            if (meta == null)
            {
                Trazenje();
            }

        }
        else
        {
            // ako nema metaka ne moze pucat x sekundi tj trenutacno vrime plus x
            zadnjePucanje = Time.time + promjenaMunicije;
            municija = maksMunicija;
        }

    }




    private void OnCollisionEnter(Collision collision)
    {

        if(collision.gameObject.layer == 8 && !nedodirljiv)
        {
            StartCoroutine(Pogoden(collision.transform.GetComponent<NeprijateljOsnova>().napad));
        }

    }
    private void OnCollisionStay(Collision collision)
    {

        if (collision.gameObject.layer == 8 && !nedodirljiv)
        {
            StartCoroutine(Pogoden(collision.transform.GetComponent<NeprijateljOsnova>().napad));
        }

    }


    // IEnumerator tj korutina je ki obicna funkcija al mozes stavit ovaj waitforseconds
    protected IEnumerator Pogoden(float steta)
    {

        if (!nedodirljiv)
        {
            zivoti -= steta;

            if (zivoti <= 0) { Destroy(this.gameObject); }

            nedodirljiv = true;
            yield return new WaitForSeconds(nedodirljivTrajanje);
            nedodirljiv = false;

        }

    }



    protected void Trazenje()
    {
        Collider[] colls = Physics.OverlapSphere(transform.position, domet);
        for (int i = 0; i < colls.Length; i++)
        {
            if (colls[i].gameObject.layer == 8)
            {
                meta = colls[i].transform; i = colls.Length;
            }
        }
    }


    public void NovaMeta(Transform neprijatelj)
    {
        meta = neprijatelj;
    }


}
