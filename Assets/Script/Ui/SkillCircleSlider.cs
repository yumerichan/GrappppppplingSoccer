using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillCircleSlider : MonoBehaviour
{
	public bool b = true;
	public Image image;

	float time = 0f;
	float maxTime;
	public Text progress;


	[SerializeField]
	private GameObject _player;

	void Start()
	{

		image = GetComponent<Image>();


		time = _player.transform.GetComponent<CharactorBasic>().
				GetCurSkillCoolTime();
		maxTime = _player.transform.GetComponent<CharactorBasic>().
				GetSkillCoolTime();
	}

	void Update()
	{
		if (b)
		{ 

			time = _player.transform.GetComponent<CharactorBasic>().
				GetCurSkillCoolTime();

			image.fillAmount = time / maxTime;

			if (progress)
			{
				progress.text = (int)(image.fillAmount * 100) + "%";
			}

			//—­‚Ü‚Á‚½ˆ—
			if (time >= maxTime)
			{
				time = maxTime;

				b = true;
			}
		}
	}


	public void StartSkill(float boost)
	{
		
	}

}
