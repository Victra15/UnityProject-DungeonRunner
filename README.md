# UnityProject-DungeonRunner
 본 프로젝트는 Unity Engine을 통해 구현된 2D 로그라이크 턴제 RPG 입니다.
 
# 기획된 사항
 - 사이드뷰 시점의 2D 도트그래픽
 - 각각의 이벤트가 배치된 맵을 탐험하며 스테이지를 통과하는 방식
 - 3명의 아군 캐릭터를 선택하여 게임을 시작
 - 각각의 아군 캐릭터는 고유한 패시브 스킬 1개와 액티브 스킬 4개를 소유
 - 전투는 턴제로 진행
 - 매 턴 각각의 캐릭터는 일반 공격, 스킬 사용, 아이템 사용, 방어 등의 행동 가능
 - 전투에서 승리하면 경험치, 돈, 아이템 등을 획득
 - 전투에서 패배할 경우 타이틀 화면으로 돌아가며 첫 스테이지부터 다시 시작
 - 아이템은 소모성 아이템과 지속 효과 아이템으로 구분되며,
   소모성 아이템은 전투 진행중 1번 사용하고 소멸하는 아이템,
   지속 효과 아이템은 획득 시점 이후 추가 능력을 부여하는 아이템
   
# 현재 구현된 기능

### 시작 화면
![Untitled 1](https://github.com/Victra15/UnityProject-DungeonRunner/assets/68954072/4a789865-4a84-4308-af0a-36e87bf04dd5)

1. 처음부터 시작: 캐릭터 선택 화면으로 전환
2. 이어하기 (미구현)
3. 나가기: 프로세스 종료
- Op. 옵션창 활성화

### 캐릭터 선택 화면
![Untitled 2](https://github.com/Victra15/UnityProject-DungeonRunner/assets/68954072/c3ace35f-6529-4c72-968e-efab7414aadd)

1. 선택 캐릭터 표시 패널: 선택된 캐릭터의 인 게임 이미지를 차례대로 보여줌
2. 캐릭터 선택 버튼 패널: 선택 가능한 캐릭터의 버튼이 나열되어 있으며,
버튼 클릭 시 해당 캐릭터의 정보가 보여지고 선택 버튼이 활성화
3. 캐릭터 정보 패널: 선택한 캐릭터의 정보를 보여주고,
선택 버튼 클릭 시, 선택 캐릭터 표시 패널에 해당 캐릭터를 추가
- Op. 옵션창 활성화
![Character_Select](https://github.com/Victra15/UnityProject-DungeonRunner/assets/68954072/69026c97-8e95-42b8-adf8-754779262aa2)
캐릭터 선택 과정 예시

### 인 게임 화면

- 초기 상태
  
    ![Untitled 3](https://github.com/Victra15/UnityProject-DungeonRunner/assets/68954072/1d362a3b-ba87-4070-a0f1-b66bab146510)
    
1. 소지한 상시 아이템 표시 패널(미구현)
2. 아군 캐릭터 진영
    - HP/MP바 표시
    - 캐릭터 스프라이트 표시
3. 소지한 골드 표시
4. 맵 표시 기능(미구현)
    
    - Debug. 디버깅용 기능 버튼
        - Load 버튼: 인 게임 씬에서 데이터 불러오기
        - 전투 버튼: 박쥐 몬스터 3마리 스폰 후 전투 상황으로 돌입
        - 그 외 나머지 버튼은 미구현
    - Op. 옵션창 활성화
- 전투 화면
  
    ![Untitled 4](https://github.com/Victra15/UnityProject-DungeonRunner/assets/68954072/3935fa4e-b621-460e-ae1a-c3732e3b4db9)
  
    전투 시작 - 방패기사 선택
  
    ![Untitled 5](https://github.com/Victra15/UnityProject-DungeonRunner/assets/68954072/827c34f4-d178-4d8b-ac47-5d5b9bd6c5b1)
  
    일반 공격 선택후 2번째 몬스터 대상 지정
  
    ![Untitled 6](https://github.com/Victra15/UnityProject-DungeonRunner/assets/68954072/c1d04094-ccba-4b88-a30a-058553b5ff2a)
  
    데미지 입힌 후, 방패기사 턴 종료
    
1. 조작 가능한 캐릭터 표시
    - 다이아몬드 문양이 있는 아군만 조작 가능
    - 빨간색 문양은 현재 선택한 아군 캐릭터
2. 적 진영
    - HP바 표시
3. 정보 표시 패널
    - 선택한 개체의 정보 표시
4. 커맨드 패널
    - 선택한 아군의 능력 표시
5. 타겟 표시
    - 일반 공격/스킬을 적용할 대상 표시

### 전투 결과 화면
![Untitled 7](https://github.com/Victra15/UnityProject-DungeonRunner/assets/68954072/54cc506b-34c1-4a28-a43a-d2e43572aacc)

전투 승리 이후, 획득한 골드와 경험치 표시

### 옵션 창
![Untitled 8](https://github.com/Victra15/UnityProject-DungeonRunner/assets/68954072/5604a230-e030-4b38-b312-9ad796b6132f)

1. 배경음악 크기 조절
2. 효과음 크기 조절
3. 옵션 창 비활성화
4. 프로세스 종료
