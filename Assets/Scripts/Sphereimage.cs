using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sphereimage : MonoBehaviour {

	private bool counter; //pointerの当たり判定
	private float time; //pointerの当たり時間
	private int _img;　//次のimageの選択用
    private int _up; //imageカウントUpの選択用
    private int _down; //imageカウントDownの選択用
    public Texture[] _map; //360°画像のimage
    public Material _material; //360°画像を表示するsphere
	public int i; //image数
	public int j; //次の階へのマップ

    void Start () {
		counter = false;
		time = 0;
        _material.SetTexture("_MainTex", _map[i]);
    }

	void Update(){
        //imageの選択
		if ( counter ) {
            //pointerがobjectを指定秒数見てから実行
			time += Time.deltaTime;
			if ( time >= 1 ) {
				switch ( _img ) {
                    //_img 0 1up,1 2up,2 3up,3 1down,4 2down, 5 3down, 6 next
                    case 0:
                    _up = 0;
					ChangeimageUp( );
					break;
                case 1:
                    _up = 1;
                    ChangeimageUp();
                    break;
                case 2:
                     _up = 2;
                     ChangeimageUp();
                     break;
                case 3:
                    _down = 0;
			        ChangeimageDown( );
					break;
                case 4:
                    _down = 1;
                    ChangeimageDown();
                    break;
                case 5:
                    _down = 2;
                    ChangeimageDown();
                    break;
                case 6:
					ChangeNext();
					break;
				}
			}
		}
	}
    //フェードアウト中に画像の切り替え
    public void ChangeimageUp() {
        Invoke("ChangeUp", 0.5f);
    }

    //フェードアウト中に画像の切り替え
    public void ChangeimageDown() {
		Invoke("ChangeDown", 0.5f);
    }

    //フェードアウト中に画像の切り替え
    public void ChangeNext() {
        Invoke("ChangeNextUp", 0.5f);
    }

    //指定imageに変更　次のimageに進む
    public void ChangeUp() {
        //いくつ進むかを_upで決める
        switch (_up) {
            case 0:
                i++;
                _material.SetTexture("_MainTex", _map[i]);
                break;
            case 1:
                i += 2;
                _material.SetTexture("_MainTex", _map[i]);
                break;
            case 2:
                i += 3;
                _material.SetTexture("_MainTex", _map[i]);
                break;
        }
        
    }

    //指定imageに変更　前のimageに戻る
    public void ChangeDown() {
        //いくつ戻るか_downで決める
        switch (_down) {
            case 0:
                i--;
                _material.SetTexture("_MainTex", _map[i]);
                break;
            case 1:
                i -= 2;
                _material.SetTexture("_MainTex", _map[i]);
                break;
            case 2:
                i -= 3;
                _material.SetTexture("_MainTex", _map[i]);
                break;
        }
    }

    //指定imageに変更 次の階へ行くmapを表示
    public void ChangeNextUp() {
        _material.SetTexture("_MainTex", _map[j]);
    }

    //EventTriger　PointerEnter(目線合わせた時)用
    //count 0 1up,1 2up,2 3up,3 1down,4 2down, 5 3down, 6 next
    public void enterImagePointer(int count){
		counter = true;
        _img = count;
	}

    //EventTriger PointerExit(目線外した時)用
    public void exitLanternPointer(){
		counter = false;
		time = 0;
	}
}
