﻿using UnityEngine;
using System.Collections;

public class SkillAmmoKit : SkillBase {

	public override void SetupSkill ()
	{
		// TODO: Mudar a maneira como a skill é carregada
		this.ID = GetInstanceID(); // TODO: Mudar a busca do ID;

		this.SkillName = "Ammo Kit";
		this.SkillDescription = "Kit de municao";
		this.SkillType = SkillTypeEnum.Buf;
		this.ActivationType = SkillActivationEnum.Activation;
		this.SkillBehaviour = SkillBehaviourEnum.Object;
		this.CoolDown = 0.5f;
		this.EnergyCost = 50f;
		this.NeedTarget = false;

		this.Activated = false;
		this.ActivationTime = 1f;

		// Medkit aplica 25% de cura de HP 
		this.AttributeModifiers = new AttributeModifier[1];
		this.AttributeModifiers[0] = new AttributeModifier();
		this.AttributeModifiers[0].OriginID = this.ID;
		this.AttributeModifiers[0].Modifier = ENUMERATORS.Attribute.AttributeBaseTypeEnum.Weapon;
		this.AttributeModifiers[0].ApplyTo = ENUMERATORS.Attribute.AttributeModifierApplyToEnum.Max;
		this.AttributeModifiers[0].AttributeType = (int)ENUMERATORS.Attribute.WeaponAttributeTypeEnum.Ammo;
		this.AttributeModifiers[0].CalcType = ENUMERATORS.Attribute.AttributeModifierCalcTypeEnum.Value;
		this.AttributeModifiers[0].ModifierType = ENUMERATORS.Attribute.AttributeModifierTypeEnum.OneTimeOnly;
		this.AttributeModifiers[0].ApplyAs = ENUMERATORS.Attribute.AttributeModifierApplyAsEnum.Constant;
		this.AttributeModifiers[0].CanSetToCurrentExceedMax = false;
		this.AttributeModifiers[0].Value = 24;


		base.SetupSkill ();
	}

	public override void OnCollisionEnter (Collision collidedWith_)
	{
		base.OnCollisionEnter (collidedWith_);
	}

	public override void OnTriggerEnter (Collider trigerWith_)
	{
		if (this.Activated)
		{
			base.OnTriggerEnter (trigerWith_);

			// Process medkit
			Player _player = trigerWith_.gameObject.GetComponent<Player>();

			if (_player != null)
			{
				AttributeModifier.AddAttributeModifier(ref _player.CurrentWeapon.AttributeModifiers, this.Pool.DefaultParent.gameObject.GetComponent<SkillAmmoKit>().AttributeModifiers);
				base.SetupSkill();
				this.ReturnToPool();
			}
		}
	}

	public override void SpawnSkill ()
	{
		base.SpawnSkill ();
		this.Activated = false;
		this.transform.position = Caster.transform.position;
	}

}
