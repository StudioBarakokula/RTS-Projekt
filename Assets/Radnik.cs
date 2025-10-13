using UnityEngine;

public class Radnik : OsnovnaJedinica
{




    void Start()
    {
        
    }

    void Update()
    {
        
    }





    protected override void Pucanje()
    {

        // ako ima metaka moze pucat
        if (municija > 0)
        {
            efektPucanja.transform.position = transform.position;
            efektPucanja.Play();
            // stvaramo rendom broj i gledamo jeli unutar postotka za pogodit
            if (Random.Range(0, 101) < sansaZaPogodak)
            {
                if (meta.GetComponent<NeprijateljOsnova>())
                {
                    meta.GetComponent<NeprijateljOsnova>().Pogoden(napad);
                }
                else if(meta.gameObject.layer == 9) { Debug.Log("rudarimo"); }
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




}
