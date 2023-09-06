VAR npcAffection = 0

-> main

=== main ===
호감도 테스트 용도입니다.
    +[Affection +2] ->AddAffection
    +[Talk] -> AffectionDialogue
    +[Exit] -> DONE
    
=== AddAffection ===
호감도를 +2 합니다.
->main

===AffectionDialogue===
호감도 확인용 스토리 입니다
{npcAffection == 0:
    현재 호감도는 0입니다. 호감도 : {npcAffection}
}
{0 < npcAffection && npcAffection < 5:
    현재 호감도는 0이상 입니다. 호감도 : {npcAffection}
}
{5 <= npcAffection && npcAffection < 10:
    현재 호감도는 5이상 입니다. 호감도 : {npcAffection}
}
{10 <= npcAffection:
    현재 호감도는 10이상 입니다. 호감도 : {npcAffection}
}
->main
