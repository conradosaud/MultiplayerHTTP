using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class HUD : MonoBehaviour
{

    static TextMeshProUGUI pontuacao;

    private void Start()
    {
        pontuacao = GameObject.Find("Canvas").transform.Find("HUD").transform.Find("ValorPontos").GetComponent<TextMeshProUGUI>();
    }

    public static void AtualizaPontos(int pontos)
    {
        pontuacao.text = pontos.ToString();
    }

    public static void AtualizaPacar(List<Usuario> usuarios)
    {

        usuarios = usuarios.OrderBy(u => u.pontos).ToList();
        usuarios.Reverse();
        Transform placar = GameObject.Find("Canvas").transform.Find("HUD").transform.Find("Placar").transform;

        foreach( Usuario usuario in usuarios)
        {
            if (usuario.pontos == 0)
                continue;
            GameObject texto = new GameObject();
            texto.AddComponent<TextMeshProUGUI>();
            texto.GetComponent<TextMeshProUGUI>().text = "<br>"+usuario.nome + " - " + usuario.pontos;
            texto.GetComponent<TextMeshProUGUI>().fontSize = 24;
            Instantiate(texto, placar);
        }
    }

}
