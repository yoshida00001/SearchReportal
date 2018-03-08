using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR;

public class SceneControl : MonoBehaviour {

	private int _scene; 
	private bool counter;
	private float time;

	public int i;
	public int j;

	void Update () {
		if ( counter ) {
			time += Time.deltaTime;
			if ( time >= 1 ) {
                switch (_scene)
                {
                    case 0:
                        TitleScene();
                        break;
                    case 1:
                        TutorialScene();
                        break;
                    case 2:
                        Map0Scene();
                        break;
                    case 3:
                        Map1Scene();
                        break;
                    case 4:
                        Map2Scene();
                        break;
                    case 5:
                        Map3Scene();
                        break;
					case 6:
						Map4Scene();
						break;
					case 7:
						Map5Scene();
						break;
					case 8:
						Map6Scene();
						break;
					case 9:
						Map6Scene();
						break;
					case 10:
						Map7Scene( );
						break;
                }
            counter = false;
				time = 0;
			}
		}
	}

    void TitleScene() {
        InputTracking.Recenter();
        AudioManager.Instance.PlaySE ("EyeTrigger");
        //AudioManager.Instance.FadeOutBGM();
        SceneNavigator.Instance.Change("Title", 2f);
        //AudioManager.Instance.PlayBGM("Main", AudioManager.BGM_FADE_SPEED_HIGH);
    }

    void TutorialScene() {
		AudioManager.Instance.PlaySE ("EyeTrigger");
		AudioManager.Instance.FadeOutBGM( );
		SceneNavigator.Instance.Change( "Tutorial", 2f );
		AudioManager.Instance.PlayBGM( "Opening", AudioManager.BGM_FADE_SPEED_HIGH );
    }

    void Map0Scene()
    {
		AudioManager.Instance.PlaySE ("EyeTrigger");
		AudioManager.Instance.FadeOutBGM( );
		SceneNavigator.Instance.Change( "Map0", 2f );
		AudioManager.Instance.PlayBGM( "Main", AudioManager.BGM_FADE_SPEED_HIGH );
    }

    void Map1Scene()
    {
		AudioManager.Instance.PlaySE ("EyeTrigger");
		//AudioManager.Instance.FadeOutBGM();
		SceneNavigator.Instance.Change( "Map1" );
    }

    void Map2Scene()
    {
		AudioManager.Instance.PlaySE ("EyeTrigger");
        //AudioManager.Instance.FadeOutBGM();
        SceneNavigator.Instance.Change("Map2");
    }
    void Map3Scene()
    {
        AudioManager.Instance.PlaySE("EyeTrigger");
        //AudioManager.Instance.FadeOutBGM();
        SceneNavigator.Instance.Change("Map3");
    }

	void Map4Scene()
	{
		AudioManager.Instance.PlaySE("EyeTrigger");
		//AudioManager.Instance.FadeOutBGM();
		SceneNavigator.Instance.Change("Map4");
	}
	void Map5Scene()
	{
		AudioManager.Instance.PlaySE("EyeTrigger");
		//AudioManager.Instance.FadeOutBGM();
		SceneNavigator.Instance.Change("Map5");
	}
	void Map6Scene()
	{
		AudioManager.Instance.PlaySE("EyeTrigger");
		//AudioManager.Instance.FadeOutBGM();
		SceneNavigator.Instance.Change("Map6");
	}

    void Map7Scene()
    {
		AudioManager.Instance.PlaySE("EyeTrigger");
        //AudioManager.Instance.FadeOutBGM();
        SceneNavigator.Instance.Change("Map7");
    }

	public void enterMap1Pointer(){
		counter = true;
		_scene = i;
	}

	public void enterMap2Pointer(){
		counter = true;
		_scene = j;
	}

	public void enterMovePointer(){
		counter = true;
		_scene = 11;
	}

	public void exitMapPointer(){
		counter = false;
		time = 0;
	}
}
