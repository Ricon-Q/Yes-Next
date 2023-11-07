using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*/
씬 도입시 PlayerManager부터 병원 평판과 같은 정보들을 불러들인다

평판에 따른 환자 수를 큐(FIFO)로 채워둔다 ex) 1단계 5명, 2단계 10명, 3단계 15명 
화면에는 한 명의 환자만 표시되게, 환자가 입실하면, 큐에서 하나를 뺀다
환자마다 병, 스크립트가 랜덤하게 배치, 병 정보에는 각 진단의 평판과 수입이 기입되어 있음

/*/
public class HospitalMain : MonoBehaviour
{
  public long hospitalLevel;
  public PatientDataBase patientDataBase;
  public Queue<PatientData> PatientQueue;

   // Start함수에서 평판 정보를 PlayerManager에서 가져온다. 이후 GeneratePatientQueue() 호출
  private void Start() 
  {
    // hospitalLevel = PlayerManager.Instance.playerData.hospitalLevel;
    hospitalLevel = 1;
    long patientCount = (long)UnityEngine.Random.Range(hospitalLevel * 5 - 3, hospitalLevel * 5);

    PatientQueue = new Queue<PatientData>();
    GeneratePatientQueue(patientCount);
  }

    // 환자의 정보를 큐 형태로 작성, 
  private void GeneratePatientQueue(long patientCount)
  {
    for (int i = 0; i < patientCount; i++)
    {
      if(patientDataBase.Items != null)
      {
        int randomPatientID = UnityEngine.Random.Range(0, patientDataBase.Items.Length);
        PatientQueue.Enqueue(patientDataBase.Items[randomPatientID]);
      }
    }
  }
}
