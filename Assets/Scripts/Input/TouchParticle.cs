using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// タッチした座標にパーティクルを出す
/// </summary>
public class TouchParticle : MonoBehaviour
{
    #region variables
    [SerializeField] private GameObject m_ParticlePrefab     = null;// パーティクルの原本
    ParticleSystem m_ParticleSystem = null;// タップエフェクト

    [SerializeField] private Camera m_ParticleCamera         = null;// パーティクルカメラ
    #endregion// variables

    #region properties
    public ParticleSystem particle { get { return m_ParticleSystem; } private set { m_ParticleSystem = value; } }
    #endregion// properties

    #region unity callbacks
    private void Awake()
    {
        Init ();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown (0)) {
            // タッチした座標にエフェクトを出す
            var camera = GetParticleCamera();
            var pos = camera.ScreenToWorldPoint(Input.mousePosition + camera.transform.forward * 10);
            PlayParticle (pos);
        }
    }
    #endregion// unity callbacks

    #region public methods
    public void Init()
    {
        var parent = GetParticleParentTF ();
        particle = Instantiate (m_ParticlePrefab, parent).GetComponent<ParticleSystem> ();
    }

    public Transform GetParticleParentTF()
    {
        return UIManager.Instance.ui.particlesParent;
    }

    public Camera GetParticleCamera()
    {
        return m_ParticleCamera;
    }
    #endregion// public methods

    #region private methods
    private void PlayParticle(Vector3 _pos)
    {
        particle.transform.position = _pos;
        particle.Emit (10);
    }
    #endregion// private methods

}// class
