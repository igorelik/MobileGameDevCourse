using UnityEngine;
using System.Collections;

public enum TriggerAction
{
	None = 0,
	EnableObject,
	DisableObject,
	EndGame
}

public class TriggeredAction : MonoBehaviour {

	public TriggerAction action;
	public GameObject actionObject;
	public string levelToLoad;
	public bool disableOnTrigger = false;

	public void Trigger()
	{

		switch(action)
		{
			case TriggerAction.EnableObject:
				actionObject.SetActive(true); break;

			case TriggerAction.DisableObject:
				actionObject.SetActive(false); break;

			case TriggerAction.EndGame:
				Application.LoadLevel(levelToLoad); break;
		}

		if (disableOnTrigger)
			gameObject.SetActive(false);
	}
}
