using UnityEngine;

public class ChargeUpInputHandler : MonoBehaviour
{
    #region Inspector Variables
    [SerializeField]
    protected ChargeUp m_chargeUp;
    #endregion

    #region Unity Event Handlers

    protected void Reset()
    {
        // not efficient, but useful if you forget to set your reference in the inspector
        m_chargeUp = GameObject.FindObjectOfType(typeof(ChargeUp)) as ChargeUp;
    }

    protected void Update()
    {
        bool chargeEngaged = Input.GetButtonDown("Fire2");
        bool chargeDisengaged = Input.GetButtonUp("Fire2");
        if (chargeEngaged)
        {
            m_chargeUp.StartCharging();
        }
        else if (chargeDisengaged)
        {
            m_chargeUp.StopCharging();
        }

        bool fired = Input.GetButtonDown("Fire1");
        if (fired)
        {
            m_chargeUp.ReleaseCharge();
        }

    }
    #endregion

}
