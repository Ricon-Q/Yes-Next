// 주석 테스트
-> Main

===Main===
이것은 환자 테스트 스크립트 3번입니다.
선택지는 3개가 있습니다.
올바른 약은 포션 3번입니다.
    +[처방하기] -> Prescribe
    +[질문하기] -> Ask
    +[진료포기] -> GiveUp
    
===Prescribe===
// 플레이어 대사
흠.. 이 약이 좋겠네요
    +[취소] -> Decision

===Ask===
혹시 기침을 자주 하시나요?
-> Decision

===GiveUp===
뭐요? 진료를 포기한다고요?
->DONE

===Decision===
어떻게 하시겠습니까?
    +[처방하기] -> Prescribe
    +[질문하기] -> Ask
    +[진료포기] -> GiveUp
    