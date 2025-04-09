//using unityengine;

//public class platform : monobehaviour
//{
//    private bool hasscored = false; // 점수 중복 방지용 플래그

//    private void oncollisionenter2d(collision2d collision)
//    {
//        if (!hasscored && collision.gameobject.comparetag("player"))
//        {
//            //충돌 지점이 아래 방향에서 온 경우에만 점수 추가
//            contactpoint2d contact = collision.getcontact(0);
//            if (contact.normal.y < -0.5f)
//            {
//                scoremanager.instance.addscore(1);
//                hasscored = true;
//            }
//        }
//    }
//}
