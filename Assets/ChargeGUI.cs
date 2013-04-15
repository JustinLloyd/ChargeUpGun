using UnityEngine;

public class ChargeGUI : MonoBehaviour
{
    #region Inspector Variables
    [SerializeField]
    protected Color m_chargeFull;

    [SerializeField]
    protected Color m_chargeReadyToRelease;

    [SerializeField]
    protected ChargeUp m_chargeUp;

    [SerializeField]
    protected Color m_chargeNormal;

    [SerializeField]
    protected Color m_chargeStarted;

    #endregion

    protected Color m_colour;

    protected void Reset()
    {
        m_chargeNormal = Color.white;
        m_chargeStarted = Color.cyan;
        m_chargeReadyToRelease = Color.yellow;
        m_chargeFull = Color.red;
        // not efficient, but useful if you forget to set your reference in the inspector
        m_chargeUp = GameObject.FindObjectOfType(typeof(ChargeUp)) as ChargeUp;
    }

    protected void Start()
    {
        m_colour = m_chargeNormal;
    }

    protected void OnChargeReleased(float charge)
    {
        m_colour = m_chargeNormal;
        Debug.Log("Charge released " + charge);
    }

    protected void OnFullyCharged(float charge)
    {
        Debug.Log("Fully charged! " + charge);
        m_colour = m_chargeFull;
    }

    protected void OnChargeStopped()
    {
        Debug.Log("Charging stopped");
        m_colour = m_chargeNormal;
    }

    protected void OnChargeStarted()
    {
        Debug.Log("Charging started");
        m_colour = m_chargeStarted;
    }

    protected void OnReadyToRelease(float charge)
    {
        Debug.Log("Ready to fire " + charge);
        m_colour = m_chargeReadyToRelease;
    }


    #region Unity Event Handlers

    protected void OnDisable()
    {
        m_chargeUp.ChargeStarted -= OnChargeStarted;
        m_chargeUp.ChargeStopped -= OnChargeStopped;
        m_chargeUp.FullyCharged -= OnFullyCharged;
        m_chargeUp.ReadyToRelease -= OnReadyToRelease;
        m_chargeUp.ChargeReleased -= OnChargeReleased;
    }

    protected void OnEnable()
    {
        m_chargeUp.ChargeStarted += OnChargeStarted;
        m_chargeUp.ChargeStopped += OnChargeStopped;
        m_chargeUp.FullyCharged += OnFullyCharged;
        m_chargeUp.ReadyToRelease += OnReadyToRelease;
        m_chargeUp.ChargeReleased += OnChargeReleased;
    }

    protected void OnGUI()
    {
        GUI.color = m_colour;
        GUI.Label(new Rect(25, 25, 200, 50), "Charge: " + m_chargeUp.CurrentCharge.ToString("N2"));
        GUI.Label(new Rect(25, 75, 200, 50), "Elapsed Time: " + m_chargeUp.ElapsedChargeTime.ToString("N2"));
    }
    #endregion

}
