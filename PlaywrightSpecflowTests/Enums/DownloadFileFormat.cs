using DescriptionAttribute = System.ComponentModel.DescriptionAttribute;

namespace PlaywrightSpecflowTests.Enums
{
    public enum DownloadFileFormat
    {
        [Description(".doc")]
        Doc,
        [Description(".rtf")]
        Rtf,
        [Description(".pdf")]
        Pdf,
        [Description(".txt")]
        Txt
    }
}
