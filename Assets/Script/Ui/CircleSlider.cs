using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CircleSlider : MonoBehaviour
{
 
     public bool b=true;
	 public Image image;
	 public float speed=0.5f;

  float time =0f;
  public Text progress;


	public float _boostNum;

	private bool _isBoost;

	[SerializeField]
	private GameObject _player;

	void Start()
    {
	  
	image = GetComponent<Image>();

		_isBoost = false;

		time = _player.transform.GetComponent<Boost>().
				GetCurBoostGage() * 0.01f;
	}
  
    void Update()
    {
		if(b)
		{
			float speed =
				_player.transform.GetComponent<Boost>().
				GetGageSpeed() * 0.01f; 

			time+=Time.deltaTime*speed;
			image.fillAmount= time;
			if(progress)
			{
				progress.text = (int)(image.fillAmount*100)+"%";
			}
			
			//溜まった処理
			if(time>1)
			{
				time = 1;

				b = false;
			}
		}




		_player.transform.GetComponent<Boost>().SetCurBoostGage(time);
	}


	public void SetBoost(float boost)
    {
		time = boost * 0.01f;
		image.fillAmount = time;

		b = true;
	}
	
	
}
