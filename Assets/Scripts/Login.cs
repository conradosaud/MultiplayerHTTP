using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Login : MonoBehaviour
{

    public Transform prefabJogador;
    TMP_InputField inputNome;

    void Start()
    {
        inputNome = GameObject.Find("Canvas").transform.Find("Login").transform.Find("InputNome").GetComponent<TMP_InputField>();
    }

    public async void Logar()
    {
        string nome = inputNome.text;
        if (nome.Length < 1)
            return;

        //IniciaJogo();
        //return;

        Usuario usuario = await RequestManager.BuscaUsuario(nome);
        
        if( usuario == null)
        {
            usuario = await RequestManager.CriarUsuario(nome);
            if (usuario == null)
            {
                Debug.Log("Algo muito errado deu errado");
                return;
            }

        }

        Debug.Log("Usuario logado: " + usuario.id+usuario.nome+usuario.pontos+usuario.created_at);

        IniciaJogo(usuario);
        MontaPlacar();

    }

    public void IniciaJogo(Usuario usuario)
    {
        GameObject.Find("Canvas").transform.Find("Login").gameObject.SetActive(false);
        Transform jogador = Instantiate(prefabJogador);
        jogador.GetComponent<PlayerController>().id = usuario.id;
        jogador.GetComponent<PlayerController>().pontos = usuario.pontos;
        HUD.AtualizaPontos(usuario.pontos);
        
    }

    public async void MontaPlacar()
    {
        List<Usuario> usuarios = await RequestManager.BuscaTodosUsuarios();
        HUD.AtualizaPacar(usuarios);
    }

}
