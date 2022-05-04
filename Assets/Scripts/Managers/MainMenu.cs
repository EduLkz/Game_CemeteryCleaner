using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    [Header("Audio")]
    public AudioMixer mixer;

    float volumeMusica;
    float volumeFX;

    int contagemInicialSegundos;
    int tempoPartidaMin;
    int tempoPartidaSec;

    [Header("UI")]
    public RectTransform settingsPanel;

    [Space(10)]
    public Text txtContagem;
    public Text txtPartidaMin;
    public Text txtPartidaSec;

    [Space(10)]
    public Slider sliderMusica;
    public Slider sliderFx;

    bool isSettingsOpen;

    void Start() {
        settingsPanel.anchoredPosition = new Vector3(300, settingsPanel.anchoredPosition.y, 0);

        if(PlayerPrefs.HasKey("VolumeMusica")) {
            Cancel();
        } else {
            volumeMusica = 100;
            volumeFX = 100;

            contagemInicialSegundos = 3;
            tempoPartidaMin = 3;
            tempoPartidaSec = 0;

            Apply();
        }

        sliderMusica.value = volumeMusica;
        sliderFx.value = volumeFX;

        mixer.SetFloat("VolumeMusica", -80f + volumeMusica);
        mixer.SetFloat("VolumeFX", -80f + volumeFX);
    }

    void Update() { 
        if(isSettingsOpen && settingsPanel.position.x != -280) {
            settingsPanel.anchoredPosition = Vector3.Lerp(settingsPanel.anchoredPosition, new Vector3(-280, settingsPanel.anchoredPosition.y, 0), Time.deltaTime * 5f);
        } else if(!isSettingsOpen && settingsPanel.position.x != 300) {
            settingsPanel.anchoredPosition = Vector3.Lerp(settingsPanel.anchoredPosition, new Vector3(300, settingsPanel.anchoredPosition.y, 0), Time.deltaTime * 5f);
        }
    }

    public void Apply() {
        PlayerPrefs.SetFloat("VolumeMusica", volumeMusica);
        PlayerPrefs.SetFloat("VolumeFX", volumeFX);

        PlayerPrefs.SetInt("ContagemInicialSegundos", contagemInicialSegundos);
        PlayerPrefs.SetInt("TempoPartidaMin", tempoPartidaMin);
        PlayerPrefs.SetInt("TempoPartidaSec", tempoPartidaSec);

        mixer.SetFloat("VolumeMusica", -80f + volumeMusica);
        mixer.SetFloat("VolumeFX", -80f + volumeFX);
    }

    public void Cancel() {
        volumeMusica = PlayerPrefs.GetFloat("VolumeMusica");
        volumeFX = PlayerPrefs.GetFloat("VolumeFX");

        contagemInicialSegundos = PlayerPrefs.GetInt("ContagemInicialSegundos");
        tempoPartidaMin = PlayerPrefs.GetInt("TempoPartidaMin");
        tempoPartidaSec = PlayerPrefs.GetInt("TempoPartidaSec");

        txtContagem.text = contagemInicialSegundos.ToString("00");

        txtPartidaMin.text = tempoPartidaMin.ToString("00");
        txtPartidaSec.text = tempoPartidaSec.ToString("00");
    }

    public void AddContagem() {
        if(contagemInicialSegundos < 10)
            contagemInicialSegundos++;

        txtContagem.text = contagemInicialSegundos.ToString("00");
    }

    public void RemoveContagem() {
        if(contagemInicialSegundos > 1)
            contagemInicialSegundos--;

        txtContagem.text = contagemInicialSegundos.ToString("00");
    }

    public void AddTempoDePartidaMin() {
        if(tempoPartidaMin < 59)
            tempoPartidaMin++;

        txtPartidaMin.text = tempoPartidaMin.ToString("00");
    }

    public void RemoveTempoDePartidaMin() {
        if(tempoPartidaMin > 1)
            tempoPartidaMin--;

        txtPartidaMin.text = tempoPartidaMin.ToString("00");
    }

    public void AddTempoDePartidaSec() {
        if(tempoPartidaSec < 50)
            tempoPartidaSec+=10;
        else{
            tempoPartidaSec = 0;
        }

        txtPartidaSec.text = tempoPartidaSec.ToString("00");
    }

    public void RemoveTempoDePartidaSec() {
        if(tempoPartidaSec >= 10)
            tempoPartidaSec -= 10;
        

        txtPartidaSec.text = tempoPartidaSec.ToString("00");
    }

    public void ChangeMusic(float _value) {
        volumeMusica = _value;
    }

    public void ChangeFx(float _value) {
        volumeFX  = _value;
    }

    public void OpenSettings() {
        isSettingsOpen = true;
    }

    public void CloseSettings() {
        isSettingsOpen = false;
    }

    public void StartGame() {
        SceneManager.LoadScene(1);
    }

    public void QuitGame() {
        Application.Quit();
    }
}


