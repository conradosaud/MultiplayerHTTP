using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public int id;
    public float velocidade = 5f;
    public int pontos = 0;

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 movimento = new Vector3(horizontal, 0, vertical);
        movimento *= velocidade * Time.deltaTime;

        transform.Translate(movimento);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coletavel"))
        {
            Destroy(other.gameObject);
            pontos++;
            RequestManager.AlterarPontos(id, pontos);
            HUD.AtualizaPontos(pontos);
        }
    }

}
