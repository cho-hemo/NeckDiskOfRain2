using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ButtonUiDefaultSetting : MonoBehaviour
{
	// Start is called before the first frame update
	void Start()
	{

		switch (transform.parent.name)
		{
			case "PlayerSelect":
				GetComponent<ButtonUi>().SelectCharacter();
				break;
			case "SummaryBtn":
				GetComponent<ButtonUi>().SelectInformaion();
				break;
			case "IconList":
				GetComponent<ButtonUi>().SelectDifficulty(2);
				break;

		}
		// GetComponent<Button>().
	}

	// Update is called once per frame
	void Update()
	{

	}
}
