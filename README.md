개요
======
프로젝트 이름: 3D Adventure
======
프로젝트 기간: 25.03.4 ~ 25.03.11
======

주요기능
- 캐릭터 이동, 점프, 대쉬, 상호작용(아이템) => Input System 활용
- 체력 감소/회복, 스태미나 감소/회복 => coroutine 사용
- 레이저 트랩 => Raycast 활용
- 점프대 => ForceMode.Impulse 사용

게임 설명

1인칭 시점으로 마우스를 이용하여 둘러볼 수 있고 WASD를 사용하여 이동, LeftShift를 사용하여 대쉬, Space를 사용하여 점프, E를 눌러 아이템을 사용할 수 있습니다. 점프대 위로 올라가면 높게 뛰어오르고 높은 곳에서 떨어지게 되면 데미지를 받아 체력이 줄어듭니다. 체력을 회복하려면 회복 아이템인 Hp포션을 사용하여 일정시간마다 체력이 회복합니다. 대쉬는 스태미나를 사용하여 속도를 증가시킬 수 있으며 스태미나는 일정시간마다 자동 회복 합니다. 공중에 있는 플랫폼을 오르면 움직이는 플랫폼이 보이고 Warning문구와 함께 레이저 트랩이 활성화됩니다. 레이저 트랩에 닿으면 플레이어의 Hp가 깎이고 일정시간동안 잠시 무적 상태가 됩니다. 마지막으로 플레이어의 hp가 0이 되면 GameOver문구가 뜨며 아무것도 할 수 없게 됩니다.

<1인칭 시점과 프롬포트>

![Image](https://github.com/user-attachments/assets/18dfc3e7-1362-406e-9e8d-d919d420bfb5)

<점프대>

![Image](https://github.com/user-attachments/assets/40c3499f-0f4d-4594-8099-61fff743a0eb)

<낙하 데미지와 회복>

![Image](https://github.com/user-attachments/assets/b49c8994-2a06-46f8-9d92-f2c820c5f8a5)

<대쉬>

![Image](https://github.com/user-attachments/assets/4b2d213f-f0bf-4273-8821-f2c3ec6783b9)

<레이저 트랩>

![Image](https://github.com/user-attachments/assets/17db2c41-4670-4454-a624-a2a1b072aed2)

<게임 오버>

![Image](https://github.com/user-attachments/assets/3339cbd5-6a3b-486e-90c4-6c244d6c73cc)
