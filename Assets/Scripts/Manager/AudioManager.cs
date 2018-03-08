using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : SingletonMonoBehaviour<AudioManager> {
    
    //ボリューム保存用
    private const string BGM_KEY = "BGM_KEY";
    private const string SE_KEY = "SE_KEY";
    private const float BGM_DEFULT = 0.8f;
    private const float SE_DEFULT = 1.0f;

    //BGMフェード時間
    public const float BGM_FADE_SPEED_HIGH = 0.5f;
    public const float BGM_FADE_SPEED_LOW = 0.3f;
    private float _bgmFadeSpeed = BGM_FADE_SPEED_HIGH;

    //次流すBGM名、SE名
    private string _nextBGMName;
    private string _nextSEName;

    //BGMフェードアウト中か
    private bool _isFadeOut = false;

    //BGM用、SE用に分けてオーディオソースを持つ
    public AudioSource AttachBGMSource, AttachSESource;

    //全Audioを保持
    private Dictionary<string, AudioClip> _bgmDic, _seDic;

    private void Awake() {
        //AudioManagerが既にある場合
        if (this != Instance)
        {
            this.gameObject.SetActive(false);
            //Destroy(this);
            return;
        }
        //AudioManagerのScene切り替え時の破棄されないようにする
        DontDestroyOnLoad(this.gameObject);

        _bgmDic = new Dictionary<string, AudioClip>();
        _seDic = new Dictionary<string, AudioClip>();

        //AudioをResourcesフォルダーから読み込む
        object[] bgmList = Resources.LoadAll("Sound/BGM");
        object[] seList = Resources.LoadAll("Sound/SE");

        foreach (AudioClip bgm in bgmList) {
            _bgmDic[bgm.name] = bgm;
        }

        foreach (AudioClip se in seList) {
            _seDic[se.name] = se;
        }
    }

    private void Start() {
        AttachBGMSource.volume = PlayerPrefs.GetFloat(BGM_KEY, BGM_DEFULT);
        AttachSESource.volume = PlayerPrefs.GetFloat(SE_KEY, SE_DEFULT);
    }

    //SE
    /// <summary>
	/// 指定したファイル名のSEを流す。第二引数のdelayに指定した時間だけ再生までの間隔を空ける
	/// </summary>

    public void PlaySE(string seName, float delay = 0.0f)
    {
        if (!_seDic.ContainsKey(seName))
        {
            Debug.Log(seName + "っていうSEがねーよ");
            return;
        }

        _nextSEName = seName;
        Invoke("DelayPlaySE", delay);
    }

    private void DelayPlaySE()
    {
        AttachSESource.PlayOneShot(_seDic[_nextSEName] as AudioClip);
    }

    //BGM
    /// <summary>
	/// 指定したファイル名のBGMを流す。ただし既に流れている場合は前の曲をフェードアウトさせてから。
	/// 第二引数のfadeSpeedRateに指定した割合でフェードアウトするスピードが変わる
	/// </summary>
    
    public void PlayBGM(string bgmName, float fadeSpeedRate = BGM_FADE_SPEED_HIGH)
    {
        if (!_bgmDic.ContainsKey(bgmName))
        {
            Debug.Log(bgmName + "っていうBGMがねーよ");
            return;
        }
        //現在BGMが流れていない時はそのまま流す
        if (!AttachBGMSource.isPlaying)
        {
            _nextBGMName = "";
            AttachBGMSource.clip = _bgmDic[bgmName] as AudioClip;
            AttachBGMSource.Play();
        }
        //次を流す。同じBGMが流れている時はスルー
        if (AttachBGMSource.clip.name != bgmName)
        {
            _nextBGMName = bgmName;
            Debug.Log(_bgmDic[bgmName]);
            //FadeOutBGM(fadeSpeedRate);
            
        }
    }

    /// <summary>
    /// 現在流れている曲をフェードアウトさせる
    /// fadeSpeedRateに指定した割合でフェードアウトするスピードが変わる
    /// </summary>

    public void FadeOutBGM(float fadeSpeedRate = BGM_FADE_SPEED_LOW)
    {
        _bgmFadeSpeed = fadeSpeedRate;
        _isFadeOut = true;
    }

    private void Update()
    {
        if (!_isFadeOut)
        {
            return;
        }

        //ボリュームを下げる→ボリュームが0→ボリュームを戻し次の曲

        AttachBGMSource.volume -= Time.deltaTime * _bgmFadeSpeed;
        if(AttachBGMSource.volume <= 0)
        {
            AttachBGMSource.Stop();
            AttachBGMSource.volume = PlayerPrefs.GetFloat(BGM_KEY, BGM_DEFULT);
            _isFadeOut = false;

            if(!string.IsNullOrEmpty(_nextBGMName))
            {
                PlayBGM(_nextBGMName);
            }
        }
    }

    //音量変更
    /// <summary>
	/// BGMとSEのボリュームを別々に変更&保存
	/// </summary>

    public void ChangeVolume(float BGMVolume, float SEVolume)
    {
        AttachBGMSource.volume = BGMVolume;
        AttachSESource.volume = SEVolume;

        PlayerPrefs.SetFloat(BGM_KEY, BGMVolume);
        PlayerPrefs.SetFloat(SE_KEY, SEVolume);
    }
}
