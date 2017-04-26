namespace Bion.PO
{
    public interface IBPOPolitics
    {
        BPOPoliticsMemberTypes MemberType { get; set; }
        BPOPoliticsReadDepth ReadDepth { get; set; }
    }
}