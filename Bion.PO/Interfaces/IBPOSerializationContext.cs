namespace Bion.PO
{
    public interface IBPOSerializationContext
    {
        byte[] Pack(IBPOPackUnpack bpoObject, bool compress, int compressLevel);
        IBPOPackUnpack Unpack(byte[] bpoData, bool compressed, int compressLevel);
    }
}