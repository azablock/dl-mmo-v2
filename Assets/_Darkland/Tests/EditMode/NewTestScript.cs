using _Darkland.Tests.Common;

namespace _Darkland.Tests.EditMode {

    public class NewTestScript : MirrorEditModeTest {

        // public struct Message2 : NetworkMessage {
        //     public int x;
        // }
        //
        // [Test]
        // public void Send_ClientToServerMessage() {
        //     // register a message handler
        //     int called = 0;
        //     NetworkServer.RegisterHandler<Message2>((conn, msg) => ++called, false);
        //
        //     // listen & connect a client
        //     NetworkServer.Listen(1);
        //     ConnectClientBlocking(out _);
        //
        //     // send message & process
        //     NetworkClient.Send(new DarklandPlayerMessages.ActionRequestMessage() {darklandPlayerNetId = 1});
        //     // NetworkClient.Send(new Message2() {x = 5});
        //     ProcessMessages();
        //
        //     //todo gowno
        //     // did it get through?
        //     // Assert.That(called, Is.EqualTo(1));
        // }
    }

}