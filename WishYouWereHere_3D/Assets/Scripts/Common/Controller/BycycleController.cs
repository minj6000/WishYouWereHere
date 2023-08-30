using Cysharp.Threading.Tasks;
using UnityEngine;

namespace WishYouWereHere3D
{
    public class BycycleController : ControllerBase
    {
        [SerializeField] SBPScripts.BicycleController _bicycleController;

        [SerializeField] Transform _lookTestTransform;

        private void Awake()
        {
            _bicycleController = GetComponent<SBPScripts.BicycleController>();
            _firstPersonMovement.enabled = false;
        }

        public override void Movable(bool enable)
        {
            _bicycleController.Controllable = enable;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F1))
            {
                Movable(false);
                Rotatable(true);
            }

            if(Input.GetKeyDown(KeyCode.F2))
            {
                LookAt(_lookTestTransform).Forget();
            }

            if (Input.GetKeyDown(KeyCode.F3))
            {
                LookForward().Forget();
            }

            if (Input.GetKeyDown(KeyCode.F4))
            {
                Movable(true);
                Rotatable(true);
            }
        }
    }
}