using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 碰撞实验测试
/// </summary>
public class CollisionTest : MonoBehaviour
{
    [SerializeField] Material testMat;
    [SerializeField] Material testMatRed;
    [SerializeField] Material testMatBlue;
    private void OnCollisionEnter(Collision other) {
        print("CollisionEnter:" + gameObject.name);
        GetComponent<MeshRenderer>().material = testMatRed;
    }
    private void OnCollisionStay(Collision other) {
        print("CollisionStay:" + gameObject.name);
    }
    private void OnCollisionExit(Collision other) {
        print("CollisionExit:" + gameObject.name);
        GetComponent<MeshRenderer>().material = testMat;
    }
    private void OnTriggerEnter(Collider other) {
        print("TriggerEnter:" + gameObject.name);
        GetComponent<MeshRenderer>().material = testMatBlue;
    }
    private void OnTriggerStay(Collider other) {
        print("TriggerStay:" + gameObject.name);
    }
    private void OnTriggerExit(Collider other) {
        print("TriggerExit:" + gameObject.name);
        GetComponent<MeshRenderer>().material = testMat;
    }
}
