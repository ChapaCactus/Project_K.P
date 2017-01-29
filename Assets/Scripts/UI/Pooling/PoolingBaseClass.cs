using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class PoolingBaseClass : MonoBehaviour
{
	#region Variables
	protected bool m_IsActive = false;
	#endregion// Variables

	#region Properties
	public bool isActive
	{
		get { return m_IsActive; }
		protected set { m_IsActive = value; }
	}
	#endregion Properties

	#region PublicMethods
	public virtual void Init()
	{
		//
	}

	public virtual void Show(float _duration = 1.0f)
	{
		//
	}
	#endregion// PublicMethods
}
