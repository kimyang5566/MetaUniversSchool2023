using UnityEngine;
using UnityEngine.EventSystems;

//using Photon.Pun;

using System.Collections;
using Invector.Utils;
using Invector.vCamera;
using Invector.vShooter;

namespace Com.yoyo.MetaSchool
{
    /// <summary>
    /// Player manager.
    /// Handles fire Input and Beams.
    /// </summary>
    public class PlayerManager : MonoBehaviour//MonoBehaviourPunCallbacks,IPunObservable
    {
        #region Private Fields

        [Tooltip("The Beams GameObject to control")]
        [SerializeField]
        private GameObject beams;
        //True, when the user is firing
        bool IsFiring;
        bool isFPS;
        #endregion

        #region public Fields
        [Tooltip("The current Health of our player")]
        public float Health = 1f;
        public float cameraDistance;
        public float cameraHeight;
        public float FPSHeight;
        public bool isMine;

        [Tooltip("The local player instance. Use this to know if the local player is represented in the Scene")]
        public static GameObject LocalPlayerInstance;
        #endregion

        #region MonoBehaviour CallBacks

        /// <summary>
        /// MonoBehaviour method called on GameObject by Unity during early initialization phase.
        /// </summary>
        void Awake()
        {
            // used in GameManager.cs: we keep track of the localPlayer instance to prevent instantiation when levels are synchronized
            //if (photonView.IsMine)
            //{
                PlayerManager.LocalPlayerInstance = this.gameObject;
                ClothControl.instance.SetPlayer(GetComponent<CapsuleCollider>());
            //}
            // #Critical
            // we flag as don't destroy on load so that instance survives level synchronization, thus giving a seamless experience when levels load.
            DontDestroyOnLoad(this.gameObject);

            if (beams == null)
            {
                Debug.LogError("<Color=Red><a>Missing</a></Color> Beams Reference.", this);
            }
            else
            {
                beams.SetActive(false);
            }

            //isMine =photonView.IsMine;
            

        }

        /// <summary>
        /// MonoBehaviour method called on GameObject by Unity during initialization phase.
        /// </summary>
        void Start()
        {

            void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, UnityEngine.SceneManagement.LoadSceneMode loadingMode)
            {
                this.CalledOnLevelWasLoaded(scene.buildIndex);
            }
            cameraDistance = 2.5f;// Camera.main.gameObject.GetComponent<vThirdPersonCamera>().CameraStateList.tpCameraStates[0].defaultDistance;
            cameraHeight = 1.6f;// Camera.main.gameObject.GetComponent<vThirdPersonCamera>().CameraStateList.tpCameraStates[0].height;
            Camera.main.gameObject.GetComponent<vThirdPersonCamera>().CameraStateList.tpCameraStates[0].defaultDistance = cameraDistance;
            Camera.main.gameObject.GetComponent<vThirdPersonCamera>().CameraStateList.tpCameraStates[0].height = cameraHeight;
            Camera.main.gameObject.GetComponent<vThirdPersonCamera>().CameraStateList.tpCameraStates[0].smooth = 1;
            /*
            CameraWork _cameraWork = this.gameObject.GetComponent<CameraWork>();


            if (_cameraWork != null)
            {
                if (photonView.IsMine)
                {
                    _cameraWork.OnStartFollowing();
                }
            }
            else
            {
                Debug.LogError("<Color=Red><a>Missing</a></Color> CameraWork Component on playerPrefab.", this);
            }
            */
        }

        /// <summary>
        /// MonoBehaviour method called on GameObject by Unity on every frame.
        /// </summary>
        void Update()
        {
            /*
            if (photonView.IsMine)
            {
                ProcessInputs();
                if (Health <= 0f)
                {
                    GameManager.instance.LeaveRoom();
                }
            }
            */
            //ProcessInputs();

            // trigger Beams active state
            if (beams != null && IsFiring != beams.activeInHierarchy)
            {
                beams.SetActive(IsFiring);
            }
        }

        /// <summary>
        /// MonoBehaviour method called when the Collider 'other' enters the trigger.
        /// Affect Health of the Player if the collider is a beam
        /// Note: when jumping and firing at the same, you'll find that the player's own beam intersects with itself
        /// One could move the collider further away to prevent this or check if the beam belongs to the player.
        /// </summary>
        void OnTriggerEnter(Collider other)
        {
            /*
            if (!photonView.IsMine)
            {
                return;
            }
            */
            if (other.tag == "EX" || other.tag == "Building")
            {
                other.gameObject.SendMessage("PlayerEnter");
            }
            // We are only interested in Beamers
            // we should be using tags but for the sake of distribution, let's simply check by name.
            if (!other.name.Contains("Beam"))
            {
                return;
            }
            Health -= 0.1f;
        }
        /// <summary>
        /// MonoBehaviour method called once per frame for every Collider 'other' that is touching the trigger.
        /// We're going to affect health while the beams are touching the player
        /// </summary>
        /// <param name="other">Other.</param>
        void OnTriggerStay(Collider other)
        {
            /*
            // we dont' do anything if we are not the local player.
            if (!photonView.IsMine)
            {
                return;
            }
            */
            
            // We are only interested in Beamers
            // we should be using tags but for the sake of distribution, let's simply check by name.
            if (!other.name.Contains("Beam"))
            {
                return;
            }
            // we slowly affect health when beam is constantly hitting us, so player has to move to prevent death.
            Health -= 0.1f * Time.deltaTime;
        }

        void OnTriggerExit(Collider other)
        {
            // we dont' do anything if we are not the local player.
            /*
            if (!photonView.IsMine)
            {
                return;
            }
            */

            if (other.tag == "EX" || other.tag == "Building")
            {
                other.gameObject.SendMessage("PlayerExit");
            }
        }

        #endregion

        #region PUN Callback
        //实时传递参数
        /*
        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {

            if (stream.IsWriting)
            {
                // We own this player: send the others our data
                stream.SendNext(IsFiring);
                stream.SendNext(Health);
            }
            else
            {
                // Network player, receive data
                this.IsFiring = (bool)stream.ReceiveNext();
                this.Health = (float)stream.ReceiveNext();
            }
        }
        */
        #endregion

        #region Custom

        /// <summary>
        /// Processes the inputs. Maintain a flag representing when the user is pressing Fire.
        /// </summary>
        void ProcessInputs()
        {
            if (Input.GetButtonDown("Fire1"))
            {
                if (!IsFiring)
                {
                    IsFiring = true;
                }
            }
            if (Input.GetButtonUp("Fire1"))
            {
                if (IsFiring)
                {
                    IsFiring = false;
                }
            }
            if (Input.GetButtonUp("ChangeView"))
            {
                ChangeView();
            }
        }

        public void ChangeView()
        {
            if (Camera.main.gameObject.GetComponent<vThirdPersonCamera>().CameraStateList.tpCameraStates[0].defaultDistance == 0)
            {
                Camera.main.gameObject.GetComponent<vThirdPersonCamera>().CameraStateList.tpCameraStates[0].defaultDistance = cameraDistance;
                Camera.main.gameObject.GetComponent<vThirdPersonCamera>().CameraStateList.tpCameraStates[0].height = cameraHeight;
                Camera.main.gameObject.GetComponent<vThirdPersonCamera>().CameraStateList.tpCameraStates[0].smooth = 1;
                //GetComponent<PlayerUI>().playerNameText.gameObject.SetActive(true);
            }
            else
            {
                Debug.Log("ChangeView " + Camera.main.gameObject.GetComponent<vThirdPersonCamera>().distance);
                Camera.main.gameObject.GetComponent<vThirdPersonCamera>().CameraStateList.tpCameraStates[0].defaultDistance = 0;
                Camera.main.gameObject.GetComponent<vThirdPersonCamera>().CameraStateList.tpCameraStates[0].height = FPSHeight;
                Camera.main.gameObject.GetComponent<vThirdPersonCamera>().CameraStateList.tpCameraStates[0].smooth = 0;
                //GetComponent<PlayerUI>().playerNameText.gameObject.SetActive(false);
            }
            //isFPS = !isFPS;
        }

        void CalledOnLevelWasLoaded(int level)
        {
            // check if we are outside the Arena and if it's the case, spawn around the center of the arena in a safe zone
            if (!Physics.Raycast(transform.position, -Vector3.up, 5f))
            {
                transform.position = new Vector3(0f, 5f, 0f);
            }
        }

        #endregion
    }
}