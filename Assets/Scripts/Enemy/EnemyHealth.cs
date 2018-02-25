using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {
	public BasicColors color;
	[Range(1, 50)]
	public int maxHealth = 3;
	[Range(0f, 1f)]
	public float maxTransparency = 0.3f;

	public float corpseTime = 1;

	private SpriteRenderer filling;
	private SpriteRenderer border;

	public Sprite deadSpriteFilling;
	public Sprite deadSpriteBorder;

	private int Y, R, B;
	private float alpha;

	private bool dead;

	private void Start(){
		alpha = 1f;
		dead = false;

		filling = GetComponent<EnemySpriteManager> ().filling;
		border = GetComponent<EnemySpriteManager> ().border;
		SetHealth ();
		SetColor ();
	}

	public void Initialize(int lifePoints, bool combined){
		filling = GetComponent<EnemySpriteManager> ().filling;
		border = GetComponent<EnemySpriteManager> ().border;
		maxHealth = lifePoints;
		switch (Random.Range (0, 3)) {
		case 0:
			if (!combined)
				color = BasicColors.AMARILLO;
			else
				color = BasicColors.NARANJA;
			break;
		case 1:
			if (!combined)
				color = BasicColors.ROJO;
			else
				color = BasicColors.VERDE;
			break;
		case 2:
			if (!combined)
				color = BasicColors.AZUL;
			else
				color = BasicColors.MORADO;
			break;
		}
		SetHealth ();
		SetColor ();
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
		
		if (Y <= 0 && R <= 0 && B <= 0)
			Die ();
		
		return hit;
	}

	public void SetColor(){
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
				c = Color.Lerp (new Color (0, 1, 0, alpha), new Color (0, 0, 1, alpha), (float)(B - Y) / maxHealth);
			else
				c = Color.Lerp (new Color (0, 1, 0, alpha), new Color (1, 1, 0, alpha), (float)(Y - B) / maxHealth);
			break;
		case BasicColors.NEGRO:
			// TODO
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

	private void Die(){
		filling.sprite = deadSpriteFilling;
		border.sprite = deadSpriteBorder;
		transform.parent.GetComponent<EnemyController> ().EnemyDied ();
		dead = true;
		Object.Destroy (this.gameObject, corpseTime);
	}

	public bool IsDead(){
		return dead;
	}
}
