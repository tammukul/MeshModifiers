﻿using UnityEngine;
using Mathx;
using MeshModifiers;

[AddComponentMenu (MeshModifierConstants.ADD_COMP_BASE_NAME + "Exponent")]
public class ExponentModifier : MeshModifierBase
{
	#region Public Properties

	public Vector3
		value = Vector3.zero;

	public bool absolute = true;

	#endregion



	#region Inherited Methods

	public override void PreMod ()
	{
		base.PreMod ();

		value.x = value.x < 0f ? 0f : value.x;
		value.y = value.y < 0f ? 0f : value.y;
		value.z = value.z < 0f ? 0f : value.x;
	}

	protected override Vector3 _ModifyOffset (Vector3 basePosition, Vector3 baseNormal)
	{
		if (absolute)
		{
			basePosition.x *= Mathf.Exp (Mathf.Abs (basePosition.x * value.x));
			basePosition.y *= Mathf.Exp (Mathf.Abs (basePosition.y * value.y));
			basePosition.z *= Mathf.Exp (Mathf.Abs (basePosition.z * value.z));
		}
		else
		{
			basePosition.x *= Mathf.Exp (basePosition.x * value.x);
			basePosition.y *= Mathf.Exp (basePosition.y * value.y);
			basePosition.z *= Mathf.Exp (basePosition.z * value.z);
		}

		return basePosition;
	}

	#endregion
}