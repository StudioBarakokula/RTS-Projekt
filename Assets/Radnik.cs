using UnityEngine;

public class Radnik : OsnovnaJedinica
{





    override protected void Trazenje()
    {
        Collider[] colls = Physics.OverlapSphere(transform.position, domet);
        for (int i = 0; i < colls.Length; i++)
        {
            if (colls[i].gameObject.layer == 8 ||colls[i].gameObject.layer == 9)
            {
                meta = colls[i].transform; i = colls.Length;
            }
        }
    }



}
