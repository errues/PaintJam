using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {
	public BasicColors color;

	public SpriteRenderer filling;

	private void OnValidate(){
		SetColor ();
	}

	private void SetColor(){
		switch (color){
		case BasicColors.AMARILLO:
			filling.color = Color.yellow;
			break;
		case BasicColors.ROJO:
			filling.color = Color.red;
			break;
		case BasicColors.AZUL:
			filling.color = Color.blue;
			break;
		case BasicColors.NARANJA:
			filling.color = new Color (1F, 0.5f, 0);
			break;
		case BasicColors.MORADO:
			filling.color = new Color (0.5f, 0, 1f);
			break;
		case BasicColors.VERDE:
			filling.color = Color.green;
			break;
		case BasicColors.NEGRO:
			filling.color = Color.black;
			break;
		}
	}
}
