using System;
using System.Collections;
using System.Collections.Generic;
using PathCreation;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Controlador_humano : MonoBehaviour
{
    [SerializeField] private GameObject[] coches;

    private Seguir_camino seguidor;

    [SerializeField] private GameObject ui;
    [SerializeField] private GameObject ui_pausa;
    [SerializeField] private GameObject ui_final;

    private Controlador_pistas controladorPistas;
    private Controlador_coches controladorCoches;

    private Controlador_escena_selector escenaSelector;
    private CountdownControl countdownControl;
    private GameControl control;

    [SerializeField] private Image imagen_pos;
    [SerializeField] private Image imagen_pos_final;
    [SerializeField] private Image imagen_vueltas;
    [SerializeField] private Image imagen_max_vueltas;
    [SerializeField] private Image imagen_contador;

    [SerializeField] private AudioSource audio_ganar;
    [SerializeField] private AudioSource audio_perder;

    [SerializeField] private AudioSource audio_contar;

    public GameObject boton_estabilidad;

    // Start is called before the first frame update
    void Start()
    {
        escenaSelector = FindObjectOfType<Controlador_escena_selector>();
        countdownControl = FindObjectOfType<CountdownControl>();
        control = FindObjectOfType<GameControl>();
        controladorPistas = FindObjectOfType<Controlador_pistas>();
        controladorCoches = FindObjectOfType<Controlador_coches>();
        seguidor = GetComponent<Seguir_camino>();
        escenaSelector.cuando_inicia_carrera += asignar_path;
        escenaSelector.cuando_inicia_carrera += asignar_path;
        countdownControl.cuando_llegue_a_zero += quitar_cartel;
        ui.SetActive(false);
        ui_pausa.SetActive(false);
        ui_final.SetActive(false);

        imagen_vueltas.sprite = control.lapsNums[seguidor.laps];
        imagen_pos.sprite = control.lapsNums[seguidor.pos];

        imagen_max_vueltas.sprite = control.lapsNums[control.maxLaps];
    }

    void quitar_cartel()
    {
        StartCoroutine(quitar_cartel_start(1.5f));
    }

    private void Update()
    {
        imagen_contador.sprite = countdownControl.numSprites[countdownControl.currNumber];
        imagen_pos.sprite = control.lapsNums[seguidor.pos];
        imagen_vueltas.sprite = control.lapsNums[seguidor.laps];
        
        boton_estabilidad.SetActive(!seguidor.estable);

        if (GameControl.instance.gameState == GameState.Race)
        {
            ui_final.SetActive(false);
            ui.SetActive(true);
        }
        
        if (GameControl.instance.gameState == GameState.Countdown)
        {
            audio_contar.enabled = true;
        }
        
        if (GameControl.instance.gameState == GameState.EndRace)
        {
            imagen_pos_final.sprite = control.lapsNums[seguidor.pos];
            ui.SetActive(false);
            ui_pausa.SetActive(false);
            ui_final.SetActive(true);
            if (seguidor.pos==1)
            {
                audio_ganar.enabled = true;
            }
            else
            {
                audio_perder.enabled = true;
            }
        }
    }

    // Update is called once per frame
    public void asignar_path()
    {
        seguidor.pathCreator = controladorPistas.pistas[controladorPistas.pista_seleccionada]
            .GetComponentInChildren<PathCreator>();
        coches[controladorCoches.coche_seleccionado].SetActive(true);
        ui.SetActive(true);
    }

    IEnumerator quitar_cartel_start(float segundos)
    {
        yield return new WaitForSeconds(segundos);
        imagen_contador.enabled = false;
        audio_contar.enabled = false;
    }

    private GameState prev_gamestate;

    public void pausar_game(bool pausar)
    {
        ui_pausa.SetActive(pausar);
        if (pausar)
        {
            prev_gamestate = GameControl.instance.gameState;
            GameControl.instance.UpdateGameState(GameState.Paused);
        }
        else
        {
            GameControl.instance.UpdateGameState(prev_gamestate);
        }
    }

    public void volver_menu()
    {
        SceneManager.LoadScene("MenuUI", LoadSceneMode.Single);
    }
}