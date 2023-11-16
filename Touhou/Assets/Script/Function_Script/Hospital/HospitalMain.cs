using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*/
씬 도입시 HospitalManager 호출하여 모드 시작

/*/
public class HospitalMain : MonoBehaviour
{
  private void Start()
  {
    HospitalManager.Instance.StartHospitalMode();
  }
}
