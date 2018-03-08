using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanternControl : MonoBehaviour {
    public GameObject[] _lantern; //lanternの取得
    public GameObject _player; //playerの値取得用
    public GameObject text; //textの取得
    public float[] _rotation; //各マップのplayerのrotationの値取得用
    public int i;
    public int j;
	public int _next; //次の階へのmapのlantern
    private int _up; //lanternカウントUpの選択用
    private int _down;  //lanternカウントdownの選択用
    private int _lan; //次のlanternの選択用
    private float time; //pointerの当たり時間
    private bool counter; //pointerの当たり判定

    void Start () {
        _lantern[i].SetActive(true);
        SetRotation();
        RotationUp();
    }
	
	void Update () {
		if ( counter ) {
            //pointerがobjectを指定秒数見てから実行
            time += Time.deltaTime;
			if ( time >= 1 ) {
				switch ( _lan ) {
                    //_lan 0 1up,1 2up,2 3up,3 1down,4 2down, 5 3down, 6 next
                    case 0:
                        _up = 0;
					    LanternSizeUp( );
					    break;
				    case 1:
                        _up = 1;
                        LanternSizeUp();
                        break;
				    case 2:
                        _up = 2;
                        LanternSizeUp();
                        break;
				    case 3:
                        _down = 0;
                        LanternSizeDown( );
					    break;
				    case 4:
                        _down = 1;
                        LanternSizeDown();
					    break;
                    case 5:
                        _down = 2;
                        LanternSizeDown();
                        break;
                    case 6:
                        LanternSizeNext();
                        break;
                }
			}
		}
	}
    //フェードアウト中に画像の切り替え
    public void LanternSizeUp() {
        switch (_up) {
            case 0:
                j++;
                SceneNavigator.Instance.ChangeLt();
                AudioManager.Instance.PlaySE("EyeTrigger");
                Invoke("LanternUp", 0.5f);
                Invoke("RotationUp", 0.5f);
                break;
            case 1:
                j += 2;
                SceneNavigator.Instance.ChangeLt();
                AudioManager.Instance.PlaySE("EyeTrigger");
                Invoke("LanternUp", 0.5f);
                Invoke("RotationUp", 0.5f);
                break;
            case 2:
                j += 3;
                SceneNavigator.Instance.ChangeLt();
                AudioManager.Instance.PlaySE("EyeTrigger");
                Invoke("LanternUp", 0.5f);
                Invoke("RotationUp", 0.5f);
                break;
        }
		
    }

    //フェードアウト中に画像の切り替え
    public void LanternSizeNext() {
		j++;
		SceneNavigator.Instance.ChangeLt();
		AudioManager.Instance.PlaySE ("EyeTrigger");
		Invoke("LanternNextUp", 0.5f);
		Invoke("RotationUp", 0.5f);
	}

    //フェードアウト中に画像の切り替え
    public void LanternSizeDown() {
        switch (_down) {
            case 0:
                j--;
                SceneNavigator.Instance.ChangeLt();
                AudioManager.Instance.PlaySE("EyeTrigger");
                Invoke("LanternDown", 0.5f);
                Invoke("RotationDown", 0.5f);
                break;
            case 1:
                j -= 2;
                SceneNavigator.Instance.ChangeLt();
                AudioManager.Instance.PlaySE("EyeTrigger");
                Invoke("LanternDown", 0.5f);
                Invoke("RotationDown", 0.5f);
                break;
            case 2:
                j -= 3;
                SceneNavigator.Instance.ChangeLt();
                AudioManager.Instance.PlaySE("EyeTrigger");
                Invoke("LanternDown", 0.5f);
                Invoke("RotationDown", 0.5f);
                break;
        }
    }

    //指定lanternの表示　次のlantern用
    void LanternUp() {
        switch (_up) {
            case 0:
                Debug.Log("ok");
                _lantern[i].SetActive(false);
                i++;
                _lantern[i].SetActive(true);
                break;
            case 1:
                _lantern[i].SetActive(false);
                i += 2;
                _lantern[i].SetActive(true);
                break;
            case 3:
                _lantern[i].SetActive(false);
                i += 3;
                _lantern[i].SetActive(true);
                break;
        }
    }

    //指定lanternの表示
    void LanternNextUp()
	{
		_lantern[i].SetActive(false);
		Debug.Log("クリックされた");
		_lantern[_next].SetActive(true);
	}

    //指定lanternの表示　前のlantern用
    void LanternDown() {
        switch (_down) {
            case 0:
                _lantern[i].SetActive(false);
                i--;
                _lantern[i].SetActive(true);
                break;
            case 1:
                _lantern[i].SetActive(false);
                i -= 2;
                _lantern[i].SetActive(true);
                break;
            case 3:
                _lantern[i].SetActive(false);
                i -= 3;
                _lantern[i].SetActive(true);
                break;
        }
    }

    //textの表示
    public void ExitText()　{
        text.SetActive(true);
    }

    //playerのrotationの値取得
    void SetRotation()
    {
        Quaternion rotation = _player.transform.localRotation;
        // クォータニオン → オイラー角への変換
        Vector3 rotationAngles = rotation.eulerAngles;
        rotationAngles.y = rotationAngles.y + _rotation[0];
        // オイラー角 → クォータニオンへの変換
        rotation = Quaternion.Euler(rotationAngles);
        _player.transform.localRotation = rotation;
    }

    //playerのrotation値変更　次の場所用
    public void RotationUp()
    {
        Quaternion rotation = _player.transform.localRotation;
        // クォータニオン → オイラー角への変換
        Vector3 rotationAngles = rotation.eulerAngles;
        rotationAngles.y = rotationAngles.y - _rotation[j - 1];
        rotationAngles.y = rotationAngles.y + _rotation[j];
        // オイラー角 → クォータニオンへの変換
        rotation = Quaternion.Euler(rotationAngles);
        _player.transform.localRotation = rotation;
    }

    //playerのrotation値変更　前の場所用
    public void RotationDown()
    {
        Quaternion rotation = _player.transform.localRotation;
        // クォータニオン → オイラー角への変換
        Vector3 rotationAngles = rotation.eulerAngles;
        rotationAngles.y = rotationAngles.y - _rotation[j + 1];
        rotationAngles.y = rotationAngles.y + _rotation[j];
        // オイラー角 → クォータニオンへの変換
        rotation = Quaternion.Euler(rotationAngles);
        _player.transform.localRotation = rotation;
    }

    //EventTriger　PointerEnter(目線合わせた時)用
    //count 0 1up,1 2up,2 3up,3 1down,4 2down, 5 3down, 6 next
    public void enterLanternPointer(int count){
		counter = true;
		_lan = count;
    }

    //EventTriger PointerExit(目線外した時)用
    public void exitLanternPointer(){
		counter = false;
		time = 0;
	}
}
