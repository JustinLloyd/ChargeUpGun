using UnityEngine;

public class ChargeUp : MonoBehaviour
{
    #region Event Declarations

    /// <summary>
    /// Fired when the charge has been reset to zero
    /// </summary>
    public event ChargeResetHandler ChargeReset;
    public delegate void ChargeResetHandler();

    /// <summary>
    /// Fired when the charge is released, e.g. the player depresses the fire button
    /// </summary>
    public event ChargeReleasedHandler ChargeReleased;
    public delegate void ChargeReleasedHandler(float charge);

    /// <summary>
    /// Fired when the charge potential has reached maximum
    /// </summary>
    public event FullyChargedHandler FullyCharged;
    public delegate void FullyChargedHandler(float charge);
    /// <summary>
    /// Fired when the charge potential has reached sufficient energy to be useful
    /// </summary>
    public event ReadyToReleaseHandler ReadyToRelease;
    public delegate void ReadyToReleaseHandler(float charge);

    /// <summary>
    /// Fired when the charging has begun, e.g. the player depresses the charge button
    /// </summary>
    public event ChargeStartedgHandler ChargeStarted;
    public delegate void ChargeStartedgHandler();

    /// <summary>
    /// Fired when the charging has stopped, e.g. the player releases the charge button
    /// </summary>
    public event ChargeStoppedHandler ChargeStopped;
    public delegate void ChargeStoppedHandler();
    #endregion

    #region Inspector Variables
    /// <summary>
    /// Should we automatically release the charge when it reaches maximum?
    /// </summary>
    [SerializeField]
    protected bool m_autoFireAtMaximum;
    /// <summary>
    /// How quickly the charge will reach maximum potential, can be any positive value less than Maximum Charge Value
    /// </summary>
    [SerializeField]
    protected float m_chargeRatePerSecond;
    /// <summary>
    /// Maximum permitted charge value, can be any positive value
    /// </summary>
    [SerializeField]
    protected float m_maximumChargeValue;

    /// <summary>
    /// Percentage to indicate the minimum value of charge required to fire
    /// </summary>
    [SerializeField]
    protected float m_readyToFireAt;

    /// <summary>
    /// Should the charge reset when told to stop but hasn't been fired?
    /// </summary>
    [SerializeField]
    protected bool m_resetOnStop;

    /// <summary>
    /// We can start the charge value at greater than zero
    /// </summary>
    [SerializeField]
    protected float m_startChargeValue;

    #endregion

    #region Member Variables

    protected readonly string m_invokeReachedFullCharge;
    protected readonly string m_invokeReachedReadyToFire;

    /// <summary>
    /// When did we start tracking the charge up?
    /// </summary>
    protected float m_startTime;
    #endregion

    public ChargeUp()
    {
        m_invokeReachedReadyToFire = new System.Action(ReachedReadyToFire).Method.Name;
        m_invokeReachedFullCharge = new System.Action(ReachedFullCharge).Method.Name;
    }

    #region Properties
    public float CurrentCharge
    {
        get
        {
            if (!IsCharging)
            {
                return (0.0f);
            }

            return (Mathf.Clamp(1.0f / TimeToReachMaximumFromStart * ElapsedChargeTime * MaximumCharge, 0.0f, m_maximumChargeValue));
        }

    }

    public float ElapsedChargeTime
    {
        get
        {
            if (!IsCharging)
            {
                return (0.0f);
            }

            return (Time.time - m_startTime);
        }

    }

    public float MaximumCharge
    {
        get
        {
            return (m_maximumChargeValue);
        }

        set
        {
            m_maximumChargeValue = value;
        }

    }

    public float TimeToReachReadyFromStart
    {
        get
        {
            return ((m_maximumChargeValue - m_startChargeValue) * ReadyToFireAt / m_chargeRatePerSecond);
        }

    }

    public float TimeToReachMaximumFromZero
    {
        get
        {
            return (m_maximumChargeValue / m_chargeRatePerSecond);
        }

    }

    public float TimeToReachMaximumFromStart
    {
        get
        {
            return ((m_maximumChargeValue - m_startChargeValue) / m_chargeRatePerSecond);
        }

    }

    public float TimeToReachMaximum
    {
        get
        {
            return ((m_maximumChargeValue - m_startChargeValue) / m_chargeRatePerSecond);
        }

    }

    public bool IsCharging
    {
        get;
        protected set;
    }

    public bool IsFull
    {
        get
        {
            return (CurrentCharge >= m_maximumChargeValue);
        }

    }

    public bool IsReady
    {
        get
        {
            return (CurrentCharge >= ReadyToFireAt);
        }

    }

    public float ReadyToFireAt
    {
        get
        {
            return (m_readyToFireAt);
        }

        set
        {
            m_readyToFireAt = value;
        }

    }

    #endregion

    #region Unity Event Handlers
    public void Awake()
    {
        System.Diagnostics.Debug.Assert(m_startChargeValue >= 0.0f && m_startChargeValue < m_maximumChargeValue);
        System.Diagnostics.Debug.Assert(m_maximumChargeValue > 0.0f);
        System.Diagnostics.Debug.Assert(m_readyToFireAt > 0.0f && m_readyToFireAt <= m_maximumChargeValue);
        System.Diagnostics.Debug.Assert(m_chargeRatePerSecond > 0.0f && m_chargeRatePerSecond < m_maximumChargeValue);
    }

    protected void Reset()
    {
        m_chargeRatePerSecond = 0.1f;
        m_resetOnStop = false;
        m_maximumChargeValue = 5.0f;
        m_startChargeValue = 0.0f;
        m_readyToFireAt = 0.5f;
    }

    #endregion

    #region General Member Methods

    public void ReleaseCharge()
    {
        if (!IsReady)
        {
            return;
        }

        FireChargeReleased();
        StopCharging();
    }

    public void StartCharging()
    {
        if (IsCharging)
        {
            return;
        }

        m_startTime = Time.time;
        Invoke(m_invokeReachedReadyToFire, TimeToReachReadyFromStart);
        Invoke(m_invokeReachedFullCharge, TimeToReachMaximumFromStart);
        IsCharging = true;
        FireChargeStarted();
    }

    public void StopCharging()
    {
        if (!IsCharging)
        {
            return;
        }

        CancelInvoke();
        IsCharging = false;
        FireChargeStopped();
    }

    public void ResetCharge()
    {
        CancelInvoke();
        IsCharging = false;
        FireChargeReset();
    }
    #endregion

    #region Event Dispatchers
    protected void FireChargeReleased()
    {
        if (ChargeReleased != null)
        {
            ChargeReleased(CurrentCharge);
        }

    }
    protected void FireChargeReset()
    {
        if (ChargeReset != null)
        {
            ChargeReset();
        }

    }

    protected void FireFullyCharged()
    {
        if (FullyCharged != null)
        {
            FullyCharged(CurrentCharge);
        }

    }

    protected void FireReadyToRelease()
    {
        if (ReadyToRelease != null)
        {
            ReadyToRelease(CurrentCharge);
        }

    }

    protected void FireChargeStarted()
    {
        if (ChargeStarted != null)
        {
            ChargeStarted();
        }

    }

    protected void FireChargeStopped()
    {
        if (ChargeStopped != null)
        {
            ChargeStopped();
        }

    }
    #endregion

    #region Invoke Handlers
    // responds to "Invoke"
    protected void ReachedFullCharge()
    {
        FireFullyCharged();
        if (m_autoFireAtMaximum)
        {
            ReleaseCharge();
        }

    }

    // responds to "Invoke"
    protected void ReachedReadyToFire()
    {
        FireReadyToRelease();
    }
    #endregion

}
