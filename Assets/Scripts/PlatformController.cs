//using unityengine;

//public class platform : monobehaviour
//{
//    private bool hasscored = false; // ���� �ߺ� ������ �÷���

//    private void oncollisionenter2d(collision2d collision)
//    {
//        if (!hasscored && collision.gameobject.comparetag("player"))
//        {
//            //�浹 ������ �Ʒ� ���⿡�� �� ��쿡�� ���� �߰�
//            contactpoint2d contact = collision.getcontact(0);
//            if (contact.normal.y < -0.5f)
//            {
//                scoremanager.instance.addscore(1);
//                hasscored = true;
//            }
//        }
//    }
//}
