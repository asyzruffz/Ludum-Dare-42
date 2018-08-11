using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {

	public float hp = 100;
	
	public void ApplyDamage (float dmg) {
		hp -= dmg;
	}

	public bool IsDead () {
		return hp <= 0;
	}
}
