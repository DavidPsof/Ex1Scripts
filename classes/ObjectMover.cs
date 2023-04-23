using UnityEngine;

namespace classes
{
    public class ObjectMover : MonoBehaviour
    {
        [SerializeField] private KeyCode pickupKey = KeyCode.E;

        [Header("Settings")] [SerializeField] private Transform holdArea;

        private GameObject heldObj;
        private Rigidbody heldObjRB;

        [Header("Physics parameters")] [SerializeField]
        private float pickupRange = 5.0f;

        private float pickupForce = 150.0f;

        private void Update()
        {
            // Проверяем, нажата ли кнопка взятия предмета
            if (Input.GetKeyDown(pickupKey))
            {
                if (heldObj == null)
                {
                    RaycastHit hit;
                    if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit) &&
                        hit.collider.CompareTag("taken"))
                    {
                        PickupObject(hit.transform.gameObject);
                    }
                }
                else
                {
                    DropObject();
                }
            }

            if (heldObj != null)
            {
                MoveObject();
            }
        }

        void MoveObject()
        {
            if (Vector3.Distance(heldObj.transform.position, holdArea.position) > 0.1f)
            {
                Vector3 moveDir = holdArea.position - heldObj.transform.position;
                heldObjRB.AddForce(moveDir * pickupForce);
            }
        }

        void PickupObject(GameObject item)
        {
            if (item.GetComponent<Rigidbody>())
            {
                heldObjRB = item.GetComponent<Rigidbody>();
                heldObjRB.useGravity = false;
                heldObjRB.drag = 10;
                heldObjRB.constraints = RigidbodyConstraints.FreezeRotation;

                heldObjRB.transform.parent = holdArea;
                heldObj = item;
            }
        }

        void DropObject()
        {
            heldObjRB.useGravity = true;
            heldObjRB.drag = 1;
            heldObjRB.constraints = RigidbodyConstraints.None;

            heldObj.transform.parent = null;
            heldObj = null;
        }
    }
}