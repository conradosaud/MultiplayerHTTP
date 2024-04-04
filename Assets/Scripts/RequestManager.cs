using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;

public class RequestManager : MonoBehaviour
{

    private static string apiUrl = "https://tyltpqzxwdtxkojuyevf.supabase.co/rest/v1/";
    private static string apiKey = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6InR5bHRwcXp4d2R0eGtvanV5ZXZmIiwicm9sZSI6ImFub24iLCJpYXQiOjE3MTIxODYyODksImV4cCI6MjAyNzc2MjI4OX0.Pz8t9va3-j2tWAJCofGNX72cVgQjKQ9r-EetJAVp3Ik";

    public static async Task<Usuario> BuscaUsuario(string nome)
    {

        string url = $"{apiUrl}usuarios?nome=eq.{nome}&apikey={apiKey}";

        UnityWebRequest request = UnityWebRequest.Get(url);
        await request.SendWebRequest();

        string resposta = request.downloadHandler.text;

        if (resposta == "[]")
            return null;

        List<Usuario> usuarios = JsonConvert.DeserializeObject<List<Usuario>>(resposta);

        return usuarios[0];

    }

    public static async Task<List<Usuario>> BuscaTodosUsuarios()
    {

        string url = $"{apiUrl}usuarios?apikey={apiKey}";

        UnityWebRequest request = UnityWebRequest.Get(url);
        await request.SendWebRequest();

        string resposta = request.downloadHandler.text;

        if (resposta == "[]")
            return null;

        List<Usuario> usuarios = JsonConvert.DeserializeObject<List<Usuario>>(resposta);
        return usuarios;

    }

    public static async Task<Usuario> CriarUsuario(string nome)
    {


        string json = $" \"nome\": \"{nome}\", \"pontos\": 0 ";
        json = "{" + json + "}";

        string url = $"{apiUrl}usuarios?apikey={apiKey}";
        UnityWebRequest request = UnityWebRequest.Post(url, json, "application/json");
        await request.SendWebRequest();

        // Depois de criar é só buscar pelo novo nome
        return await BuscaUsuario(nome);

    }

    public static async void AlterarPontos(int id, int pontos)
    {

        string json = "{ \"pontos\": "+pontos+" }";

        string url = $"{apiUrl}usuarios?id=eq.{id}&apikey={apiKey}";

        UnityWebRequest request = UnityWebRequest.Put(url, json);
        request.method = "PATCH";
        request.SetRequestHeader("Content-Type", "application/json");

        await request.SendWebRequest();

        Debug.Log("Pontos alterados!");

    }

}
