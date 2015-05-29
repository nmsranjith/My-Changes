
namespace DotNetNuke.Modules.SampleRequestForm
{
    public class SRFDigitalItems
    {
        public string Isbn { get; set; }
        public string Type { get; set; }

    }
    public enum ItemType
    {
        SSOREEDAMDIGITAL, SSOREEDAMCOURSESMART, PRINT, NONE
    }
    public enum ISbnType
    {
        FORMAT, SUPPLIEMENT
    }
    public enum ListItemType
    {
        ebook, eac, ess
    }
    public enum TypeOfSale
    {
        SSO,Physical
    }
}
