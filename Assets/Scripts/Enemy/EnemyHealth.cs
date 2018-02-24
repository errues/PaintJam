using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {
	public BasicColors color;
	[Range(1, 50)]
	public int maxHealth = 3;
	[Range(0f, 1f)]
	public float maxTransparency = 0.3f;

	public SpriteRenderer filling;

	private int Y, R, B;
	private float alpha;

	private void Awake(){		
		alpha = 1f;
		SetHealth ();
	}

	private void OnValidate(){
		SetInitialColor ();
	}

	public void HitAmarillo(){
		Hit (BasicColors.AMARILLO, 1);
	}

	public void HitRojo(){
		Hit (BasicColors.ROJO, 1);
	}

	public void HitAzul(){
		Hit (BasicColors.AZUL, 1);
	}
		
	public bool Hit(/*Bullet bullet*/BasicColors bulletColor, int bulletDamage){
		bool hit = false;

		//BasicColors bulletColor = bullet.getBulletColor ();
		//int bulletDamage = bullet.getDamage ();	

		switch (color){
		case BasicColors.AMARILLO:
			if (bulletColor == BasicColors.AMARILLO) {
				Y -= bulletDamage;
				alpha = Mathf.Lerp (maxTransparency, 1f, (float)Y / maxHealth);
				hit = true;
			}
			break;
		case BasicColors.ROJO:
			if (bulletColor == BasicColors.ROJO) {
				R -= bulletDamage;
				alpha = Mathf.Lerp (maxTransparency, 1f, (float)R / maxHealth);
				hit = true;
			}
			break;
		case BasicColors.AZUL:
			if (bulletColor == BasicColors.AZUL) {
				B -= bulletDamage;
				alpha = Mathf.Lerp (maxTransparency, 1f, (float)B / maxHealth);
				hit = true;
			}
			break;
		case BasicColors.NARANJA:
			if (bulletColor == BasicColors.AMARILLO && Y > 0) {				
				Y -= bulletDamage;
				if (Y <= 0)
					color = BasicColors.ROJO;
				hit = true;
			} else if (bulletColor == BasicColors.ROJO && R > 0) {
				R -= bulletDamage;
				if (R <= 0)
					color = BasicColors.AMARILLO;
				hit = true;
			}
			if (hit)
				alpha = Mathf.Lerp (maxTransparency, 1f, (float)Mathf.Max (Y, R) / maxHealth);			
			break;
		case BasicColors.MORADO:
			if (bulletColor == BasicColors.ROJO && R > 0) {				
				R -= bulletDamage;
				if (R <= 0)
					color = BasicColors.AZUL;
				hit = true;
			} else if (bulletColor == BasicColors.AZUL && B > 0) {
				B -= bulletDamage;
				if (B <= 0)
					color = BasicColors.ROJO;
				hit = true;
			}

			if (hit)
				alpha = Mathf.Lerp (maxTransparency, 1f, (float)Mathf.Max (R, B) / maxHealth);			
			break;
		case BasicColors.VERDE:
			if (bulletColor == BasicColors.AMARILLO && Y > 0) {				
				Y -= bulletDamage;
				if (Y <= 0)
					color = BasicColors.AZUL;
				hit = true;
			} else if (bulletColor == BasicColors.AZUL && B > 0) {
				B -= bulletDamage;
				if (B <= 0)
					color = BasicColors.AMARILLO;
				hit = true;
			}

			if (hit)
				alpha = Mathf.Lerp (maxTransparency, 1f, (float)Mathf.Max (Y, B) / maxHealth);			
			break;
		case BasicColors.NEGRO:
			// TODO
			break;
		}

		if (hit)
			SetColor ();

		return hit;
	}

	private void SetColor(){
		Color c = new Color();
		switch (color){
		case BasicColors.AMARILLO:
			c = new Color (1, 1, 0, alpha);
			break;
		case BasicColors.ROJO:
			c = new Color (1, 0, 0, alpha);
			break;
		case BasicColors.AZUL:
			c = new Color (0, 0, 1, alpha);
			break;
		case BasicColors.NARANJA:
			if (Y == R)
				c = new Color (1, 0.5f, 0, alpha);
			else if (Y < R)
				c = Color.Lerp (new Color (1, 0.5f, 0, alpha), new Color (1, 0, 0, alpha), (float)(R - Y) / maxHealth);
			else
				c = Color.Lerp (new Color (1, 0.5f, 0, alpha), new Color (1, 1, 0, alpha), (float)(Y - R) / maxHealth);
			break;
		case BasicColors.MORADO:
			if (R == B)
				c = new Color (0.5f, 0, 0.5f, alpha);
			else if (R < B)
				c = Color.Lerp (new Color (0.5f, 0, 0.5f, alpha), new Color (0, 0, 1, alpha), (float)(B - R) / maxHealth);
			else
				c = Color.Lerp (new Color (0.5f, 0, 0.5f, alpha), new Color (1, 0, 0, alpha), (float)(R - B) / maxHealth);
			break;
		case BasicColors.VERDE:
			if (Y == B)
				c = new Color (0, 1, 0, alpha);
			else if (Y < B)
				c = Color.Lerp (new Color (0, 1, 0, alpha), new Color (0, 0, 1, alpha), (float)(R - B) / maxHealth);
			else
				c = Color.Lerp (new Color (0, 1, 0, alpha), new Color (1, 1, 0, alpha), (float)(B - R) / maxHealth);
			break;
		case BasicColors.NEGRO:
			// TODO
			break;
		}

		filling.color = c;
	}

	private void SetInitialColor(){		
		alpha = 1;	
		Color c = new Color (0, 0, 0, alpha);
		switch (color){
		case BasicColors.AMARILLO:
			c.r = 1f;
			c.g = 1f;
			c.b = 0f;
			break;
		case BasicColors.ROJO:
			c.r = 1f;
			c.g = 0f;
			c.b = 0f;
			break;
		case BasicColors.AZUL:
			c.r = 0f;
			c.g = 0f;
			c.b = 1f;
			break;
		case BasicColors.NARANJA:
			c.r = 1f;
			c.g = 0.5f;
			c.b = 0f;
			break;
		case BasicColors.MORADO:
			c.r = 0.5f;
			c.g = 0f;
			c.b = 0.5f;
			break;
		case BasicColors.VERDE:
			c.r = 0f;
			c.g = 1f;
			c.b = 0f;
			break;
		case BasicColors.NEGRO:
			c.r = 0.1f;
			c.g = 0.1f;
			c.b = 0.1f;
			break;
		}
		filling.color = c;
	}

	private void SetHealth(){
		Y = R = B = 0;
		switch (color){
		case BasicColors.AMARILLO:
			Y = maxHealth;
			break;
		case BasicColors.ROJO:
			R = maxHealth;
			break;
		case BasicColors.AZUL:
			B = maxHealth;
			break;
		case BasicColors.NARANJA:
			Y = maxHealth;
			R = maxHealth;
			break;
		case BasicColors.MORADO:
			R = maxHealth;
			B = maxHealth;
			break;
		case BasicColors.VERDE:
			Y = maxHealth;
			B = maxHealth;
			break;
		case BasicColors.NEGRO:
			Y = maxHealth;
			R = maxHealth;
			B = maxHealth;
			break;
		}
	}
}
