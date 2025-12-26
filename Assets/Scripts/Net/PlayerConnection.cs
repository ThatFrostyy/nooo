using System;

[Serializable]
public struct PlayerConnection
{
    public ulong PlayerId;
    public ulong PawnNetId;

    public PlayerConnection(ulong playerId, ulong pawnNetId)
    {
        PlayerId = playerId;
        PawnNetId = pawnNetId;
    }
}
