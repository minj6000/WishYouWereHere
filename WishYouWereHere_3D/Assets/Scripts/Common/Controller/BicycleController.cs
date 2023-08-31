using System.Linq;
using UnityEngine;

namespace WishYouWereHere3D
{
    public class BicycleController : ControllerBase
    {
        [SerializeField] bool _isPlayer;
        public bool IsPlayer
        {
            get { return _isPlayer; }
        }

        [SerializeField] Animator _chenAnimator;
        [SerializeField] SBPScripts.BicycleController _bicycleController;

        static BicycleController _instance = null;
        public static BicycleController Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectsOfType<BicycleController>().FirstOrDefault(controller => controller.IsPlayer);
                }
                return _instance;
            }
        }

        private void Awake()
        {
            _bicycleController = GetComponent<SBPScripts.BicycleController>();

            if(_firstPersonMovement != null)
            {
                _firstPersonMovement.enabled = false;
            }
        }

        private void Start()
        {
            if (_chenAnimator != null)
            {
                _chenAnimator.enabled = false;
            }
        }

        public override void Movable(bool enable)
        {
            if(enable)
            {
                if (_isPlayer)
                {
                    _bicycleController.ControllState = SBPScripts.BicycleController.ControllStates.PlayerControl;
                }
                else
                {
                    _bicycleController.ControllState = SBPScripts.BicycleController.ControllStates.WaypointControl;
                    if(_chenAnimator != null)
                    {
                        _chenAnimator.enabled = true;
                    }
                }
            }
            else
            {
                _bicycleController.ControllState = SBPScripts.BicycleController.ControllStates.Stop;
                if (_chenAnimator != null)
                {
                    _chenAnimator.enabled = false;
                }
            }
        }

        public void LoadWaypointData(string waypointJsonData)
        {
            JsonUtility.FromJsonOverwrite(waypointJsonData, _bicycleController.wayPointSystem);
        }
    }
}