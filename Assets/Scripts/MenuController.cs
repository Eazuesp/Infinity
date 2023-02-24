using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public AudioClip audioClip;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        PlaySound();
        SceneManager.LoadScene("Level001");
    }

    public void MainMenu()
    {
        PlaySound();
        SceneManager.LoadScene("MainMenu");
    }

    public void PlaySound()
    {
        if (audioClip)
        {            
            AudioSource.PlayClipAtPoint(audioClip, player.transform.position);
        }
        
    }
}
