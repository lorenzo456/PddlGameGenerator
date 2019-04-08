using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    Renderer renderer;
    bool blowUp;
    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<Renderer>();
        StartCoroutine(StartBomb());
    }

    IEnumerator StartBomb()
    {

        yield return new WaitForSeconds(2);
        renderer.material.color = Color.red;
        blowUp = true;
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }

    private void Update()
    {
        if (blowUp)
        {
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, 3);
            int i = 0;
            while (i < hitColliders.Length)
            {
                if (hitColliders[i].gameObject.CompareTag("Interactable"))
                {
                    hitColliders[i].gameObject.GetComponent<IKillable>().Damaged(10);
                    Destroy(gameObject);
                    return;
                }
                i++;
            }
        }
    }

}
