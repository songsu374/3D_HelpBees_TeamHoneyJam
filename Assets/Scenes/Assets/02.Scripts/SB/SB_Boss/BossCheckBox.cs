using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCheckBox : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GetComponent<BoxCollider>().isTrigger = true;
            Debug.Log("BossBoxCheck");
            StartCoroutine("BossStartCheck");



        }
    }

    IEnumerator BossStartCheck()
    {
        yield return new WaitForSeconds(1f);
        Player.instance.transform.position = new Vector3(85.64f, 36.3f, 278.11f);

        yield return new WaitForSeconds(1.5f);
        MonsterManager.instance.BossStart();

    }

}
