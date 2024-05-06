using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoController : MonoBehaviour
{
    [SerializeField] AssetReference assetReference;
    [SerializeField] GameObject objControl;
    [SerializeField] Slider slider;
    [SerializeField] Image imgPlay1, imgPlay2;
    [SerializeField] VideoPlayer videoPlayer;
    [SerializeField] Sprite spritePlay;
    [SerializeField] Sprite spritePause;
    [SerializeField] Sprite spriteRestart;

    private bool isSlide;


    private void Start()
    {
        LoadVideo();
        DisableControls();
        videoPlayer.loopPointReached += OnVideoEnd;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ActiveControls();
            DisableControls();
        }

        if (!isSlide && videoPlayer.isPlaying)
        {
            slider.value = (float)videoPlayer.frame / (float)videoPlayer.frameCount;
        }
    }


    private void OnVideoEnd(VideoPlayer videoPlayer)
    {
        ActiveControls();
        imgPlay1.sprite = spriteRestart;
        imgPlay2.sprite = spriteRestart;
    }

    private void OnClipLoaded(VideoClip clip)
    {
        videoPlayer.clip = clip;
        ActiveControls();
        Loading.instance.Disable();
    }


    private void InDisableControls()
    {
        objControl.SetActive(false);
    }

    private void InDisableSlide()
    {
        isSlide = false;
    }

    public void InButtonBack()
    {
        Loading.instance.LoadLevel(1, 1);
    }


    private void DisableControls()
    {
        CancelInvoke();
        Invoke(nameof(InDisableControls), 2);
    }

    private void ActiveControls()
    {
        CancelInvoke();
        objControl.SetActive(true);
    }

    private void LoadVideo()
    {
        // string path = Application.dataPath + "/Bundles";
        // AssetBundle assetBundle = AssetBundle.LoadFromFile(path + "/video");
        // VideoClip clip = assetBundle.LoadAsset<VideoClip>("video");
        // videoPlayer.clip = clip;
        // ActiveControls();
        // Loading.instance.Disable();
        // assetBundle.Unload(false);

        AddressablesManager.instance.ButtonLoad<VideoClip>(assetReference, OnClipLoaded);
    }

    private void Play()
    {
        if (!videoPlayer.clip) return;

        videoPlayer.Play();
        imgPlay1.sprite = spritePause;
        imgPlay2.sprite = spritePause;
        DisableControls();
    }

    private void Pause()
    {
        videoPlayer.Pause();
        imgPlay1.sprite = spritePlay;
        imgPlay2.sprite = spritePlay;
        ActiveControls();
    }


    public void SliderPointerDown()
    {
        ActiveControls();
        isSlide = true;
    }

    public void SliderPointerUp()
    {
        videoPlayer.frame = (long)(slider.value * videoPlayer.frameCount);

        DisableControls();
        Invoke(nameof(InDisableSlide), 0.5f);
    }

    public void ButtonControl()
    {
        if (videoPlayer.isPlaying) Pause();
        else Play();
    }

    public void ButtonStop()
    {
        slider.value = 0;
        videoPlayer.Stop();
        imgPlay1.sprite = spritePlay;
        imgPlay2.sprite = spritePlay;
        ActiveControls();
    }

    public void ButtonBack()
    {
        ButtonStop();
        Invoke(nameof(InButtonBack), 0.4f);
    }
}